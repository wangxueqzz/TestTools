
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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    public class SelectPresentationsInformationsCollection : ICollection<ISelectPresentationsInformation>, IDisposable
    {
        private ICollection<ISelectPresentationsInformation> _presentationCollection;

        public SelectPresentationsInformationsCollection(IList<ISelectPresentationsInformation> list)
        {
            this._presentationCollection = new Collection<ISelectPresentationsInformation>(list);
        }

        #region ICollection<ISelectPresentationsInformation> 成员

        public void Add(ISelectPresentationsInformation item)
        {
            if (item == null)
            {
                return;
            }

            if (!_presentationCollection.Contains(item))
            {
                _presentationCollection.Add(item);
            }
        }

        public void Clear()
        {
            _presentationCollection.Clear();
        }

        public bool Contains(ISelectPresentationsInformation item)
        {
            if (_presentationCollection.Contains(item))
            {
                return true;
            }
            return false;
        }

        public void CopyTo(ISelectPresentationsInformation[] array, int arrayIndex)
        {
            _presentationCollection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _presentationCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return _presentationCollection.IsReadOnly; }
        }

        public bool Remove(ISelectPresentationsInformation item)
        {
            if (_presentationCollection.Contains(item))
            {
                _presentationCollection.Remove(item);
            }
            return true;
        }

        #endregion

        #region IEnumerable<ISelectPresentationsInformation> 成员

        public IEnumerator<ISelectPresentationsInformation> GetEnumerator()
        {
            foreach (var item in _presentationCollection)
            {
                yield return item;
            }
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _presentationCollection.GetEnumerator();
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this.Dispose(true);
            GC.Collect();
        }

        protected virtual void Dispose(bool flag)
        {
            foreach (var presentationsInformation in _presentationCollection)
            {
                presentationsInformation.Dispose();
            }
        }

        #endregion
    }
}
