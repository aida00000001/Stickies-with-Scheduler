using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data.SQLite;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace SwS
{
    public partial class frmCalendar : Form
    {
        private const int MAX_UC_DAY = 42;
        private const int MAX_UC_DAY_COL = 7;
        private const int MAX_UC_DAY_ROW = 6;
        private const int MAX_UC_DAY_WIDTH = 20;
        private const int MAX_UC_DAY_HEIGHT = 20;

        private Point mousePoint;
        private DateTime calendarDate = DateTime.Today;
        private DateTime beforeDate = DateTime.Now;
        private Hashtable holiday = new Hashtable();
        private Hashtable dayOfWeekToNumber = new Hashtable();
        private Hashtable dayOfWeekToKanji = new Hashtable();
        private Hashtable effectiveClickHitTest = new Hashtable();
        private Hashtable effectiveBeforeMonthClickHitTest = new Hashtable();
        private Hashtable effectiveNextMonthClickHitTest = new Hashtable();
        private Hashtable monthlySchedule = new Hashtable();
        private SQLiteAccess sqliteAccess = new SQLiteAccess();

        private frmListInputSTD frmInput = new frmListInputSTD();
        private frmInputTMA frmInputMta = new frmInputTMA();
        private frmQuery frmQuery = new frmQuery();
        private frmAbout frmAbout = new frmAbout();
        private frmLauncher frmLauncher = new frmLauncher();
        private frmLauncherSetting frmLauncherSetting = new frmLauncherSetting();
        private frmCommonSettings frmCommonSettings = new frmCommonSettings();
        private frmCommandlineLauncher frmCommandlineLauncher = new frmCommandlineLauncher();

        private Size formSize = new Size();
        private Size ulsbLeftButtonSize = new Size();
        private Size ulsbRightButtonSize = new Size();
        private Point label1Location = new Point();
        private Point pictureBox2Location = new Point();
        private Point pictureBox1Location = new Point();

        public static DBManager.Contents.ContentsList contentsList = new DBManager.Contents.ContentsList();
        public static DBManager.MailChecker.MailCheckerList mailCheckerList = new DBManager.MailChecker.MailCheckerList();

        public Icon icon1 = null;
        public Icon icon2 = null;

        private bool bMailCheckerAnimation = false;
        private Int32 bMailCheckerAnimationType = 0;

        private bool bDragMode = false;
        private bool bAdjustStartup = false;
        private bool bIntervalAdjust = false;
        private bool bRedrawCalendar = false;       // Windows 復帰時画面再描画指定
        private int nIntervalType = 0;
        private int nIntervalTime = 0;
        private DateTime dtAdjustTime;
        private static string strNTPServer = "";

        public frmCalendar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// マウスボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCalendar_MouseDown(object sender, MouseEventArgs e)
        {
            // 左ボタン押下時
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mousePoint = new Point(e.X, e.Y);
            }
        }

        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCalendar_MouseMove(object sender, MouseEventArgs e)
        {
            this.SuspendLayout();

            // 左ボタン押下ドラッグ時
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                int moveX = e.X - mousePoint.X;
                int moveY = e.Y - mousePoint.Y;

                // デスクトップの四辺にスナップする場合
                if (DBManager.Setting.main_desktop_snapping == true)
                {
                    int h, w;
                    //ディスプレイの作業領域の高さ
                    h = System.Windows.Forms.Screen.GetWorkingArea(this).Height;
                    //ディスプレイの作業領域の幅
                    w = System.Windows.Forms.Screen.GetWorkingArea(this).Width;
                    int band = DBManager.Setting.main_desktop_snapping_band;

                    if (this.Left + moveX >= 0 && this.Left + moveX <= band)
                    {
                        this.Left = 0;
                    }
                    else if (this.Left + this.Width + moveX <= w && this.Left + this.Width + moveX >= w - band)
                    {
                        this.Left = w - this.Width;
                    }
                    else
                    {
                        this.Left += moveX;
                    }
                    if (this.Top + moveY >= 0 && this.Top + moveY <= band)
                    {
                        this.Top = 0;
                    }
                    else if (this.Top + this.Height + moveY <= h && this.Top + this.Height + moveY >= h - band)
                    {
                        this.Top = h - this.Height;
                    }
                    else
                    {
                        this.Top += moveY;
                    }
                }
                else
                {
                    this.Left += moveX;
                    this.Top += moveY;
                }
                bDragMode = true;
            }
            else
            {
                if (mousePoint.X != 0 && mousePoint.Y != 0)
                {
                    //位置を記憶する
                    mousePoint = new Point(0, 0);
                }

                PictureBox pb = (sender as PictureBox);
                if (pb != null && "Cal".Equals(pb.Tag.ToString()))
                {
                    // ヒットしたポイントを７ｘ６の升目の０オリジンインデックスに変換(０～４１)
                    int row = e.Y / MAX_UC_DAY_HEIGHT;
                    int col = e.X / MAX_UC_DAY_WIDTH;
                    int index = row * MAX_UC_DAY_COL + col;

                    // HitTest 成功なら
                    if (effectiveClickHitTest[index] != null)
                    {
                        int x = col * MAX_UC_DAY_WIDTH + pictureBox1.Left;
                        int y = row * MAX_UC_DAY_HEIGHT + pictureBox1.Top;
                        label2.Location = new Point(x, y);
                        label3.Location = new Point(x, y + MAX_UC_DAY_HEIGHT - 2);
                        label4.Location = new Point(x, y);
                        label5.Location = new Point(x + MAX_UC_DAY_WIDTH - 2, y);
                        label2.Show();
                        label3.Show();
                        label4.Show();
                        label5.Show();

                        // 有効な表示可能領域である確認
                        if (e.Y >= (row * MAX_UC_DAY_HEIGHT + 2) && e.Y <= (row * MAX_UC_DAY_HEIGHT + 17)
                            && e.X >= (col * MAX_UC_DAY_WIDTH + 2) && e.X <= (col * MAX_UC_DAY_WIDTH + 17))
                        {
                            // タスクの有無を確認
                            DateTime checkDate = (DateTime)effectiveClickHitTest[index];
                            string holidayName = (string)holiday[int.Parse(checkDate.ToString("dd"))];
                            ArrayList dailyList = (ArrayList)monthlySchedule[index];

                            // 対象日が、データが在り/休日/本日なら表示
                            if (dailyList.Count != 0 || holidayName != null || checkDate == DateTime.Today)
                            {
                                // タスクをカレンダに表示
                                if (toolTip1.ToolTipTitle.Equals(""))
                                {
                                    string tips = "";
                                    lock (dailyList)
                                    {
                                        foreach (DBManager.Contents.data data in dailyList)
                                        {
                                            if (tips.Equals("") == false)
                                            {
                                                tips = tips + "\r\n\r\n";
                                            }
                                            tips = tips + "■" + data.contents;
                                        }
                                    }
                                    toolTip1.IsBalloon = true;
                                    tips = tips.Equals("") ? "　" : tips;
                                    toolTip1.Hide(pictureBox1);
                                    // タイトルに「日付、本日・休日」を設定
                                    toolTip1.ToolTipTitle =
                                        ((DateTime)effectiveClickHitTest[index]).ToString("yyyy/MM/dd")
                                        + ((checkDate == DateTime.Today) ? "　[ 本日 ]" : "")
                                        + ((holidayName != null) ? ("　[ " + holidayName + " ]") : "");
                                    // コンテンツ内容にスケジュールを設定
                                    toolTip1.SetToolTip(pictureBox1, tips);
                                }
                            }
                            else
                            {
                                if (toolTip1.ToolTipTitle != null && toolTip1.ToolTipTitle.Equals("") == false)
                                {
                                    toolTip1.Hide(pictureBox1);
                                    toolTip1.ToolTipTitle = "";
                                    toolTip1.RemoveAll();
                                }
                            }
                        }
                        else
                        {
                            if (toolTip1.ToolTipTitle != null && toolTip1.ToolTipTitle.Equals("") == false)
                            {
                                toolTip1.Hide(pictureBox1);
                                toolTip1.ToolTipTitle = "";
                                toolTip1.RemoveAll();
                            }
                        }
                    }
                    else
                    {
                        label2.Hide();
                        label3.Hide();
                        label4.Hide();
                        label5.Hide();
                        if (toolTip1.ToolTipTitle != null && toolTip1.ToolTipTitle.Equals("") == false)
                        {
                            toolTip1.Hide(pictureBox1);
                            toolTip1.ToolTipTitle = "";
                            toolTip1.RemoveAll();
                        }
                    }
                }
                else
                {
                    label2.Hide();
                    label3.Hide();
                    label4.Hide();
                    label5.Hide();
                    if (toolTip1.ToolTipTitle != null && toolTip1.ToolTipTitle.Equals("") == false)
                    {
                        toolTip1.Hide(pictureBox1);
                        toolTip1.ToolTipTitle = "";
                        toolTip1.RemoveAll();
                    }
                }
            }

            this.ResumeLayout();
        }

        /// <summary>
        /// ドラッグモードでマウスボタンが離れたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCalendar_MouseUp(object sender, MouseEventArgs e)
        {
            if (bDragMode == true)
            {
                bDragMode = false;
                DBManager.Setting.main_startUp_position_x = this.Location.X;
                DBManager.Setting.main_startUp_position_y = this.Location.Y;
                DBManager.Setting.save(sqliteAccess);
            }
        }

        /// <summary>
        /// コントロールからマウスが外れたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            toolTip1.Hide(pictureBox1);
            toolTip1.ToolTipTitle = "";
            toolTip1.RemoveAll();
        }

        /// <summary>
        /// カレンダダブルクリックにて入力ウィンドウ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 左のダブルクリックのみ
            if (e.Button == MouseButtons.Left)
            {
                // ヒットしたポイントを７ｘ６の升目の０オリジンインデックスに変換(０～４１)
                int row = e.Y / MAX_UC_DAY_HEIGHT;
                int col = e.X / MAX_UC_DAY_WIDTH;
                int index = row * MAX_UC_DAY_COL + col;

                // 当月HitTest 成功なら
                if (effectiveClickHitTest[index] != null)
                {
                    frmInput.inputData((DateTime)effectiveClickHitTest[index], ref contentsList, this);
                }
                else if (effectiveBeforeMonthClickHitTest[index] != null)
                {
                    calendarDate = calendarDate.AddMonths(-1);
                    setCalendar();
                }
                else if (effectiveNextMonthClickHitTest[index] != null)
                {
                    calendarDate = calendarDate.AddMonths(1);
                    setCalendar();
                }
            }
        }

        /// <summary>
        /// カレンダ画面ロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCalendar_Load(object sender, EventArgs e)
        {
#if DEBUG
            DBManager.dbPath = Application.ExecutablePath;
#else
            DBManager.dbPath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Aida\\Stickies with Scheduler\\";
#endif

            //フォームコンストラクタなどの適当な位置に記述してもよい
            Microsoft.Win32.SystemEvents.PowerModeChanged +=
                new Microsoft.Win32.PowerModeChangedEventHandler(
                    SystemEvents_PowerModeChanged);

            // アイコン準備
            icon1 = new Icon(this.Icon, 128, 128);     // 通常アイコン
            icon2 = new Icon(niSwS.Icon, 128, 128);    // アニメーション時凹み
            niSwS.Icon = icon1;

            // 曜日取り扱い初期化
            dayOfWeekToNumber.Add(DayOfWeek.Sunday, 0);
            dayOfWeekToNumber.Add(DayOfWeek.Monday, 1);
            dayOfWeekToNumber.Add(DayOfWeek.Tuesday, 2);
            dayOfWeekToNumber.Add(DayOfWeek.Wednesday, 3);
            dayOfWeekToNumber.Add(DayOfWeek.Thursday, 4);
            dayOfWeekToNumber.Add(DayOfWeek.Friday, 5);
            dayOfWeekToNumber.Add(DayOfWeek.Saturday, 6);
            dayOfWeekToKanji.Add(DayOfWeek.Sunday, "日");
            dayOfWeekToKanji.Add(DayOfWeek.Monday, "月");
            dayOfWeekToKanji.Add(DayOfWeek.Tuesday, "火");
            dayOfWeekToKanji.Add(DayOfWeek.Wednesday, "水");
            dayOfWeekToKanji.Add(DayOfWeek.Thursday, "木");
            dayOfWeekToKanji.Add(DayOfWeek.Friday, "金");
            dayOfWeekToKanji.Add(DayOfWeek.Saturday, "土");

            // 再画抑制
            this.SuspendLayout();

            // テンプレコントロール隠蔽
            ucLabel1.Hide();

            // 選択中カレンダ日付強調用ラベル設定
            label2.Size = new Size(MAX_UC_DAY_WIDTH, 2);
            label3.Size = new Size(MAX_UC_DAY_WIDTH, 2);
            label4.Size = new Size(2, MAX_UC_DAY_HEIGHT);
            label5.Size = new Size(2, MAX_UC_DAY_HEIGHT);
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();

            // 初期起動はフォーム無しタスクトレイアイコンのみ
            this.Text = "S.w.S. Calendar";

            // SQL発行後の環境設定
            sqliteAccess.setEnviroment(DBManager.dbPath, "SwS.db");

            // DB更新処理
            DBManager.UpdateManager.Update(sqliteAccess);

            // デスクトップフック開始
            desktopHookStartUp();

            formSize = this.Size;
            ulsbLeftButtonSize = ulsbLeftButton.Size;
            ulsbRightButtonSize = ulsbRightButton.Size;
            label1Location = label1.Location;
            pictureBox2Location = pictureBox2.Location;
            pictureBox1Location = pictureBox1.Location;

            // 設定ファイル読み込み
            readIniFile();

            // 起動時の時刻同期処理
            // 刻紡開始時時刻同期機能が有効でタイムサーバが指定されているとき
            if (bAdjustStartup == true && !strNTPServer.Equals(""))
            {
                syncTime();
            }

            // 各種データ読み込み
            readContentsData();

            // カレンダ設定
            setCalendar();

            // メールチェッカ設定
            readMailCheckerData();

            RegisterHotKey();

            this.Show();
        }

        /// <summary>
        /// 電源状態変更通知イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemEvents_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case Microsoft.Win32.PowerModes.StatusChange:
                    break;
                case Microsoft.Win32.PowerModes.Suspend:

                    // バックグラウンドワークスレッドのキャンセルを有効にする
                    if (bgwWindowList.IsBusy == true)
                    {
                        bgwWindowList.WorkerSupportsCancellation = true;
                        bgwWindowList.CancelAsync();
                        bgwWindowList.Dispose();
                    }
                    if (bgwTimeTick.IsBusy == true)
                    {
                        bgwTimeTick.WorkerSupportsCancellation = true;
                        bgwTimeTick.CancelAsync();
                        bgwTimeTick.Dispose();
                    }
                    if (bgwDesktopHook.IsBusy == true)
                    {
                        bgwDesktopHook.WorkerSupportsCancellation = true;
                        bgwDesktopHook.CancelAsync();
                        bgwDesktopHook.Dispose();
                    }
                    if (bgwMailChecker.IsBusy == true)
                    {
                        bgwMailChecker.WorkerSupportsCancellation = true;
                        bgwMailChecker.CancelAsync();
                        bgwMailChecker.Dispose();
                    }

                    break;
                case Microsoft.Win32.PowerModes.Resume:

                    // バックグラウンドワークスレッドのキャンセルを有効にする
                    if (bgwWindowList.IsBusy == true)
                    {
                        bgwWindowList.WorkerSupportsCancellation = true;
                        bgwWindowList.CancelAsync();
                        bgwWindowList.Dispose();
                    }
                    if (bgwTimeTick.IsBusy == true)
                    {
                        bgwTimeTick.WorkerSupportsCancellation = true;
                        bgwTimeTick.CancelAsync();
                        bgwTimeTick.Dispose();
                    }
                    if (bgwDesktopHook.IsBusy == true)
                    {
                        bgwDesktopHook.WorkerSupportsCancellation = true;
                        bgwDesktopHook.CancelAsync();
                        bgwDesktopHook.Dispose();
                    }
                    if (bgwMailChecker.IsBusy == true)
                    {
                        bgwMailChecker.WorkerSupportsCancellation = true;
                        bgwMailChecker.CancelAsync();
                        bgwMailChecker.Dispose();
                    }
                    bRedrawCalendar = true;     // Windows 復帰時画面再描画指定

                    break;
            }
        }

        /// <summary>
        /// Window Process override
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // ホットキー制御
            if (m.Msg == Win32APIs.HotKey.WM_HOTKEY)
            {
                switch ((int)m.WParam)
                {
                    case Win32APIs.HotKey.HOTKEY_ID_0001:
                        /* コマンドメニュー起動 */
                        this.Activate();
                        cmsMenu.Show(Control.MousePosition);
                        this.Activate();
                        break;
                    case Win32APIs.HotKey.HOTKEY_ID_0002:
                        /* タスク登録起動 */
                        frmInput.inputData(DateTime.Today, ref contentsList, this);
                        break;
                    case Win32APIs.HotKey.HOTKEY_ID_0003:
                        /* 新規付箋起動 */
                        createNewStickie();
                        break;
                    case Win32APIs.HotKey.HOTKEY_ID_0004:
                        /* 新規タスク起動 */
                        frmInputMta.inputData(DateTime.Today, ref contentsList, this);
                        break;
                    case Win32APIs.HotKey.HOTKEY_ID_0005:
                        /* コマンドラインランチャ */
                        frmCommandlineLauncher.showLauncherMenu();
                        break;
                }
            }
