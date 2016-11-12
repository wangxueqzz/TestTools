
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
using System.Collections;
using System.Globalization;
using System.Windows.Forms;
using Macro.Desktop.View.WinForms;
using Macro.ImageViewer.Utilities.Print.Dicom;

namespace Macro.ImageViewer.Utilities.Print.View.WinForm
{
    public partial class AdvancedConfigurationComponentControl : ApplicationComponentUserControl
    {
        private IDicomPrinterAdvancedConfigurationComponent _component;
        private bool flag;

        public AdvancedConfigurationComponentControl(IDicomPrinterAdvancedConfigurationComponent component)
            : base(component)
        {
            InitializeComponent();
            _component = component;
            this._component.ValidationVisibleChanged += new EventHandler(this.ValidationVisibleChanged);
            this.FilmSizeList.Items.Clear();
            IEnumerator enumerator1 = _component.AutoSelectFilmSizes.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                object current = enumerator1.Current;
                int num1 = this.FilmSizeList.Items.Add(current);
            }
            flag = true;
            IEnumerator enumerator2 = _component.AvailableFilmSizes.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                object obj3 = enumerator2.Current;
                int index = this.FilmSizeList.Items.IndexOf(obj3);
                this.FilmSizeList.SetItemChecked(index, true);
            }

            flag = false;
            this.FilmSizeList.ItemCheck += ItemChecked;

            this.HorizontalMargin.DataBindings.Add("Text", this, "HorizontalMargins", true, DataSourceUpdateMode.OnPropertyChanged);
            this.VerticalMargin.DataBindings.Add("Text", this, "VerticalMargins", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void ValidationVisibleChanged(object sender, EventArgs args)
        {
            string str = string.Empty;
            IList list1 = _component.AvailableFilmSizes;
            if (list1.Count < 1)
            {
                str = "没有选中的排版格式";
            }
            base.ErrorProvider.SetError(this.FilmSizeList, str);
        }

        private void ItemChecked(object sender, ItemCheckEventArgs args)
        {
            if (!flag)
            {
                CheckedListBox.ObjectCollection items = this.FilmSizeList.Items;
                int index = args.Index;
                object obj2 = items[index];
                if (args.NewValue == CheckState.Checked)
                {
                    int num2 = _component.AvailableFilmSizes.Add(obj2);
                }
                else
                {
                    _component.AvailableFilmSizes.Remove(obj2);
                }
            }
        }

        // Properties
        public string HorizontalMargins
        {
            get
            {
                float num = _component.HorizontalMargins;
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                return num.ToString(currentCulture);
            }
            set
            {

                float result = 0f;
                if (!string.IsNullOrEmpty(value))
                {
                    CultureInfo currentCulture = CultureInfo.CurrentCulture;
                    if (!float.TryParse(value, NumberStyles.Float, currentCulture, out result))
                    {

                        base.ErrorProvider.SetError(this.HorizontalMargin, "输入错误");
                    }
                }
                _component.HorizontalMargins = result;
            }
        }

        public string VerticalMargins
        {
            get
            {
                float num = _component.VerticalMargins;
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                return num.ToString(currentCulture);
            }
            set
            {

                float result = 0f;
                if (!string.IsNullOrEmpty(value))
                {
                    CultureInfo currentCulture = CultureInfo.CurrentCulture;
                    if (!float.TryParse(value, NumberStyles.Float, currentCulture, out result))
                    {
                        base.ErrorProvider.SetError(this.VerticalMargin, "输入错误");
                    }
                }
                _component.VerticalMargins = result;

            }
        }

    }
}
