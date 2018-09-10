using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MenuSample
{
    /// <summary>
    /// Buffered list view
    /// </summary>
    /// <remarks>
    /// Avoid to flick menu items on scroll
    /// </remarks>
    internal partial class DoubleBufferingListView : ListView
    {
        #region Properties
        /// <summary>
        /// Height for each items
        /// </summary>
        [DefaultValue(50)]
        public int ItemHeight { get; set; } = 50;
        /// <summary>
        /// Padding for each items
        /// </summary>
        [DefaultValue(typeof(Padding), "5, 0, 0, 0")]
        public Padding ItemPadding { get; set; } = new Padding(5, 0, 0, 0);

        /// <summary>
        /// Title font setting
        /// </summary>
        [DefaultValue(null)]
        public Font TitleFont { get; set; } = null;
        /// <summary>
        /// Subtitle font settings
        /// </summary>
        [DefaultValue(null)]
        public Font SubFont { get; set; } = null;
        /// <summary>
        /// Margin
        /// </summary>
        [DefaultValue(typeof(Padding), "0, 0, 0, 0")]
        public new Padding Margin {
            get { return base.Margin; }
            set { base.Margin = value; }
        }
        /// <summary>
        /// Multi select (disabled)
        /// </summary>
        [DefaultValue(false)]
        [Browsable(false)]
        public new bool MultiSelect
        {
            get { return base.MultiSelect; }
            set { base.MultiSelect = value; }
        }
        /// <summary>
        /// Owner draw (strict)
        /// </summary>
        [DefaultValue(true)]
        [Browsable(false)]
        public new bool OwnerDraw
        {
            get { return base.OwnerDraw; }
            set { base.OwnerDraw = value; }
        }
        /// <summary>
        /// Hide selection after focus out
        /// </summary>
        [DefaultValue(false)]
        [Browsable(false)]
        public new bool HideSelection
        {
            get { return base.HideSelection; }
            set { base.HideSelection = value; }
        }
        /// <summary>
        /// Border style
        /// </summary>
        [DefaultValue(BorderStyle.None)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }
        /// <summary>
        /// Header style
        /// </summary>
        [DefaultValue(ColumnHeaderStyle.None)]
        [Browsable(false)]
        public new ColumnHeaderStyle HeaderStyle
        {
            get { return base.HeaderStyle; }
            set { base.HeaderStyle = value; }
        }
        #endregion

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
            if (!isEnabled || !Enabled)
            {
                // On disabled, change color
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
            // Setup focus rectangle (dot rect)
            if (e.State.HasFlag(ListViewItemStates.Focused))
                e.DrawFocusRectangle();

            // Calcurate title size and draw text
            var rect = new RectangleF(e.Bounds.X + ItemPadding.Left, e.Bounds.Y + ItemPadding.Top, e.Bounds.Width - ItemPadding.Horizontal, e.Bounds.Height - ItemPadding.Vertical);
            if (TitleFont == null)
                TitleFont = new Font(Font.FontFamily, 12);
            e.Graphics.DrawString(e.Item.Text, TitleFont, text, rect.Left, rect.Top);

            // Calcurate sub title and draw text
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

        /// <summary>
        /// Selection changed event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event parameter</param>
        /// <remarks>
        /// Unselect item on disabled item
        /// </remarks>
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
