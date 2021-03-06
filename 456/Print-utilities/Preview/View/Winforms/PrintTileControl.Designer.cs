
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

#region License

// Copyright (c) 2006-2008, Macro Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice, 
//      this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, 
//      this list of conditions and the following disclaimer in the documentation 
//      and/or other materials provided with the distribution.
//    * Neither the name of Macro Inc. nor the names of its contributors 
//      may be used to endorse or promote products derived from this software without 
//      specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.

#endregion

using System;
using System.Drawing;
using Macro.ImageViewer.InputManagement;
using Macro.Common.Utilities;

namespace Macro.ImageViewer.Utilities.Print.Dicom.Preview.View.Winforms
{
    partial class PrintTileControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            	_tilesToRepaint.Remove(this);

				DisposeSurface();

				if (components != null)
					components.Dispose();

				if (_tile != null)
				{
					_tile.Drawing -= new EventHandler(OnTileDrawing);
					_tile.RendererChanged -= new EventHandler(OnRendererChanged);
					_tile.EditBoxChanged -= new EventHandler(OnEditBoxChanged);
					_tile.InformationBoxChanged -= new EventHandler<InformationBoxChangedEventArgs>(OnInformationBoxChanged);
					_tile.SelectionChanged -= new EventHandler<ItemEventArgs<ITile>>(OnTileSelectionChanged);

					_tileController.CursorTokenChanged -= new EventHandler(OnCursorTokenChanged);
					_tileController.ContextMenuRequested -= new EventHandler<ItemEventArgs<Point>>(OnContextMenuRequested);
					_tileController.CaptureChanging -= new EventHandler<ItemEventArgs<IMouseButtonHandler>>(OnCaptureChanging);
					_tileController.Dispose();

					_tile = null;
				}

				_tileController = null;
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintTileControl));
            this._contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // _contextMenuStrip
            // 
            this._contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._contextMenuStrip.Name = "_contextMenuStrip";
            resources.ApplyResources(this._contextMenuStrip, "_contextMenuStrip");
            // 
            // _toolTip
            // 
            this._toolTip.Active = false;
            this._toolTip.AutomaticDelay = 0;
            this._toolTip.UseAnimation = false;
            this._toolTip.UseFading = false;
            // 
            // PrintTileControl
            // 
            this.ContextMenuStrip = this._contextMenuStrip;
            resources.ApplyResources(this, "$this");
            this.Name = "PrintTileControl";
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ContextMenuStrip _contextMenuStrip;
        private System.Windows.Forms.ToolTip _toolTip;
    }
}