#if false
            // 電源制御
            else if (m.Msg == Win32APIs.PowerManagement.WM_POWERBROADCAST)
            {
                switch ((int)m.WParam)
                {
                    case Win32APIs.PowerManagement.PBT_APMRESUMEAUTOMATIC:  // システムが自動的に復帰しようとしている
                    case Win32APIs.PowerManagement.PBT_APMRESUMECRITICAL:   // 致命的な待機状態からシステムが復帰しようとしている
                    case Win32APIs.PowerManagement.PBT_APMRESUMESUSPEND:    // 待機状態から復帰しようとしている

                        // バックグラウンドワークスレッドのキャンセルを有効にする
                        if (bgwTimeTick.IsBusy == true)
                        {
                            bgwTimeTick.WorkerSupportsCancellation = true;
                            bgwTimeTick.CancelAsync();
                            bgwTimeTick.Dispose();
                        }
                        if (bgwDesktopHook.IsBusy == true)
                        {
                            bgwDesktopHook.WorkerSupportsCancellation = true;
                            bgwDesktopHook.CancelAsync();
                            bgwDesktopHook.Dispose();
                        }
                        if (bgwMailChecker.IsBusy == true)
                        {
                            bgwMailChecker.WorkerSupportsCancellation = true;
                            bgwMailChecker.CancelAsync();
                            bgwMailChecker.Dispose();
                        }

                        break;
                }
            }
