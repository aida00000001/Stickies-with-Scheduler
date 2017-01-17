namespace SwS
{
    partial class frmLauncherSetting
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLauncherSetting));
            this.tvLauncher = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cmsRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.alterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.makeFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeChildFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeChildItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtParameter = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnFileSelect = new System.Windows.Forms.Button();
            this.btnFolderSelect = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ofdFile = new System.Windows.Forms.OpenFileDialog();
            this.cmsRightClickMenu.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvLauncher
            // 
            this.tvLauncher.AllowDrop = true;
            this.tvLauncher.Dock = System.Windows.Forms.DockStyle.Top;
            this.tvLauncher.Location = new System.Drawing.Point(0, 0);
            this.tvLauncher.Name = "tvLauncher";
            this.tvLauncher.Size = new System.Drawing.Size(305, 344);
            this.tvLauncher.TabIndex = 0;
            this.tvLauncher.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvLauncher_MouseUp);
            this.tvLauncher.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvLauncher_DragDrop);
            this.tvLauncher.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvLauncher_MouseDown);
            this.tvLauncher.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvLauncher_DragEnter);
            this.tvLauncher.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvLauncher_ItemDrag);
            this.tvLauncher.DragOver += new System.Windows.Forms.DragEventHandler(this.tvLauncher_DragOver);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cmsRightClickMenu
            // 
            this.cmsRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alterToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.makeFolderToolStripMenuItem,
            this.makeItemToolStripMenuItem,
            this.makeChildFolderToolStripMenuItem,
            this.makeChildItemToolStripMenuItem});
            this.cmsRightClickMenu.Name = "cmsRightClickMenu";
            this.cmsRightClickMenu.Size = new System.Drawing.Size(166, 142);
            this.cmsRightClickMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsRightClickMenu_Opening);
            // 
            // alterToolStripMenuItem
            // 
            this.alterToolStripMenuItem.Name = "alterToolStripMenuItem";
            this.alterToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.alterToolStripMenuItem.Text = "修正";
            this.alterToolStripMenuItem.Click += new System.EventHandler(this.alterToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.deleteToolStripMenuItem.Text = "削除";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // makeFolderToolStripMenuItem
            // 
            this.makeFolderToolStripMenuItem.Name = "makeFolderToolStripMenuItem";
            this.makeFolderToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.makeFolderToolStripMenuItem.Text = "新規フォルダ作成";
            this.makeFolderToolStripMenuItem.Click += new System.EventHandler(this.makeFolderToolStripMenuItem_Click);
            // 
            // makeItemToolStripMenuItem
            // 
            this.makeItemToolStripMenuItem.Name = "makeItemToolStripMenuItem";
            this.makeItemToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.makeItemToolStripMenuItem.Text = "新規項目作成";
            this.makeItemToolStripMenuItem.Click += new System.EventHandler(this.makeItemToolStripMenuItem_Click);
            // 
            // makeChildFolderToolStripMenuItem
            // 
            this.makeChildFolderToolStripMenuItem.Name = "makeChildFolderToolStripMenuItem";
            this.makeChildFolderToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.makeChildFolderToolStripMenuItem.Text = "新規子フォルダ作成";
            this.makeChildFolderToolStripMenuItem.Click += new System.EventHandler(this.makeChildFolderToolStripMenuItem_Click);
            // 
            // makeChildItemToolStripMenuItem
            // 
            this.makeChildItemToolStripMenuItem.Name = "makeChildItemToolStripMenuItem";
            this.makeChildItemToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.makeChildItemToolStripMenuItem.Text = "新規子項目作成";
            this.makeChildItemToolStripMenuItem.Click += new System.EventHandler(this.makeChildItemToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 348);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(305, 84);
            this.panel2.TabIndex = 4;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnCancel);
            this.panel7.Controls.Add(this.btnApply);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 60);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(305, 24);
            this.panel7.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancel.Location = new System.Drawing.Point(68, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnApply.Location = new System.Drawing.Point(0, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(68, 24);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "適用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtParameter);
            this.panel3.Controls.Add(this.txtPath);
            this.panel3.Controls.Add(this.panel9);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(305, 60);
            this.panel3.TabIndex = 0;
            // 
            // txtParameter
            // 
            this.txtParameter.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtParameter.Location = new System.Drawing.Point(64, 39);
            this.txtParameter.Name = "txtParameter";
            this.txtParameter.Size = new System.Drawing.Size(241, 19);
            this.txtParameter.TabIndex = 13;
            // 
            // txtPath
            // 
            this.txtPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtPath.Location = new System.Drawing.Point(64, 20);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(241, 19);
            this.txtPath.TabIndex = 12;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.txtName);
            this.panel9.Controls.Add(this.btnFileSelect);
            this.panel9.Controls.Add(this.btnFolderSelect);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(64, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(241, 20);
            this.panel9.TabIndex = 11;
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtName.Location = new System.Drawing.Point(0, 0);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(153, 19);
            this.txtName.TabIndex = 9;
            // 
            // btnFileSelect
            // 
            this.btnFileSelect.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFileSelect.Location = new System.Drawing.Point(153, 0);
            this.btnFileSelect.Name = "btnFileSelect";
            this.btnFileSelect.Size = new System.Drawing.Size(42, 20);
            this.btnFileSelect.TabIndex = 8;
            this.btnFileSelect.Text = "File";
            this.btnFileSelect.UseVisualStyleBackColor = true;
            this.btnFileSelect.Click += new System.EventHandler(this.btnFileSelect_Click);
            // 
            // btnFolderSelect
            // 
            this.btnFolderSelect.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFolderSelect.Location = new System.Drawing.Point(195, 0);
            this.btnFolderSelect.Name = "btnFolderSelect";
            this.btnFolderSelect.Size = new System.Drawing.Size(46, 20);
            this.btnFolderSelect.TabIndex = 7;
            this.btnFolderSelect.Text = "Folder";
            this.btnFolderSelect.UseVisualStyleBackColor = true;
            this.btnFolderSelect.Click += new System.EventHandler(this.btnFolderSelect_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(64, 60);
            this.panel4.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "パラメータ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "パス";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "表示名";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ofdFile
            // 
            this.ofdFile.Filter = "(全て)|*.*";
            this.ofdFile.RestoreDirectory = true;
            this.ofdFile.SupportMultiDottedExtensions = true;
            // 
            // frmLauncherSetting
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 432);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tvLauncher);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLauncherSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ランチャ設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLauncherSetting_FormClosing);
            this.Resize += new System.EventHandler(this.frmLauncherSetting_Resize);
            this.cmsRightClickMenu.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvLauncher;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip cmsRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem alterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeChildFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem makeChildItemToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.OpenFileDialog ofdFile;
        private System.Windows.Forms.TextBox txtParameter;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnFileSelect;
        private System.Windows.Forms.Button btnFolderSelect;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}