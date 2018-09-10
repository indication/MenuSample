namespace MenuSample
{
    partial class ListMenu
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listGroup = new MenuSample.DoubleBufferingListView();
            this.listDetail = new MenuSample.DoubleBufferingListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listGroup);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listDetail);
            this.splitContainer1.Size = new System.Drawing.Size(631, 407);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // listGroup
            // 
            this.listGroup.AutoArrange = false;
            this.listGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.listGroup.LabelWrap = false;
            this.listGroup.Location = new System.Drawing.Point(0, 0);
            this.listGroup.Name = "listGroup";
            this.listGroup.ShowItemToolTips = true;
            this.listGroup.Size = new System.Drawing.Size(186, 407);
            this.listGroup.TabIndex = 0;
            this.listGroup.TileSize = new System.Drawing.Size(169, 50);
            this.listGroup.UseCompatibleStateImageBehavior = false;
            this.listGroup.View = System.Windows.Forms.View.Tile;
            this.listGroup.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listGroup_ItemSelectionChanged);
            this.listGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listGroup_KeyDown);
            this.listGroup.Resize += new System.EventHandler(this.listGroup_Resize);
            // 
            // listDetail
            // 
            this.listDetail.AutoArrange = false;
            this.listDetail.Dock = System.Windows.Forms.DockStyle.Left;
            this.listDetail.LabelWrap = false;
            this.listDetail.Location = new System.Drawing.Point(0, 0);
            this.listDetail.Name = "listDetail";
            this.listDetail.ShowItemToolTips = true;
            this.listDetail.Size = new System.Drawing.Size(371, 407);
            this.listDetail.TabIndex = 0;
            this.listDetail.TileSize = new System.Drawing.Size(354, 50);
            this.listDetail.UseCompatibleStateImageBehavior = false;
            this.listDetail.View = System.Windows.Forms.View.Tile;
            this.listDetail.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listDetail_ItemSelectionChanged);
            this.listDetail.DoubleClick += new System.EventHandler(this.listDetail_DoubleClick);
            this.listDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listDetail_KeyDown);
            // 
            // ListMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ListMenu";
            this.Size = new System.Drawing.Size(631, 407);
            this.Load += new System.EventHandler(this.ListMenu_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DoubleBufferingListView listGroup;
        private DoubleBufferingListView listDetail;
    }
}
