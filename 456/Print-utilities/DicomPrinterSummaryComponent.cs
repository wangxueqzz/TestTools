
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
using Macro.Common;
using Macro.Common.Utilities;
using Macro.Desktop;
using Macro.Desktop.Actions;
using Macro.Desktop.Configuration;
using Macro.Desktop.Tables;
using Macro.Desktop.Validation;
using Macro.Dicom.Network.Scu;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    [ExtensionPoint]
    public sealed class DicomPrinterSummaryViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {
    }

    [AssociateView(typeof(DicomPrinterSummaryViewExtensionPoint))]
    internal sealed class DicomPrinterSummaryComponent : ConfigurationApplicationComponent, IDicomPrinterSummaryComponent
    {
        private readonly CrudActionModelExtension _crudActionModelExtension;
        private readonly DicomPrinterTable _dicomPrinterTable;
        private Checkable<DicomPrinter> _checkableDicomPrinter;
        private static Converter<DicomPrinter, Checkable<DicomPrinter>> _converter;

        public DicomPrinterSummaryComponent()
        {
            Type type = base.GetType();
            this._crudActionModelExtension = new CrudActionModelExtension(new ApplicationThemeResourceResolver(type, true));
            this._dicomPrinterTable = new DicomPrinterTable(true);
        }

        private void Add()
        {
            DicomPrinterAddValidation nih = new DicomPrinterAddValidation
            {
                dicomPrinterSummaryComponent = this
            };
            nih.dicomPringer = new DicomPrinter();
            DicomPrinterEditorComponent component = new DicomPrinterEditorComponent(nih.dicomPringer);
            component.Validation.Add(new ValidationRule("Name", new ValidationRule.ValidationDelegate(nih.Validation)));
            DesktopWindow desktopWindow = base.Host.DesktopWindow;
            if (ApplicationComponentExitCode.Accepted == ApplicationComponent.LaunchAsDialog(desktopWindow, component, "EditorDicomPrinter"))
            {
                this._dicomPrinterTable.Items.Add(new Checkable<DicomPrinter>(nih.dicomPringer));
                this.Modified = true;
            }
        }


        private static Checkable<DicomPrinter> ToCheckable(DicomPrinter dicomPrinter)
        {
            return new Checkable<DicomPrinter>(dicomPrinter);
        }

        private bool Equals(DicomPrinter dicomPrinter1, DicomPrinter dicomPrinter2)
        {
            JudgeDicomPrinter pih = new JudgeDicomPrinter
            {
                dicomPrinter1 = dicomPrinter1,
                dicomPrinter2 = dicomPrinter2,
                dicomPrinterSummaryComponent = this
            };
            ItemCollection<Checkable<DicomPrinter>> items = this._dicomPrinterTable.Items;
            bool flag = CollectionUtils.Contains<Checkable<DicomPrinter>>((IEnumerable<Checkable<DicomPrinter>>)items, new Predicate<Checkable<DicomPrinter>>(pih.Equals));
            return !flag;
        }

        private void LocalPropertyChanged(object sender, EventArgs args)
        {
            this.Modified = true;
        }

        private void Edit()
        {
            DicomPrinterEditValidation oih = new DicomPrinterEditValidation
            {
                dicomPrinterSummaryComponent = this
            };

            oih.dicomPrinter = this._checkableDicomPrinter.Item;
            Checkable<DicomPrinter> oldCheckAble = this._checkableDicomPrinter;
            DicomPrinterEditorComponent component = new DicomPrinterEditorComponent(oih.dicomPrinter);
            component.Validation.Add(new ValidationRule("Name", new ValidationRule.ValidationDelegate(oih.Validation)));
            IApplicationComponentHost host = base.Host;
            DesktopWindow desktopWindow = host.DesktopWindow;
            if (ApplicationComponentExitCode.Accepted == ApplicationComponent.LaunchAsDialog(desktopWindow, component, "Edit DICOM Printer"))
            {
                int num = this._dicomPrinterTable.Items.IndexOf(oldCheckAble);
                this._dicomPrinterTable.Items.Remove(oldCheckAble);
                bool isChecked = oldCheckAble.IsChecked;
                Checkable<DicomPrinter> checkable = new Checkable<DicomPrinter>(oih.dicomPrinter, isChecked);
                this._dicomPrinterTable.Items.Insert(num, checkable);
                this.Modified = true;
            }
        }

        private void DeleteDicomPrinter()
        {
            if (DialogBoxAction.No != base.Host.ShowMessageBox("是否要删除选中的Dicom Printer", MessageBoxActions.YesNo))
            {
                this._dicomPrinterTable.Items.Remove(this._checkableDicomPrinter);
                this.Modified = true;
            }
        }

        private void VerificationDicomPrinter()
        {
            try
            {
                using (VerificationScu scu = new VerificationScu())
                {
                    IApplicationComponentHost host1 = base.Host;
                    string str;
                    string offlineAETitle = "MyAETitle";
                    DicomPrinter item = this._checkableDicomPrinter.Item;
                    string remoteAE = item.AETitle;
                    string remoteHost = item.Host;
                    int remotePort = item.Port;
                    VerificationResult result = scu.Verify(offlineAETitle, remoteAE, remoteHost, remotePort);
                    TimeSpan timeout = TimeSpan.FromSeconds(2.0);
                    scu.Join(timeout);
                    if (result == VerificationResult.Success)
                    {
                        str = "成功！";
                        host1.ShowMessageBox(str, MessageBoxActions.Ok);
                    }
                    else
                    {
                        str = "失败！";
                        host1.ShowMessageBox(str, MessageBoxActions.Ok);
                    }
                }
            }
            catch (Exception exception)
            {
                DesktopWindow desktopWindow = base.Host.DesktopWindow;
                ExceptionHandler.Report(exception, desktopWindow);
            }
        }

        private void SelectedItemChanged()
        {
            ClickAction edit = this._crudActionModelExtension.Edit;
            edit.Enabled = this._crudActionModelExtension.Delete.Enabled = this._crudActionModelExtension.Click.Enabled = this._checkableDicomPrinter != null;
        }

        public void OnItemDoubleClicked()
        {
            this.Edit();
        }

        public override void Save()
        {
            DicomPrinterCollection collection = new DicomPrinterCollection((IEnumerable<DicomPrinter>)this._dicomPrinterTable.DicomPrinterCollection);
            DicomPrintSettings.LocalDicomPrinterCollection = collection;
            if (this._dicomPrinterTable.SelectFirstCheckedCheckableDicomPrinter != null)
            {
                Checkable<DicomPrinter> checkable = this._dicomPrinterTable.SelectFirstCheckedCheckableDicomPrinter;
                DicomPrintSettings.LocalDefaultPrinterName = checkable.Item.Name;
            }
            DicomPrintSettings.Default.Save();
        }


        public override void Start()
        {
            this._crudActionModelExtension.Add.SetClickHandler(new ClickHandlerDelegate(this.Add));
            this._crudActionModelExtension.Edit.SetClickHandler(new ClickHandlerDelegate(this.Edit));
            this._crudActionModelExtension.Delete.SetClickHandler(new ClickHandlerDelegate(this.DeleteDicomPrinter));
            this._crudActionModelExtension.Click.SetClickHandler(new ClickHandlerDelegate(this.VerificationDicomPrinter));
            this._crudActionModelExtension.Edit.Enabled = false;
            this._crudActionModelExtension.Delete.Enabled = false;
            this._crudActionModelExtension.Click.Enabled = false;
            ItemCollection<Checkable<DicomPrinter>> items = this._dicomPrinterTable.Items;
            if (_converter == null)
            {
                _converter = new Converter<DicomPrinter, Checkable<DicomPrinter>>(DicomPrinterSummaryComponent.ToCheckable);
            }
            List<Checkable<DicomPrinter>> enumerable = CollectionUtils.Map<DicomPrinter, Checkable<DicomPrinter>>(DicomPrintSettings.LocalDicomPrinterCollection, _converter);
            items.AddRange(enumerable);
            this._dicomPrinterTable.SelectDicomPrinter(DicomPrintSettings.Default.DefaultPrinterName);
            this._dicomPrinterTable.PropertyChanged += new EventHandler<EventArgs>(this.LocalPropertyChanged);
            if (items.Count > 0)
            {
                this.SelectedItem = new Selection(this._dicomPrinterTable.Items[0]);
            }
            base.Start();
        }


        public ITable PrintersTable
        {
            get
            {
                return this._dicomPrinterTable;
            }
        }

        public ISelection SelectedItem
        {
            get
            {
                return new Selection(this._checkableDicomPrinter);
            }
            set
            {
                Selection selection = new Selection(this._checkableDicomPrinter);
                if (!selection.Equals(value))
                {
                    object item = value.Item;
                    this._checkableDicomPrinter = (Checkable<DicomPrinter>)item;
                    this.SelectedItemChanged();
                    string propertyName = "SelectedItem";
                    base.NotifyPropertyChanged(propertyName);
                }
            }
        }


        public ActionModelNode TableActionModel
        {
            get
            {
                return this._crudActionModelExtension;
            }
        }


        public sealed class CrudActionModelExtension : CrudActionModel
        {
            private static readonly object obj = new object();

            public CrudActionModelExtension(IResourceResolver resource)
                : base(true, true, true, resource)
            {
                string icon = "Icons.VerifyDicomPrinterToolSmall.png";
                string displayName = "VerifyDicomPrinter";
                base.AddAction(obj, displayName, icon);
            }
            public ClickAction Click
            {
                get
                {
                    Macro.Desktop.Actions.Action action1 = base[obj];
                    return (ClickAction)action1;
                }
            }
        }
        private sealed class DicomPrinterAddValidation
        {
            public DicomPrinterSummaryComponent dicomPrinterSummaryComponent;
            public DicomPrinter dicomPringer;
            public ValidationResult Validation(IApplicationComponent applicationComponent)
            {
                string format = "DicomPringerName:{0}";
                string text2 = this.dicomPringer.Name;
                return new ValidationResult(this.dicomPrinterSummaryComponent.Equals(null, this.dicomPringer), string.Format(format, text2));
            }
        }
        private sealed class DicomPrinterEditValidation
        {
            public DicomPrinterSummaryComponent dicomPrinterSummaryComponent;
            public DicomPrinter dicomPrinter;
            public ValidationResult Validation(IApplicationComponent component)
            {
                DicomPrinter item = dicomPrinterSummaryComponent._checkableDicomPrinter.Item;
                bool success = this.dicomPrinterSummaryComponent.Equals(item, this.dicomPrinter);
                string format = "DicomPrinterName{0}";
                return new ValidationResult(success, string.Format(format, dicomPrinter.Name));
            }
        }
        private sealed class JudgeDicomPrinter
        {
            public DicomPrinterSummaryComponent dicomPrinterSummaryComponent;
            public DicomPrinter dicomPrinter1;
            public DicomPrinter dicomPrinter2;
            public bool Equals(Checkable<DicomPrinter> checkableDicomPrinter)
            {
                if (this.dicomPrinter1 != null)
                {
                    DicomPrinter local1 = checkableDicomPrinter.Item;
                    if (object.ReferenceEquals(local1, this.dicomPrinter1))
                    {
                        return false;
                    }
                }
                DicomPrinter item = checkableDicomPrinter.Item;
                return object.Equals(item.Name, dicomPrinter2.Name);
            }
        }
    }
}