#endif
        }

        /// <summary>
        /// デスクトップフック開始
        /// </summary>
        private static void desktopHookStartUp()
        {
            // デスクトップツリービューウィンドウハンドル取得
            DesktopHook.GetFindDesktopListViewWnd.FindDesktopListViewWnd();

            // デスクトップフック
            DesktopHook.SharedMemory.GetSharedMemory(DesktopHook.GetFindDesktopListViewWnd.getDesktopListViewAddress());
        }

        /// <summary>
        /// 初回表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCalendar_Shown(object sender, EventArgs e)
        {
            // ランチャ隠し画面表示
            frmLauncher.Show();

            // 定義済み付箋表示
            showStickies();

            // 起動時入力フォーム連動起動設定在りの場合
            if (DBManager.Setting.main_inputform_startup == true)
            {
                // 本日のスケジュールが存在していれば
                DBManager.Contents.ContentsList dailyList = contentsList.SelectDataType(0).SelectDateRange(DateTime.Today, DateTime.Today);
                foreach (DBManager.Contents.data schedule in dailyList)
                {
                    if (schedule.DateJudgeByTaskTrigger(DateTime.Today) == true)
                    {
                        // 入力画面を開始時表示とする
                        frmInput.inputData(DateTime.Today, ref contentsList, this);
                        break;
                    }
                }
            }

            // バックグラウンドワークスレッド起動
            // ウィンドウ一覧監視プロセスを別スレッドで実行
            bgwWindowList.RunWorkerAsync(0);
            // 時間起動及び連想起動監視プロセスを別スレッドで実行
            bgwTimeTick.RunWorkerAsync(0);
            // デスクトップフック監視プロセスを別スレッドで実行
            bgwDesktopHook.RunWorkerAsync(0);
        }

        /// <summary>
        /// 祝祭日取得
        /// </summary>
        private void getHoliday()
        {
            SQLiteDataReader result = readMonthlyHolidayData();
            holiday.Clear();
            while (result.Read())
            {
                holiday.Add(int.Parse(result.GetDateTime(0).ToString("dd")), result.GetString(2));
            }
            // リーダクローズ
            sqliteAccess.readerClose();
        }

        /// <summary>
        /// 設定ファイル読込処理
        /// </summary>
        private void readIniFile()
        {
            try
            {
                // SELECT文の作成
                string strSQL = "select * from setting ";

                SQLiteDataReader result = sqliteAccess.select(strSQL);
                if (result.Read())
                {
                    // バージョン表記
                    DBManager.Setting.version = result.GetString((int)DBManager.Setting.enum_setting.version);
                    // [Main StartUp Position (X,Y)]
                    DBManager.Setting.main_startUp_position_x = result.GetInt32((int)DBManager.Setting.enum_setting.main_startUp_position_x);
                    DBManager.Setting.main_startUp_position_y = result.GetInt32((int)DBManager.Setting.enum_setting.main_startUp_position_y);
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(DBManager.Setting.main_startUp_position_x, DBManager.Setting.main_startUp_position_y);
                    // [Main Time Format]
                    DBManager.Setting.main_time_format = result.GetString((int)DBManager.Setting.enum_setting.main_time_format);
                    // [Main Date Format]
                    DBManager.Setting.main_date_format = result.GetString((int)DBManager.Setting.enum_setting.main_date_format);
                    // [Main Alpha]
                    DBManager.Setting.main_alpha = result.GetInt32((int)DBManager.Setting.enum_setting.main_alpha);
                    // [Main Time Adjust StartUp]
                    DBManager.Setting.main_time_adjust_startup = result.GetBoolean((int)DBManager.Setting.enum_setting.main_time_adjust_startup);
                    bAdjustStartup = DBManager.Setting.main_time_adjust_startup;
                    // [Main Time Adjust Interval (Adjust,IntervalType,Interval)]
                    DBManager.Setting.main_time_adjust_interval = result.GetBoolean((int)DBManager.Setting.enum_setting.main_time_adjust_interval);
                    bIntervalAdjust = DBManager.Setting.main_time_adjust_interval;
                    DBManager.Setting.main_time_adjust_interval_type = result.GetInt32((int)DBManager.Setting.enum_setting.main_time_adjust_interval_type);
                    nIntervalType = DBManager.Setting.main_time_adjust_interval_type;
                    DBManager.Setting.main_time_adjust_interval_count = result.GetInt32((int)DBManager.Setting.enum_setting.main_time_adjust_interval_count);
                    nIntervalTime = DBManager.Setting.main_time_adjust_interval_count;
                    if (bIntervalAdjust == true)
                    {
                        createNextSyncTime();
                    }
                    // [Main Time Adjust NTP Server]
                    DBManager.Setting.main_time_adjust_ntp_server = result.GetString((int)DBManager.Setting.enum_setting.main_time_adjust_ntp_server);
                    strNTPServer = DBManager.Setting.main_time_adjust_ntp_server.Trim();
                    // [Main InputForm StartUp]
                    DBManager.Setting.main_inputform_startup = result.GetBoolean((int)DBManager.Setting.enum_setting.main_inputform_startup);
                    // [Main No TimeDisplay]
                    DBManager.Setting.main_no_timedisplay = result.GetBoolean((int)DBManager.Setting.enum_setting.main_no_timedisplay);
                    // [Main Desktop Access]
                    DBManager.Setting.main_desktop_access = result.GetBoolean((int)DBManager.Setting.enum_setting.main_desktop_access);
                    // [Main Desktop Snapping]
                    DBManager.Setting.main_desktop_snapping = result.GetBoolean((int)DBManager.Setting.enum_setting.main_desktop_snapping);
                    // [Main Desktop Snapping Band]
                    DBManager.Setting.main_desktop_snapping_band = result.GetInt32((int)DBManager.Setting.enum_setting.main_desktop_snapping_band);
                    // [main Query String]
                    DBManager.Setting.main_query_string = result.GetString((int)DBManager.Setting.enum_setting.main_query_string);
                    // [main Query Object]
                    DBManager.Setting.main_query_object = result.GetString((int)DBManager.Setting.enum_setting.main_query_object);
                    // [main Query Expr]
                    DBManager.Setting.main_query_expr = result.GetInt32((int)DBManager.Setting.enum_setting.main_query_expr);
                    // [main Mailchecker Blink Cancel]
                    DBManager.Setting.main_mailchecker_blinkcancel = result.GetBoolean((int)DBManager.Setting.enum_setting.main_mailchecker_blinkcancel);
                    // [Main hotkey 000]
                    DBManager.Setting.main_hotkey_000 = result.GetString((int)DBManager.Setting.enum_setting.main_hotkey_000);
                    // [Main hotkey 001]
                    DBManager.Setting.main_hotkey_001 = result.GetString((int)DBManager.Setting.enum_setting.main_hotkey_001);
                    // [Main hotkey 002]
                    DBManager.Setting.main_hotkey_002 = result.GetString((int)DBManager.Setting.enum_setting.main_hotkey_002);
                    // [Main hotkey 003]
                    DBManager.Setting.main_hotkey_003 = result.GetString((int)DBManager.Setting.enum_setting.main_hotkey_003);
                    // [Main hotkey 004]
                    DBManager.Setting.main_hotkey_004 = result.GetString((int)DBManager.Setting.enum_setting.main_hotkey_004);
                    // [Main Task Display Target]
                    DBManager.Setting.main_task_display_target = result.GetString((int)DBManager.Setting.enum_setting.main_task_display_target);
                    // [Main hotkey 005]
                    DBManager.Setting.main_hotkey_005 = result.GetString((int)DBManager.Setting.enum_setting.main_hotkey_005);
                    // [Main hotkey 006]
                    DBManager.Setting.main_hotkey_006 = result.GetString((int)DBManager.Setting.enum_setting.main_hotkey_006);
                    // [Main hotkey 007]
                    DBManager.Setting.main_hotkey_007 = result.GetString((int)DBManager.Setting.enum_setting.main_hotkey_007);
                    // [Main hotkey 008]
                    DBManager.Setting.main_hotkey_008 = result.GetString((int)DBManager.Setting.enum_setting.main_hotkey_008);
                    // [Main hotkey 009]
                    DBManager.Setting.main_hotkey_009 = result.GetString((int)DBManager.Setting.enum_setting.main_hotkey_009);
                    // [Main Default Backcolor Stickies]
                    DBManager.Setting.main_default_backcolor_stickies = result.GetString((int)DBManager.Setting.enum_setting.main_default_backcolor_stickies);

                    // カレンダ表示設定
                    setCalendarDisplay();
                }
                sqliteAccess.readerClose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 時間調整プロパティ更新(設定画面からの更新用)
        /// </summary>
        public void renewTimeAdjust()
        {
            bAdjustStartup = DBManager.Setting.main_time_adjust_startup;
            bIntervalAdjust = DBManager.Setting.main_time_adjust_interval;
            nIntervalType = DBManager.Setting.main_time_adjust_interval_type;
            nIntervalTime = DBManager.Setting.main_time_adjust_interval_count;
            if (bIntervalAdjust == true)
            {
                createNextSyncTime();
            }
        }

        /// <summary>
        /// 次回時刻同期時間の作成
        /// </summary>
        private void createNextSyncTime()
        {
            if (nIntervalType == 0)
            {
                dtAdjustTime = DateTime.Parse(DateTime.Now.AddMonths(nIntervalTime).ToString());
            }
            else if (nIntervalType == 1)
            {
                dtAdjustTime = DateTime.Parse(DateTime.Now.AddDays(nIntervalTime).ToString());
            }
            else if (nIntervalType == 2)
            {
                dtAdjustTime = DateTime.Parse(DateTime.Now.AddHours(nIntervalTime).ToString());
            }
        }

        /// <summary>
        /// 設定ファイル書込処理
        /// </summary>
        private void writeIniFile()
        {
            DBManager.Setting.save(sqliteAccess);
        }

        /// <summary>
        /// 時刻同期処理
        /// </summary>
        private static void syncTime()
        {
            DateTime newTime = DateTime.MaxValue;

            NtpClient ntp = new NtpClient(strNTPServer);
            ntp.GetCurrentTime(ref newTime);
            Win32APIs.SysDateTime.SetNowDateTime(newTime);
        }

        /// <summary>
        /// カレンダ画面アンロード前処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCalendar_FormClosing(object sender, FormClosingEventArgs e)
        {
            writeIniFile();
            // 終了理由が×ボタン押下のとき(正しくはユーザ操作による終了処理)
            // (タスクトレイアイコン右クリックでの終了処理の場合は通常処理を行う)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // 本来の終了処理ではなくフォーム非表示とする
                e.Cancel = true;                // 終了処理キャンセル
                this.Visible = false;           // フォーム非表示
            }

            UnregisterHotKey();

            // バックグラウンドワークスレッドのキャンセルを有効にする
            if (bgwWindowList.IsBusy == true)
            {
                bgwWindowList.WorkerSupportsCancellation = true;
                bgwWindowList.CancelAsync();
                bgwWindowList.Dispose();
            }
            if (bgwTimeTick.IsBusy == true)
            {
                bgwTimeTick.WorkerSupportsCancellation = true;
                bgwTimeTick.CancelAsync();
                bgwTimeTick.Dispose();
            }
            if (bgwDesktopHook.IsBusy == true)
            {
                bgwDesktopHook.WorkerSupportsCancellation = true;
                bgwDesktopHook.CancelAsync();
                bgwDesktopHook.Dispose();
                DesktopHook.SharedMemory.Parge();
                System.Threading.Thread.Sleep(500);
            }
            if (bgwMailChecker.IsBusy == true)
            {
                bgwMailChecker.WorkerSupportsCancellation = true;
                bgwMailChecker.CancelAsync();
                bgwMailChecker.Dispose();
            }

            // SQL終了処理
            sqliteAccess.disposeEnviroment();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void termToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // タスクトレイからアイコンを取り除く
            this.niSwS.Visible = false;
            // アプリケーション終了
            Application.Exit();
        }

        /// <summary>
        /// 新規付箋起動処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void niSwS_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            createNewStickie();
        }

        /// <summary>
        /// 新規付箋生成
        /// </summary>
        private void createNewStickie()
        {
            frmStickies frm = new frmStickies();

            DBManager.Contents.data stickyData = new DBManager.Contents.data();
            stickyData.id = -1;
            stickyData.effective = true;
            stickyData.data_type = 3;
            stickyData.contents = "";
            stickyData.backcolor_stickies = DBManager.Setting.main_default_backcolor_stickies;
            stickyData.point_stickies = frm.Location.X.ToString() + ", " + frm.Location.Y.ToString() + ", " + frm.Size.Width.ToString() + ", " + frm.Size.Height.ToString();
            stickyData.time_trigger = false;
            stickyData.time_trigger_datetime = DateTime.Now;
            stickyData.time_trigger_dayofweek = (int)dayOfWeekToNumber[DateTime.Now.DayOfWeek];
            stickyData.attach_trigger = false;
            stickyData.attach_trigger_title = "";
            stickyData.alpha_stickies = 100;
            stickyData.display_font_stickies = frm.Font;
            stickyData.displayed = true;
            contentsList.Add(stickyData);

            frm.showSticky(ref stickyData);
            stickyData.frm = frm;
            frm.Show();
            frm.Activate();
        }

        /// <summary>
        /// 付箋最前面化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void niSwS_MouseClick(object sender, MouseEventArgs e)
        {
            bMailCheckerAnimation = false;
            bMailCheckerAnimationType = 0;

            // 左ボタン押下時のみ付箋最前面化
            if (e.Button == MouseButtons.Left)
            {
                lock (contentsList)
                {
                    foreach (DBManager.Contents.data data in contentsList.SelectStickies().SelectDisplayed())
                    {
                        data.frm.Activate();
                    }
                }
                // カレンダ最前面化
                this.Activate();

                // メールチェック
                if (mailCheckerList.Count != 0)
                {
                    string tips = "";

                    lock (mailCheckerList)
                    {
                        foreach (DBManager.MailChecker.data mailChecker in mailCheckerList.SelectEffective())
                        {
                            if (tips.Equals("") == false)
                            {
                                tips = tips + "\n";
                            }
                            tips = tips + mailChecker.mail_connect_name + " : " + mailChecker.mail_message;
                        }
                    }
                    if (tips.Equals("") == false)
                    {
                        niSwS.BalloonTipTitle = "新着メール(" + DBManager.Setting.mailCheckerExecuteTime.ToString() + ")";
                        niSwS.BalloonTipText = tips;
                        niSwS.ShowBalloonTip(5000);
                    }
                }
            }
        }

        /// <summary>
        /// 時間監視処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tTimer_Tick(object sender, EventArgs e)
        {
            // 今回時刻バックアップ
            DateTime nowDate = DateTime.Now;

            // 時間フィールド更新
            lblTime.Text = nowDate.ToString("HH:mm:ss");

            // 前回日付と今回日付が異なるならカレンダ設定
            if ((!beforeDate.ToString("yyyy/MM/dd").Equals(nowDate.ToString("yyyy/MM/dd")))
                || bRedrawCalendar == true)
            {
                bRedrawCalendar = false;
                calendarDate = DateTime.Today;
                // カレンダ設定
                setCalendar();
            }

            // ラーメンタイマーチェック
            if (DBManager.Setting.bCountdownTimer == true)
            {
                // 現在時刻が終了時刻と同じか越えたら
                if (nowDate >= DBManager.Setting.countdownTermTime)
                {
                    frmMessage frm = new frmMessage();
                    frm.showWithMessage("ご指定の時間が経過しました。", "カウントダウンメッセージ");
                    // frm.TopMost = true;
                    DBManager.Setting.bCountdownTimer = false;
                }
            }

            // バックグラウンドワークスレッド起動
            // ウィンドウ一覧監視プロセスを別スレッドで実行
            if (bgwWindowList.IsBusy == false)
            {
                bgwWindowList.RunWorkerAsync(0);
            }
            // 時間起動及び連想起動及びウィンドウ貼付監視プロセスを別スレッドで実行
            if (bgwTimeTick.IsBusy == false)
            {
                bgwTimeTick.RunWorkerAsync(0);
            }
            // デスクトップフック監視プロセスを別スレッドで実行
            if (bgwDesktopHook.IsBusy == false)
            {
                bgwDesktopHook.RunWorkerAsync(0);
            }

            // メールチェック監視プロセス初回起動
            // (起動プロセス内で実行すると描画が固まるので
            //  当監視プロセスのみ時間監視(timer処理)内で初回起動を行う)
            if (bgwMailChecker.IsBusy == false)
            {
                // メールチェックプロセスを別スレッドで実行
                bgwMailChecker.RunWorkerAsync(0);
            }

            // メールチェッカアニメーション指示チェック
            if (bMailCheckerAnimation == false)
            {
                // 指示なし
                niSwS.Icon = icon1;
            }
            else
            {
                // 指示あり
                if (bMailCheckerAnimationType <= 1)
                {
                    niSwS.Icon = icon2;
                }
                else
                {
                    niSwS.Icon = icon1;
                }

                bMailCheckerAnimationType++;
                if (bMailCheckerAnimationType > 3)
                {
                    bMailCheckerAnimationType = 0;
                }
            }

            // 今回時刻バックアップ
            beforeDate = nowDate;

            // オンマウスTips設定
            niSwS.Text = nowDate.ToString() + " (" + (string)dayOfWeekToKanji[nowDate.DayOfWeek] + ")";
        }

        /// <summary>
        /// SQLから指定月の休日データ読込
        /// </summary>
        private SQLiteDataReader readMonthlyHolidayData()
        {
            ArrayList result = new ArrayList();

            // SELECT文の作成
            string fromDate = DateTime.Parse(calendarDate.ToString("yyyy-MM-01")).ToShortDateString().Replace('/', '-');
            string toDate = DateTime.Parse(calendarDate.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString().Replace('/', '-');

            string strSQL = "select * from holiday_list where "
                + "holiday_date between '" + fromDate + "' and '" + toDate + "'";

            return sqliteAccess.select(strSQL);
        }

        /// <summary>
        /// SQLから指定日のデータ読込
        /// </summary>
        private SQLiteDataReader readDayData(DateTime day)
        {
            ArrayList result = new ArrayList();

            // SELECT文の作成
            string tempDate = DateTime.Parse(day.ToString("yyyy-MM-dd")).ToShortDateString().Replace('/', '-');

            string strSQL = "select contents from contents_list where effective = 1 and data_type = 1 and "
                + "strftime('%Y-%m-%d', date_time) = '" + tempDate + "'";

            return sqliteAccess.select(strSQL);
        }

        /// <summary>
        /// カレンダ設定
        /// </summary>
        public void setCalendar()
        {
            this.SuspendLayout();

            // 月表示設定
            if (calendarDate.Equals(DateTime.Today))
            {
                ulsbMonth.LabelText = calendarDate.ToString("yyyy/MM/dd");
            }
            else
            {
                ulsbMonth.LabelText = calendarDate.ToString("yyyy/MM");
            }

            // フォント、ペン、ブラシの定義
            Font font = label8.Font;
            SolidBrush brushLR = new SolidBrush(Color.Pink);
            SolidBrush brushDR = new SolidBrush(Color.Red);
            SolidBrush brushLG = new SolidBrush(Color.LightGreen);
            SolidBrush brushDG = new SolidBrush(Color.DarkGreen);
            SolidBrush brushLB = new SolidBrush(Color.LightBlue);
            SolidBrush brushDB = new SolidBrush(Color.Blue);
            SolidBrush brushLGRAY = new SolidBrush(Color.LightGray);
            SolidBrush brushDGRAY = new SolidBrush(Color.Gray);
            Pen penY = new Pen(Color.Gold, 3);

            // カレンダ週タイトル(曜日)
            // Graphics オブジェクトの取得
            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics grfxTitle = Graphics.FromImage(pictureBox2.Image);

            // カレンダ日付
            // Graphics オブジェクトの取得
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics grfx = Graphics.FromImage(pictureBox1.Image);

            // フォント、ペン、ブラシの定義
            SolidBrush brushCR = new SolidBrush(Color.FromArgb(255, 230, 230));
            grfxTitle.FillRectangle(brushCR, 0, 0, MAX_UC_DAY_WIDTH, MAX_UC_DAY_HEIGHT);
            grfx.FillRectangle(brushCR, 0, 0, MAX_UC_DAY_WIDTH, MAX_UC_DAY_HEIGHT * 6);

            // フォント、ペン、ブラシの定義
            SolidBrush brushCB = new SolidBrush(Color.FromArgb(230, 230, 255));
            grfxTitle.FillRectangle(brushCB, MAX_UC_DAY_WIDTH * 6, 0, MAX_UC_DAY_WIDTH, MAX_UC_DAY_HEIGHT);
            grfx.FillRectangle(brushCB, MAX_UC_DAY_WIDTH * 6, 0, MAX_UC_DAY_WIDTH, MAX_UC_DAY_HEIGHT * 6);

            string[] weekKanji = { "日", "月", "火", "水", "木", "金", "土" };
            // 週タイトル(曜日)設定
            for (int i = 0; i < MAX_UC_DAY_COL; i++)
            {
                // 日付描画
                if (i == 0)
                {
                    // 日曜日列
                    grfxTitle.DrawString(weekKanji[i], font, brushDR, 2 + i * MAX_UC_DAY_WIDTH, 4);
                }
                else if (i == 6)
                {
                    grfxTitle.DrawString(weekKanji[i], font, brushDB, 2 + i * MAX_UC_DAY_WIDTH, 4);
                }
                else
                {
                    grfxTitle.DrawString(weekKanji[i], font, brushDG, 2 + i * MAX_UC_DAY_WIDTH, 4);
                }
            }

            // 指定月前月分作成
            DateTime tempDate = DateTime.Parse(calendarDate.ToString("yyyy-MM-01"));
            tempDate = tempDate.AddDays(-1);
            DayOfWeek weekday = tempDate.DayOfWeek;
            int startColIndex = (int)dayOfWeekToNumber[weekday];
            int startRowIndex = 0;
            int OffXNormal = 1;
            int OffYNormal = 3;
            int OffXShadow = 3;
            int OffYShadow = 5;

            effectiveBeforeMonthClickHitTest.Clear();

            // 前月分設定
            // 6 未満の判定にすれば１週目がすべて前月になることはなく、7未満にすればそれがありえる
            if (startColIndex < 7)
            {
                for (int i = startColIndex; i >= 0; i--)
                {
                    // 描画文字列(日)
                    string dayString = "  " + int.Parse(tempDate.ToString("dd")).ToString("#0");
                    dayString = dayString.Substring(dayString.Length - 2, 2);
                    grfx.DrawString(dayString, font, brushLGRAY, OffXShadow + i * MAX_UC_DAY_WIDTH, OffYShadow);
                    grfx.DrawString(dayString, font, brushDGRAY, OffXNormal + i * MAX_UC_DAY_WIDTH, OffYNormal);

                    // HitTest 用のハッシュテーブル作成
                    effectiveBeforeMonthClickHitTest.Add(i, tempDate);

                    tempDate = tempDate.AddDays(-1);
                }
                startColIndex ++;
                startRowIndex = (startColIndex == 7) ? 1 : 0;
                startColIndex = (startColIndex == 7) ? 0 : startColIndex;
            }
            else
            {
                startColIndex = 0;
            }

            // 指定月に変更
            tempDate = DateTime.Parse(calendarDate.ToString("yyyy-MM-01"));

            // 指定月休日取得
            getHoliday();

            effectiveClickHitTest.Clear();
            effectiveNextMonthClickHitTest.Clear();
            monthlySchedule.Clear();
            bool nextMonth = false;

            // 指定月分・指定月翌月分作成
            for (int i = startRowIndex; i < MAX_UC_DAY_ROW; i++)
            {
                for (int j = startColIndex; j < MAX_UC_DAY_COL; j++)
                {
                    // 描画文字列(日)
                    string dayString = "  " + int.Parse(tempDate.ToString("dd")).ToString("#0");
                    dayString = dayString.Substring(dayString.Length - 2, 2);

                    // 翌月処理ならこのifブロックにて完結する
                    if (nextMonth == true)
                    {
                        // 日付描画
                        grfx.DrawString(dayString, font, brushLGRAY, OffXShadow + j * MAX_UC_DAY_WIDTH, OffYShadow + i * MAX_UC_DAY_HEIGHT);
                        grfx.DrawString(dayString, font, brushDGRAY, OffXNormal + j * MAX_UC_DAY_WIDTH, OffYNormal + i * MAX_UC_DAY_HEIGHT);

                        // HitTest 用のハッシュテーブル作成
                        effectiveNextMonthClickHitTest.Add(i * MAX_UC_DAY_COL + j, tempDate);

                        tempDate = tempDate.AddDays(1);
                    }
                    // 以降は当月処理
                    else
                    {
                        // 祝日だったら背景を赤に設定
                        string holidayString = (string)holiday[int.Parse(dayString)];
                        if (holidayString != null)
                        {
                            grfx.FillRectangle(brushCR, j * MAX_UC_DAY_WIDTH, i * MAX_UC_DAY_HEIGHT, MAX_UC_DAY_WIDTH, MAX_UC_DAY_HEIGHT);
                        }
                        // 本日の設定
                        if (tempDate.Equals(DateTime.Today))
                        {
                            grfx.DrawRectangle(penY, 3 + j * MAX_UC_DAY_WIDTH, 3 + i * MAX_UC_DAY_WIDTH, MAX_UC_DAY_WIDTH - 7, MAX_UC_DAY_HEIGHT - 7);
                        }

                        // 日付描画
                        if (j == 0 || holidayString != null)
                        {
                            // 日曜日列、あるいは祝日設定があれば
                            grfx.DrawString(dayString, font, brushLR, OffXShadow + j * MAX_UC_DAY_WIDTH, OffYShadow + i * MAX_UC_DAY_HEIGHT);
                            grfx.DrawString(dayString, font, brushDR, OffXNormal + j * MAX_UC_DAY_WIDTH, OffYNormal + i * MAX_UC_DAY_HEIGHT);
                        }
                        else if (j == 6)
                        {
                            grfx.DrawString(dayString, font, brushLB, OffXShadow + j * MAX_UC_DAY_WIDTH, OffYShadow + i * MAX_UC_DAY_HEIGHT);
                            grfx.DrawString(dayString, font, brushDB, OffXNormal + j * MAX_UC_DAY_WIDTH, OffYNormal + i * MAX_UC_DAY_HEIGHT);
                        }
                        else
                        {
                            grfx.DrawString(dayString, font, brushLG, OffXShadow + j * MAX_UC_DAY_WIDTH, OffYShadow + i * MAX_UC_DAY_HEIGHT);
                            grfx.DrawString(dayString, font, brushDG, OffXNormal + j * MAX_UC_DAY_WIDTH, OffYNormal + i * MAX_UC_DAY_HEIGHT);
                        }

                        // スケジュールをカレンダに表示
                        DBManager.Contents.ContentsList dailyList = contentsList.SelectDataType(0).SelectDateRange(tempDate, tempDate);
                        ArrayList daily = new ArrayList();
                        if (dailyList.Count != 0)
                        {
                            // タスク繰り返し表示設定に従い有効日の判定を行う
                            bool exitTodayData = false;
                            foreach (DBManager.Contents.data schedule in dailyList)
                            {
                                if (schedule.DateJudgeByTaskTrigger(tempDate) == true)
                                {
                                    exitTodayData = true;
                                    daily.Add(schedule);
                                }
                            }

                            if (exitTodayData == true)
                            {
                                Point[] points = {
                                new Point(j * MAX_UC_DAY_WIDTH, i * MAX_UC_DAY_HEIGHT),
                                new Point(j * MAX_UC_DAY_WIDTH + 5, i * MAX_UC_DAY_HEIGHT), 
                                new Point(j * MAX_UC_DAY_WIDTH, i * MAX_UC_DAY_HEIGHT + 5) };
                                grfx.FillPolygon(brushDR, points, System.Drawing.Drawing2D.FillMode.Alternate);
                            }
                        }
                        // アラームをカレンダに表示
                        dailyList = contentsList.SelectDataType(4).SelectDateRange(tempDate, tempDate);
                        if (dailyList.Count != 0)
                        {
                            // タスク繰り返し表示設定に従い有効日の判定を行う
                            bool exitTodayData = false;
                            foreach (DBManager.Contents.data alarm in dailyList)
                            {
                                if (alarm.DateJudgeByTaskTriggerForCalendar(tempDate) == true
                                    && alarm.task_to_calendar == true)
                                {
                                    exitTodayData = true;
                                    daily.Add(alarm);
                                }
                            }

                            if (exitTodayData == true)
                            {
                                Point[] points = {
                                new Point(j * MAX_UC_DAY_WIDTH, i * MAX_UC_DAY_HEIGHT),
                                new Point(j * MAX_UC_DAY_WIDTH + 5, i * MAX_UC_DAY_HEIGHT), 
                                new Point(j * MAX_UC_DAY_WIDTH, i * MAX_UC_DAY_HEIGHT + 5) };
                                grfx.FillPolygon(brushDR, points, System.Drawing.Drawing2D.FillMode.Alternate);
                            }
                        }
                        monthlySchedule.Add(i * MAX_UC_DAY_COL + j, daily);

                        // HitTest 用のハッシュテーブル作成
                        effectiveClickHitTest.Add(i * MAX_UC_DAY_COL + j, tempDate);

                        // 日付カウントアップ
                        tempDate = tempDate.AddDays(1);

                        // 翌月移行判定
                        if (tempDate.ToString("yyyy/MM").CompareTo(calendarDate.ToString("yyyy/MM")) > 0)
                        {
                            nextMonth = true;
                        }
                    }
                }
                startColIndex = 0;
            }

            this.ResumeLayout();
        }

        /// <summary>
        /// メールチェッカ設定
        /// </summary>
        public void readMailCheckerData()
        {
            string strSQL = "select * from mail_list";

            lock (mailCheckerList)
            {
                mailCheckerList.Clear();

                // 祝祭日DBに該当年のデータがあるかどうか確認
                SQLiteDataReader result = sqliteAccess.select(strSQL);
                if (result != null)
                {
                    while (result.Read())
                    {
                        DBManager.MailChecker.data mailChecker = new DBManager.MailChecker.data();
                        mailChecker.id = result.GetInt32((Int32)DBManager.MailChecker.enum_data.id);
                        mailChecker.mail_connect_name = result.GetString((Int32)DBManager.MailChecker.enum_data.mail_connect_name);
                        mailChecker.mail_effective = result.GetBoolean((Int32)DBManager.MailChecker.enum_data.mail_effective);
                        mailChecker.mail_pop3 = result.GetString((Int32)DBManager.MailChecker.enum_data.mail_pop3);
                        mailChecker.mail_user = result.GetString((Int32)DBManager.MailChecker.enum_data.mail_user);
                        mailChecker.mail_pass = result.GetString((Int32)DBManager.MailChecker.enum_data.mail_pass);
                        mailChecker.mail_check_span = result.GetInt32((Int32)DBManager.MailChecker.enum_data.mail_check_span);
                        mailCheckerList.Add(mailChecker);
                    }
                }
                sqliteAccess.readerClose();
            }
        }

        /// <summary>
        /// マウスボタン開放時当月戻し処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ulsbMonth_MouseUp(object sender, MouseEventArgs e)
        {
            if (bDragMode == true)
            {
                bDragMode = false;
                DBManager.Setting.main_startUp_position_x = this.Location.X;
                DBManager.Setting.main_startUp_position_y = this.Location.Y;
                DBManager.Setting.save(sqliteAccess);
            }
            calendarDate = DateTime.Today;
            setCalendar();
        }

        /// <summary>
        /// マウスボタン開放時月加算処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ulsbRightButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (bDragMode == true)
            {
                bDragMode = false;
                DBManager.Setting.main_startUp_position_x = this.Location.X;
                DBManager.Setting.main_startUp_position_y = this.Location.Y;
                DBManager.Setting.save(sqliteAccess);
            }
            calendarDate = calendarDate.AddMonths(1);
            setCalendar();
        }

        /// <summary>
        /// マウスボタン開放時月減算処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ulsbLeftButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (bDragMode == true)
            {
                bDragMode = false;
                DBManager.Setting.main_startUp_position_x = this.Location.X;
                DBManager.Setting.main_startUp_position_y = this.Location.Y;
                DBManager.Setting.save(sqliteAccess);
            }
            calendarDate = calendarDate.AddMonths(-1);
            setCalendar();
        }

        /// <summary>
        /// 定義済み付箋表示
        /// </summary>
        private void showStickies()
        {
            ArrayList frmList = new ArrayList();

            foreach (DBManager.Contents.data data in contentsList.SelectStickies().SelectDisplayed())
            {
                DBManager.Contents.data stickyData = data;
                frmStickies frm = loadUndisplaySticky(ref stickyData);
                frmList.Add(frm);
            }
            foreach (frmStickies frm in frmList)
            {
                frm.Show();
            }
        }

        /// <summary>
        /// 各種データ読込
        /// </summary>
        private void readContentsData()
        {
            // SELECT文の作成
            string strSQL = "select * from contents_list";

            SQLiteDataReader result = sqliteAccess.select(strSQL);
            while (result.Read())
            {
                DBManager.Contents.data data = new DBManager.Contents.data();

                data.id = result.GetInt32((Int32)DBManager.Contents.enum_data.id);
                data.effective = result.GetBoolean((Int32)DBManager.Contents.enum_data.effective);
                data.data_type = result.GetInt32((Int32)DBManager.Contents.enum_data.data_type);
                // データはすべて読み込むが、付箋かつ無効化されたデータは読み飛ばす。
                if (data.data_type == 3 && data.effective == false)
                {
                    continue;
                }
                data.date_time = result.GetDateTime((Int32)DBManager.Contents.enum_data.date_time);
                data.contents = result.GetString((Int32)DBManager.Contents.enum_data.contents);
                data.show_stickies = result.GetBoolean((Int32)DBManager.Contents.enum_data.show_stickies);
                data.backcolor_stickies = result.GetString((Int32)DBManager.Contents.enum_data.backcolor_stickies);
                data.point_stickies = result.GetString((Int32)DBManager.Contents.enum_data.point_stickies);
                data.time_trigger = result.GetBoolean((Int32)DBManager.Contents.enum_data.time_trigger);
                data.time_trigger_type = result.GetInt32((Int32)DBManager.Contents.enum_data.time_trigger_type);
                if (data.time_trigger == true)
                {
                    data.time_trigger_datetime = result.GetDateTime((Int32)DBManager.Contents.enum_data.time_trigger_datetime);
                }
                data.time_trigger_dayofweek = result.GetInt32((Int32)DBManager.Contents.enum_data.time_trigger_dayofweek);
                // 0:付箋で通常表示
                // 1:未完了のスケジュールデータで付箋送り
                if ((data.data_type == 3 && data.effective == true && data.time_trigger == true)
                    || (data.data_type == 0 && data.effective == true && data.time_trigger == true && data.show_stickies == true))
                {
                    while (data.time_trigger_datetime.CompareTo(DateTime.Now) < 0)
                    {
                        // 時限起動の加算
                        data.TimeTriggerJudgment(DateTime.Now);
                    }
                }
                data.attach_trigger = result.GetBoolean((Int32)DBManager.Contents.enum_data.attach_trigger);
                if (data.attach_trigger == true)
                {
                    data.attach_trigger_title = result.GetString((Int32)DBManager.Contents.enum_data.attach_trigger_title);
                }
                data.display_backimage_stickies = result.GetBoolean((Int32)DBManager.Contents.enum_data.display_backimage_stickies);
                data.display_backimage_type_stickies = result.GetInt32((Int32)DBManager.Contents.enum_data.display_backimage_type_stickies);
                if (data.display_backimage_stickies == true)
                {
                    data.display_backimage_path_stickies = result.GetString((Int32)DBManager.Contents.enum_data.display_backimage_path_stickies);
                }
                data.display_backimage_region_stickies = result.GetBoolean((Int32)DBManager.Contents.enum_data.display_backimage_region_stickies);
                data.alpha_stickies = result.GetInt32((Int32)DBManager.Contents.enum_data.alpha_stickies);

                object objFontSerializeble = result.GetValue((Int32)DBManager.Contents.enum_data.display_font_stickies);
                if (objFontSerializeble != null)
                {
                    string fontSerializeble = objFontSerializeble.ToString();
                    if (!fontSerializeble.Trim().Equals(""))
                    {
                        FontConverter fc = new FontConverter();
                        data.display_font_stickies = (Font)fc.ConvertFromString(fontSerializeble);
                    }
                    else
                    {
                        data.display_font_stickies = this.Font;
                    }
                }
                else
                {
                    data.display_font_stickies = this.Font;
                }
                data.task_to_calendar = result.GetBoolean((Int32)DBManager.Contents.enum_data.task_to_calendar);
                data.task_trigger_type = result.GetInt32((Int32)DBManager.Contents.enum_data.task_trigger_type);
                data.task_trigger_datetime = result.GetDateTime((Int32)DBManager.Contents.enum_data.task_trigger_datetime);
                data.task_trigger_dayofweek = result.GetInt32((Int32)DBManager.Contents.enum_data.task_trigger_dayofweek);
                data.task_trigger_span = result.GetInt32((Int32)DBManager.Contents.enum_data.task_trigger_span);
                data.task_range = result.GetBoolean((Int32)DBManager.Contents.enum_data.task_range);
                data.task_range_startdate = result.GetDateTime((Int32)DBManager.Contents.enum_data.task_range_startdate);
                data.task_range_enddate = result.GetDateTime((Int32)DBManager.Contents.enum_data.task_range_enddate);
                data.task_type = result.GetInt32((Int32)DBManager.Contents.enum_data.task_type);
                data.temp_task_trigger_datetime = data.task_trigger_datetime;
                data.setNextSpan();
                // アラームかつ日付指定以外の場合のみ
                if (data.data_type == 4 && data.effective == true)
                {
                    while (data.task_trigger_datetime.CompareTo(DateTime.Now) < 0 && data.effective == true)
                    {
                        data.TaskTriggerJudgment(DateTime.Now);
                    }
                }


                // 初回表示用に表示可否として動作し、以降は非表示の起動対象付箋チェック用として使用する
                // 0:付箋で通常表示
                // 1:未完了のスケジュールデータで付箋送り
                if ((data.data_type == 3 && data.effective == true && data.time_trigger == false && data.attach_trigger == false)
                    || (data.data_type == 0 && data.effective == true && data.show_stickies == true))
                {
                    data.displayed = true;
                }
                else
                {
                    data.displayed = false;
                }

                // データ格納
                contentsList.Add(data);
            }
            // リーダクローズ
            sqliteAccess.readerClose();

            // 有効無効更新
            foreach (DBManager.Contents.data data in contentsList)
            {
                effectiveUpdate(data);
            }
        }

        /// <summary>
        /// カレンダ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showCalendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Visible = true;
            this.TopMost = true;
            this.TopMost = false;
        }

        /// <summary>
        /// カレンダ非表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hideCalendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        /// <summary>
        /// コンテキストメニューオープン時付箋一覧作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsMenu_Opening(object sender, CancelEventArgs e)
        {
            // 初期化
            listStickiesToolStripMenuItem.DropDownItems.Clear();

            lock (contentsList)
            {
                foreach (DBManager.Contents.data data in contentsList.SelectStickies())
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();
                    int contentsLength = -1;
                    string tempString = "";
                    if (data.contents.Contains("\r\n"))
                    {
                        contentsLength = data.contents.Replace("\r\n", "").Length;
                        tempString = data.contents.Replace("\r\n", "").Substring(0, contentsLength < 20 ? contentsLength : 20);
                    }
                    else
                    {
                        contentsLength = data.contents.Length;
                        tempString = data.contents.Substring(0, contentsLength < 20 ? contentsLength : 20);
                    }
                    if (contentsLength > 20)
                    {
                        tempString += "...";
                    }
                    else if (contentsLength == 0)
                    {
                        tempString += "新規付箋";
                    }
                    tsmi.Text = tempString;
                    tsmi.Checked = data.displayed;
                    tsmi.Tag = data.id;
                    tsmi.Click += showStickiesToolStripMenuItem_Click;
                    listStickiesToolStripMenuItem.DropDownItems.Add(tsmi);
                }
            }
        }

        /// <summary>
        /// 付箋表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showStickiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // イベントが起きたコントロールは sender 引数から取得する
            ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);

            // 表示済み付箋だったら
            if (tsmi.Checked == true)
            {
                lock (contentsList)
                {
                    // 対象付箋データ取得
                    DBManager.Contents.data data = contentsList.SelectId((int)tsmi.Tag);
                    if (data != null)
                    {
                            data.frm.Activate();
                    }
                }
            }
            // 未表示付箋だったら
            else
            {
                lock (contentsList)
                {
                    // 付箋リストから対象付箋データ検索
                    DBManager.Contents.data data = contentsList.SelectId((int)tsmi.Tag);
                    if (data != null)
                    {
                        // 新規付箋を表示
                        DBManager.Contents.data stickyData = data;
                        stickyData.displayed = true;
                        frmStickies frm = loadUndisplaySticky(ref stickyData);
                        frm.Show();
                    }
                }
            }
        }

        /// <summary>
        /// 定義済みの未表示付箋表示
        /// </summary>
        /// <param name="stickyData"></param>
        /// <returns></returns>
        private frmStickies loadUndisplaySticky(ref DBManager.Contents.data stickyData)
        {
            frmStickies frm = new frmStickies();
            frm.showSticky(ref stickyData);
            frm.Tag = stickyData.id;
            stickyData.frm = frm;
            return frm;
        }

        /// <summary>
        /// 定義済みのアラーム表示
        /// </summary>
        /// <param name="stickyData"></param>
        /// <returns></returns>
        private frmMessage loadUndisplayAlarm(ref DBManager.Contents.data stickyData, string temp)
        {
            frmMessage frm = new frmMessage();
            frm.showWithMessage(temp + stickyData.contents, "アラームメッセージ");
            frm.Tag = stickyData.id;
            return frm;
        }

        /// <summary>
        /// ウィンドウ貼り付け付箋のコントロール
        /// </summary>
        /// <param name="stickyData"></param>
        /// <returns></returns>
        private frmStickies snapWindowSticky(ref DBManager.Contents.data stickyData, Win32APIs.EnumWindow.WindowInfoStruct wInfo)
        {
            frmStickies frm = null;
            if (stickyData.frm == null)
            {
                frm = new frmStickies();
                frm.showSticky(ref stickyData);
                frm.Tag = stickyData.id;
                stickyData.frm = frm;
            }
            else
            {
                frm = stickyData.frm;
                stickyData.displayed = true;
            }

            // 付箋位置のコントロール
            frm.StartPosition = FormStartPosition.Manual;
            // 貼付先ウィンドウの元位置取得
            string[] pos_size = stickyData.point_snap_window.Split(',');
            int x = int.Parse(pos_size[0]);
            int y = int.Parse(pos_size[1]);
            // 貼付先ウィンドウの現在位置取得
            Point point = Win32APIs.WindowInfo.GetNormalWindowLocation(wInfo.wHnd);

            // 移動していたら付箋も移動
//            if (wInfo.x != x || wInfo.y != y)
            {
                int h, w;
                //ディスプレイの作業領域の高さ
                h = System.Windows.Forms.Screen.GetWorkingArea(frm).Height;
                //ディスプレイの作業領域の幅
                w = System.Windows.Forms.Screen.GetWorkingArea(frm).Width;

                Boolean bMoved = false;
                int top = 0;
                int left = 0;

                int moveX = point.X - x;
                int moveY = point.Y - y;

                top = frm.Top;
                left = frm.Left;

                if (point.X != x || point.Y != y)
                {
                    bMoved = true;
                }

                if (frm.Top + moveY < 0)
                {
                    top = 0;
                    bMoved = true;
                }
                if (frm.Left + moveX < 0)
                {
                    left = 0;
                    bMoved = true;
                }
                if (frm.Top + frm.Height > h)
                {
                    top = h - frm.Height;
                    bMoved = true;
                }
                if (frm.Left + frm.Width > w)
                {
                    left = w - frm.Width;
                    bMoved = true;
                }
                if (bMoved == true)
                {
                    top = frm.Top + moveY;
                    left = frm.Left + moveX;
                }

                if (wInfo.windowTitle.Equals(stickyData.snap_window_title)
                    && (wInfo.isChanged == true || bMoved == true || wInfo.isChanged == false && bMoved == false))
                {
                    frm.SetDesktopLocation(left, top);
//                    frm.Activate();
                    frm.saveForceMoved();
                    stickyData.point_snap_window = point.X + "," + point.Y;
                    Win32APIs.WindowInfo.SetWindowPos(frm.Handle, Win32APIs.WindowInfo.HWND_TOPMOST, 0, 0, 0, 0, Win32APIs.WindowInfo.TOPMOST_FLAGS);
                    System.Threading.Thread.Sleep(2);
                    Win32APIs.WindowInfo.SetWindowPos(frm.Handle, Win32APIs.WindowInfo.HWND_NOTOPMOST, 0, 0, 0, 0, Win32APIs.WindowInfo.TOPMOST_FLAGS);
                }
                else
                {
                    Win32APIs.WindowInfo.SetWindowPos(frm.Handle, Win32APIs.WindowInfo.HWND_NOTOPMOST, 0, 0, 0, 0, Win32APIs.WindowInfo.TOPMOST_FLAGS);
                }
            }

            return frm;
        }

        /// <summary>
        /// ウィンドウ一覧監視プロセスを別スレッドで実行
        /// バックグラウンドワークスレッド起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwWindowList_DoWork(object sender, DoWorkEventArgs e)
        {
            // このメソッドへのパラメータ
            int bgWorkerArg = (int)e.Argument;

            // senderの値はbgWorkerの値と同じ
            BackgroundWorker worker = (BackgroundWorker)sender;

            while (true)
            {
                // キャンセルされてないか定期的にチェック
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                System.Threading.Thread.Sleep(10);
#if false
                lock (Win32APIs.EnumWindow.windowList)
                {
                    // コールバックで一覧作成
                    SwS.Win32APIs.EnumWindow.listClear();
                    Win32APIs.EnumWindow.EnumWindows(
                        new Win32APIs.EnumWindow.EnumerateWindowsCallback(
                            Win32APIs.EnumWindow.EnumerateWindowsIsTop), 0);
                }
#endif
                System.Threading.Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 時間監視及び連想監視及びウィンドウ貼付監視プロセスを別スレッドで実行
        /// バックグラウンドワークスレッド起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwTimeTick_DoWork(object sender, DoWorkEventArgs e)
        {
            // このメソッドへのパラメータ
            int bgWorkerArg = (int)e.Argument;

            // senderの値はbgWorkerの値と同じ
            BackgroundWorker worker = (BackgroundWorker)sender;

            while (true)
            {
                // キャンセルされてないか定期的にチェック
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                // 現在日時取得
                DateTime nowDate = DateTime.Now;

                // 時限起動関連チェック
                // 時刻同期あり
                if (bIntervalAdjust == true)
                {
                    // 現在日時が設定日時より大きくなったら
                    if (nowDate > dtAdjustTime)
                    {
                        // 時刻同期処理
                        syncTime();

                        // 次回時刻同期時間の生成
                        createNextSyncTime();
                    }
                }

                System.Threading.Thread.Sleep(500);

                lock (Win32APIs.EnumWindow.windowList)
                {
                    // コールバックで一覧作成
                    SwS.Win32APIs.EnumWindow.listClear();
                    Win32APIs.EnumWindow.GetForgroundWindowInfo();
#if false
                    Win32APIs.EnumWindow.EnumWindows(
                        new Win32APIs.EnumWindow.EnumerateWindowsCallback(
                            Win32APIs.EnumWindow.EnumerateWindowsIsTop), 0);
#endif
                }

                System.Threading.Thread.Sleep(2);

                // 連想起動ウィンドウタイトル列挙
                ArrayList alWindowTitle = new ArrayList();
                // 最前面のウィンドウを取得
                Win32APIs.EnumWindow.WindowInfoStruct wInfoStruct = new Win32APIs.EnumWindow.WindowInfoStruct();

                lock (Win32APIs.EnumWindow.windowList)
                {

                    System.Threading.Thread.Sleep(10);
                    alWindowTitle.Clear();

                    // ウィンドウタイトルリスト作成
                    foreach (Win32APIs.EnumWindow.WindowInfoStruct wInfo in Win32APIs.EnumWindow.windowList)
                    {
                        // キャンセルされてないか定期的にチェック
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        System.Threading.Thread.Sleep(2);

                        // 初回だけ最前面ウィンドウの情報を取得
                        if (wInfoStruct.wHnd == IntPtr.Zero)
                        {
                            wInfoStruct.windowTitle = wInfo.windowTitle;
                            wInfoStruct.wHnd = wInfo.wHnd;
                            wInfoStruct.isChanged = wInfo.isChanged;
                        }

                        alWindowTitle.Add(wInfo.windowTitle);
                    }
                }

                // 連想起動、時限起動対象チェック
                lock (contentsList)
                {
                    // ウィンドウ貼付対象の付箋をチェック
                    foreach (DBManager.Contents.data data in contentsList.SelectSnapWindow())
                    {
                        // キャンセルされてないか定期的にチェック
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        System.Threading.Thread.Sleep(2);
                        DBManager.Contents.data stickyData = data;
                        // 付箋の貼付対象ウィンドウが最前面に出ていたら
                        if (stickyData.snap_window == true
                            && wInfoStruct.windowTitle != null && wInfoStruct.windowTitle.Equals(stickyData.snap_window_title))
                        {
                            // 付箋を起動
                            wInfoStruct.stickyId = stickyData.id;
                            worker.ReportProgress(0, wInfoStruct);
                        }
                    }
                    // 非表示の付箋で連想起動、時限起動対象の付箋をチェック
                    foreach (DBManager.Contents.data data in contentsList.SelectUndisplayed().SelectStickies())
                    {
                        // キャンセルされてないか定期的にチェック
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        System.Threading.Thread.Sleep(2);
                        DBManager.Contents.data stickyData = data;
                        // 付箋起動チェック
                        // 0:時限起動,連想起動
                        // 2:スケジュールデータかつ未完了かつ付箋送りかつ非表示
                        if ((stickyData.time_trigger == true && stickyData.TimeTriggerJudgment(nowDate) == true)
                            || (stickyData.attach_trigger == true
                                && alWindowTitle.Contains(stickyData.attach_trigger_title) == true)
                            || (stickyData.data_type == 0 && stickyData.effective == true
                                && stickyData.show_stickies == true && stickyData.displayed == false))
                        {
                            stickyData.displayed = true;
                            // 付箋を起動
                            worker.ReportProgress(1, stickyData.id);
                        }
                    }
                    // アラームをチェック
                    foreach (DBManager.Contents.data data in contentsList.SelectDataType(4))
                    {
                        // キャンセルされてないか定期的にチェック
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        System.Threading.Thread.Sleep(2);
                        DBManager.Contents.data stickyData = data;
                        // アラーム起動チェック
                        if (stickyData.TaskTriggerJudgment(nowDate) == true && stickyData.DateJudgeByTaskTriggerForCalendar(nowDate) == true)
                        {
                            // アラームを起動
                            worker.ReportProgress(2, stickyData.id);
                        }
                    }

                    // 終了前にリストクリア(最前面解除後対応のため)
                    SwS.Win32APIs.EnumWindow.listClear();

                }

                System.Threading.Thread.Sleep(10);
            }
        }

        /// <summary>
        /// バックグラウンドワークスレッドからの付箋/アラーム起動イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwTimeTick_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // 対象付箋/アラームを検索し表示
            // コンテンツリストから対象付箋/アラームデータ検索
            lock (contentsList)
            {
                // ウィンドウ貼付付箋を対象
                if ((int)e.ProgressPercentage == 0)
                {
                    // ウィンドウ貼付付箋を対象
                    DBManager.Contents.data contents = contentsList.SelectId((int)(((Win32APIs.EnumWindow.WindowInfoStruct)e.UserState).stickyId));
                    if (contents != null)
                    {
                        // 付箋を表示
                        frmStickies frm = snapWindowSticky(ref contents, (Win32APIs.EnumWindow.WindowInfoStruct)e.UserState);
                    }
                }
                else
                {
                    // 付箋/アラームを対象
                    DBManager.Contents.data contents = contentsList.SelectId((int)e.UserState);
                    if (contents != null)
                    {
                        // 付箋を対象
                        if ((int)e.ProgressPercentage == 1)
                        {
                            // 新規付箋を表示
                            frmStickies frm = loadUndisplaySticky(ref contents);
                            frm.Show();
                        }
                        // アラームを対象
                        else if ((int)e.ProgressPercentage == 2)
                        {
                            if (contents.task_type == 0)
                            {
                                // アラームを表示(メッセージ)
                                frmMessage frm = loadUndisplayAlarm(ref contents, "");
                            }
                            else
                            {
                                // 存在するファイルなら実行
                                if (File.Exists(contents.contents) == true)
                                {
                                    // アラームを起動(コマンド)
                                    Process.Start(contents.contents);
                                }
                                else
                                {
                                    string temp = "以下のコマンドは対象ファイルが存在しませんでした。\n\n　>>";
                                    // アラームを表示(メッセージ)
                                    frmMessage frm = loadUndisplayAlarm(ref contents, temp);
                                }
                            }

                            contents.displayed = false;
                            effectiveUpdate(contents);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// デスクトップフックを別スレッドで実行
        /// バックグラウンドワークスレッド起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwDesktopHook_DoWork(object sender, DoWorkEventArgs e)
        {
            // senderの値はbgWorkerの値と同じ
            BackgroundWorker worker = (BackgroundWorker)sender;

            while (true)
            {
                // キャンセルされてないか定期的にチェック
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // デスクトップ操作許可設定ありなら
                if (DBManager.Setting.main_desktop_access == true)
                {
                    // デスクトップツリービュー取得
                    DesktopHook.GetFindDesktopListViewWnd.FindDesktopListViewWnd();
                    IntPtr hDesktopListView = DesktopHook.GetFindDesktopListViewWnd.getDesktopListViewAddress();
                    DesktopHook.SharedMemory.GetSharedMemory(hDesktopListView);
                    // アイコン一覧取得
                    DesktopHook.ListViewAccessor.ReadIconList();
                }

                System.Threading.Thread.Sleep(1000);
            }
        }
        
        /// <summary>
        /// メールチェックを別スレッドで実行
        /// バックグラウンドワークスレッド起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMailChecker_DoWork(object sender, DoWorkEventArgs e)
        {
            // senderの値はbgWorkerの値と同じ
            BackgroundWorker worker = (BackgroundWorker)sender;
            bool bFirst = true;
            bool bMailCheckerExecuted = false;

            while (true)
            {
                if (bFirst == true)
                {
                    System.Threading.Thread.Sleep(1000);

                    // キャンセルされてないか定期的にチェック
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    // メールチェッカチェック
                    if (mailCheckerList.Count != 0)
                    {
                        lock (mailCheckerList)
                        {
                            foreach (DBManager.MailChecker.data mailChecker in mailCheckerList.SelectEffective())
                            {
                                System.Threading.Thread.Sleep(1);

                                // キャンセルされてないか定期的にチェック
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }

                                // 次回チェック時間が現在時刻より小さくなったらメールチェック実行
                                if (mailChecker.nextCheckTime <= DateTime.Now)
                                {
                                    // メールチェック実行
                                    popMailChecker(mailChecker, worker, e);
                                    bMailCheckerExecuted = true;
                                }
                            }
                            if (bMailCheckerExecuted == true)
                            {
                                bMailCheckerExecuted = false;
                                // 新着通知用アイコン点滅を初期化する設定なら
                                if (DBManager.Setting.main_mailchecker_blinkcancel == true)
                                {
                                    // アニメーションキャンセル
                                    bMailCheckerAnimation = false;
                                }
                                worker.ReportProgress(0, true);
                            }
                        }
                    }
                }
                bFirst = true;
            }
        }

        /// <summary>
        /// バックグラウンドワークスレッドからのタスクトレイアイコンポップアップイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMailChecker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // メールチェック
            if (mailCheckerList.Count != 0)
            {
                string tips = "";

                lock (mailCheckerList)
                {
                    foreach (DBManager.MailChecker.data mailChecker in mailCheckerList.SelectEffective())
                    {
                        if (tips.Equals("") == false)
                        {
                            tips = tips + "\n";
                        }
                        // 「新着はありません。」はポップアップ対象外とする
                        if ("新着はありません。".Equals(mailChecker.mail_message) == false)
                        {
                            tips = tips + mailChecker.mail_connect_name + " : " + mailChecker.mail_message;
                        }
                    }
                }

                DBManager.Setting.mailCheckerExecuteTime = DateTime.Now;
                niSwS.BalloonTipTitle = "新着メール(" + DBManager.Setting.mailCheckerExecuteTime.ToString() + ")";
                
                // 表示対象ありならポップアップ
                if (tips.Equals("") == false)
                {
                    niSwS.BalloonTipText = tips;
                    niSwS.ShowBalloonTip(60000);
                    bMailCheckerAnimation = true;
                }
            }
        }

        /// <summary>
        /// POP に接続しメール件数を取得
        /// </summary>
        private void popMailChecker(DBManager.MailChecker.data mailChecker, BackgroundWorker worker, DoWorkEventArgs e)
        {
            // 表示メッセージクリア
            mailChecker.mail_message = "";
            // POP サーバに接続します。
            PopClient pop = new PopClient(mailChecker.mail_pop3, 110);

            try
            {
                // ログインします。
                pop.Login(mailChecker.mail_user, mailChecker.mail_pass);

                // メールサーバに溜まっているメールのUIDLリストを取得します。
                // ArrayList list = pop.GetList();
                ArrayList list = pop.GetUidlList(worker, e);

                // キャンセルされてないか定期的にチェック
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Int32 newCount = 0;
                string uidl = "";

                // 逆読みとする
                list.Reverse();

                lock (list)
                {
                    foreach (string localUidl in list)
                    {
                        System.Threading.Thread.Sleep(1);

                        // キャンセルされてないか定期的にチェック
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        // 前回のUIDL値を持っていれば、新着読み
                        if (mailChecker.mail_uidl.Equals("") == false)
                        {
                            if (localUidl.Equals(mailChecker.mail_uidl))
                            {
                                // 前回の最新UIDLを見つけたらbreak
                                break;
                            }

                            // 前回以降の新着をカウント
                            newCount++;
                        }
                        else
                        {
                            // 全件数をカウント
                            newCount++;
                        }
                        if (uidl.Equals(""))
                        {
                            uidl = localUidl;
                        }

#if false
                    // 前回のUIDL値を持っていれば、新着読み
                    if (mailChecker.mail_uidl.Equals("") == false)
                    {
                        if (list[i].Equals(mailChecker.mail_uidl))
                        {
                            // 前回の最新UIDLを見つけたら、カウント開始
                            bExist = true;
                        }

                        // 前回の最新
                        if (bExist == true)
                        {
                            // 前回以降の新着をカウント
                            newCount++;
                        }
                    }
                    else
                    {
                        // 全件数をカウント
                        newCount++;
                    }
                    uidl = localUidl;
#endif

                        // メール本体を取得します。
                        // string mail = pop.GetMail((string)list[i]);

                        // 確認用に取得したメールをそのままカレントディレクトリに書き出します。
                        // using (StreamWriter sw = new StreamWriter(DateTime.Now.ToString("yyyyMMddHHmmssfff") + i.ToString("0000") + ".txt"))
                        // {
                        //     sw.Write(mail);
                        // }

                        // メールを POP サーバから取得します。
                        // ★注意★
                        // 削除したメールを元に戻すことはできません。
                        // 本当に削除していい場合は以下のコメントをはずしてください。
                        //pop.DeleteMail((string)list[i]);
                    }
                }
                if (uidl.Equals("") == false)
                {
                    mailChecker.mail_uidl = uidl;
                }

                // 表示するツールチップ生成
                if (newCount > 0)
                {
                    mailChecker.mail_message = newCount.ToString() + " 件の新着があります。";
                }
                else
                {
                    mailChecker.mail_message = "新着はありません。";
                }
            }
            catch (MailPopClientException me)
            {
                // 表示するツールチップ生成
                mailChecker.mail_message = mailChecker.mail_connect_name + " : " + me.Message;
            }
            finally
            {
                // 次回確認時間が今回時間を越えるまで加算(スタンバイ対処)
                while (mailChecker.nextCheckTime.CompareTo(DateTime.Now) <= 0)
                {
                    mailChecker.nextCheckTime = mailChecker.nextCheckTime.AddMinutes(mailChecker.mail_check_span);
                }
            }

            try
            {
                // 切断します。
                pop.Close();
            }
            catch (MailPopClientException me)
            {
                if (mailChecker.mail_message.Equals(""))
                {
                    // 表示するツールチップ生成
                    mailChecker.mail_message = mailChecker.mail_connect_name + " : " + me.Message;
                }
            }
        }

        /// <summary>
        /// 設定画面起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* 
            // 休日マスタ修正時はこちらをコメントアウト解除
            frmMstHoliday frm = new frmMstHoliday();
            frm.Show();
            */

            if (frmCommonSettings.Visible == false)
            {
                frmCommonSettings.showDialog(this, frmLauncher);
            }
            else
            {
                frmCommonSettings.Activate();
            }
        }

        /// <summary>
        /// カレンダ日付部クリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // ヒットしたポイントを７ｘ６の升目の０オリジンインデックスに変換(０～４１)
            int row = e.Y / MAX_UC_DAY_WIDTH;
            int col = e.X / MAX_UC_DAY_HEIGHT;
            int index = row * MAX_UC_DAY_COL + col;
        }

        /// <summary>
        /// バージョン情報、その他表記
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmAbout.Visible == false)
            {
                frmAbout.showDialog();
            }
            else
            {
                frmAbout.Activate();
            }
        }

        /// <summary>
        /// カレンダ画面再表示
        /// </summary>
        public void setCalendarDisplay()
        {
            this.SuspendLayout();

            this.Opacity = ((double)DBManager.Setting.main_alpha / 100);
            if (DBManager.Setting.main_no_timedisplay == true)
            {
                this.Size = new Size(formSize.Width, formSize.Height - lblTime.Height);
                ulsbLeftButton.Size = new Size(ulsbLeftButtonSize.Width, ulsbLeftButtonSize.Height - lblTime.Height);
                ulsbRightButton.Size = new Size(ulsbRightButtonSize.Width, ulsbRightButtonSize.Height - lblTime.Height);
                label1.Location = new Point(label1Location.X, label1Location.Y - lblTime.Height);
                pictureBox2.Location = new Point(pictureBox2Location.X, pictureBox2Location.Y - lblTime.Height);
                pictureBox1.Location = new Point(pictureBox1Location.X, pictureBox1Location.Y - lblTime.Height);
            }
            else
            {
                this.Size = new Size(formSize.Width, formSize.Height);
                ulsbLeftButton.Size = new Size(ulsbLeftButtonSize.Width, ulsbLeftButtonSize.Height);
                ulsbRightButton.Size = new Size(ulsbRightButtonSize.Width, ulsbRightButtonSize.Height);
                label1.Location = new Point(label1Location.X, label1Location.Y);
                pictureBox2.Location = new Point(pictureBox2Location.X, pictureBox2Location.Y);
                pictureBox1.Location = new Point(pictureBox1Location.X, pictureBox1Location.Y);
            }
            this.ResumeLayout();
        }

        /// <summary>
        /// ToDo一括付箋化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void todoBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = "ToDo";

            batchToSticky(title, 1);
        }

        /// <summary>
        /// 一括付箋化
        /// </summary>
        /// <param name="title"></param>
        /// <param name="selectId"></param>
        private void batchToSticky(string title, int selectId)
        {
            lock (contentsList)
            {
                frmStickies frm = new frmStickies();
                DBManager.Contents.data stickyDatas = new DBManager.Contents.data();
                stickyDatas.contents = "「" + title + " 一括付箋化一覧」\r\n";
                stickyDatas.id = -1;
                stickyDatas.effective = true;
                stickyDatas.data_type = 3;
                stickyDatas.backcolor_stickies = frm.BackColor.R.ToString() + ", " + frm.BackColor.G.ToString() + ", " + frm.BackColor.B.ToString();
                stickyDatas.point_stickies = frm.Location.X.ToString() + ", " + frm.Location.Y.ToString() + ", " + frm.Size.Width.ToString() + ", " + frm.Size.Height.ToString();
                stickyDatas.time_trigger = false;
                stickyDatas.time_trigger_datetime = DateTime.Now;
                stickyDatas.time_trigger_dayofweek = (int)dayOfWeekToNumber[DateTime.Now.DayOfWeek];
                stickyDatas.attach_trigger = false;
                stickyDatas.attach_trigger_title = "";
                stickyDatas.alpha_stickies = 100;
                stickyDatas.display_font_stickies = frm.Font;
                stickyDatas.displayed = true;

                // 有効なToDo 収集
                foreach (DBManager.Contents.data data in contentsList.SelectDataType(selectId))
                {
                    DBManager.Contents.data stickyData = data;
                    stickyDatas.contents = stickyDatas.contents + "\r\n---\r\n" + stickyData.contents;
                }
                contentsList.Add(stickyDatas);
                frm.showSticky(ref stickyDatas);
                stickyDatas.frm = frm;
                frm.Show();
                frm.Activate();
            }
        }

        /// <summary>
        /// メモ一括付箋化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void memoBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = "メモ";

            batchToSticky(title, 2);
        }

        /// <summary>
        /// クエリ検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuery.query(this);
        }

        /// <summary>
        /// SQL接続(finished)
        /// </summary>
        private void effectiveUpdate(DBManager.Contents.data contents)
        {
            string strSQL = "update contents_list set "
                + "effective = " + ((contents.effective == true) ? 1 : 0)
                + ", task_trigger_datetime = datetime('" + contents.task_trigger_datetime.ToString("yyyy-MM-dd HH:mm:ss") + "')"
                + " where id = '" + contents.id + "' ";

            sqliteAccess.update(strSQL);
        }

        /// <summary>
        /// 最前面処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void topMostToolStripCalendarMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = topMostToolStripCalendarMenuItem.Checked == true ? false : true;
        }

        /// <summary>
        /// コンテキストメニューオープン時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsCalendarMenu_Opening(object sender, CancelEventArgs e)
        {
            // 最前面
            topMostToolStripCalendarMenuItem.Checked = this.TopMost;
        }

        /// <summary>
        /// ランチャ設定画面起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void launcherSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmLauncherSetting.Visible == false)
            {
                frmLauncherSetting.showDialog();
            }
            else
            {
                frmLauncherSetting.Activate();
            }
        }

        /// <summary>
        /// ランチャメニュー起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void launcherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLauncher.showLauncherMenu();
        }

        /// <summary>
        /// ホットキー登録
        /// </summary>
        public void RegisterHotKey()
        {
            // コマンドメニュー
            if (!DBManager.Setting.main_hotkey_001.Equals(""))
            {
                Win32APIs.HotKey.RegisterHotKey(this.Handle, Win32APIs.HotKey.HOTKEY_ID_0001, Win32APIs.HotKey.GetControlKey(DBManager.Setting.main_hotkey_001), Win32APIs.HotKey.GetNormalKey(DBManager.Setting.main_hotkey_001));
            }
            // タスク登録
            if (!DBManager.Setting.main_hotkey_002.Equals(""))
            {
                Win32APIs.HotKey.RegisterHotKey(this.Handle, Win32APIs.HotKey.HOTKEY_ID_0002, Win32APIs.HotKey.GetControlKey(DBManager.Setting.main_hotkey_002), Win32APIs.HotKey.GetNormalKey(DBManager.Setting.main_hotkey_002));
            }
            // 新規付箋
            if (!DBManager.Setting.main_hotkey_003.Equals(""))
            {
                Win32APIs.HotKey.RegisterHotKey(this.Handle, Win32APIs.HotKey.HOTKEY_ID_0003, Win32APIs.HotKey.GetControlKey(DBManager.Setting.main_hotkey_003), Win32APIs.HotKey.GetNormalKey(DBManager.Setting.main_hotkey_003));
            }
            // 新規タスク
            if (!DBManager.Setting.main_hotkey_004.Equals(""))
            {
                Win32APIs.HotKey.RegisterHotKey(this.Handle, Win32APIs.HotKey.HOTKEY_ID_0004, Win32APIs.HotKey.GetControlKey(DBManager.Setting.main_hotkey_004), Win32APIs.HotKey.GetNormalKey(DBManager.Setting.main_hotkey_004));
            }
            // コマンドラインランチャ
            if (!DBManager.Setting.main_hotkey_005.Equals(""))
            {
                Win32APIs.HotKey.RegisterHotKey(this.Handle, Win32APIs.HotKey.HOTKEY_ID_0005, Win32APIs.HotKey.GetControlKey(DBManager.Setting.main_hotkey_005), Win32APIs.HotKey.GetNormalKey(DBManager.Setting.main_hotkey_005));
            }
        }

        /// <summary>
        /// ホットキー解除
        /// </summary>
        public void UnregisterHotKey()
        {
            // コマンドメニュー
            Win32APIs.HotKey.UnregisterHotKey(this.Handle, Win32APIs.HotKey.HOTKEY_ID_0001);
            // タスク登録
            Win32APIs.HotKey.UnregisterHotKey(this.Handle, Win32APIs.HotKey.HOTKEY_ID_0002);
            // 新規付箋
            Win32APIs.HotKey.UnregisterHotKey(this.Handle, Win32APIs.HotKey.HOTKEY_ID_0003);
            // 新規タスク
            Win32APIs.HotKey.UnregisterHotKey(this.Handle, Win32APIs.HotKey.HOTKEY_ID_0004);
            // コマンドラインランチャ
            Win32APIs.HotKey.UnregisterHotKey(this.Handle, Win32APIs.HotKey.HOTKEY_ID_0005);
        }

        /// <summary>
        /// 全付箋を画面中央へ。使用目的：デスクトップ外に出ていった付箋の救済
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int h, w;
            //ディスプレイの作業領域の高さ
            h = System.Windows.Forms.Screen.GetWorkingArea(this).Height;
            //ディスプレイの作業領域の幅
            w = System.Windows.Forms.Screen.GetWorkingArea(this).Width;

            Boolean bMoved = false;

            // 付箋のみを対象とする
            DBManager.Contents.ContentsList stickyList = contentsList.SelectDataType(3).SelectDateRange(DateTime.Today, DateTime.Today);
            foreach (DBManager.Contents.data sticky in stickyList)
            {
                bMoved = false;

                if (sticky.frm.Top < 0)
                {
                    bMoved = true;
                }
                if (sticky.frm.Left < 0)
                {
                    bMoved = true;
                }
                if (sticky.frm.Top + sticky.frm.Height > h)
                {
                    bMoved = true;
                }
                if (sticky.frm.Left + sticky.frm.Width > w)
                {
                    bMoved = true;
                }

                if (bMoved == true)
                {
                    sticky.frm.SetDesktopLocation(w / 2 - sticky.frm.Width / 2, h / 2 - sticky.frm.Height / 2);
                    sticky.frm.Activate();
                    sticky.frm.saveForceMoved();
                }
            }
            bMoved = false;

            if (this.Top < 0)
            {
                bMoved = true;
            }
            if (this.Left < 0)
            {
                bMoved = true;
            }
            if (this.Top + this.Height > h)
            {
                bMoved = true;
            }
            if (this.Left + this.Width > w)
            {
                bMoved = true;
            }

            if (bMoved == true)
            {
                this.SetDesktopLocation(w / 2 - this.Width / 2, h / 2 - this.Height / 2);
                this.Activate();
                DBManager.Setting.main_startUp_position_x = this.Location.X;
                DBManager.Setting.main_startUp_position_y = this.Location.Y;
                DBManager.Setting.save(sqliteAccess);
            }
        }

        /// <summary>
        /// 全付箋をデスクトップサイズ内へ。使用目的：デスクトップ外に出ていった付箋の救済
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void innerDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int h, w;
            //ディスプレイの作業領域の高さ
            h = System.Windows.Forms.Screen.GetWorkingArea(this).Height;
            //ディスプレイの作業領域の幅
            w = System.Windows.Forms.Screen.GetWorkingArea(this).Width;

            Boolean bMoved = false;
            int top = 0;
            int left = 0;

            // 付箋のみを対象とする
            DBManager.Contents.ContentsList stickyList = contentsList.SelectDataType(3).SelectDateRange(DateTime.Today, DateTime.Today);
            foreach (DBManager.Contents.data sticky in stickyList)
            {
                bMoved = false;
                top = sticky.frm.Top;
                left = sticky.frm.Left;

                if (sticky.frm.Top < 0)
                {
                    top = 0;
                    bMoved = true;
                }
                if (sticky.frm.Left < 0)
                {
                    left = 0;
                    bMoved = true;
                }
                if (sticky.frm.Top + sticky.frm.Height > h)
                {
                    top = h - sticky.frm.Height;
                    bMoved = true;
                }
                if (sticky.frm.Left + sticky.frm.Width > w)
                {
                    left = w - sticky.frm.Width;
                    bMoved = true;
                }

                if (bMoved == true)
                {
                    sticky.frm.SetDesktopLocation(left, top);
                    sticky.frm.Activate();
                    sticky.frm.saveForceMoved();
                }
            }
            bMoved = false;
            top = this.Top;
            left = this.Left;

            if (this.Top < 0)
            {
                top = 0;
                bMoved = true;
            }
            if (this.Left < 0)
            {
                left = 0;
                bMoved = true;
            }
            if (this.Top + this.Height > h)
            {
                top = h - this.Height;
                bMoved = true;
            }
            if (this.Left + this.Width > w)
            {
                left = w - this.Width;
                bMoved = true;
            }

            if (bMoved == true)
            {
                this.SetDesktopLocation(left, top);
                this.Activate();
                DBManager.Setting.main_startUp_position_x = this.Location.X;
                DBManager.Setting.main_startUp_position_y = this.Location.Y;
                DBManager.Setting.save(sqliteAccess);
            }
        }

        /// <summary>
        /// タスク登録起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* タスク登録起動 */
            frmInput.inputData(DateTime.Today, ref contentsList, this);
        }

        /// <summary>
        /// コマンドラインランチャ起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandlineLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
