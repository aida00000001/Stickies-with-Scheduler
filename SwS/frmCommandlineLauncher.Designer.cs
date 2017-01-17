namespace SwS
{
    partial class frmCommandlineLauncher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCommandlineLauncher));
            this.txtCommandline = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ulsbClose = new UCLabel.ucLabelSunkenButton();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCommandline
            // 
            this.txtCommandline.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCommandline.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCommandline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCommandline.Location = new System.Drawing.Point(4, 20);
            this.txtCommandline.Name = "txtCommandline";
            this.txtCommandline.Size = new System.Drawing.Size(292, 19);
            this.txtCommandline.TabIndex = 0;
            this.txtCommandline.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommandline_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SwS.Properties.Resources.DragBar;
            this.pictureBox1.InitialImage = global::SwS.Properties.Resources.DragBar;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 15);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "コマンドラインランチャ";
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // ulsbClose
            // 
            this.ulsbClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(230)))));
            this.ulsbClose.BevelColor = System.Drawing.Color.Gray;
            this.ulsbClose.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ulsbClose.LabelText = "×";
            this.ulsbClose.Location = new System.Drawing.Point(300, 21);
            this.ulsbClose.Margin = new System.Windows.Forms.Padding(0);
            this.ulsbClose.Name = "ulsbClose";
            this.ulsbClose.Size = new System.Drawing.Size(16, 16);
            this.ulsbClose.TabIndex = 5;
            this.ulsbClose.Toggle = false;
            this.ulsbClose.ToolTipText = "付箋を解除";
            this.ulsbClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ulsbClose_MouseUp);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(321, 43);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            // 
            // frmCommandlineLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(321, 43);
            this.ControlBox = false;
            this.Controls.Add(this.ulsbClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtCommandline);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(1, 1);
            this.Name = "frmCommandlineLauncher";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "コマンドラインランチャ";
            this.Load += new System.EventHandler(this.frmCommandlineLauncher_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCommandline;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private UCLabel.ucLabelSunkenButton ulsbClose;
        private System.Windows.Forms.Label label2;
    }
}