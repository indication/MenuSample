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

            var subgroup1 = new ListViewGroup("sub group1");
            var subgroup2 = new ListViewGroup("sub group2");
            listMenu1.Items.Add(new MenuListViewItem(new[] { "bbbbb02", "subtitle" }, group1)
                .SubMenuAdd(
                    new MenuListViewItem(new[] { "bbbbb05", "subtitle" }, subgroup1)
                    , new MenuListViewItem(new[] { "bbbbb06", "subtitle" }, subgroup1)
                    , new MenuListViewItem(new[] { "bbbbb07", "subtitle" }, subgroup2)
                    )
                .SubMenuAdd(subgroup1, subgroup2));
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

        private void listMenu1_MenuGroupSelectionChanged(object sender, MenuEventArgs e)
        {
            textLog.AppendText(string.Format("Group selected: {0}\r\n", e.Item.Text));
        }

        private void listMenu1_MenuDetailSelectionChanged(object sender, MenuEventArgs e)
        {
            textLog.AppendText(string.Format("Detail selected: {0}\r\n", e.Item.Text));
        }

        private void listMenu1_MenuDetailDoubleClick(object sender, MenuEventArgs e)
        {
            textLog.AppendText(string.Format("Detail double clicked: {0}\r\n", e.Item.Text));
        }
    }
}
