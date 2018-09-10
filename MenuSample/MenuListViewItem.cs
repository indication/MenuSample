using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenuSample
{
    public class MenuListViewItem : ListViewItem
    {

        public bool Enabled { get; set; } = true;
        public List<ListViewGroup> SubMenuGroups { get; } = new List<ListViewGroup>();
        public List<MenuListViewItem> SubMenuItems { get; } = new List<MenuListViewItem>();
        public MenuListViewItem()
        {
        }

        public MenuListViewItem(string text) : base(text)
        {
        }

        public MenuListViewItem(string[] items) : base(items)
        {
        }

        public MenuListViewItem(ListViewGroup group) : base(group)
        {
        }

        public MenuListViewItem(string text, int imageIndex) : base(text, imageIndex)
        {
        }

        public MenuListViewItem(string[] items, int imageIndex) : base(items, imageIndex)
        {
        }

        public MenuListViewItem(ListViewSubItem[] subItems, int imageIndex) : base(subItems, imageIndex)
        {
        }

        public MenuListViewItem(string text, ListViewGroup group) : base(text, group)
        {
        }

        public MenuListViewItem(string[] items, ListViewGroup group) : base(items, group)
        {
        }

        public MenuListViewItem(string text, string imageKey) : base(text, imageKey)
        {
        }

        public MenuListViewItem(string[] items, string imageKey) : base(items, imageKey)
        {
        }

        public MenuListViewItem(ListViewSubItem[] subItems, string imageKey) : base(subItems, imageKey)
        {
        }

        public MenuListViewItem(string text, int imageIndex, ListViewGroup group) : base(text, imageIndex, group)
        {
        }

        public MenuListViewItem(string[] items, int imageIndex, ListViewGroup group) : base(items, imageIndex, group)
        {
        }

        public MenuListViewItem(ListViewSubItem[] subItems, int imageIndex, ListViewGroup group) : base(subItems, imageIndex, group)
        {
        }

        public MenuListViewItem(string text, string imageKey, ListViewGroup group) : base(text, imageKey, group)
        {
        }

        public MenuListViewItem(string[] items, string imageKey, ListViewGroup group) : base(items, imageKey, group)
        {
        }

        public MenuListViewItem(ListViewSubItem[] subItems, string imageKey, ListViewGroup group) : base(subItems, imageKey, group)
        {
        }

        public MenuListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font) : base(items, imageIndex, foreColor, backColor, font)
        {
        }

        public MenuListViewItem(string[] items, string imageKey, Color foreColor, Color backColor, Font font) : base(items, imageKey, foreColor, backColor, font)
        {
        }

        public MenuListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font, ListViewGroup group) : base(items, imageIndex, foreColor, backColor, font, group)
        {
        }

        public MenuListViewItem(string[] items, string imageKey, Color foreColor, Color backColor, Font font, ListViewGroup group) : base(items, imageKey, foreColor, backColor, font, group)
        {
        }

        public MenuListViewItem SubMenuAdd(params MenuListViewItem[] items)
        {
            SubMenuItems.AddRange(items);
            return this;
        }

        public MenuListViewItem SubMenuAdd(IEnumerable<MenuListViewItem> items)
        {
            SubMenuItems.AddRange(items);
            return this;
        }
        public MenuListViewItem SubMenuAdd(params ListViewGroup[] items)
        {
            SubMenuGroups.AddRange(items);
            return this;
        }

        public MenuListViewItem SubMenuAdd(IEnumerable<ListViewGroup> items)
        {
            SubMenuGroups.AddRange(items);
            return this;
        }
    }
}
