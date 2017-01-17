namespace SwS
{
    partial class frmCalendar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalendar));
            this.tTimer = new System.Windows.Forms.Timer(this.components);
            this.niSwS = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launcherSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.todoBatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoBatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.goCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.innerDesktopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.showCalendarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideCalendarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.listStickiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.launcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.termToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bgwTimeTick = new System.ComponentModel.BackgroundWorker();
            this.cmsCalendarMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingToolStripCalendarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryToolStripCalendarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.hideCalendarToolStripCalendarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.topMostToolStripCalendarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.termToolStripCalendarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.bgwDesktopHook = new System.ComponentModel.BackgroundWorker();
            this.bgwMailChecker = new System.ComponentModel.BackgroundWorker();
            this.bgwWindowList = new System.ComponentModel.BackgroundWorker();
            this.ulsbRightButton = new UCLabel.ucLabelSunkenButton();
            this.ulsbLeftButton = new UCLabel.ucLabelSunkenButton();
            this.ulsbMonth = new UCLabel.ucLabelSunkenButton();
            this.ucLabel1 = new UCLabel.ucLabelCalendar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.taskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandlineLauncherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMenu.SuspendLayout();
            this.cmsCalendarMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tTimer
            // 
            this.tTimer.Enabled = true;
            this.tTimer.Interval = 500;
            this.tTimer.Tick += new System.EventHandler(this.tTimer_Tick);
            // 
            // niSwS
            // 
            this.niSwS.ContextMenuStrip = this.cmsMenu;
            this.niSwS.Icon = ((System.Drawing.Icon)(resources.GetObject("niSwS.Icon")));
            this.niSwS.Visible = true;
            this.niSwS.MouseClick += new System.Windows.Forms.MouseEventHandler(this.niSwS_MouseClick);
            this.niSwS.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niSwS_MouseDoubleClick);
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.toolStripSeparator1,
            this.settingToolStripMenuItem,
            this.launcherSettingToolStripMenuItem,
            this.queryToolStripMenuItem,
            this.taskToolStripMenuItem,
            this.toolStripSeparator5,
            this.todoBatchToolStripMenuItem,
            this.memoBatchToolStripMenuItem,
            this.toolStripSeparator9,
            this.goCenterToolStripMenuItem,
            this.innerDesktopToolStripMenuItem,
            this.toolStripSeparator3,
            this.showCalendarToolStripMenuItem,
            this.hideCalendarToolStripMenuItem,
            this.toolStripSeparator4,
            this.listStickiesToolStripMenuItem,
            this.toolStripSeparator8,
            this.launcherToolStripMenuItem,
            this.commandlineLauncherToolStripMenuItem,
            this.toolStripSeparator2,
            this.termToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(213, 398);
            this.cmsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsMenu_Opening);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.aboutToolStripMenuItem.Text = "バージョン情報(&V)";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(209, 6);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.settingToolStripMenuItem.Text = "設定(&S)";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // launcherSettingToolStripMenuItem
            // 
            this.launcherSettingToolStripMenuItem.Name = "launcherSettingToolStripMenuItem";
            this.launcherSettingToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.launcherSettingToolStripMenuItem.Text = "ランチャ設定(&S)";
            this.launcherSettingToolStripMenuItem.Click += new System.EventHandler(this.launcherSettingToolStripMenuItem_Click);
            // 
            // queryToolStripMenuItem
            // 
            this.queryToolStripMenuItem.Name = "queryToolStripMenuItem";
            this.queryToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.queryToolStripMenuItem.Text = "クエリ検索(&Q)";
            this.queryToolStripMenuItem.Click += new System.EventHandler(this.queryToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(209, 6);
            // 
            // todoBatchToolStripMenuItem
            // 
            this.todoBatchToolStripMenuItem.Name = "todoBatchToolStripMenuItem";
            this.todoBatchToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.todoBatchToolStripMenuItem.Text = "ToDo一括付箋化";
            this.todoBatchToolStripMenuItem.Click += new System.EventHandler(this.todoBatchToolStripMenuItem_Click);
            // 
            // memoBatchToolStripMenuItem
            // 
            this.memoBatchToolStripMenuItem.Name = "memoBatchToolStripMenuItem";
            this.memoBatchToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.memoBatchToolStripMenuItem.Text = "メモ一括付箋化";
            this.memoBatchToolStripMenuItem.Click += new System.EventHandler(this.memoBatchToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(209, 6);
            // 
            // goCenterToolStripMenuItem
            // 
            this.goCenterToolStripMenuItem.Name = "goCenterToolStripMenuItem";
            this.goCenterToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.goCenterToolStripMenuItem.Text = "全付箋をデスクトップ中央へ";
            this.goCenterToolStripMenuItem.Click += new System.EventHandler(this.goCenterToolStripMenuItem_Click);
            // 
            // innerDesktopToolStripMenuItem
            // 
            this.innerDesktopToolStripMenuItem.Name = "innerDesktopToolStripMenuItem";
            this.innerDesktopToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.innerDesktopToolStripMenuItem.Text = "全付箋をデスクトップ範囲内へ";
            this.innerDesktopToolStripMenuItem.Click += new System.EventHandler(this.innerDesktopToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(209, 6);
            // 
            // showCalendarToolStripMenuItem
            // 
            this.showCalendarToolStripMenuItem.Name = "showCalendarToolStripMenuItem";
            this.showCalendarToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.showCalendarToolStripMenuItem.Text = "カレンダ表示(&A)";
            this.showCalendarToolStripMenuItem.Click += new System.EventHandler(this.showCalendarToolStripMenuItem_Click);
            // 
            // hideCalendarToolStripMenuItem
            // 
            this.hideCalendarToolStripMenuItem.Name = "hideCalendarToolStripMenuItem";
            this.hideCalendarToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.hideCalendarToolStripMenuItem.Text = "カレンダ非表示(&D)";
            this.hideCalendarToolStripMenuItem.Click += new System.EventHandler(this.hideCalendarToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(209, 6);
            // 
            // listStickiesToolStripMenuItem
            // 
            this.listStickiesToolStripMenuItem.Name = "listStickiesToolStripMenuItem";
            this.listStickiesToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.listStickiesToolStripMenuItem.Text = "付箋一覧(&L)";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(209, 6);
            // 
            // launcherToolStripMenuItem
            // 
            this.launcherToolStripMenuItem.Name = "launcherToolStripMenuItem";
            this.launcherToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.launcherToolStripMenuItem.Text = "ランチャメニュー(&L)";
            this.launcherToolStripMenuItem.Click += new System.EventHandler(this.launcherToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(209, 6);
            // 
            // termToolStripMenuItem
            // 
            this.termToolStripMenuItem.Name = "termToolStripMenuItem";
            this.termToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.termToolStripMenuItem.Text = "終了(&E)";
            this.termToolStripMenuItem.Click += new System.EventHandler(this.termToolStripMenuItem_Click);
            // 
            // bgwTimeTick
            // 
            this.bgwTimeTick.WorkerReportsProgress = true;
            this.bgwTimeTick.WorkerSupportsCancellation = true;
            this.bgwTimeTick.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwTimeTick_DoWork);
            this.bgwTimeTick.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwTimeTick_ProgressChanged);
            // 
            // cmsCalendarMenu
            // 
            this.cmsCalendarMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripCalendarMenuItem,
            this.queryToolStripCalendarMenuItem,
            this.toolStripSeparator6,
            this.hideCalendarToolStripCalendarMenuItem,
            this.toolStripSeparator10,
            this.topMostToolStripCalendarMenuItem,
            this.toolStripSeparator7,
            this.termToolStripCalendarMenuItem});
            this.cmsCalendarMenu.Name = "cmsMenu";
            this.cmsCalendarMenu.Size = new System.Drawing.Size(179, 132);
            this.cmsCalendarMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsCalendarMenu_Opening);
            // 
            // settingToolStripCalendarMenuItem
            // 
            this.settingToolStripCalendarMenuItem.Name = "settingToolStripCalendarMenuItem";
            this.settingToolStripCalendarMenuItem.Size = new System.Drawing.Size(178, 22);
            this.settingToolStripCalendarMenuItem.Text = "設定(&S)";
            this.settingToolStripCalendarMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // queryToolStripCalendarMenuItem
            // 
            this.queryToolStripCalendarMenuItem.Name = "queryToolStripCalendarMenuItem";
            this.queryToolStripCalendarMenuItem.Size = new System.Drawing.Size(178, 22);
            this.queryToolStripCalendarMenuItem.Text = "クエリ検索(&Q)";
            this.queryToolStripCalendarMenuItem.Click += new System.EventHandler(this.queryToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(175, 6);
            // 
            // hideCalendarToolStripCalendarMenuItem
            // 
            this.hideCalendarToolStripCalendarMenuItem.Name = "hideCalendarToolStripCalendarMenuItem";
            this.hideCalendarToolStripCalendarMenuItem.Size = new System.Drawing.Size(178, 22);
            this.hideCalendarToolStripCalendarMenuItem.Text = "カレンダ非表示(&D)";
            this.hideCalendarToolStripCalendarMenuItem.Click += new System.EventHandler(this.hideCalendarToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(175, 6);
            // 
            // topMostToolStripCalendarMenuItem
            // 
            this.topMostToolStripCalendarMenuItem.Name = "topMostToolStripCalendarMenuItem";
            this.topMostToolStripCalendarMenuItem.Size = new System.Drawing.Size(178, 22);
            this.topMostToolStripCalendarMenuItem.Text = "カレンダを最前面化(&T)";
            this.topMostToolStripCalendarMenuItem.Click += new System.EventHandler(this.topMostToolStripCalendarMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(175, 6);
            // 
            // termToolStripCalendarMenuItem
            // 
            this.termToolStripCalendarMenuItem.Name = "termToolStripCalendarMenuItem";
            this.termToolStripCalendarMenuItem.Size = new System.Drawing.Size(178, 22);
            this.termToolStripCalendarMenuItem.Text = "終了(&E)";
            this.termToolStripCalendarMenuItem.Click += new System.EventHandler(this.termToolStripMenuItem_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(60, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "label8";
            this.label8.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(60, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "label9";
            this.label9.Visible = false;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.ContextMenuStrip = this.cmsCalendarMenu;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Location = new System.Drawing.Point(0, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 2);
            this.label1.TabIndex = 16;
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseDown);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DarkGray;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(24, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 8);
            this.label2.TabIndex = 17;
            this.label2.Visible = false;
            this.label2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.label2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.DarkGray;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(24, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 8);
            this.label3.TabIndex = 18;
            this.label3.Visible = false;
            this.label3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.label3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.DarkGray;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(24, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 10);
            this.label4.TabIndex = 19;
            this.label4.Visible = false;
            this.label4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.label4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.DarkGray;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(24, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 10);
            this.label5.TabIndex = 20;
            this.label5.Visible = false;
            this.label5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.label5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true;
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblTime.ContextMenuStrip = this.cmsCalendarMenu;
            this.lblTime.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTime.Location = new System.Drawing.Point(20, 20);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(100, 20);
            this.lblTime.TabIndex = 21;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTime.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.lblTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseDown);
            this.lblTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseUp);
            // 
            // bgwDesktopHook
            // 
            this.bgwDesktopHook.WorkerReportsProgress = true;
            this.bgwDesktopHook.WorkerSupportsCancellation = true;
            this.bgwDesktopHook.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDesktopHook_DoWork);
            // 
            // bgwMailChecker
            // 
            this.bgwMailChecker.WorkerReportsProgress = true;
            this.bgwMailChecker.WorkerSupportsCancellation = true;
            this.bgwMailChecker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwMailChecker_DoWork);
            this.bgwMailChecker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwMailChecker_ProgressChanged);
            // 
            // bgwWindowList
            // 
            this.bgwWindowList.WorkerReportsProgress = true;
            this.bgwWindowList.WorkerSupportsCancellation = true;
            this.bgwWindowList.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwWindowList_DoWork);
            // 
            // ulsbRightButton
            // 
            this.ulsbRightButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ulsbRightButton.BevelColor = System.Drawing.Color.Gray;
            this.ulsbRightButton.ContextMenuStrip = this.cmsCalendarMenu;
            this.ulsbRightButton.LabelText = ">>";
            this.ulsbRightButton.Location = new System.Drawing.Point(120, 0);
            this.ulsbRightButton.Margin = new System.Windows.Forms.Padding(0);
            this.ulsbRightButton.Name = "ulsbRightButton";
            this.ulsbRightButton.Size = new System.Drawing.Size(20, 40);
            this.ulsbRightButton.TabIndex = 3;
            this.ulsbRightButton.Toggle = false;
            this.ulsbRightButton.ToolTipText = "翌月へ";
            this.ulsbRightButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.ulsbRightButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseDown);
            this.ulsbRightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ulsbRightButton_MouseUp);
            // 
            // ulsbLeftButton
            // 
            this.ulsbLeftButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ulsbLeftButton.BevelColor = System.Drawing.Color.Gray;
            this.ulsbLeftButton.ContextMenuStrip = this.cmsCalendarMenu;
            this.ulsbLeftButton.LabelText = "<<";
            this.ulsbLeftButton.Location = new System.Drawing.Point(0, 0);
            this.ulsbLeftButton.Margin = new System.Windows.Forms.Padding(0);
            this.ulsbLeftButton.Name = "ulsbLeftButton";
            this.ulsbLeftButton.Size = new System.Drawing.Size(20, 40);
            this.ulsbLeftButton.TabIndex = 2;
            this.ulsbLeftButton.Toggle = false;
            this.ulsbLeftButton.ToolTipText = "前月へ";
            this.ulsbLeftButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.ulsbLeftButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseDown);
            this.ulsbLeftButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ulsbLeftButton_MouseUp);
            // 
            // ulsbMonth
            // 
            this.ulsbMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ulsbMonth.BevelColor = System.Drawing.Color.Gray;
            this.ulsbMonth.ContextMenuStrip = this.cmsCalendarMenu;
            this.ulsbMonth.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ulsbMonth.LabelText = "2009/07/03";
            this.ulsbMonth.Location = new System.Drawing.Point(20, 0);
            this.ulsbMonth.Margin = new System.Windows.Forms.Padding(0);
            this.ulsbMonth.Name = "ulsbMonth";
            this.ulsbMonth.Size = new System.Drawing.Size(100, 20);
            this.ulsbMonth.TabIndex = 4;
            this.ulsbMonth.Toggle = false;
            this.ulsbMonth.ToolTipText = "今日の日付及び当月へ";
            this.ulsbMonth.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.ulsbMonth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseDown);
            this.ulsbMonth.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ulsbMonth_MouseUp);
            // 
            // ucLabel1
            // 
            this.ucLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(230)))));
            this.ucLabel1.BevelColor = System.Drawing.Color.Blue;
            this.ucLabel1.LabelText = "31";
            this.ucLabel1.LabelTextColor = System.Drawing.Color.Black;
            this.ucLabel1.Location = new System.Drawing.Point(20, 0);
            this.ucLabel1.Name = "ucLabel1";
            this.ucLabel1.Size = new System.Drawing.Size(20, 20);
            this.ucLabel1.TabIndex = 0;
            this.ucLabel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.ucLabel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseDown);
            // 
            // pictureBox2
            // 
            this.pictureBox2.ContextMenuStrip = this.cmsCalendarMenu;
            this.pictureBox2.Location = new System.Drawing.Point(0, 40);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(140, 20);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Tag = "Title";
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseDown);
            this.pictureBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.cmsCalendarMenu;
            this.pictureBox1.Location = new System.Drawing.Point(0, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(140, 120);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Tag = "Cal";
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseUp);
            // 
            // taskToolStripMenuItem
            // 
            this.taskToolStripMenuItem.Name = "taskToolStripMenuItem";
            this.taskToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.taskToolStripMenuItem.Text = "タスク登録(&T)";
            this.taskToolStripMenuItem.Click += new System.EventHandler(this.taskToolStripMenuItem_Click);
            // 
            // commandlineLauncherToolStripMenuItem
            // 
            this.commandlineLauncherToolStripMenuItem.Name = "commandlineLauncherToolStripMenuItem";
            this.commandlineLauncherToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.commandlineLauncherToolStripMenuItem.Text = "コマンドラインランチャ(&C)";
            this.commandlineLauncherToolStripMenuItem.Click += new System.EventHandler(this.commandlineLauncherToolStripMenuItem_Click);
            // 
            // frmCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(140, 185);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ulsbRightButton);
            this.Controls.Add(this.ulsbLeftButton);
            this.Controls.Add(this.ulsbMonth);
            this.Controls.Add(this.ucLabel1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblTime);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCalendar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "S.w.S.";
            this.Load += new System.EventHandler(this.frmCalendar_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseUp);
            this.Shown += new System.EventHandler(this.frmCalendar_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseDown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCalendar_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalendar_MouseMove);
            this.cmsMenu.ResumeLayout(false);
            this.cmsCalendarMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCLabel.ucLabelCalendar ucLabel1;
        private System.Windows.Forms.Timer tTimer;
        private System.Windows.Forms.NotifyIcon niSwS;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem termToolStripMenuItem;
        private UCLabel.ucLabelSunkenButton ulsbLeftButton;
        private UCLabel.ucLabelSunkenButton ulsbRightButton;
        private UCLabel.ucLabelSunkenButton ulsbMonth;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem showCalendarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideCalendarToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgwTimeTick;
        private System.Windows.Forms.ToolStripMenuItem listStickiesToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem todoBatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoBatchToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgwDesktopHook;
        private System.Windows.Forms.ContextMenuStrip cmsCalendarMenu;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripCalendarMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem hideCalendarToolStripCalendarMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem termToolStripCalendarMenuItem;
        private System.ComponentModel.BackgroundWorker bgwMailChecker;
        private System.Windows.Forms.ToolStripMenuItem queryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queryToolStripCalendarMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem topMostToolStripCalendarMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launcherSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem launcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem goCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem innerDesktopToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgwWindowList;
        private System.Windows.Forms.ToolStripMenuItem taskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandlineLauncherToolStripMenuItem;






    }
}