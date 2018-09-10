using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenuSample
{
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

        public class MenuSelectedEventArgs : EventArgs
        {
            public MenuListViewItem Item { get; private set; }
            public MenuSelectedEventArgs(MenuListViewItem item)
            {
                Item = item;
            }
        }
        public event EventHandler<MenuSelectedEventArgs> MenuGroupSelectionChanged;
        public event EventHandler<MenuSelectedEventArgs> MenuDetailSelectionChanged;
        public event EventHandler<MenuSelectedEventArgs> MenuDetailDoubleClick;


        /// <summary>
        /// Redraw menu on property changed
        /// </summary>
        private void RedrawMenu()
        {
            listGroup.Invalidate();
            listDetail.Invalidate();
        }
        public ListMenu()
        {
            InitializeComponent();
            var group1 = new ListViewGroup("group1");
            listGroup.Groups.Add(group1);
            var group2 = new ListViewGroup("group2");
            listGroup.Groups.Add(group2);
            listGroup.Items.Add(new MenuListViewItem(new []{ "aaaaa01" , "subtitle"}, group1));
            listGroup.Items.Add(new MenuListViewItem(new[] { "bbbbb02", "subtitle"}, group1).SubMenuAdd(new MenuListViewItem(new[] { "bbbbb05", "subtitle" }), new MenuListViewItem(new[] { "bbbbb06", "subtitle" })));
            listGroup.Items.Add(new MenuListViewItem(new[] { "aaaaa03", "subtitle"}, group1).SubMenuAdd(new MenuListViewItem(new[] { "bbbbb05", "subtitle" })));
            listGroup.Items.Add(new MenuListViewItem(new[] { "bbbbb04", "subtitle"}, group1));
            listGroup.Items.Add(new MenuListViewItem(new[] { "aaaaa05", "subtitle"}, group2));
            listGroup.Items.Add(new MenuListViewItem(new[] { "bbbbb06", "subtitle"}, group2));
            listGroup.Items.Add(new MenuListViewItem(new[] { "aaaaa07", "subtitle"}, group2));
            listGroup.Items.Add(new MenuListViewItem(new[] { "bbbbb08", "subtitle"}, group2));
            listGroup.Items.Add(new MenuListViewItem(new[] { "aaaaa09", "subtitle"}, group2));
            listGroup.Items.Add(new MenuListViewItem(new[] { "bbbbb10", "subtitle"}, group2));
            (listGroup.Items[0] as MenuListViewItem).Enabled = false;
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
        /// <param name="e">event</param>
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

        private void listGroup_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var item = e.Item as MenuListViewItem;
            if (item == null)
                return;
            MenuGroupSelectionChanged?.Invoke(this, new MenuSelectedEventArgs(item));
            listDetail.SelectedIndices.Clear();
            listDetail.Items.Clear();
            listDetail.Groups.Clear();
            listDetail.Groups.AddRange(item.SubMenuGroups.ToArray());
            listDetail.Items.AddRange(item.SubMenuItems.ToArray());
        }

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

        private void listDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left)
            {
                FocusGroup();
            }
        }

        private void listDetail_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var item = e.Item as MenuListViewItem;
            if (item == null)
                return;
            MenuDetailSelectionChanged?.Invoke(this, new MenuSelectedEventArgs(item));
        }
        public void FocusGroup()
        {
            listGroup.Focus();
        }
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
            MenuDetailDoubleClick?.Invoke(this, new MenuSelectedEventArgs(item));
        }
    }
}
