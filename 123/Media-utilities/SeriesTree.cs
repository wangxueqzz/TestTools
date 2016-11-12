using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Macro.ImageViewer.StudyManagement;
using Macro.Desktop.Trees;
using Macro.ImageViewer.StudyManagement.Core;

namespace Macro.ImageViewer.Utilities.Media
{
    public interface ISeriesTreeItem
    {
        IStudyTreeItem ParentStudy { get; }

        bool Ischecked { get; }
        string Modality { get; }
        string Description { get; }
        int Seriesnumber { get; }
        void SetChecked(bool check);
    }

    public class SeriesTreeItem : ISeriesTreeItem
    {
        private readonly ISeries _series;

        public ISeries Series
        {
            get { return _series; }
        } 

        private readonly IStudyTreeItem _parentStudy;
        private bool isChecked = true;

        internal SeriesTreeItem(ISeries series, IStudyTreeItem parent)
        {
            _series = series;
            _parentStudy = parent;
        }

        public IStudyTreeItem ParentStudy
        {
            get { return _parentStudy; }
        }

        public string Modality
        {
            get { return _series.Modality; }
        }

        public string Description
        {
            get { return _series.SeriesDescription; }
        }

        public int Seriesnumber
        {
            get { return _series.SeriesNumber; }
        }

        public bool Ischecked
        {
            get { return isChecked; }
        }

        public void SetChecked(bool check)
        {
            isChecked = check;
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", this.Seriesnumber, this.Modality, this.Description);
        }
    }

    public class SeriesTreeItemBinding : TreeItemBindingBase
    {

        public override string GetNodeText(object item)
        {
            return ((ISeriesTreeItem)item).ToString();
        }

        public override bool CanHaveSubTree(object item)
        {
            return false;
        }

        public override ITree GetSubTree(object item)
        {
            return null;
        }

        public override bool GetIsChecked(object item)
        {
            return ((ISeriesTreeItem)item).Ischecked;
        }

        public override void SetIsChecked(object item, bool value)
        {
            ((ISeriesTreeItem)item).SetChecked(value);
        }
    }
}
