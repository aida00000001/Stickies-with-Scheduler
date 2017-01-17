namespace SwS
{
    partial class frmStickies
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStickies));
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stickiesSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stickiesCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stickiesDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stickiesDestroyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.topMostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.alphaSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alphaSetting100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alphaSetting080ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alphaSetting060ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alphaSetting050ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alphaSetting040ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alphaSetting020ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.背景画像配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutSettingNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutSettingTileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutSettingCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutSettingStretchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutSettingZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSticky = new System.Windows.Forms.TextBox();
            this.ttTips = new System.Windows.Forms.ToolTip(this.components);
            this.tTimer = new System.Windows.Forms.Timer(this.components);
            this.ulsbClose = new UCLabel.ucLabelSunkenButton();
            this.llblDisplay = new UCLabel.ucLinkLabel();
            this.cmsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stickiesSettingToolStripMenuItem,
            this.toolStripSeparator1,
            this.stickiesCloseToolStripMenuItem,
            this.stickiesDeleteToolStripMenuItem,
            this.stickiesDestroyToolStripMenuItem,
            this.toolStripSeparator4,
            this.topMostToolStripMenuItem,
            this.toolStripSeparator2,
            this.alphaSettingToolStripMenuItem,
            this.toolStripSeparator3,
            this.背景画像配置ToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(170, 182);
            this.cmsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsMenu_Opening);
            // 
            // stickiesSettingToolStripMenuItem
            // 
            this.stickiesSettingToolStripMenuItem.Name = "stickiesSettingToolStripMenuItem";
            this.stickiesSettingToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.stickiesSettingToolStripMenuItem.Text = "付箋個別設定";
            this.stickiesSettingToolStripMenuItem.Click += new System.EventHandler(this.stickiesSettingToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // stickiesCloseToolStripMenuItem
            // 
            this.stickiesCloseToolStripMenuItem.Name = "stickiesCloseToolStripMenuItem";
            this.stickiesCloseToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.stickiesCloseToolStripMenuItem.Text = "付箋を閉じる";
            this.stickiesCloseToolStripMenuItem.Click += new System.EventHandler(this.stickiesCloseToolStripMenuItem_Click);
            // 
            // stickiesDeleteToolStripMenuItem
            // 
            this.stickiesDeleteToolStripMenuItem.Name = "stickiesDeleteToolStripMenuItem";
            this.stickiesDeleteToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.stickiesDeleteToolStripMenuItem.Text = "付箋を解除";
            this.stickiesDeleteToolStripMenuItem.Click += new System.EventHandler(this.stickiesDeleteToolStripMenuItem_Click);
            // 
            // stickiesDestroyToolStripMenuItem
            // 
            this.stickiesDestroyToolStripMenuItem.Name = "stickiesDestroyToolStripMenuItem";
            this.stickiesDestroyToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.stickiesDestroyToolStripMenuItem.Text = "付箋・内容共に破棄";
            this.stickiesDestroyToolStripMenuItem.Click += new System.EventHandler(this.stickiesDestroyToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(166, 6);
            // 
            // topMostToolStripMenuItem
            // 
            this.topMostToolStripMenuItem.Name = "topMostToolStripMenuItem";
            this.topMostToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.topMostToolStripMenuItem.Text = "付箋を最前面化";
            this.topMostToolStripMenuItem.Click += new System.EventHandler(this.topMostToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
            // 
            // alphaSettingToolStripMenuItem
            // 
            this.alphaSettingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alphaSetting100ToolStripMenuItem,
            this.alphaSetting080ToolStripMenuItem,
            this.alphaSetting060ToolStripMenuItem,
            this.alphaSetting050ToolStripMenuItem,
            this.alphaSetting040ToolStripMenuItem,
            this.alphaSetting020ToolStripMenuItem});
            this.alphaSettingToolStripMenuItem.Name = "alphaSettingToolStripMenuItem";
            this.alphaSettingToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.alphaSettingToolStripMenuItem.Text = "非透過率設定";
            // 
            // alphaSetting100ToolStripMenuItem
            // 
            this.alphaSetting100ToolStripMenuItem.Name = "alphaSetting100ToolStripMenuItem";
            this.alphaSetting100ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.alphaSetting100ToolStripMenuItem.Text = "100%";
            this.alphaSetting100ToolStripMenuItem.Click += new System.EventHandler(this.alphaSetting100ToolStripMenuItem_Click);
            // 
            // alphaSetting080ToolStripMenuItem
            // 
            this.alphaSetting080ToolStripMenuItem.Name = "alphaSetting080ToolStripMenuItem";
            this.alphaSetting080ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.alphaSetting080ToolStripMenuItem.Text = "80%";
            this.alphaSetting080ToolStripMenuItem.Click += new System.EventHandler(this.alphaSetting080ToolStripMenuItem_Click);
            // 
            // alphaSetting060ToolStripMenuItem
            // 
            this.alphaSetting060ToolStripMenuItem.Name = "alphaSetting060ToolStripMenuItem";
            this.alphaSetting060ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.alphaSetting060ToolStripMenuItem.Text = "60%";
            this.alphaSetting060ToolStripMenuItem.Click += new System.EventHandler(this.alphaSetting060ToolStripMenuItem_Click);
            // 
            // alphaSetting050ToolStripMenuItem
            // 
            this.alphaSetting050ToolStripMenuItem.Name = "alphaSetting050ToolStripMenuItem";
            this.alphaSetting050ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.alphaSetting050ToolStripMenuItem.Text = "50%";
            this.alphaSetting050ToolStripMenuItem.Click += new System.EventHandler(this.alphaSetting050ToolStripMenuItem_Click);
            // 
            // alphaSetting040ToolStripMenuItem
            // 
            this.alphaSetting040ToolStripMenuItem.Name = "alphaSetting040ToolStripMenuItem";
            this.alphaSetting040ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.alphaSetting040ToolStripMenuItem.Text = "40%";
            this.alphaSetting040ToolStripMenuItem.Click += new System.EventHandler(this.alphaSetting040ToolStripMenuItem_Click);
            // 
            // alphaSetting020ToolStripMenuItem
            // 
            this.alphaSetting020ToolStripMenuItem.Name = "alphaSetting020ToolStripMenuItem";
            this.alphaSetting020ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.alphaSetting020ToolStripMenuItem.Text = "20%";
            this.alphaSetting020ToolStripMenuItem.Click += new System.EventHandler(this.alphaSetting020ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(166, 6);
            // 
            // 背景画像配置ToolStripMenuItem
            // 
            this.背景画像配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layoutSettingNoneToolStripMenuItem,
            this.layoutSettingTileToolStripMenuItem,
            this.layoutSettingCenterToolStripMenuItem,
            this.layoutSettingStretchToolStripMenuItem,
            this.layoutSettingZoomToolStripMenuItem});
            this.背景画像配置ToolStripMenuItem.Name = "背景画像配置ToolStripMenuItem";
            this.背景画像配置ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.背景画像配置ToolStripMenuItem.Text = "背景画像配置";
            // 
            // layoutSettingNoneToolStripMenuItem
            // 
            this.layoutSettingNoneToolStripMenuItem.Name = "layoutSettingNoneToolStripMenuItem";
            this.layoutSettingNoneToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.layoutSettingNoneToolStripMenuItem.Text = "なし";
            this.layoutSettingNoneToolStripMenuItem.Click += new System.EventHandler(this.layoutSettingNoneToolStripMenuItem_Click);
            // 
            // layoutSettingTileToolStripMenuItem
            // 
            this.layoutSettingTileToolStripMenuItem.Name = "layoutSettingTileToolStripMenuItem";
            this.layoutSettingTileToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.layoutSettingTileToolStripMenuItem.Text = "並べて表示";
            this.layoutSettingTileToolStripMenuItem.Click += new System.EventHandler(this.layoutSettingTileToolStripMenuItem_Click);
            // 
            // layoutSettingCenterToolStripMenuItem
            // 
            this.layoutSettingCenterToolStripMenuItem.Name = "layoutSettingCenterToolStripMenuItem";
            this.layoutSettingCenterToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.layoutSettingCenterToolStripMenuItem.Text = "中央に表示";
            this.layoutSettingCenterToolStripMenuItem.Click += new System.EventHandler(this.layoutSettingCenterToolStripMenuItem_Click);
            // 
            // layoutSettingStretchToolStripMenuItem
            // 
            this.layoutSettingStretchToolStripMenuItem.Name = "layoutSettingStretchToolStripMenuItem";
            this.layoutSettingStretchToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.layoutSettingStretchToolStripMenuItem.Text = "併せて表示";
            this.layoutSettingStretchToolStripMenuItem.Click += new System.EventHandler(this.layoutSettingStretchToolStripMenuItem_Click);
            // 
            // layoutSettingZoomToolStripMenuItem
            // 
            this.layoutSettingZoomToolStripMenuItem.Name = "layoutSettingZoomToolStripMenuItem";
            this.layoutSettingZoomToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.layoutSettingZoomToolStripMenuItem.Text = "同比率拡縮";
            this.layoutSettingZoomToolStripMenuItem.Click += new System.EventHandler(this.layoutSettingZoomToolStripMenuItem_Click);
            // 
            // txtSticky
            // 
            this.txtSticky.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.txtSticky.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSticky.Location = new System.Drawing.Point(4, 24);
            this.txtSticky.Multiline = true;
            this.txtSticky.Name = "txtSticky";
            this.txtSticky.Size = new System.Drawing.Size(312, 212);
            this.txtSticky.TabIndex = 1;
            this.txtSticky.Visible = false;
            this.txtSticky.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSticky_KeyDown);
            this.txtSticky.Leave += new System.EventHandler(this.txtSticky_Leave);
            // 
            // ttTips
            // 
            this.ttTips.ShowAlways = true;
            // 
            // tTimer
            // 
            this.tTimer.Interval = 500;
            this.tTimer.Tick += new System.EventHandler(this.tTimer_Tick);
            // 
            // ulsbClose
            // 
            this.ulsbClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.ulsbClose.BevelColor = System.Drawing.Color.Gray;
            this.ulsbClose.LabelText = "×";
            this.ulsbClose.Location = new System.Drawing.Point(300, 4);
            this.ulsbClose.Margin = new System.Windows.Forms.Padding(0);
            this.ulsbClose.Name = "ulsbClose";
            this.ulsbClose.Size = new System.Drawing.Size(16, 16);
            this.ulsbClose.TabIndex = 4;
            this.ulsbClose.Toggle = false;
            this.ulsbClose.ToolTipText = "付箋を解除";
            this.ulsbClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ulsbClose_MouseUp);
            // 
            // llblDisplay
            // 
            this.llblDisplay.BackColor = System.Drawing.Color.Transparent;
            this.llblDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.llblDisplay.ContextMenuStrip = this.cmsMenu;
            this.llblDisplay.Location = new System.Drawing.Point(0, 0);
            this.llblDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.llblDisplay.Name = "llblDisplay";
            this.llblDisplay.Padding = new System.Windows.Forms.Padding(4);
            this.llblDisplay.Size = new System.Drawing.Size(320, 240);
            this.llblDisplay.TabIndex = 5;
            this.llblDisplay.Visible = false;
            this.llblDisplay.MouseLeave += new System.EventHandler(this.llblDisplay_MouseLeave);
            this.llblDisplay.MouseDoubleClickEmulate += new System.Windows.Forms.MouseEventHandler(this.llblDisplay_MouseDoubleClick);
            this.llblDisplay.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblDisplay_LinkClicked);
            this.llblDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.llblDisplay_MouseMove);
            this.llblDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.llblDisplay_MouseClick);
            this.llblDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llblDisplay_MouseDown);
            this.llblDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.llblDisplay_MouseUp);
            // 
            // frmStickies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.ContextMenuStrip = this.cmsMenu;
            this.ControlBox = false;
            this.Controls.Add(this.ulsbClose);
            this.Controls.Add(this.txtSticky);
            this.Controls.Add(this.llblDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmStickies";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Deactivate += new System.EventHandler(this.frmStickies_Deactivate);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.frmStickies_MouseDoubleClick);
            this.Shown += new System.EventHandler(this.frmStickies_Shown);
            this.MouseLeave += new System.EventHandler(this.frmStickies_MouseLeave);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStickies_FormClosing);
            this.Resize += new System.EventHandler(this.frmStickies_Resize);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmStickies_MouseMove);
            this.cmsMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSticky;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem stickiesSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem stickiesDeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stickiesDestroyToolStripMenuItem;
        private UCLabel.ucLabelSunkenButton ulsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem alphaSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alphaSetting100ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alphaSetting080ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alphaSetting060ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alphaSetting050ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alphaSetting040ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alphaSetting020ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 背景画像配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutSettingNoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutSettingTileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutSettingCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutSettingStretchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutSettingZoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stickiesCloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem topMostToolStripMenuItem;
        private System.Windows.Forms.ToolTip ttTips;
        private System.Windows.Forms.Timer tTimer;
        private UCLabel.ucLinkLabel llblDisplay;
    }
}