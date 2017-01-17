namespace SwS
{
    partial class frmStickiesSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStickiesSetting));
            this.label1 = new System.Windows.Forms.Label();
            this.tbAlpha = new System.Windows.Forms.TrackBar();
            this.lblAlpha = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelectDialog = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblFilename = new System.Windows.Forms.Label();
            this.ofdBackImage = new System.Windows.Forms.OpenFileDialog();
            this.cmbLayout = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTimeTrigger = new System.Windows.Forms.CheckBox();
            this.cbAttachTrigger = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnColor = new System.Windows.Forms.Button();
            this.lblColor = new System.Windows.Forms.Label();
            this.cdColorDialog = new System.Windows.Forms.ColorDialog();
            this.cbRegion = new System.Windows.Forms.CheckBox();
            this.btnFont = new System.Windows.Forms.Button();
            this.lblFont = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbSnapWindow = new System.Windows.Forms.CheckBox();
            this.uatAttachTrigger = new UCLabel.ucAttachTrigger();
            this.uttTimeTrigger = new UCLabel.ucTimeTrigger();
            this.uatSnapWindow = new UCLabel.ucAttachTrigger();
            ((System.ComponentModel.ISupportInitialize)(this.tbAlpha)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "非透過率";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbAlpha
            // 
            this.tbAlpha.AutoSize = false;
            this.tbAlpha.Location = new System.Drawing.Point(76, 4);
            this.tbAlpha.Maximum = 100;
            this.tbAlpha.Minimum = 1;
            this.tbAlpha.Name = "tbAlpha";
            this.tbAlpha.Size = new System.Drawing.Size(216, 36);
            this.tbAlpha.TabIndex = 1;
            this.tbAlpha.TickFrequency = 5;
            this.tbAlpha.Value = 100;
            this.tbAlpha.Scroll += new System.EventHandler(this.tbAlpha_Scroll);
            // 
            // lblAlpha
            // 
            this.lblAlpha.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlpha.Location = new System.Drawing.Point(8, 20);
            this.lblAlpha.Name = "lblAlpha";
            this.lblAlpha.Size = new System.Drawing.Size(64, 16);
            this.lblAlpha.TabIndex = 2;
            this.lblAlpha.Text = "100%";
            this.lblAlpha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(4, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(348, 3);
            this.label3.TabIndex = 4;
            this.label3.Text = "100%";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "背景画像";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelectDialog
            // 
            this.btnSelectDialog.Location = new System.Drawing.Point(68, 48);
            this.btnSelectDialog.Name = "btnSelectDialog";
            this.btnSelectDialog.Size = new System.Drawing.Size(44, 20);
            this.btnSelectDialog.TabIndex = 6;
            this.btnSelectDialog.Text = "選択";
            this.btnSelectDialog.UseVisualStyleBackColor = true;
            this.btnSelectDialog.Click += new System.EventHandler(this.btnSelectDialog_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(116, 48);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(44, 20);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "クリア";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblFilename
            // 
            this.lblFilename.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFilename.Location = new System.Drawing.Point(164, 48);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(188, 68);
            this.lblFilename.TabIndex = 8;
            // 
            // ofdBackImage
            // 
            this.ofdBackImage.Filter = "JPEG, ビットマップ, PNG (画像ファイル)|*.jpg; *.bmp; *.png";
            this.ofdBackImage.RestoreDirectory = true;
            this.ofdBackImage.SupportMultiDottedExtensions = true;
            // 
            // cmbLayout
            // 
            this.cmbLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayout.FormattingEnabled = true;
            this.cmbLayout.Items.AddRange(new object[] {
            "なし",
            "並べて表示",
            "中央に表示",
            "併せて表示",
            "同比率拡縮"});
            this.cmbLayout.Location = new System.Drawing.Point(44, 72);
            this.cmbLayout.Name = "cmbLayout";
            this.cmbLayout.Size = new System.Drawing.Size(116, 20);
            this.cmbLayout.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(196, 268);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 24);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(276, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 24);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(4, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(348, 3);
            this.label2.TabIndex = 15;
            this.label2.Text = "100%";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(4, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(348, 3);
            this.label5.TabIndex = 20;
            this.label5.Text = "100%";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbTimeTrigger
            // 
            this.cbTimeTrigger.AutoCheck = false;
            this.cbTimeTrigger.AutoSize = true;
            this.cbTimeTrigger.Location = new System.Drawing.Point(8, 128);
            this.cbTimeTrigger.Name = "cbTimeTrigger";
            this.cbTimeTrigger.Size = new System.Drawing.Size(72, 16);
            this.cbTimeTrigger.TabIndex = 21;
            this.cbTimeTrigger.Text = "時限起動";
            this.cbTimeTrigger.UseVisualStyleBackColor = true;
            this.cbTimeTrigger.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbTimeTrigger_MouseClick);
            // 
            // cbAttachTrigger
            // 
            this.cbAttachTrigger.AutoCheck = false;
            this.cbAttachTrigger.AutoSize = true;
            this.cbAttachTrigger.Location = new System.Drawing.Point(88, 128);
            this.cbAttachTrigger.Name = "cbAttachTrigger";
            this.cbAttachTrigger.Size = new System.Drawing.Size(72, 16);
            this.cbAttachTrigger.TabIndex = 22;
            this.cbAttachTrigger.Text = "連想起動";
            this.cbAttachTrigger.UseVisualStyleBackColor = true;
            this.cbAttachTrigger.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbAttachTrigger_MouseClick);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "配置";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(296, 4);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(56, 20);
            this.btnColor.TabIndex = 24;
            this.btnColor.Text = "背景色";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // lblColor
            // 
            this.lblColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblColor.Location = new System.Drawing.Point(296, 24);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(56, 16);
            this.lblColor.TabIndex = 25;
            this.lblColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbRegion
            // 
            this.cbRegion.AutoSize = true;
            this.cbRegion.Location = new System.Drawing.Point(8, 100);
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(153, 16);
            this.cbRegion.TabIndex = 12;
            this.cbRegion.Text = "リージョン切抜き(x=0, y=0) ";
            this.cbRegion.UseVisualStyleBackColor = true;
            // 
            // btnFont
            // 
            this.btnFont.Location = new System.Drawing.Point(8, 196);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(24, 60);
            this.btnFont.TabIndex = 26;
            this.btnFont.Text = "フォント";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // lblFont
            // 
            this.lblFont.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFont.Location = new System.Drawing.Point(36, 196);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(316, 60);
            this.lblFont.TabIndex = 27;
            this.lblFont.Text = "Aaあぁアァ亜宇";
            this.lblFont.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(4, 260);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(348, 3);
            this.label7.TabIndex = 28;
            this.label7.Text = "100%";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbSnapWindow
            // 
            this.cbSnapWindow.AutoCheck = false;
            this.cbSnapWindow.AutoSize = true;
            this.cbSnapWindow.Location = new System.Drawing.Point(168, 128);
            this.cbSnapWindow.Name = "cbSnapWindow";
            this.cbSnapWindow.Size = new System.Drawing.Size(91, 16);
            this.cbSnapWindow.TabIndex = 23;
            this.cbSnapWindow.Text = "ウィンドウ貼付";
            this.cbSnapWindow.UseVisualStyleBackColor = true;
            this.cbSnapWindow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbSnapWindow_MouseClick);
            // 
            // uatAttachTrigger
            // 
            this.uatAttachTrigger.Location = new System.Drawing.Point(8, 148);
            this.uatAttachTrigger.Name = "uatAttachTrigger";
            this.uatAttachTrigger.Size = new System.Drawing.Size(344, 36);
            this.uatAttachTrigger.TabIndex = 19;
            this.uatAttachTrigger.Visible = false;
            // 
            // uttTimeTrigger
            // 
            this.uttTimeTrigger.Date = new System.DateTime(2009, 7, 8, 13, 33, 8, 656);
            this.uttTimeTrigger.DateEnd = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.uttTimeTrigger.DateStart = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.uttTimeTrigger.Location = new System.Drawing.Point(8, 148);
            this.uttTimeTrigger.Name = "uttTimeTrigger";
            this.uttTimeTrigger.Size = new System.Drawing.Size(264, 36);
            this.uttTimeTrigger.TabIndex = 18;
            this.uttTimeTrigger.Time = new System.DateTime(2009, 7, 8, 13, 33, 8, 656);
            this.uttTimeTrigger.Visible = false;
            // 
            // uatSnapWindow
            // 
            this.uatSnapWindow.Location = new System.Drawing.Point(8, 148);
            this.uatSnapWindow.Name = "uatSnapWindow";
            this.uatSnapWindow.Size = new System.Drawing.Size(344, 36);
            this.uatSnapWindow.TabIndex = 29;
            this.uatSnapWindow.Visible = false;
            // 
            // frmStickiesSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 296);
            this.Controls.Add(this.uatSnapWindow);
            this.Controls.Add(this.cbSnapWindow);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblFont);
            this.Controls.Add(this.btnFont);
            this.Controls.Add(this.cbRegion);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbAttachTrigger);
            this.Controls.Add(this.cbTimeTrigger);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.uatAttachTrigger);
            this.Controls.Add(this.uttTimeTrigger);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbLayout);
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSelectDialog);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblAlpha);
            this.Controls.Add(this.tbAlpha);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStickiesSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "付箋個別設定";
            ((System.ComponentModel.ISupportInitialize)(this.tbAlpha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbAlpha;
        private System.Windows.Forms.Label lblAlpha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectDialog;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.OpenFileDialog ofdBackImage;
        private System.Windows.Forms.ComboBox cmbLayout;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private UCLabel.ucTimeTrigger uttTimeTrigger;
        private UCLabel.ucAttachTrigger uatAttachTrigger;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbTimeTrigger;
        private System.Windows.Forms.CheckBox cbAttachTrigger;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ColorDialog cdColorDialog;
        private System.Windows.Forms.CheckBox cbRegion;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbSnapWindow;
        private UCLabel.ucAttachTrigger uatSnapWindow;

    }
}