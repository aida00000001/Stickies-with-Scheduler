namespace SwS
{
    partial class frmInputTMA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInputTMA));
            this.ttTips = new System.Windows.Forms.ToolTip(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbRange = new System.Windows.Forms.CheckBox();
            this.ucCommandTrigger = new UCLabel.ucCommandTrigger();
            this.ucRangeTrigger = new UCLabel.ucRangeTrigger();
            this.ucTimeTrigger = new UCLabel.ucTimeTrigger();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSendStickies = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEntry = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ttTips
            // 
            this.ttTips.IsBalloon = true;
            this.ttTips.ShowAlways = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(512, 273);
            this.panel3.TabIndex = 4;
            this.panel3.Resize += new System.EventHandler(this.panel3_Resize);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbRange);
            this.panel1.Controls.Add(this.ucCommandTrigger);
            this.panel1.Controls.Add(this.ucRangeTrigger);
            this.panel1.Controls.Add(this.ucTimeTrigger);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cbSendStickies);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbDataType);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 92);
            this.panel1.TabIndex = 5;
            // 
            // cbRange
            // 
            this.cbRange.AutoSize = true;
            this.cbRange.Location = new System.Drawing.Point(4, 50);
            this.cbRange.Name = "cbRange";
            this.cbRange.Size = new System.Drawing.Size(157, 16);
            this.cbRange.TabIndex = 15;
            this.cbRange.Text = "繰り返し表示期間設定有り";
            this.cbRange.UseVisualStyleBackColor = true;
            this.cbRange.CheckedChanged += new System.EventHandler(this.cbRange_CheckedChanged);
            // 
            // ucCommandTrigger
            // 
            this.ucCommandTrigger.Location = new System.Drawing.Point(340, 68);
            this.ucCommandTrigger.Name = "ucCommandTrigger";
            this.ucCommandTrigger.Size = new System.Drawing.Size(168, 20);
            this.ucCommandTrigger.TabIndex = 14;
            // 
            // ucRangeTrigger
            // 
            this.ucRangeTrigger.EndDate = new System.DateTime(2009, 9, 2, 14, 3, 46, 890);
            this.ucRangeTrigger.Location = new System.Drawing.Point(164, 48);
            this.ucRangeTrigger.Name = "ucRangeTrigger";
            this.ucRangeTrigger.Size = new System.Drawing.Size(272, 20);
            this.ucRangeTrigger.StartDate = new System.DateTime(2009, 9, 2, 14, 3, 46, 890);
            this.ucRangeTrigger.TabIndex = 13;
            // 
            // ucTimeTrigger
            // 
            this.ucTimeTrigger.Date = new System.DateTime(2009, 9, 2, 14, 3, 26, 968);
            this.ucTimeTrigger.DateEnd = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.ucTimeTrigger.DateStart = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.ucTimeTrigger.Location = new System.Drawing.Point(4, 68);
            this.ucTimeTrigger.Name = "ucTimeTrigger";
            this.ucTimeTrigger.Size = new System.Drawing.Size(336, 20);
            this.ucTimeTrigger.Span = 0;
            this.ucTimeTrigger.TabIndex = 12;
            this.ucTimeTrigger.Time = new System.DateTime(2009, 9, 2, 14, 3, 26, 968);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Red;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(252, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "☆☆☆　完了　☆☆☆";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // cbSendStickies
            // 
            this.cbSendStickies.AutoSize = true;
            this.cbSendStickies.Enabled = false;
            this.cbSendStickies.Location = new System.Drawing.Point(172, 28);
            this.cbSendStickies.Name = "cbSendStickies";
            this.cbSendStickies.Size = new System.Drawing.Size(68, 16);
            this.cbSendStickies.TabIndex = 10;
            this.cbSendStickies.Text = "付箋送り";
            this.cbSendStickies.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "予定種類";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDataType
            // 
            this.cmbDataType.DisplayMember = "0, 1, 2, 4";
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.Enabled = false;
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Location = new System.Drawing.Point(64, 24);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(96, 20);
            this.cmbDataType.TabIndex = 8;
            this.cmbDataType.ValueMember = "0, 1, 2, 4";
            this.cmbDataType.SelectedIndexChanged += new System.EventHandler(this.cmbDataType_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnEntry);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(512, 20);
            this.panel2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(20, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(492, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "タスク新規登録";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEntry
            // 
            this.btnEntry.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnEntry.Enabled = false;
            this.btnEntry.Location = new System.Drawing.Point(0, 0);
            this.btnEntry.Name = "btnEntry";
            this.btnEntry.Size = new System.Drawing.Size(20, 20);
            this.btnEntry.TabIndex = 11;
            this.btnEntry.Text = "▲";
            this.btnEntry.UseVisualStyleBackColor = true;
            this.btnEntry.Click += new System.EventHandler(this.btnEntry_Click);
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(0, 51);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(512, 222);
            this.textBox1.TabIndex = 4;
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // frmInputTMA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 273);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(520, 300);
            this.Name = "frmInputTMA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新規タスク　(ＴｏＤｏ・メモ・アラーム)";
            this.Load += new System.EventHandler(this.frmInputTMA_Load);
            this.Activated += new System.EventHandler(this.frmInputTMA_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInputTMA_FormClosing);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttTips;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbRange;
        private UCLabel.ucCommandTrigger ucCommandTrigger;
        private UCLabel.ucRangeTrigger ucRangeTrigger;
        private UCLabel.ucTimeTrigger ucTimeTrigger;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbSendStickies;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEntry;
        private System.Windows.Forms.TextBox textBox1;


    }
}