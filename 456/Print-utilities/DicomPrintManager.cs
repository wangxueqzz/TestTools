
#region License

// Copyright (c) 2013, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This file is part of the ClearCanvas RIS/PACS open source project.
//
// The ClearCanvas RIS/PACS open source project is free software: you can
// redistribute it and/or modify it under the terms of the GNU General Public
// License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// The ClearCanvas RIS/PACS open source project is distributed in the hope that it
// will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
// Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// the ClearCanvas RIS/PACS open source project.  If not, see
// <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using Macro.Common;
using Macro.Common.Utilities;
using Macro.Desktop;
using Macro.Dicom.Iod.Modules;
using Macro.Dicom.Network;
using Macro.Dicom.Network.Scu;
using Macro.ImageViewer.StudyManagement;
using Macro.ImageViewer.Utilities.Print.Dicom.Preview;
using Path = System.IO.Path;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    public class DicomPrintManager
    {
        private BackgroundTask _task;
        private ProgressDialogComponentForPrint _progressDialog;
        private SynchronizationContext _context;
        private readonly IDesktopWindow _desktopWindow;
        private IShelf _shelf = null;
        private string _statusMessage;
        private DicomPrintSession _dicomPrintSession = null;
        private static List<ISelectPresentationsInformation> _selectPresentationsInformations = null;
        private DicomPrinter _dicomPrinter = null;
        private SelectPresentationsInformationsCollection selects = null;
        private int _tileCount = 0;
        private static List<string> _studyInstanceUid = null;
        private DicomPrintComponent _dicomPrintComponent = null;
        private bool _isAllPages = false;
        private bool _isPrintedDelete = false;

        private event EventHandler _closeShelf;

        public DicomPrintManager(IDesktopWindow desktopWindow, DicomPrintComponent dicomPrintComponent)
        {
            _desktopWindow = desktopWindow;
            _task = new BackgroundTask(DoPrint, true);
            _task.ThreadUICulture = Application.CurrentUICulture;
            _context = SynchronizationContext.Current;
            _progressDialog = new ProgressDialogComponentForPrint(_task, false, ProgressBarStyle.Marquee);
            _desktopWindow.Closing += this.DesktopWindowsClose;
            _task.Terminated += TaskTerminated;
            _dicomPrintComponent = dicomPrintComponent;

        }

        public void Print(
            List<ISelectPresentationsInformation> selectPresentationsInformations,
            DicomPrinter dicomPrinter,
            int tileCount,
            bool isAllPages,
            bool isDelete)
        {
            if (_task.IsRunning)
            {
                _desktopWindow.ShowMessageBox("请稍等正在打印中...", MessageBoxActions.Ok);
                return;
            }

            _selectPresentationsInformations = selectPresentationsInformations;
            _dicomPrinter = dicomPrinter;
            _tileCount = tileCount;
            _isAllPages = isAllPages;
            _isPrintedDelete = isDelete;
            _task.Run();

        }

        private void DoPrint(IBackgroundTaskContext context)
        {
            try
            {
                if (_selectPresentationsInformations == null || _selectPresentationsInformations.Count == 0)
                {
                    return;
                }
                Thread.Sleep(20);
                BackgroundTaskReportStatus(context, "Dicom打印开始", 0);
                Thread.Sleep(10);
                BackgroundTaskReportStatus(context, "开始处理图像...", 0);
                Thread.Sleep(10);
                //UISynchronizationContext.Send(DoImageProcess, null);
                DoImageProcess(null);
                if (_selectPresentationsInformations.Count == 0)
                {
                    BackgroundTaskReportStatus(context, "处理图像处理结果为0", 0);
                    return;
                }
                BackgroundTaskReportStatus(context, "开始向打印机发送图像", 0);
                Thread.Sleep(10);
                PrintScu scu = new PrintScu();
                DicomPrintProgressUpdate progressUpdate = new DicomPrintProgressUpdate();
                progressUpdate.dicomPrintManager = this;
                progressUpdate.printScu = scu;
                progressUpdate.Task = context;
                scu.ProgressUpdated += progressUpdate.Update;
                _dicomPrintSession = new DicomPrintSession(DicomPrinter, new SelectPresentationsInformationsCollection(_selectPresentationsInformations));
                PrintScu.FilmSession filmSession = DicomPrintSession.GetFilmSession(_dicomPrintSession.SelectPresentationsCollection, _dicomPrintSession.DicomPrinter.Config);
                DicomState dicomState = scu.Print("MacroAETile", _dicomPrintSession.DicomPrinter.AETitle, _dicomPrintSession.DicomPrinter.Host, _dicomPrintSession.DicomPrinter.Port, filmSession);
                scu.Join();
                if (dicomState == DicomState.Success)
                {
                    //UpDicomPrintStatus(context);
                    if (_dicomPrintComponent != null)
                    {
                        UISynchronizationContext.Send(_dicomPrintComponent.PrintedDeleteImage, this);
                    }

                }
            }
            catch (Exception e)
            {
                Platform.Log(LogLevel.Debug, e);
                BackgroundTaskReportStatus(context, e.Message, 100);
            }

            if (_studyInstanceUid != null)
            {
                _studyInstanceUid.Clear();
                _studyInstanceUid = null;
            }
            GC.Collect();
        }

        private void TaskTerminated(object sender, BackgroundTaskTerminatedEventArgs args)
        {

            if (_dicomPrintSession != null)
            {
                _dicomPrintSession.Dispose();
                _dicomPrintSession = null;
            }

            if (_selectPresentationsInformations != null)
            {
                foreach (var item in _selectPresentationsInformations)
                {
                    IImageSopProvider imageSopProvider = item.Image as IImageSopProvider;
                    ILocalSopDataSource localSource = null;
                    if (imageSopProvider != null)
                        localSource = imageSopProvider.ImageSop.DataSource as ILocalSopDataSource;

                    item.Dispose();
                    if (localSource != null)
                    {
                        localSource.Dispose();
                    }
                }

                _dicomPrinter = null;
            }
        }

        private void DoImageProcess(object state)
        {
            FilmSize filmsize = DicomPrinter.Config.FilmBox.FilmSize.ToFilmSize();
            int filmDpi = DicomPrinter.Config.StandardResolutionDPI;
            FilmOrientation filmOrientation = DicomPrinter.Config.FilmBox.FilmOrientation;
            PresentationMode presentationMode = DicomPrinter.Config.PresentationMode;
            ColorMode colorMode = this.DicomPrinter.Config.ColorMode;

            List<IPresentationImage> presentationImage =
                MergerDicomImageToBitmap.Merger(filmsize,
                                                filmDpi,
                                                filmOrientation,
                                                _selectPresentationsInformations,
                                                presentationMode,
                                                colorMode,
                                                _tileCount);

            _studyInstanceUid = new List<string>();
            foreach (var selectPresentationsInformation in _selectPresentationsInformations)
            {
                IImageSopProvider image = (IImageSopProvider)selectPresentationsInformation.Image;
                if (image != null && !_studyInstanceUid.Contains(image.Sop.StudyInstanceUid))
                {
                    _studyInstanceUid.Add(image.Sop.StudyInstanceUid);
                }
                selectPresentationsInformation.Dispose();
            }
            _selectPresentationsInformations.Clear();

            foreach (var image in presentationImage)
            {
                var frame = ((IImageSopProvider)image).Frame;
                Rectangle client = new Rectangle(0, 0, frame.Columns, frame.Rows);
                _selectPresentationsInformations.Add(new SelectPresentionInformation(image, client));
            }

            DicomPrinter.Config.FilmBox.ImageDisplayFormat = new PrinterImageDisplayFormat() { Value = @"STANDARD\1,1" };
        }

        private void BackgroundTaskReportStatus(IBackgroundTaskContext Context, string message, int percent)
        {
            _statusMessage = message;
            Context.ReportProgress(new BackgroundTaskProgress(percent, _statusMessage));
        }

        private void UpDicomPrintStatus(IBackgroundTaskContext context)
        {
            try
            {
                BackgroundTaskReportStatus(context, "更新打印状态", 100);
                Thread.Sleep(10);
                string str = Path.Combine(Environment.CurrentDirectory, "PrintStatus.dll");
                if (!File.Exists(str))
                {
                    this.BackgroundTaskReportStatus(context, "执行更新的DLL不存在", 100);
                    Thread.Sleep(10);
                    return;
                }
                Assembly assembly = Assembly.LoadFile(str);
                Type type = assembly.GetType("PrintStatus.StudyKeyList");
                object list = Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { type }), (object[])null);
                MethodInfo listAdd = list.GetType().GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
                foreach (string studyInstanceUid in _studyInstanceUid)
                {
                    object studyKeyList = assembly.CreateInstance("PrintStatus.StudyKeyList");
                    MethodInfo setSpecialNeeds = studyKeyList.GetType().GetMethod("set_Special_needs", BindingFlags.Public | BindingFlags.Instance);
                    MethodInfo setStudyInstanceUid = studyKeyList.GetType().GetMethod("set_Study_instance_uid", BindingFlags.Public | BindingFlags.Instance);
                    setSpecialNeeds.Invoke(studyKeyList, new object[] { "已打印" });
                    setStudyInstanceUid.Invoke(studyKeyList, new object[] { studyInstanceUid });
                    listAdd.Invoke(list, new object[] { studyKeyList });
                }
                object study = assembly.CreateInstance("PrintStatus.Study");
                bool flag = (bool)(study.GetType().GetMethod("UpdateStudySpecial_needs", BindingFlags.Public | BindingFlags.Instance).Invoke(study, new object[] { list }));
                if (flag)
                {
                    BackgroundTaskReportStatus(context, "更新打印状态成功", 100);
                    Thread.Sleep(10);
                }
                else
                {
                    BackgroundTaskReportStatus(context, "更新打印状态失败", 100);
                    Thread.Sleep(10);
                }
            }
            catch (Exception exception)
            {
                BackgroundTaskReportStatus(context, "更新打印状态失败:" + exception.Message, 100);
                Thread.Sleep(10);
            }
        }

        public void Show()
        {
            if (_shelf == null)
            {
                _shelf = ApplicationComponent.LaunchAsShelf(_desktopWindow, _progressDialog, "Dicom打印", ShelfDisplayHint.DockAutoHide | ShelfDisplayHint.DockBottom);
                _shelf.Closed += ShelfClose;
                _shelf.Activate();
            }
            else
            {
                _shelf.Activate();
            }
        }

        private void ShelfClose(object Sender, ClosedEventArgs args)
        {
            _desktopWindow.Closing -= DesktopWindowsClose;
            _shelf.Closed -= ShelfClose;
            if (_task.IsRunning)
            {
                _task.RequestCancel();
            }
            _task.Dispose();
            _task = null;
            _shelf = null;
            EventsHelper.Fire(_closeShelf, this, null);
        }

        private void DesktopWindowsClose(object sender, ClosingEventArgs args)
        {
            UserInteraction interaction = args.Interaction;

            if (((interaction == UserInteraction.Allowed) && !args.Cancel) && (_task != null && this._task.IsRunning))
            {
                string message = "是否取消打印";
                if (DialogBoxAction.No == _desktopWindow.ShowMessageBox(message, MessageBoxActions.YesNo))
                {
                    args.Cancel = true;
                }
                else if ((this._task != null) && this._task.IsRunning)
                {
                    Platform.Log(LogLevel.Debug, "执行打印", new object[0]);
                    _task.RequestCancel();
                }
            }
        }

        internal IShelf ProgressShelf
        {
            get { return _shelf; }
            private set
            {
                _shelf = value;
            }
        }

        public string StatusMessage
        {
            get { return _statusMessage; }
            private set
            {
                _statusMessage = value;
            }
        }

        public ProgressDialogComponentForPrint ProgressDialog
        {
            get { return _progressDialog; }
            set { _progressDialog = value; }
        }

        public SynchronizationContext UISynchronizationContext
        {
            get { return _context; }
            set { _context = value; }
        }

        public DicomPrintSession PrintSession
        {
            get { return _dicomPrintSession; }
        }

        internal DicomPrinter DicomPrinter
        {
            get { return _dicomPrinter; }
        }

        public bool IsPrinting
        {
            get { return _task.IsRunning; }
        }

        public event EventHandler CloseShelf
        {
            add { _closeShelf += value; }
            remove { _closeShelf -= value; }
        }

        public bool IsAllPages
        {
            get { return _isAllPages; }
        }

        public bool IsPrintedDelete
        {
            get { return _isPrintedDelete; }
        }
    }

    internal sealed class DicomPrintProgressUpdate
    {

        public DicomPrintManager dicomPrintManager = null;
        public IBackgroundTaskContext Task = null;
        public PrintScu printScu = null;
        private string Cancel = "取消打印";
        private string Scuess = "打印成功";
        private string Format = "向打印机{0}发送第{1}张胶片的图像，共{2}张胶片";

        private void ProgressBarStyle(object obj)
        {
            dicomPrintManager.ProgressDialog.ProgressBarStyle = Macro.Desktop.ProgressBarStyle.Blocks;
        }

        public void Update(object sender, PrintScu.ProgressUpdateEventArgs args)
        {
            SendOrPostCallback sendOrPostCallback = null;
            if (Task.CancelRequested)
            {
                Task.ReportProgress(new BackgroundTaskProgress(100, this.Cancel));
                printScu.Cancel();
            }
            else if (args.NumberOfImageBoxesSent == 0)
            {
                Task.ReportProgress(new BackgroundTaskProgress(0, dicomPrintManager.StatusMessage));
            }
            else
            {
                if (args.NumberOfImageBoxesSent == dicomPrintManager.PrintSession.SelectPresentationsCollection.Count)
                {
                    this.Task.ReportProgress(new BackgroundTaskProgress(100, Scuess));
                }
                else
                {
                    if (dicomPrintManager.ProgressDialog.ProgressBarStyle == Macro.Desktop.ProgressBarStyle.Marquee)
                    {
                        if (sendOrPostCallback == null)
                        {
                            sendOrPostCallback = this.ProgressBarStyle;
                        }
                        dicomPrintManager.UISynchronizationContext.Post(sendOrPostCallback, null);
                    }
                    int index = args.NumberOfImageBoxesSent - 1;
                    int total = dicomPrintManager.PrintSession.SelectPresentationsCollection.Count;
                    string message = string.Format(this.Format, dicomPrintManager.PrintSession.DicomPrinter.Name, args.NumberOfImageBoxesSent, total);
                    Task.ReportProgress(new BackgroundTaskProgress(index, total, message));
                }
            }
        }
    }

}
