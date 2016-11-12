
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

namespace Macro.ImageViewer.Utilities.Print.Dicom
{
    public class FilmSession : ICloneable
    {
        private Macro.Dicom.Iod.Modules.FilmDestination _filmDestination;
        private Macro.Dicom.Iod.Modules.MediumType _ediumType;
        private Macro.Dicom.Iod.Modules.PrintPriority _printPriority;
        private int _numberOfCopies = 1;

        public FilmSession()
        {
            _filmDestination = Macro.Dicom.Iod.Modules.FilmDestination.None;
            _printPriority = Macro.Dicom.Iod.Modules.PrintPriority.None;
            _ediumType = Macro.Dicom.Iod.Modules.MediumType.None;
        }

        public FilmSession(Macro.Dicom.Iod.Modules.FilmDestination filmDestination, Macro.Dicom.Iod.Modules.PrintPriority printPriority, Macro.Dicom.Iod.Modules.MediumType mediumType, int numberOfCopies)
        {
            _filmDestination = filmDestination;
            _ediumType = mediumType;
            _printPriority = printPriority;
            _numberOfCopies = numberOfCopies;
        }

        public object Clone()
        {
            FilmSession session = new FilmSession(this.FilmDestination, this.PrintPriority, this.MediumType, this.NumberOfCopies);
            return session;
        }

        public Macro.Dicom.Iod.Modules.FilmDestination FilmDestination
        {
            get { return _filmDestination; }
            set { _filmDestination = value; }
        }

        public Macro.Dicom.Iod.Modules.MediumType MediumType
        {
            get { return _ediumType; }
            set { _ediumType = value; }
        }

        public int NumberOfCopies
        {
            get { return _numberOfCopies; }
            set { _numberOfCopies = value; }
        }

        public Macro.Dicom.Iod.Modules.PrintPriority PrintPriority
        {
            get { return _printPriority; }
            set { _printPriority = value; }
        }
    }
}

