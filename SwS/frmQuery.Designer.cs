namespace SwS
{
    partial class frmQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuery));
            this.cmbQueryString = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSchedule = new System.Windows.Forms.CheckBox();
            this.cbTodo = new System.Windows.Forms.CheckBox();
            this.cbMemo = new System.Windows.Forms.CheckBox();
            this.cbSticky = new System.Windows.Forms.CheckBox();
            this.cbAlarm = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbOr = new System.Windows.Forms.RadioButton();
            this.rbAnd = new System.Windows.Forms.RadioButton();
            this.rbComplete = new System.Windows.Forms.RadioButton();
            this.btnQuery = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbQueryString
            // 
            this.cmbQueryString.FormattingEnabled = true;
            this.cmbQueryString.Location = new System.Drawing.Point(80, 4);
            this.cmbQueryString.Name = "cmbQueryString";
            this.cmbQueryString.Size = new System.Drawing.Size(508, 20);
            this.cmbQueryString.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "検索文字列";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "検索対象";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "検索方法";
            // 
            // cbSchedule
            // 
            this.cbSchedule.AutoSize = true;
            this.cbSchedule.Location = new System.Drawing.Point(80, 28);
            this.cbSchedule.Name = "cbSchedule";
            this.cbSchedule.Size = new System.Drawing.Size(80, 16);
            this.cbSchedule.TabIndex = 18;
            this.cbSchedule.Text = "スケジュール";
            this.cbSchedule.UseVisualStyleBackColor = true;
            // 
            // cbTodo
            // 
            this.cbTodo.AutoSize = true;
            this.cbTodo.Location = new System.Drawing.Point(164, 28);
            this.cbTodo.Name = "cbTodo";
            this.cbTodo.Size = new System.Drawing.Size(51, 16);
            this.cbTodo.TabIndex = 19;
            this.cbTodo.Text = "ToDo";
            this.cbTodo.UseVisualStyleBackColor = true;
            // 
            // cbMemo
            // 
            this.cbMemo.AutoSize = true;
            this.cbMemo.Location = new System.Drawing.Point(220, 28);
            this.cbMemo.Name = "cbMemo";
            this.cbMemo.Size = new System.Drawing.Size(41, 16);
            this.cbMemo.TabIndex = 20;
            this.cbMemo.Text = "メモ";
            this.cbMemo.UseVisualStyleBackColor = true;
            // 
            // cbSticky
            // 
            this.cbSticky.AutoSize = true;
            this.cbSticky.Location = new System.Drawing.Point(268, 28);
            this.cbSticky.Name = "cbSticky";
            this.cbSticky.Size = new System.Drawing.Size(48, 16);
            this.cbSticky.TabIndex = 21;
            this.cbSticky.Text = "付箋";
            this.cbSticky.UseVisualStyleBackColor = true;
            // 
            // cbAlarm
            // 
            this.cbAlarm.AutoSize = true;
            this.cbAlarm.Location = new System.Drawing.Point(320, 28);
            this.cbAlarm.Name = "cbAlarm";
            this.cbAlarm.Size = new System.Drawing.Size(61, 16);
            this.cbAlarm.TabIndex = 22;
            this.cbAlarm.Text = "アラーム";
            this.cbAlarm.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbOr);
            this.panel2.Controls.Add(this.rbAnd);
            this.panel2.Controls.Add(this.rbComplete);
            this.panel2.Location = new System.Drawing.Point(80, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 20);
            this.panel2.TabIndex = 23;
            // 
            // rbOr
            // 
            this.rbOr.AutoSize = true;
            this.rbOr.Location = new System.Drawing.Point(156, 0);
            this.rbOr.Name = "rbOr";
            this.rbOr.Size = new System.Drawing.Size(86, 16);
            this.rbOr.TabIndex = 16;
            this.rbOr.TabStop = true;
            this.rbOr.Text = "いずれか含む";
            this.rbOr.UseVisualStyleBackColor = true;
            // 
            // rbAnd
            // 
            this.rbAnd.AutoSize = true;
            this.rbAnd.Location = new System.Drawing.Point(76, 0);
            this.rbAnd.Name = "rbAnd";
            this.rbAnd.Size = new System.Drawing.Size(74, 16);
            this.rbAnd.TabIndex = 15;
            this.rbAnd.TabStop = true;
            this.rbAnd.Text = "すべて含む";
            this.rbAnd.UseVisualStyleBackColor = true;
            // 
            // rbComplete
            // 
            this.rbComplete.AutoSize = true;
            this.rbComplete.Location = new System.Drawing.Point(0, 0);
            this.rbComplete.Name = "rbComplete";
            this.rbComplete.Size = new System.Drawing.Size(71, 16);
            this.rbComplete.TabIndex = 14;
            this.rbComplete.TabStop = true;
            this.rbComplete.Text = "完全一致";
            this.rbComplete.UseVisualStyleBackColor = true;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(512, 28);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 24;
            this.btnQuery.Text = "検索";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(4, 72);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(584, 376);
            this.splitContainer1.SplitterDistance = 203;
            this.splitContainer1.TabIndex = 25;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(584, 203);
            this.listView1.TabIndex = 26;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "種別";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "日時";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "内容";
            this.columnHeader3.Width = 323;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(584, 169);
            this.textBox1.TabIndex = 0;
            // 
            // frmQuery
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 451);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cbAlarm);
            this.Controls.Add(this.cbSticky);
            this.Controls.Add(this.cbMemo);
            this.Controls.Add(this.cbTodo);
            this.Controls.Add(this.cbSchedule);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbQueryString);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(599, 478);
            this.Name = "frmQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "クエリ検索";
            this.Load += new System.EventHandler(this.frmQuery_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmQuery_FormClosing);
            this.Resize += new System.EventHandler(this.frmQuery_Resize);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbQueryString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbSchedule;
        private System.Windows.Forms.CheckBox cbTodo;
        private System.Windows.Forms.CheckBox cbMemo;
        private System.Windows.Forms.CheckBox cbSticky;
        private System.Windows.Forms.CheckBox cbAlarm;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbOr;
        private System.Windows.Forms.RadioButton rbAnd;
        private System.Windows.Forms.RadioButton rbComplete;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TextBox textBox1;


    }
}