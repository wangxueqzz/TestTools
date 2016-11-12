using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Macro.ImageViewer.StudyManagement;
using Macro.Desktop.Trees;
using Macro.ImageViewer.StudyManagement.Core;

namespace Macro.ImageViewer.Utilities.Media
{
    public class UIStudyTree
    {
        private Tree<IStudyTreeItem> _tree;

        public Tree<IStudyTreeItem> Tree
        {
            get { return _tree; }
            set { _tree = value; }
        }

        public UIStudyTree()
        {
            _tree = new Tree<IStudyTreeItem>(new StudyTreeItemBinding());
        }

        public void AddStudy(IStudy study)
        {
            StudyTreeItem item = new StudyTreeItem(study);
            _tree.Items.Add(item);
        }

    }

    public interface IStudyTreeItem
    {
        bool Ischecked { get; }
        string PatientName { get; }
        string Studydate { get; }
        string Description { get; }
        void SetChecked(bool check);
    }

    public class StudyTreeItem : IStudyTreeItem
    {
        private readonly IStudy _study;

        public IStudy Study
        {
            get { return _study; }
        } 

        private readonly Tree<ISeriesTreeItem> _tree;
        private bool isChecked = true;

        public Tree<ISeriesTreeItem> Tree
        {
            get { return _tree; }
        }


        internal StudyTreeItem(IStudy study)
        {
            _study = study;
            _tree = new Tree<ISeriesTreeItem>(new SeriesTreeItemBinding());
            Initialize();
        }

        private void Initialize()
        {
            foreach (var item in _study.GetSeries())
            {
                SeriesTreeItem seriesitem = new SeriesTreeItem(item, this);
                _tree.Items.Add(seriesitem);
            }
        }

        public string PatientName
        {
            get { return _study.PatientsName; }
        }

        public string Studydate
        {
            get { return _study.StudyDate; }
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", this.PatientName, this.Studydate);
        }


        public string Description
        {
            get { return _study.StudyDescription; }
        }

        public bool Ischecked
        {
            get { return isChecked; }
        }

        public void SetChecked(bool check)
        {
            isChecked = check;
        }
    }

    public class StudyTreeItemBinding : TreeItemBindingBase
    {
        public override string GetNodeText(object item)
        {
            return ((IStudyTreeItem)item).ToString();
        }

        public override string GetTooltipText(object item)
        {
            return ((IStudyTreeItem)item).Description;
        }

        public override bool CanHaveSubTree(object item)
        {
            return item is StudyTreeItem;
        }

        public override ITree GetSubTree(object item)
        {
            if (item is StudyTreeItem)
            {
                return ((StudyTreeItem)item).Tree;
            }

            return null;
        }

        public override bool GetIsChecked(object item)
        {
            return ((IStudyTreeItem)item).Ischecked;
        }

        public override void SetIsChecked(object item, bool value)
        {
            ((IStudyTreeItem)item).SetChecked(value);
        }
    }

}
