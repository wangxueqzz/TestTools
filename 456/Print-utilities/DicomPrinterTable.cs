
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
using Macro.Common.Utilities;
using Macro.Desktop;
using Macro.Desktop.Tables;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{

    public sealed class DicomPrinterTable : Table<Checkable<DicomPrinter>>
    {

        private static TableColumn<Checkable<DicomPrinter>, bool>.GetColumnValueDelegate<Checkable<DicomPrinter>, bool> _default;
        private static TableColumn<Checkable<DicomPrinter>, int>.GetColumnValueDelegate<Checkable<DicomPrinter>, int> _port;
        private static TableColumn<Checkable<DicomPrinter>, string>.GetColumnValueDelegate<Checkable<DicomPrinter>, string> _aetile;
        private static Converter<Checkable<DicomPrinter>, DicomPrinter> _delegateConverterDicomPrinterToCheckable;
        private static Predicate<Checkable<DicomPrinter>> _checkablePrinterPredicate;
        private static TableColumn<Checkable<DicomPrinter>, bool>.GetColumnValueDelegate<Checkable<DicomPrinter>, bool> _noUpdateSelectStatusDefault;
        private static TableColumn<Checkable<DicomPrinter>, string>.GetColumnValueDelegate<Checkable<DicomPrinter>, string> _name;
        private static TableColumn<Checkable<DicomPrinter>, string>.GetColumnValueDelegate<Checkable<DicomPrinter>, string> _host;
        private event EventHandler<EventArgs> _propertyChanged;

        public event EventHandler<EventArgs> PropertyChanged
        {
            add
            {
                this._propertyChanged += value;
            }
            remove
            {

                this._propertyChanged -= value;
            }
        }

        public DicomPrinterTable(bool updateSelectStatus)
        {
            TableColumnCollection<Checkable<DicomPrinter>> columns = base.Columns;
            string columnName = SR.ColumnAETitle;
            if (DicomPrinterTable._aetile == null)
            {
                DicomPrinterTable._aetile = new TableColumn<Checkable<DicomPrinter>, string>.GetColumnValueDelegate<Checkable<DicomPrinter>, string>(DicomPrinterTable.AETitle);
            }
            columns.Add(new TableColumn<Checkable<DicomPrinter>, string>(columnName, DicomPrinterTable._aetile, 0.3f));
            if (DicomPrinterTable._name == null)
            {
                DicomPrinterTable._name = new TableColumn<Checkable<DicomPrinter>, string>.GetColumnValueDelegate<Checkable<DicomPrinter>, string>(DicomPrinterTable.Name);
            }
            columns.Add(new TableColumn<Checkable<DicomPrinter>, string>(SR.ColumnName, DicomPrinterTable._name, 0.3f));
            if (DicomPrinterTable._host == null)
            {
                DicomPrinterTable._host = new TableColumn<Checkable<DicomPrinter>, string>.GetColumnValueDelegate<Checkable<DicomPrinter>, string>(DicomPrinterTable.Host);
            }
            columns.Add(new TableColumn<Checkable<DicomPrinter>, string>(SR.ColumnHost, DicomPrinterTable._host, 0.2f));
            if (DicomPrinterTable._port == null)
            {
                DicomPrinterTable._port = new TableColumn<Checkable<DicomPrinter>, int>.GetColumnValueDelegate<Checkable<DicomPrinter>, int>(DicomPrinterTable.Port);
            }
            columns.Add(new TableColumn<Checkable<DicomPrinter>, int>(SR.ColumnPort, DicomPrinterTable._port, 0.1f));
            if (updateSelectStatus)
            {
                if (DicomPrinterTable._default == null)
                {
                    DicomPrinterTable._default = new TableColumn<Checkable<DicomPrinter>, bool>.GetColumnValueDelegate<Checkable<DicomPrinter>, bool>(DicomPrinterTable.IsChecked);
                }
                columns.Add(new TableColumn<Checkable<DicomPrinter>, bool>(SR.ColumnDefault, DicomPrinterTable._default, new TableColumn<Checkable<DicomPrinter>, bool>.SetColumnValueDelegate<Checkable<DicomPrinter>, bool>(this.UpdateCheckableItemSelectStatus), 0.1f));
            }
            else
            {
                if (DicomPrinterTable._noUpdateSelectStatusDefault == null)
                {
                    DicomPrinterTable._noUpdateSelectStatusDefault = new TableColumn<Checkable<DicomPrinter>, bool>.GetColumnValueDelegate<Checkable<DicomPrinter>, bool>(DicomPrinterTable.IsChecked);
                }
                columns.Add(new TableColumn<Checkable<DicomPrinter>, bool>(SR.ColumnColor, DicomPrinterTable._noUpdateSelectStatusDefault, 0.1f));
            }
        }

        public Checkable<DicomPrinter> SelectDicomPrinter(string dicomPrinterName)
        {
            DicomPrinterNameUtility Utility = new DicomPrinterNameUtility
            {
                DicomPrinterName = dicomPrinterName
            };
            ItemCollection<Checkable<DicomPrinter>> items = base.Items;
            Checkable<DicomPrinter> table = CollectionUtils.SelectFirst(items, new Predicate<Checkable<DicomPrinter>>(Utility.Equals));
            if (table != null)
            {
                this.UpdateCheckableItemSelectStatus(table, true);
            }

            return table;
        }

        private static DicomPrinter GetDicomPrinter(Checkable<DicomPrinter> checkableDicomPrinter)
        {
            return checkableDicomPrinter.Item;
        }

        private static bool IsChecked(Checkable<DicomPrinter> checkableDicomPrinter)
        {
            return checkableDicomPrinter.IsChecked;
        }

        private static int Port(Checkable<DicomPrinter> printer)
        {
            DicomPrinter item = printer.Item;
            return item.Port;
        }

        private static string Name(Checkable<DicomPrinter> printer)
        {
            DicomPrinter item = printer.Item;
            return item.Name;
        }

        private void UpdateCheckableItemSelectStatus(Checkable<DicomPrinter> item, bool flag)
        {
            if (flag)
            {
                IEnumerator<Checkable<DicomPrinter>> enumerator1 = base.Items.GetEnumerator();
                using (IEnumerator<Checkable<DicomPrinter>> enumerator = enumerator1)
                {
                    while (enumerator.MoveNext())
                    {
                        Checkable<DicomPrinter> current = enumerator.Current;
                        current.IsChecked = false;
                        base.Items.NotifyItemUpdated(current);
                    }
                }
            }
            item.IsChecked = flag;
            base.Items.NotifyItemUpdated(item);
            EventsHelper.Fire(this._propertyChanged, this, EventArgs.Empty);
        }

        private static string AETitle(Checkable<DicomPrinter> printer)
        {
            DicomPrinter item = printer.Item;
            return item.AETitle;
        }

        private static string Host(Checkable<DicomPrinter> printer)
        {
            return printer.Item.Host;
        }

        public Checkable<DicomPrinter> SelectFirstCheckedCheckableDicomPrinter
        {
            get
            {
                ItemCollection<Checkable<DicomPrinter>> items = base.Items;
                if (_checkablePrinterPredicate == null)
                {
                    _checkablePrinterPredicate = new Predicate<Checkable<DicomPrinter>>(DicomPrinterTable.IsChecked);
                }
                return CollectionUtils.SelectFirst<Checkable<DicomPrinter>>((IEnumerable<Checkable<DicomPrinter>>)items, _checkablePrinterPredicate);
            }
        }

        public List<DicomPrinter> DicomPrinterCollection
        {
            get
            {
                ItemCollection<Checkable<DicomPrinter>> items = base.Items;
                if (_delegateConverterDicomPrinterToCheckable == null)
                {
                    _delegateConverterDicomPrinterToCheckable = new Converter<Checkable<DicomPrinter>, DicomPrinter>(DicomPrinterTable.GetDicomPrinter);
                }
                return CollectionUtils.Map<Checkable<DicomPrinter>, DicomPrinter>(items, _delegateConverterDicomPrinterToCheckable);
            }
        }

        private sealed class DicomPrinterNameUtility
        {
            public string DicomPrinterName;

            public bool Equals(Checkable<DicomPrinter> dicomPrinter)
            {
                DicomPrinter item = dicomPrinter.Item;
                string text1 = item.Name;
                return (text1 == this.DicomPrinterName);
            }
        }
    }
}

