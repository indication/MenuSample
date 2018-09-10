using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenuSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var group1 = new ListViewGroup("group1");
            listMenu1.Groups.Add(group1);
            var group2 = new ListViewGroup("group2");
            listMenu1.Groups.Add(group2);
            listMenu1.Items.Add(new MenuListViewItem(new[] { "aaaaa01", "subtitle" }, group1));
            listMenu1.Items.Add(new MenuListViewItem(new[] { "bbbbb02", "subtitle" }, group1).SubMenuAdd(new MenuListViewItem(new[] { "bbbbb05", "subtitle" }), new MenuListViewItem(new[] { "bbbbb06", "subtitle" })));
            listMenu1.Items.Add(new MenuListViewItem(new[] { "aaaaa03", "subtitle" }, group1).SubMenuAdd(new MenuListViewItem(new[] { "bbbbb05", "subtitle" })));
            listMenu1.Items.Add(new MenuListViewItem(new[] { "bbbbb04", "subtitle" }, group1));
            listMenu1.Items.Add(new MenuListViewItem(new[] { "aaaaa05", "subtitle" }, group2));
            listMenu1.Items.Add(new MenuListViewItem(new[] { "bbbbb06", "subtitle" }, group2));
            listMenu1.Items.Add(new MenuListViewItem(new[] { "aaaaa07", "subtitle" }, group2));
            listMenu1.Items.Add(new MenuListViewItem(new[] { "bbbbb08", "subtitle" }, group2));
            listMenu1.Items.Add(new MenuListViewItem(new[] { "aaaaa09", "subtitle" }, group2));
            listMenu1.Items.Add(new MenuListViewItem(new[] { "bbbbb10", "subtitle" }, group2));
            (listMenu1.Items[0] as MenuListViewItem).Enabled = false;
        }
    }
}
