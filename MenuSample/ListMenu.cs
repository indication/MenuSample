using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MenuSample
{
    /// <summary>
    /// Menu control
    /// </summary>
    public partial class ListMenu : UserControl
    {
        /// <summary>
        /// Padding for each list item
        /// </summary>
        [Browsable(true)]
        [DefaultValue(typeof(Padding), "5, 0, 0, 0")]
        public Padding ItemPadding
        {
            get { return listGroup.ItemPadding; }
            set
            {
                if (listGroup.ItemPadding == value)
                    return;
                listGroup.ItemPadding = value;
                listDetail.ItemPadding = value;
                RedrawMenu();
            }
        }
        /// <summary>
        /// Item height
        /// </summary>
        [Browsable(true)]
        [DefaultValue(50)]
        public int ItemHeight
        {
            get { return listGroup.ItemHeight; }
            set
            {
                if (listGroup.ItemHeight == value)
                    return;
                listGroup.ItemHeight = value;
                listDetail.ItemHeight = value;
                RedrawMenu();
            }
        }

        /// <summary>
        /// Menu item group selection changed
        /// </summary>
        public event EventHandler<MenuEventArgs> MenuGroupSelectionChanged;
        /// <summary>
        /// Menu item detail selection changed
        /// </summary>
        public event EventHandler<MenuEventArgs> MenuDetailSelectionChanged;
        /// <summary>
        /// Menu item detail double clicked
        /// </summary>
        public event EventHandler<MenuEventArgs> MenuDetailDoubleClick;

        /// <summary>
        /// Menu items
        /// </summary>
        [Browsable(false)]
        public ListView.ListViewItemCollection Items
        {
            get { return listGroup.Items; }
        }

        /// <summary>
        /// Menu groups
        /// </summary>
        [Browsable(false)]
        public ListViewGroupCollection Groups
        {
            get { return listGroup.Groups; }
        }

        /// <summary>
        /// Redraw menu on property changed
        /// </summary>
        private void RedrawMenu()
        {
            listGroup.Invalidate();
            listDetail.Invalidate();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public ListMenu()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event</param>
        private void ListMenu_Load(object sender, EventArgs e)
        {
            listMenu_Resized();
        }
        /// <summary>
        /// Resize event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event</param>
        private void listGroup_Resize(object sender, EventArgs e)
        {
            listMenu_Resized();
        }
        /// <summary>
        /// Splitter resized event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event parameter</param>
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            listMenu_Resized();
        }
        /// <summary>
        /// Recalcurate form object sizes
        /// </summary>
        private void listMenu_Resized()
        {
            var width = Width - Padding.Horizontal;
            var leftSize = splitContainer1.SplitterDistance;
            var rightSize = width - leftSize - splitContainer1.SplitterWidth;

            listGroup.TileSize = new Size(leftSize - SystemInformation.VerticalScrollBarWidth, listGroup.TileSize.Height);
            listGroup.Width = leftSize;
            listDetail.TileSize = new Size(rightSize - SystemInformation.VerticalScrollBarWidth, listGroup.TileSize.Height);
            listDetail.Width = rightSize;
        }

        /// <summary>
        /// Group selection changed event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event parameter</param>
        private void listGroup_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var item = e.Item as MenuListViewItem;
            if (item == null || !e.IsSelected)
                return;
            MenuGroupSelectionChanged?.Invoke(this, new MenuEventArgs(item));
            listDetail.SelectedIndices.Clear();
            var storedGroups = new List<ListViewGroup>();
            var keys = listDetail.Items.Cast<ListViewItem>().ToDictionary(k => k, i => i.Group);
            listDetail.Items.Clear();
            listDetail.Groups.Clear();
            foreach (var pair in keys.Where(w => w.Value != null))
                pair.Key.Group = pair.Value;
            listDetail.Items.AddRange(item.SubMenuItems.ToArray());
            listDetail.Groups.AddRange(item.SubMenuGroups.ToArray());
        }
        /// <summary>
        /// Detail selection changed event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event parameter</param>
        private void listDetail_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var item = e.Item as MenuListViewItem;
            if (item == null || !e.IsSelected)
                return;
            MenuDetailSelectionChanged?.Invoke(this, new MenuEventArgs(item));
        }

        #region Key events
        /// <summary>
        /// Group key down event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event parameter</param>
        private void listGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Right)
            {
                if (listDetail.Items.Count > 0)
                {
                    FocusDetail(true);
                }
            }
        }

        /// <summary>
        /// Detail key down event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event parameter</param>
        private void listDetail_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Left:
                    FocusGroup();
                    break;
                case Keys.Enter:
                    listDetail_DoubleClick(sender, e);
                    break;
                default:
                    break;
            }
        }
        #endregion

        /// <summary>
        /// Focus to group
        /// </summary>
        public void FocusGroup()
        {
            listGroup.Focus();
        }
        /// <summary>
        /// Focus to detail
        /// </summary>
        /// <param name="isSelect">If not selected, select first item</param>
        public void FocusDetail(bool isSelect)
        {
            listDetail.Focus();
            if (isSelect)
            {
                if (listDetail.SelectedItems.Count == 0)
                    foreach (var item in listDetail.Items)
                    {
                        var menuItem = item as MenuListViewItem;
                        if (menuItem == null || !menuItem.Enabled)
                            continue;
                        menuItem.Selected = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Double click event on detail
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event parameter</param>
        private void listDetail_DoubleClick(object sender, EventArgs e)
        {
            var list = sender as ListView;
            if (list == null)
                return;
            if (list.SelectedItems.Count != 1)
                return;
            var item = list.SelectedItems[0] as MenuListViewItem;
            if (item == null || !item.Enabled)
                return;
            MenuDetailDoubleClick?.Invoke(this, new MenuEventArgs(item));
        }
    }
}
