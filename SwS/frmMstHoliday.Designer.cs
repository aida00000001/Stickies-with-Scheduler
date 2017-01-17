namespace SwS
{
    partial class frmMstHoliday
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMstHoliday));
            this.dgvHoliday = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbHolidayType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHolidayName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDayOrWeek = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDayOfWeek = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpYear = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCompare = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblId = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoliday)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHoliday
            // 
            this.dgvHoliday.AllowUserToAddRows = false;
            this.dgvHoliday.AllowUserToDeleteRows = false;
            this.dgvHoliday.AllowUserToResizeColumns = false;
            this.dgvHoliday.AllowUserToResizeRows = false;
            this.dgvHoliday.ColumnHeadersVisible = false;
            this.dgvHoliday.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvHoliday.EnableHeadersVisualStyles = false;
            this.dgvHoliday.Location = new System.Drawing.Point(4, 4);
            this.dgvHoliday.MultiSelect = false;
            this.dgvHoliday.Name = "dgvHoliday";
            this.dgvHoliday.RowHeadersVisible = false;
            this.dgvHoliday.RowTemplate.Height = 21;
            this.dgvHoliday.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoliday.Size = new System.Drawing.Size(152, 200);
            this.dgvHoliday.TabIndex = 0;
            this.dgvHoliday.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHoliday_CellContentDoubleClick);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(160, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 200);
            this.label1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(172, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "休日種別";
            // 
            // cmbHolidayType
            // 
            this.cmbHolidayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHolidayType.FormattingEnabled = true;
            this.cmbHolidayType.Items.AddRange(new object[] {
            "固定休日",
            "月・週固定休日",
            "その他休日(現在は春分・秋分の日に適用)"});
            this.cmbHolidayType.Location = new System.Drawing.Point(256, 4);
            this.cmbHolidayType.Name = "cmbHolidayType";
            this.cmbHolidayType.Size = new System.Drawing.Size(304, 20);
            this.cmbHolidayType.TabIndex = 3;
            this.cmbHolidayType.SelectedIndexChanged += new System.EventHandler(this.cmbHolidayType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(172, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "休日名称";
            // 
            // txtHolidayName
            // 
            this.txtHolidayName.Location = new System.Drawing.Point(256, 28);
            this.txtHolidayName.Name = "txtHolidayName";
            this.txtHolidayName.Size = new System.Drawing.Size(304, 19);
            this.txtHolidayName.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(172, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "対象月";
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Items.AddRange(new object[] {
            " 1月",
            " 2月",
            " 3月",
            " 4月",
            " 5月",
            " 6月",
            " 7月",
            " 8月",
            " 9月",
            "10月",
            "11月",
            "12月"});
            this.cmbMonth.Location = new System.Drawing.Point(256, 52);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(52, 20);
            this.cmbMonth.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(172, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 8;
            // 
            // cmbDayOrWeek
            // 
            this.cmbDayOrWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDayOrWeek.FormattingEnabled = true;
            this.cmbDayOrWeek.Items.AddRange(new object[] {
            " 1月",
            " 2月",
            " 3月",
            " 4月",
            " 5月",
            " 6月",
            " 7月",
            " 8月",
            " 9月",
            "10月",
            "11月",
            "12月"});
            this.cmbDayOrWeek.Location = new System.Drawing.Point(256, 76);
            this.cmbDayOrWeek.Name = "cmbDayOrWeek";
            this.cmbDayOrWeek.Size = new System.Drawing.Size(52, 20);
            this.cmbDayOrWeek.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(172, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 10;
            // 
            // cmbDayOfWeek
            // 
            this.cmbDayOfWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDayOfWeek.FormattingEnabled = true;
            this.cmbDayOfWeek.Items.AddRange(new object[] {
            "日曜日",
            "月曜日",
            "火曜日",
            "水曜日",
            "木曜日",
            "金曜日",
            "土曜日"});
            this.cmbDayOfWeek.Location = new System.Drawing.Point(256, 100);
            this.cmbDayOfWeek.Name = "cmbDayOfWeek";
            this.cmbDayOfWeek.Size = new System.Drawing.Size(80, 20);
            this.cmbDayOfWeek.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(172, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 12;
            this.label7.Text = "有効年";
            // 
            // dtpYear
            // 
            this.dtpYear.CustomFormat = "yyyy年";
            this.dtpYear.Location = new System.Drawing.Point(256, 124);
            this.dtpYear.Name = "dtpYear";
            this.dtpYear.ShowUpDown = true;
            this.dtpYear.Size = new System.Drawing.Size(60, 19);
            this.dtpYear.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(172, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "有効判定";
            // 
            // cmbCompare
            // 
            this.cmbCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompare.FormattingEnabled = true;
            this.cmbCompare.Items.AddRange(new object[] {
            "<",
            ">",
            "<=",
            ">=",
            "==",
            "!="});
            this.cmbCompare.Location = new System.Drawing.Point(256, 148);
            this.cmbCompare.Name = "cmbCompare";
            this.cmbCompare.Size = new System.Drawing.Size(80, 20);
            this.cmbCompare.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(168, 172);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(392, 2);
            this.label9.TabIndex = 16;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(172, 180);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(72, 24);
            this.btnNew.TabIndex = 17;
            this.btnNew.Text = "新規登録";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(408, 180);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(72, 24);
            this.btnUpdate.TabIndex = 18;
            this.btnUpdate.Text = "更新登録";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(484, 180);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(72, 24);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "削除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblId
            // 
            this.lblId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblId.Location = new System.Drawing.Point(356, 184);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(44, 16);
            this.lblId.TabIndex = 20;
            this.lblId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(332, 184);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 16);
            this.label10.TabIndex = 21;
            this.label10.Text = "ID";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMstHoliday
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(563, 207);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbCompare);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpYear);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbDayOfWeek);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbDayOrWeek);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbMonth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtHolidayName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbHolidayType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvHoliday);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMstHoliday";
            this.Text = "祝祭日マスタ";
            this.Load += new System.EventHandler(this.frmMstHoliday_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoliday)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHoliday;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbHolidayType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHolidayName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDayOrWeek;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDayOfWeek;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpYear;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbCompare;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label label10;


    }
}