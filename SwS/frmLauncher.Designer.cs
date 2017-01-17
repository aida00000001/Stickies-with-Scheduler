namespace SwS
{
    partial class frmLauncher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLauncher));
            this.cmsLaunchMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // cmsLaunchMenu
            // 
            this.cmsLaunchMenu.Name = "cmsLaunchMenu";
            this.cmsLaunchMenu.Size = new System.Drawing.Size(61, 4);
            this.cmsLaunchMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsLaunchMenu_Opening);
            this.cmsLaunchMenu.Click += new System.EventHandler(this.cmsLaunchMenu_Click);
            // 
            // frmLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(104, 85);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLauncher";
            this.Opacity = 0;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "S.w.S. Launcher";
            this.Deactivate += new System.EventHandler(this.frmLauncher_Deactivate);
            this.Load += new System.EventHandler(this.frmLauncher_Load);
            this.DragLeave += new System.EventHandler(this.frmLauncher_Leave);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLauncher_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmsLaunchMenu;
    }
}