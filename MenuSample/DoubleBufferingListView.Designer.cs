namespace MenuSample
{
    partial class DoubleBufferingListView
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DoubleBufferingListView
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.HideSelection = false;
            this.LabelWrap = false;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MultiSelect = false;
            this.OwnerDraw = true;
            this.ShowItemToolTips = true;
            this.View = System.Windows.Forms.View.Tile;
            this.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.DoubleBufferingListView_DrawItem);
            this.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.DoubleBufferingListView_ItemSelectionChanged);
            this.Resize += new System.EventHandler(this.DoubleBufferingListView_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
