namespace tsumugi
{
    partial class frmTaskTray
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTaskTray));
            this.tTimer = new System.Windows.Forms.Timer(this.components);
            this.niTsumugi = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.termToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tTimer
            // 
            this.tTimer.Tick += new System.EventHandler(this.tTimer_Tick);
            // 
            // niTsumugi
            // 
            this.niTsumugi.BalloonTipText = "カレンダ兼スケジューラ";
            this.niTsumugi.BalloonTipTitle = "< 刻紡 >";
            this.niTsumugi.ContextMenuStrip = this.cmsMenu;
            this.niTsumugi.Icon = ((System.Drawing.Icon)(resources.GetObject("niTsumugi.Icon")));
            this.niTsumugi.Visible = true;
            this.niTsumugi.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niTsumugi_MouseDoubleClick);
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem,
            this.toolStripSeparator2,
            this.termToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(140, 82);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.settingToolStripMenuItem.Text = "設定";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(136, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.aboutToolStripMenuItem.Text = "バージョン情報";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(136, 6);
            // 
            // termToolStripMenuItem
            // 
            this.termToolStripMenuItem.Name = "termToolStripMenuItem";
            this.termToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.termToolStripMenuItem.Text = "終了";
            this.termToolStripMenuItem.Click += new System.EventHandler(this.termToolStripMenuItem_Click);
            // 
            // frmTaskTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 177);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTaskTray";
            this.ShowInTaskbar = false;
            this.Text = "< 刻紡 >";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.frmTaskTray_Load);
            this.cmsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tTimer;
        private System.Windows.Forms.NotifyIcon niTsumugi;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem termToolStripMenuItem;
    }
}

