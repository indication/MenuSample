using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MenuSample
{
    /// <summary>
    /// Buffered list view
    /// </summary>
    public partial class DoubleBufferingListView : ListView
    {
        [DefaultValue(50)]
        public int ItemHeight { get; set; } = 50;
        /// <summary>
        /// Padding for each items
        /// </summary>
        [DefaultValue(typeof(Padding), "5, 0, 0, 0")]
        public Padding ItemPadding { get; set; } = new Padding(5, 0, 0, 0);

        private Font TitleFont { get; set; } = null;
        private Font SubFont { get; set; } = null;

        [DefaultValue(typeof(Padding), "0, 0, 0, 0")]
        public new Padding Margin {
            get { return base.Margin; }
            set { base.Margin = value; }
        }

        [DefaultValue(false)]
        [Browsable(false)]
        public new bool MultiSelect
        {
            get { return base.MultiSelect; }
            set { base.MultiSelect = value; }
        }

        [DefaultValue(true)]
        [Browsable(false)]
        public new bool OwnerDraw
        {
            get { return base.OwnerDraw; }
            set { base.OwnerDraw = value; }
        }

        [DefaultValue(false)]
        [Browsable(false)]
        public new bool HideSelection
        {
            get { return base.HideSelection; }
            set { base.HideSelection = value; }
        }

        [DefaultValue(BorderStyle.None)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }

        [DefaultValue(ColumnHeaderStyle.None)]
        [Browsable(false)]
        public new ColumnHeaderStyle HeaderStyle
        {
            get { return base.HeaderStyle; }
            set { base.HeaderStyle = value; }
        }

        //public Brush 

        /// <summary>
        /// Constructor
        /// </summary>
        public DoubleBufferingListView()
        {
            InitializeComponent();
            // Enable double buffering
            DoubleBuffered = true;
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /// <summary>
        /// User draw the object
        /// </summary>
        /// <param name="pe">event</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        /// <summary>
        /// Draw items
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event parameter</param>
        private void DoubleBufferingListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            var list = sender as ListView;
            if (list == null)
                return;
            //e.DrawBackground();
            Brush background;
            Brush text;
            var isEnabled = (e.Item as MenuListViewItem)?.Enabled ?? true;
            if (!isEnabled)
            {
                background = SystemBrushes.ControlDark;
                text = SystemBrushes.WindowText;
            }
            else if (SelectedIndices.Contains(e.ItemIndex))
            /*if (e.State.HasFlag(ListViewItemStates.Focused)
                 || (e.State.HasFlag(ListViewItemStates.Selected) && !e.State.HasFlag(ListViewItemStates.ShowKeyboardCues) && !list.Focused)
                ) */
            {
                if (Focused)
                {
                    background = SystemBrushes.Highlight;
                    text = SystemBrushes.HighlightText;
                } else
                {
                    background = SystemBrushes.GradientActiveCaption;
                    text = SystemBrushes.WindowText;
                }
            }
            else if (e.State.HasFlag(ListViewItemStates.Hot))
            {
                background = SystemBrushes.GradientActiveCaption;
                text = SystemBrushes.WindowText;
            }
            else
            {
                background = SystemBrushes.Window;
                text = SystemBrushes.WindowText;
            }
            e.Graphics.FillRectangle(background, e.Bounds);

            if (e.State.HasFlag(ListViewItemStates.Focused))
                e.DrawFocusRectangle();

            var rect = new RectangleF(e.Bounds.X + ItemPadding.Left, e.Bounds.Y + ItemPadding.Top, e.Bounds.Width - ItemPadding.Horizontal, e.Bounds.Height - ItemPadding.Vertical);
            if (TitleFont == null)
                TitleFont = new Font(Font.FontFamily, 12);
            e.Graphics.DrawString(e.Item.Text, TitleFont, text, rect.Left, rect.Top);

            if (e.Item.SubItems.Count > 1)
            {
                var titleSize = e.Graphics.MeasureString(e.Item.Text, TitleFont);
                var sub = new RectangleF(e.Bounds.X + ItemPadding.Left, rect.Top + titleSize.Height, rect.Width, rect.Height - titleSize.Height - ItemPadding.Bottom);
                //Debug.WriteLine("bound: {3}, rect: {0}, title: {1}, sub: {2}", rect, titleSize, sub, e.Bounds);
                if (SubFont == null)
                    SubFont = new Font(Font.FontFamily, 10);
                e.Graphics.DrawString(e.Item.SubItems[1].Text, SubFont, text, sub);
            }

            //e.DrawText();
        }
        /// <summary>
        /// ListView Resize event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event parameter</param>
        private void DoubleBufferingListView_Resize(object sender, System.EventArgs e)
        {
            var list = sender as ListView;
            if (list == null)
                return;
            var size = new Size(list.Width/* - list.Margin.Horizontal - list.Padding.Horizontal*/ - SystemInformation.VerticalScrollBarWidth, ItemHeight);
            if (TileSize != size)
                TileSize = size;
        }

        private void DoubleBufferingListView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            
        }

        private void DoubleBufferingListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var isEnabled = (e.Item as MenuListViewItem)?.Enabled ?? true;
            if (!isEnabled && e.IsSelected)
            {
                e.Item.Selected = false;
            }
        }

    }
}
