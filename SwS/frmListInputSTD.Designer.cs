namespace SwS
{
    partial class frmListInputSTD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListInputSTD));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpSelecter = new System.Windows.Forms.DateTimePicker();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cbDispAlarm = new System.Windows.Forms.CheckBox();
            this.cbDispMemo = new System.Windows.Forms.CheckBox();
            this.cbDispTodo = new System.Windows.Forms.CheckBox();
            this.cbDispSchedule = new System.Windows.Forms.CheckBox();
            this.cbDispFinished = new System.Windows.Forms.CheckBox();
            this.cbDispUnfinished = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.cmsContentsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.finishedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unfinishedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.destroyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ttTips = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.cmsContentsMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dtpSelecter);
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            this.splitContainer1.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(732, 501);
            this.splitContainer1.SplitterDistance = 218;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(108, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "月間予定一覧";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpSelecter
            // 
            this.dtpSelecter.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpSelecter.Location = new System.Drawing.Point(0, 0);
            this.dtpSelecter.Name = "dtpSelecter";
            this.dtpSelecter.Size = new System.Drawing.Size(108, 19);
            this.dtpSelecter.TabIndex = 1;
            this.dtpSelecter.ValueChanged += new System.EventHandler(this.dtpSelecter_ValueChanged);
            // 
            // listView1
            // 
            this.listView1.AutoArrange = false;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(0, 20);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(218, 481);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Resize += new System.EventHandler(this.listView1_Resize);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "日付";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "内容";
            this.columnHeader5.Width = 200;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.cbDispAlarm);
            this.splitContainer2.Panel1.Controls.Add(this.cbDispMemo);
            this.splitContainer2.Panel1.Controls.Add(this.cbDispTodo);
            this.splitContainer2.Panel1.Controls.Add(this.cbDispSchedule);
            this.splitContainer2.Panel1.Controls.Add(this.cbDispFinished);
            this.splitContainer2.Panel1.Controls.Add(this.cbDispUnfinished);
            this.splitContainer2.Panel1.Controls.Add(this.label6);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.listView2);
            this.splitContainer2.Panel1.Resize += new System.EventHandler(this.splitContainer2_Panel1_Resize);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Panel2.Controls.Add(this.textBox1);
            this.splitContainer2.Panel2.Resize += new System.EventHandler(this.splitContainer2_Panel2_Resize);
            this.splitContainer2.Size = new System.Drawing.Size(510, 501);
            this.splitContainer2.SplitterDistance = 223;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.Resize += new System.EventHandler(this.splitContainer2_Resize);
            // 
            // cbDispAlarm
            // 
            this.cbDispAlarm.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbDispAlarm.Checked = true;
            this.cbDispAlarm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDispAlarm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbDispAlarm.Location = new System.Drawing.Point(380, 20);
            this.cbDispAlarm.Name = "cbDispAlarm";
            this.cbDispAlarm.Size = new System.Drawing.Size(60, 20);
            this.cbDispAlarm.TabIndex = 10;
            this.cbDispAlarm.Text = "アラーム";
            this.cbDispAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbDispAlarm.UseVisualStyleBackColor = true;
            this.cbDispAlarm.CheckedChanged += new System.EventHandler(this.cbDispXXX_CheckedChanged);
            // 
            // cbDispMemo
            // 
            this.cbDispMemo.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbDispMemo.Checked = true;
            this.cbDispMemo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDispMemo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbDispMemo.Location = new System.Drawing.Point(320, 20);
            this.cbDispMemo.Name = "cbDispMemo";
            this.cbDispMemo.Size = new System.Drawing.Size(60, 20);
            this.cbDispMemo.TabIndex = 9;
            this.cbDispMemo.Text = "メモ";
            this.cbDispMemo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbDispMemo.UseVisualStyleBackColor = true;
            this.cbDispMemo.CheckedChanged += new System.EventHandler(this.cbDispXXX_CheckedChanged);
            // 
            // cbDispTodo
            // 
            this.cbDispTodo.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbDispTodo.Checked = true;
            this.cbDispTodo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDispTodo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbDispTodo.Location = new System.Drawing.Point(260, 20);
            this.cbDispTodo.Name = "cbDispTodo";
            this.cbDispTodo.Size = new System.Drawing.Size(60, 20);
            this.cbDispTodo.TabIndex = 8;
            this.cbDispTodo.Text = "ToDo";
            this.cbDispTodo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbDispTodo.UseVisualStyleBackColor = true;
            this.cbDispTodo.CheckedChanged += new System.EventHandler(this.cbDispXXX_CheckedChanged);
            // 
            // cbDispSchedule
            // 
            this.cbDispSchedule.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbDispSchedule.Checked = true;
            this.cbDispSchedule.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDispSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbDispSchedule.Location = new System.Drawing.Point(188, 20);
            this.cbDispSchedule.Name = "cbDispSchedule";
            this.cbDispSchedule.Size = new System.Drawing.Size(72, 20);
            this.cbDispSchedule.TabIndex = 7;
            this.cbDispSchedule.Text = "スケジュール";
            this.cbDispSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbDispSchedule.UseVisualStyleBackColor = true;
            this.cbDispSchedule.CheckedChanged += new System.EventHandler(this.cbDispXXX_CheckedChanged);
            // 
            // cbDispFinished
            // 
            this.cbDispFinished.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbDispFinished.Checked = true;
            this.cbDispFinished.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDispFinished.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbDispFinished.Location = new System.Drawing.Point(124, 20);
            this.cbDispFinished.Name = "cbDispFinished";
            this.cbDispFinished.Size = new System.Drawing.Size(60, 20);
            this.cbDispFinished.TabIndex = 6;
            this.cbDispFinished.Text = "完了";
            this.cbDispFinished.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbDispFinished.UseVisualStyleBackColor = true;
            this.cbDispFinished.CheckedChanged += new System.EventHandler(this.cbDispXXX_CheckedChanged);
            // 
            // cbDispUnfinished
            // 
            this.cbDispUnfinished.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbDispUnfinished.Checked = true;
            this.cbDispUnfinished.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDispUnfinished.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbDispUnfinished.Location = new System.Drawing.Point(64, 20);
            this.cbDispUnfinished.Name = "cbDispUnfinished";
            this.cbDispUnfinished.Size = new System.Drawing.Size(60, 20);
            this.cbDispUnfinished.TabIndex = 5;
            this.cbDispUnfinished.Text = "未完了";
            this.cbDispUnfinished.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbDispUnfinished.UseVisualStyleBackColor = true;
            this.cbDispUnfinished.CheckedChanged += new System.EventHandler(this.cbDispXXX_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "表示対象";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(510, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "日毎予定一覧";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView2.ContextMenuStrip = this.cmsContentsMenu;
            this.listView2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView2.FullRowSelect = true;
            this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(0, 40);
            this.listView2.Name = "listView2";
            this.listView2.ShowItemToolTips = true;
            this.listView2.Size = new System.Drawing.Size(510, 183);
            this.listView2.TabIndex = 0;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.Resize += new System.EventHandler(this.listView2_Resize);
            this.listView2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "種別";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "内容";
            this.columnHeader2.Width = 300;
            // 
            // cmsContentsMenu
            // 
            this.cmsContentsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finishedToolStripMenuItem,
            this.unfinishedToolStripMenuItem,
            this.toolStripSeparator1,
            this.destroyToolStripMenuItem});
            this.cmsContentsMenu.Name = "cmsContentsMenu";
            this.cmsContentsMenu.Size = new System.Drawing.Size(119, 76);
            // 
            // finishedToolStripMenuItem
            // 
            this.finishedToolStripMenuItem.Name = "finishedToolStripMenuItem";
            this.finishedToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.finishedToolStripMenuItem.Text = "完了";
            this.finishedToolStripMenuItem.Click += new System.EventHandler(this.finishedToolStripMenuItem_Click);
            // 
            // unfinishedToolStripMenuItem
            // 
            this.unfinishedToolStripMenuItem.Name = "unfinishedToolStripMenuItem";
            this.unfinishedToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.unfinishedToolStripMenuItem.Text = "完了取消";
            this.unfinishedToolStripMenuItem.Click += new System.EventHandler(this.unfinishedToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(115, 6);
            // 
            // destroyToolStripMenuItem
            // 
            this.destroyToolStripMenuItem.Name = "destroyToolStripMenuItem";
            this.destroyToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.destroyToolStripMenuItem.Text = "削除";
            this.destroyToolStripMenuItem.Click += new System.EventHandler(this.destroyToolStripMenuItem_Click);
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
            this.panel1.Size = new System.Drawing.Size(510, 92);
            this.panel1.TabIndex = 1;
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
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnNew);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(510, 20);
            this.panel2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(60, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(430, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "予定登録/編集";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEntry
            // 
            this.btnEntry.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnEntry.Enabled = false;
            this.btnEntry.Location = new System.Drawing.Point(40, 0);
            this.btnEntry.Name = "btnEntry";
            this.btnEntry.Size = new System.Drawing.Size(20, 20);
            this.btnEntry.TabIndex = 11;
            this.btnEntry.Text = "▲";
            this.btnEntry.UseVisualStyleBackColor = true;
            this.btnEntry.Click += new System.EventHandler(this.btnEntry_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(490, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(20, 20);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "×";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(20, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(20, 20);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "□";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNew
            // 
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnNew.Location = new System.Drawing.Point(0, 0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(20, 20);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "▽";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(0, 52);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(510, 222);
            this.textBox1.TabIndex = 0;
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // ttTips
            // 
            this.ttTips.IsBalloon = true;
            this.ttTips.ShowAlways = true;
            // 
            // frmListInputSTD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 501);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(740, 528);
            this.Name = "frmListInputSTD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "タスク登録　(スケジュール・ＴｏＤｏ・メモ・アラーム)";
            this.Load += new System.EventHandler(this.frmListInputSTD_Load);
            this.Activated += new System.EventHandler(this.frmListInputSTD_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmListInputSTD_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.cmsContentsMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEntry;
        private System.Windows.Forms.ToolTip ttTips;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.CheckBox cbSendStickies;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip cmsContentsMenu;
        private System.Windows.Forms.ToolStripMenuItem finishedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unfinishedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem destroyToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpSelecter;
        private UCLabel.ucCommandTrigger ucCommandTrigger;
        private UCLabel.ucRangeTrigger ucRangeTrigger;
        private UCLabel.ucTimeTrigger ucTimeTrigger;
        private System.Windows.Forms.CheckBox cbRange;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbDispTodo;
        private System.Windows.Forms.CheckBox cbDispSchedule;
        private System.Windows.Forms.CheckBox cbDispFinished;
        private System.Windows.Forms.CheckBox cbDispUnfinished;
        private System.Windows.Forms.CheckBox cbDispAlarm;
        private System.Windows.Forms.CheckBox cbDispMemo;


    }
}