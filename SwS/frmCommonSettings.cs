using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections;

namespace SwS
{
    public partial class frmCommonSettings : Form
    {
        private SQLiteAccess sqliteAccess = new SQLiteAccess();
        private ArrayList masterList = new ArrayList();
        private Hashtable dayOfWeekToNumber = new Hashtable();
        private bool bNewEntry = false;
        private bool bNewEntry2 = false;
        private ArrayList holidayList = new ArrayList();
        private DBManager.MailChecker.MailCheckerList mailCheckerList = new DBManager.MailChecker.MailCheckerList();
        private DBManager.MailChecker.data mailChecker = new DBManager.MailChecker.data();
        private frmCalendar parentForm;
        private frmLauncher launcherForm;

        private int selectIndex = -1;

        private struct holidayMaster
        {
            public Int32 id;
            public Int32 holiday_type;	//休日種別。0:fix(固定。fix,名称,月,日), 1:week(xxxに指定された週とyyyに指定された曜日を有効年と有効判定子により判定。week,名称,月,週,曜日,有効年,有効判定子(<,>,<=,>=,==,><)), 2:equinox(xxxは月を表す。現在は春分と秋分の日が対象。equinox,名称,月)
            public string holiday_name;	//休日名
            public Int32 third;		    //月
            public Int32 fourth;		//fix:日, week:週
            public Int32 fifth;		    //week:曜日
            public Int32 sixth;		    //week:有効年
            public Int32 seventh;		//week:有効判定子。0:<, 1:>,2:<=,3:>=,4:==,5:><
        }

        // 休日データ
        private struct holidays
        {
            public DateTime date;
            public int type;
            public string name;
        }

        public frmCommonSettings()
        {
            InitializeComponent();
        }

        public bool showDialog(frmCalendar frm, frmLauncher frmLauncher)
        {
            parentForm = frm;
            launcherForm = frmLauncher;

            dayOfWeekToNumber.Clear();

            dayOfWeekToNumber.Add(DayOfWeek.Sunday, 0);
            dayOfWeekToNumber.Add(DayOfWeek.Monday, 1);
            dayOfWeekToNumber.Add(DayOfWeek.Tuesday, 2);
            dayOfWeekToNumber.Add(DayOfWeek.Wednesday, 3);
            dayOfWeekToNumber.Add(DayOfWeek.Thursday, 4);
            dayOfWeekToNumber.Add(DayOfWeek.Friday, 5);
            dayOfWeekToNumber.Add(DayOfWeek.Saturday, 6);

            ttTips.SetToolTip(label2, "タイムサーバはプロバイダ、あるいはネットワーク内のものを設定してください");
            ttTips.SetToolTip(btnNew, "新規作成");
            ttTips.SetToolTip(btnCancel, "キャンセル");
            ttTips.SetToolTip(btnEntry, "登録・更新");
            ttTips.SetToolTip(btnDelete, "削除");
            ttTips.SetToolTip(btnNew2, "新規作成");
            ttTips.SetToolTip(btnCancel2, "キャンセル");
            ttTips.SetToolTip(btnEntry2, "登録・更新");
            ttTips.SetToolTip(btnDelete2, "削除");

            // 休日設定ウィンドウ幅
            columnHeader2.Width = listView1.Width - columnHeader1.Width - 20;
            resetInputFields();

            // メールチェッカ設定ウィンドウ幅
            columnHeader3.Width = listView2.Width - columnHeader4.Width - 20;
            resetInputFields2();

            checkBox1.Checked = DBManager.Setting.main_time_adjust_startup;
            checkBox2.Checked = DBManager.Setting.main_time_adjust_interval;
            comboBox1.Text = DBManager.Setting.main_time_adjust_ntp_server;
            numericUpDown1.Value = DBManager.Setting.main_time_adjust_interval_count;
            switch (DBManager.Setting.main_time_adjust_interval_type)
            {
                case 0:     // 月毎
                    radioButton1.Checked = true;
                    break;
                case 1:     // 日毎
                    radioButton2.Checked = true;
                    break;
                case 2:     // 時間毎
                    radioButton3.Checked = true;
                    break;
            }

            // SQL発行前の環境設定
            sqliteAccess.setEnviroment(DBManager.dbPath, "SwS.db");

            // 年間祝祭日設定ロード
            dtpHolidayYear.Value = DateTime.Today;
            loadYearyHoliday();

            // メールチェッカ設定ロード
            loadMailChecker();
            cbMailcheckerBlinkCancel.Checked = DBManager.Setting.main_mailchecker_blinkcancel;

            tbAlpha.Value = DBManager.Setting.main_alpha;
            lblAlpha.Text = DBManager.Setting.main_alpha.ToString() + "%";

            cbNoTimeDisplay.Checked = DBManager.Setting.main_no_timedisplay;
            cbDesktopAccess.Checked = DBManager.Setting.main_desktop_access;

            cbDesktopSnapping.Checked = DBManager.Setting.main_desktop_snapping;
            if (DBManager.Setting.main_desktop_snapping == true)
            {
                numericUpDown3.Enabled = true;
            }
            else
            {
                numericUpDown3.Enabled = false;
            }
            numericUpDown3.Value = DBManager.Setting.main_desktop_snapping_band;

            string[] rgb = DBManager.Setting.main_default_backcolor_stickies.Split(',');
            lblColor.BackColor = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));

            string tempKey = DBManager.Setting.main_hotkey_000;
            txtLauncher.Text = Win32APIs.HotKey.BuildHotKey(tempKey);
            txtLauncher.Tag = DBManager.Setting.main_hotkey_000;

            tempKey = DBManager.Setting.main_hotkey_001;
            txtCommand.Text = Win32APIs.HotKey.BuildHotKey(tempKey);
            txtCommand.Tag = DBManager.Setting.main_hotkey_001;

            tempKey = DBManager.Setting.main_hotkey_002;
            txtTask.Text = Win32APIs.HotKey.BuildHotKey(tempKey);
            txtTask.Tag = DBManager.Setting.main_hotkey_002;

            tempKey = DBManager.Setting.main_hotkey_003;
            txtNewStickie.Text = Win32APIs.HotKey.BuildHotKey(tempKey);
            txtNewStickie.Tag = DBManager.Setting.main_hotkey_003;

            tempKey = DBManager.Setting.main_hotkey_004;
            txtNewTask.Text = Win32APIs.HotKey.BuildHotKey(tempKey);
            txtNewTask.Tag = DBManager.Setting.main_hotkey_004;

            tempKey = DBManager.Setting.main_hotkey_005;
            txtCommandlineLauncher.Text = Win32APIs.HotKey.BuildHotKey(tempKey);
            txtCommandlineLauncher.Tag = DBManager.Setting.main_hotkey_005;

            this.ShowDialog();

            return true;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string year = dtpHolidayYear.Value.ToString("yyyy");
            int intYear = int.Parse(year);
            ArrayList holidayList = new ArrayList();
            ArrayList holidayList2 = new ArrayList();
            ArrayList holidayList3 = new ArrayList();

            // 警告メッセージ
            if (MessageBox.Show(this, "この処理を実行すると一旦当該年度の祝祭日を消去し作り直します。\r\n手動設定の情報は削除されます。実行してよろしいですか？", "警告", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            string strSQL = "select count(*) from holiday_list where strftime('%Y', holiday_date) = '" + year + "' ";

            // 祝祭日DBに該当年のデータがあるかどうか確認
            SQLiteDataReader result = sqliteAccess.select(strSQL);
            if (result != null && result.Read())
            {
                if (result.GetInt32(0) > 0)
                {
                    sqliteAccess.readerClose();
                    strSQL = "delete from holiday_list where strftime('%Y', holiday_date) = '" + year + "' ";
                    sqliteAccess.update(strSQL);
                }
            }
            sqliteAccess.readerClose();

            // 祝祭日マスタ読込
            strSQL = "select * from holiday_master order by id";
            result = sqliteAccess.select(strSQL);
            if (result != null)
            {
                masterList.Clear();
                while (result.Read())
                {
                    holidayMaster hm = new holidayMaster();

                    hm.id = result.GetInt32(0);
                    hm.holiday_type = result.GetInt32(1);
                    hm.holiday_name = result.GetString(2);
                    hm.third = result.GetInt32(3);
                    hm.fourth = result.GetInt32(4);
                    hm.fifth = result.GetInt32(5);
                    hm.sixth = result.GetInt32(6);
                    hm.seventh = result.GetInt32(7);

                    masterList.Add(hm);
                }
                sqliteAccess.readerClose();

                foreach (holidayMaster hm in masterList)
                {
                    DateTime date = DateTime.Today;
                    int weekCount = 0;
                    bool effective = true;

                    // 有効期日指定があるなら
                    if (hm.seventh != -1) {
                        int effectiveYear = hm.sixth;
                        effective = false;

                        // 判定方法分岐
                        switch (hm.seventh)
                        {
                            case 0:     // <
                                if (intYear < effectiveYear) {
                                    effective = true;
                                }
                                break;
                            case 1:     // >
                                if (intYear > effectiveYear)
                                {
                                    effective = true;
                                }
                                break;
                            case 2:     // <=
                                if (intYear <= effectiveYear)
                                {
                                    effective = true;
                                }
                                break;
                            case 3:     // >=
                                if (intYear >= effectiveYear)
                                {
                                    effective = true;
                                }
                                break;
                            case 4:     // ==
                                if (effectiveYear == intYear)
                                {
                                    effective = true;
                                }
                                break;
                            case 5:     // ><
                                if (effectiveYear != intYear)
                                {
                                    effective = true;
                                }
                                break;
                        }
                    }

                    if (effective == false)
                    {
                        continue;
                    }
                    switch (hm.holiday_type)
                    {
                        case 0:     // 固定休日
                            date = DateTime.Parse(year + "/" + (hm.third + 1).ToString("00") + "/" + (hm.fourth + 1).ToString("00"));
                            break;
                        case 1:     // 週指定
                            date = DateTime.Parse(year + "/" + (hm.third + 1).ToString("00") + "/01");
                            for (int i = 0; i < 32; i++)
                            {
                                // 曜日が一致したら
                                if (hm.fifth == (int)dayOfWeekToNumber[date.DayOfWeek])
                                {
                                    // 何週目かをカウント
                                    weekCount++;
                                }
                                // 週カウンタが週指定に一致したら
                                if (weekCount == (hm.fourth + 1))
                                {
                                    break;
                                }
                                date = date.AddDays(1);
                            }
                            break;
                        case 2:     // 春分・秋分の日
                            // 春分の日(３月)
                            if ((hm.third + 1) == 3)
                            {
                                double dDay = 20.69115;     // 春分日の１０進法換算値
                                int day = 0;                // 春分日の１０進法換算値
                                double diff = 0.242194;     // 年間移動量

                                double move = ((double)(intYear - 2000) * diff) - ((intYear - 2000) / 4);
                                day = (int)(dDay + move);

                                date = DateTime.Parse(year + "/" + (hm.third + 1).ToString("00") + "/" + day.ToString("00"));
                            }
                            else if ((hm.third + 1) == 9)
                            {
                                double dDay = 23.09;        // 春分日の１０進法換算値
                                int day = 0;                // 春分日の１０進法換算値
                                double diff = 0.242194;     // 年間移動量

                                double move = ((double)(intYear - 2000) * diff) - ((intYear - 2000) / 4);
                                day = (int)(dDay + move);

                                date = DateTime.Parse(year + "/" + (hm.third + 1).ToString("00") + "/" + day.ToString("00"));
                            }
                            break;
                    }

                    holidays holiday = new holidays();
                    holiday.date = new DateTime(date.Year, date.Month, date.Day);
                    holiday.type = hm.holiday_type;
                    holiday.name = hm.holiday_name;
                    holidayList.Add(holiday);
                }

                holidayList2.Clear();
                bool alternative = false;
                DateTime alternativeDay = new DateTime(intYear, 1, 1);
                foreach (holidays hd in holidayList)
                {
                    // 対象日付が俗称"改定振り替え休日"施行日以降なら
                    // (”祝日が日曜にあたるときは、その日後において、その日に最も近い「国民の祝日」でない日を休日”)
                    if (int.Parse(hd.date.ToString("yyyy")) >= 2007)
                    {
                        // 振り替え判定が行われていたら
                        if (alternative == true)
                        {
                            // 次の休日にかぶっていないかを判定
                            if (alternativeDay < hd.date)
                            {
                                // かぶっていなければ振替休日を設定
                                holidays holiday = new holidays();
                                holiday.date = new DateTime(alternativeDay.Year, alternativeDay.Month, alternativeDay.Day);
                                holiday.type = 0;
                                holiday.name = "国民の休日";
                                holidayList2.Add(holiday);
                                alternative = false;
                            }
                            else
                            {
                                // かぶっていたら、さらに翌日送り
                                alternativeDay = hd.date.AddDays(1);
                            }
                        }
                        if (hd.date.DayOfWeek == DayOfWeek.Sunday)
                        {
                            alternative = true;
                            alternativeDay = hd.date.AddDays(1);
                        }
                    }
                    // 対象日付が俗称"振り替え休日"施行日以降なら
                    // (祝日が日曜にあたるときは、その翌日を休日”)
                    else if (hd.date >= DateTime.Parse("1973/04/12"))
                    {
                        // 振り替え判定が行われていたら
                        if (alternative == true)
                        {
                            // 次の休日にかぶっていないかを判定
                            if (alternativeDay < hd.date)
                            {
                                holidays holiday = new holidays();
                                holiday.date = new DateTime(alternativeDay.Year, alternativeDay.Month, alternativeDay.Day);
                                holiday.type = 0;
                                holiday.name = "国民の休日";
                                holidayList2.Add(holiday);
                                alternative = false;
                            }
                            else
                            {
                                // かぶっていたら、その振替休日はなかったことに(涙)
                                alternative = false;
                            }
                        }
                        if (hd.date.DayOfWeek == DayOfWeek.Sunday)
                        {
                            alternative = true;
                            alternativeDay = hd.date.AddDays(1);
                        }
                    }
                    holidayList2.Add(hd);
                }
                
                holidayList3.Clear();
                DateTime beforeDate = new DateTime(intYear, 1, 1);
                foreach (holidays hd in holidayList2)
                {
                    // 直前休日と今回休日が一日飛ばしの場合で
                    if (hd.date.Subtract(beforeDate).Days == 2)
                    {
                        beforeDate = beforeDate.AddDays(1);
                        if (beforeDate.DayOfWeek != DayOfWeek.Sunday)
                        {
                            holidays holiday = new holidays();
                            holiday.date = new DateTime(beforeDate.Year, beforeDate.Month, beforeDate.Day);
                            holiday.type = 0;
                            holiday.name = "国民の休日";
                            holidayList3.Add(holiday);
                        }
                    }
                    beforeDate = hd.date;
                    holidayList3.Add(hd);
                }

                foreach (holidays holiday in holidayList3)
                {
                    strSQL = "insert into holiday_list ("
                        + "holiday_date"
                        + ", holiday_type"
                        + ", holiday_name"
                        + ") values ( "
                        + "datetime('" + holiday.date.ToString("yyyy-MM-dd 00:00:00") + "')"
                        + ", " + holiday.type
                        + ", '" + holiday.name + "' )";

                    sqliteAccess.insert(strSQL, null);
                }
                sqliteAccess.readerClose();
            }
            loadYearyHoliday();

            // オーナフォーム(カレンダ)再表示
            parentForm.setCalendar();
        }

        /// <summary>
        /// 年間祝祭日設定ロード
        /// </summary>
        private void loadYearyHoliday()
        {
            this.SuspendLayout();

            string strSQL = "select * from holiday_list where strftime('%Y', holiday_date) = '" + dtpHolidayYear.Value.ToString("yyyy") + "' order by holiday_date";

            listView1.Items.Clear();
            holidayList.Clear();
            int index = 0;

            // 祝祭日DBに該当年のデータがあるかどうか確認
            SQLiteDataReader result = sqliteAccess.select(strSQL);
            if (result != null)
            {
                while (result.Read())
                {
                    holidays holiday = new holidays();
                    holiday.date = DateTime.Parse(result.GetDateTime(0).ToString("yyyy/MM/dd"));
                    holiday.type = result.GetInt32(1);
                    holiday.name = result.GetString(2);
                    holidayList.Add(holiday);

                    string[] strHoliday = { holiday.date.ToString("yyyy/MM/dd"), holiday.name };

                    ListViewItem lvi = new ListViewItem(strHoliday);
                    lvi.Tag = index;
                    listView1.Items.Add(lvi);
                    index++;
                }
            }
            sqliteAccess.readerClose();

            this.ResumeLayout();
        }

        /// <summary>
        /// メールチェッカ設定ロード
        /// </summary>
        private void loadMailChecker()
        {
            this.SuspendLayout();

            string strSQL = "select * from mail_list";

            listView2.Items.Clear();
            mailCheckerList.Clear();
            int index = 0;

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

                    string[] strMailChecker = { mailChecker.mail_effective ? "○" : "×", mailChecker.mail_connect_name };

                    ListViewItem lvi = new ListViewItem(strMailChecker);
                    lvi.Tag = index;
                    listView2.Items.Add(lvi);
                    index++;
                }
            }
            sqliteAccess.readerClose();

            this.ResumeLayout();
        }

        /// <summary>
        /// 色選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColor_Click(object sender, EventArgs e)
        {
            cdColorDialog.AnyColor = true;
            cdColorDialog.Color = lblColor.BackColor;
            cdColorDialog.ShowDialog();
            lblColor.BackColor = cdColorDialog.Color;
        }

        private void dtpHolidayYear_ValueChanged(object sender, EventArgs e)
        {
            loadYearyHoliday();
            resetInputFields();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = listView1.SelectedItems[0];

            selectIndex = int.Parse(lvi.Tag.ToString());
            setInputFields((holidays)holidayList[selectIndex]);
        }

        /// <summary>
        /// 指定されたコンテンツデータより編集領域へ
        /// </summary>
        /// <param name="data"></param>
        private void setInputFields(holidays holiday)
        {
            btnNew.Enabled = false;
            btnCancel.Enabled = true;
            btnEntry.Enabled = true;
            btnDelete.Enabled = true;

            dtpHoliday.Enabled = false;
            textBox1.Enabled = true;

            bNewEntry = false;
            dtpHoliday.Value = holiday.date;
            textBox1.Text = holiday.name;
        }

        /// <summary>
        /// 編集領域初期化
        /// </summary>
        /// <param name="data"></param>
        private void initInputFields()
        {
            btnNew.Enabled = false;
            btnCancel.Enabled = true;
            btnEntry.Enabled = true;
            btnDelete.Enabled = false;

            dtpHoliday.Enabled = true;
            textBox1.Enabled = true;

            selectIndex = -1;
            bNewEntry = true;
            dtpHoliday.Value = DateTime.Today;
            textBox1.Text = "";
        }

        /// <summary>
        /// 編集領域非活性化
        /// </summary>
        private void resetInputFields()
        {
            btnNew.Enabled = true;
            btnCancel.Enabled = false;
            btnEntry.Enabled = false;
            btnDelete.Enabled = false;

            selectIndex = -1;
            dtpHoliday.Enabled = false;
            textBox1.Enabled = false;
            dtpHoliday.Value = DateTime.Today;
            textBox1.Text = "";
        }

        private void setToContents(ref holidays holiday)
        {
            holiday.date = DateTime.Parse(dtpHoliday.Value.ToString("yyyy/MM/dd"));
            holiday.name = textBox1.Text;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            initInputFields();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            resetInputFields();
        }

        private void btnEntry_Click(object sender, EventArgs e)
        {
            saveAndDisplay();
        }

        private void saveAndDisplay()
        {
            holidays holiday = new holidays();

            // 新規登録
            if (bNewEntry)
            {
                setToContents(ref holiday);
                insert(holiday);
            }
            // 更新
            else
            {
                lock (holidayList)
                {
                    // 対象のコンテンツ一覧から検索取得
                    holidays data = (holidays)holidayList[selectIndex];
                    holiday = data;
                    setToContents(ref holiday);
                }
                update(holiday);
            }
            resetInputFields();

            // 年間祝祭日設定ロード
            loadYearyHoliday();

            // オーナフォーム(カレンダ)再表示
            parentForm.setCalendar();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            holidays holiday = new holidays();
            lock (holidayList)
            {
                // 対象のコンテンツ一覧から検索取得
                holiday = (holidays)holidayList[selectIndex];
            }

            destroy(holiday);
            resetInputFields();

            // 年間祝祭日設定ロード
            loadYearyHoliday();

            // オーナフォーム(カレンダ)再表示
            parentForm.setCalendar();
        }

        /// <summary>
        /// SQL接続(insert)
        /// </summary>
        private void insert(holidays holiday)
        {
            string strSQL = "insert into holiday_list ("
                + "holiday_date"
                + ", holiday_type"
                + ", holiday_name"
                + ") values ("
                + "datetime('" + holiday.date.ToString("yyyy-MM-dd 00:00:00") + "')"
                + ", " + holiday.type
                + ", '" + holiday.name + "'"
                + ")";

            sqliteAccess.insert(strSQL, null);
        }

        /// <summary>
        /// SQL接続(update)
        /// </summary>
        private void update(holidays holiday)
        {
            string strSQL = "update holiday_list set "
                + "holiday_type = " + holiday.type
                + ", holiday_name = '" + holiday.name + "'"
                + " where holiday_date = datetime('" + holiday.date.ToString("yyyy-MM-dd 00:00:00") + "')";

            sqliteAccess.update(strSQL);
        }

        /// <summary>
        /// SQL接続(destroy)
        /// </summary>
        private void destroy(holidays holiday)
        {
            string strSQL = "delete from holiday_list"
                + " where holiday_date = datetime('" + holiday.date.ToString("yyyy-MM-dd 00:00:00") + "')";

            sqliteAccess.update(strSQL);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // ctrl+s 実現
            if (e.Control && e.KeyCode.ToString().Equals("S"))
            {
                saveAndDisplay();
            }
            // ctrl+a 実現
            if (e.Control && e.KeyCode.ToString().Equals("A"))
            {
                textBox1.SelectAll();
            }
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            DBManager.Setting.bCountdownTimer = true;
            DBManager.Setting.countdownTermTime = DateTime.Now.AddSeconds(int.Parse(numericUpDown2.Value.ToString()));
            saveSetting();
            parentForm.setCalendarDisplay();
            this.Close();
        }

        private void saveSetting()
        {
            DBManager.Setting.main_time_adjust_startup = checkBox1.Checked;
            DBManager.Setting.main_time_adjust_interval = checkBox2.Checked;
            DBManager.Setting.main_time_adjust_ntp_server = comboBox1.Text;
            DBManager.Setting.main_time_adjust_interval_count = int.Parse(numericUpDown1.Value.ToString());
            if (radioButton1.Checked == true)
            {
                DBManager.Setting.main_time_adjust_interval_type = 0;
            }
            else if (radioButton2.Checked == true)
            {
                DBManager.Setting.main_time_adjust_interval_type = 1;
            }
            else if (radioButton3.Checked == true)
            {
                DBManager.Setting.main_time_adjust_interval_type = 2;
            }
            DBManager.Setting.main_alpha = tbAlpha.Value;
            DBManager.Setting.main_no_timedisplay = cbNoTimeDisplay.Checked;
            DBManager.Setting.main_desktop_access = cbDesktopAccess.Checked;
            DBManager.Setting.main_desktop_snapping = cbDesktopSnapping.Checked;
            DBManager.Setting.main_desktop_snapping_band = int.Parse(numericUpDown3.Value.ToString());
            DBManager.Setting.main_mailchecker_blinkcancel = cbMailcheckerBlinkCancel.Checked;
            DBManager.Setting.main_hotkey_000 = txtLauncher.Tag.ToString();
            DBManager.Setting.main_hotkey_001 = txtCommand.Tag.ToString();
            DBManager.Setting.main_hotkey_002 = txtTask.Tag.ToString();
            DBManager.Setting.main_hotkey_003 = txtNewStickie.Tag.ToString();
            DBManager.Setting.main_hotkey_004 = txtNewTask.Tag.ToString();
            DBManager.Setting.main_hotkey_005 = txtCommandlineLauncher.Tag.ToString();
            DBManager.Setting.main_default_backcolor_stickies = lblColor.BackColor.R.ToString() + ", " + lblColor.BackColor.G.ToString() + ", " + lblColor.BackColor.B.ToString();

            DBManager.Setting.save(sqliteAccess);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveSetting();
            parentForm.setCalendarDisplay();
            parentForm.renewTimeAdjust();
            disableHotKey();
            enableHotKey();

            this.Close();
        }

        private void tbAlpha_Scroll(object sender, EventArgs e)
        {
            lblAlpha.Text = tbAlpha.Value.ToString() + "%";
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = listView2.SelectedItems[0];

            if (lvi != null)
            {
                selectIndex = int.Parse(lvi.Tag.ToString());
                setInputFields2((DBManager.MailChecker.data)mailCheckerList[selectIndex]);
            }
        }

        /// <summary>
        /// 指定されたコンテンツデータより編集領域へ
        /// </summary>
        /// <param name="data"></param>
        private void setInputFields2(DBManager.MailChecker.data mailChecker)
        {
            btnNew2.Enabled = false;
            btnCancel2.Enabled = true;
            btnEntry2.Enabled = true;
            btnDelete2.Enabled = true;

            txtConnectName.Enabled = true;
            cbMailCheckEffective.Enabled = true;
            txtPop3.Enabled = true;
            txtUser.Enabled = true;
            txtPass.Enabled = true;
            nudCheckSpan.Enabled = true;

            bNewEntry2 = false;
            txtConnectName.Text = mailChecker.mail_connect_name;
            cbMailCheckEffective.Checked = mailChecker.mail_effective;
            txtPop3.Text = mailChecker.mail_pop3;
            txtUser.Text = mailChecker.mail_user;
            txtPass.Text = mailChecker.mail_pass;
            nudCheckSpan.Value = mailChecker.mail_check_span;
        }

        /// <summary>
        /// 編集領域初期化
        /// </summary>
        /// <param name="data"></param>
        private void initInputFields2()
        {
            btnNew2.Enabled = false;
            btnCancel2.Enabled = true;
            btnEntry2.Enabled = true;
            btnDelete2.Enabled = false;

            txtConnectName.Enabled = true;
            cbMailCheckEffective.Enabled = true;
            txtPop3.Enabled = true;
            txtUser.Enabled = true;
            txtPass.Enabled = true;
            nudCheckSpan.Enabled = true;

            selectIndex = -1;
            bNewEntry2 = true;
            dtpHoliday.Value = DateTime.Today;

            txtConnectName.Text = "";
            cbMailCheckEffective.Checked = true;
            txtPop3.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            nudCheckSpan.Value = 5;
        }

        /// <summary>
        /// 編集領域非活性化
        /// </summary>
        private void resetInputFields2()
        {
            btnNew2.Enabled = true;
            btnCancel2.Enabled = false;
            btnEntry2.Enabled = false;
            btnDelete2.Enabled = false;

            selectIndex = -1;
            dtpHoliday.Enabled = false;
            txtConnectName.Enabled = false;
            cbMailCheckEffective.Enabled = false;
            txtPop3.Enabled = false;
            txtUser.Enabled = false;
            txtPass.Enabled = false;
            nudCheckSpan.Enabled = false;

            txtConnectName.Text = "";
            cbMailCheckEffective.Checked = true;
            txtPop3.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            nudCheckSpan.Value = 5;
        }

        private void setToContents2(ref DBManager.MailChecker.data mailChecker)
        {
            mailChecker.mail_connect_name = txtConnectName.Text;
            mailChecker.mail_effective = cbMailCheckEffective.Checked;
            mailChecker.mail_pop3 = txtPop3.Text;
            mailChecker.mail_user = txtUser.Text;
            mailChecker.mail_pass = txtPass.Text;
            mailChecker.mail_check_span = Int32.Parse(nudCheckSpan.Value.ToString());
        }

        private void btnNew2_Click(object sender, EventArgs e)
        {
            initInputFields2();
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            resetInputFields2();
        }

        private void btnEntry2_Click(object sender, EventArgs e)
        {
            saveAndDisplay2();
        }

        private void saveAndDisplay2()
        {
            DBManager.MailChecker.data mailChecker = new DBManager.MailChecker.data();

            // 新規登録
            if (bNewEntry2)
            {
                setToContents2(ref mailChecker);
                insert2(mailChecker);
            }
            // 更新
            else
            {
                lock (mailCheckerList)
                {
                    // 対象のコンテンツ一覧から検索取得
                    mailChecker = (DBManager.MailChecker.data)mailCheckerList[selectIndex];
                    setToContents2(ref mailChecker);
                }
                update2(mailChecker);
            }
            resetInputFields2();

            // メールチェッカ設定ロード
            loadMailChecker();

            // オーナフォーム(メールチェッカ)再ロード
            parentForm.readMailCheckerData();
        }

        private void btnDelete2_Click(object sender, EventArgs e)
        {
            DBManager.MailChecker.data mailChecker = new DBManager.MailChecker.data();
            lock (mailCheckerList)
            {
                // 対象のコンテンツ一覧から検索取得
                mailChecker = (DBManager.MailChecker.data)mailCheckerList[selectIndex];
            }

            destroy2(mailChecker);
            resetInputFields2();

            // メールチェッカ設定ロード
            loadMailChecker();

            // オーナフォーム(メールチェッカ)再ロード
            parentForm.readMailCheckerData();
        }

        /// <summary>
        /// SQL接続(insert)
        /// </summary>
        private void insert2(DBManager.MailChecker.data mailChecker)
        {
            string strSQL = "insert into mail_list ("
                + "mail_connect_name"
                + ", mail_effective"
                + ", mail_pop3"
                + ", mail_user"
                + ", mail_pass"
                + ", mail_check_span"
                + ") values ("
                + "'" + mailChecker.mail_connect_name + "'"
                + ", " + ((mailChecker.mail_effective == true) ? 1 : 0) + ""
                + ", '" + mailChecker.mail_pop3 + "'"
                + ", '" + mailChecker.mail_user + "'"
                + ", '" + mailChecker.mail_pass + "'"
                + ", " + mailChecker.mail_check_span + ""
                + ")";

            sqliteAccess.insert(strSQL, null);
        }

        /// <summary>
        /// SQL接続(update)
        /// </summary>
        private void update2(DBManager.MailChecker.data mailChecker)
        {
            string strSQL = "update mail_list set "
                + "mail_connect_name = '" + mailChecker.mail_connect_name + "'"
                + ", mail_effective = " + ((mailChecker.mail_effective == true) ? 1 : 0)
                + ", mail_pop3 = '" + mailChecker.mail_pop3 + "'"
                + ", mail_user = '" + mailChecker.mail_user + "'"
                + ", mail_pass = '" + mailChecker.mail_pass + "'"
                + ", mail_check_span = " + mailChecker.mail_check_span + ""
                + " where id = " + mailChecker.id;

            sqliteAccess.update(strSQL);
        }

        /// <summary>
        /// SQL接続(destroy)
        /// </summary>
        private void destroy2(DBManager.MailChecker.data mailChecker)
        {
            string strSQL = "delete from mail_list"
                + " where id = " + mailChecker.id;

            sqliteAccess.update(strSQL);
        }

        private void cbDesktopSnapping_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDesktopSnapping.Checked == true)
            {
                numericUpDown3.Enabled = true;
            }
            else
            {
                numericUpDown3.Enabled = false;
            }
        }

        /// <summary>
        /// ランチャメニューホットキー設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLauncher_KeyDown(object sender, KeyEventArgs e)
        {
            /**
             * 有効なキーは
             * Ctrl, Shift, Alt
             * A ～ Z
             * F1 ～ F12
             * のみとする
             */
            textboxKeyDown(e, ref txtLauncher);
        }

        private void textboxKeyDown(KeyEventArgs e, ref TextBox control)
        {
            control.Text = "";
            control.Tag = "";

            KeysConverter kc = new KeysConverter();

            if (e.Control)
            {
                control.Text = control.Text + Keys.Control + " + ";
                control.Tag = control.Tag + Keys.Control.ToString() + " + ";
            }
            if (e.Shift)
            {
                control.Text = control.Text + Keys.Shift + " + ";
                control.Tag = control.Tag + Keys.Shift.ToString() + " + ";
            }
            if (e.Alt)
            {
                control.Text = control.Text + Keys.Alt + " + ";
                control.Tag = control.Tag + Keys.Alt.ToString() + " + ";
            }
            if ((e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)        // 英字キー
                || (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)   // 数値キー
                || (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)  // ファンクションキー
                || (e.KeyCode == Keys.Space))                       // スペースキー
            {
                control.Text = control.Text + kc.ConvertToString(e.KeyValue);
//                control.Text = control.Text + e.KeyCode;
                control.Tag = control.Tag + e.KeyValue.ToString();
            }
            else
            {
                control.Tag = "";
            }
        }

        private void txtLauncher_KeyUp(object sender, KeyEventArgs e)
        {
            textboxKeyUp(ref txtLauncher);
        }

        private void textboxKeyUp(ref TextBox control)
        {
            if (control.Tag == null || control.Tag.Equals(""))
            {
                control.Text = "";
                control.Tag = "";
            }
        }

        private void txtLauncher_Enter(object sender, EventArgs e)
        {
            disableHotKey();
        }

        private void disableHotKey()
        {
            launcherForm.UnregisterHotKey();
            parentForm.UnregisterHotKey();
        }

        private void txtLauncher_Leave(object sender, EventArgs e)
        {
            enableHotKey();
        }

        private void enableHotKey()
        {
            launcherForm.RegisterHotKey();
            parentForm.RegisterHotKey();
        }

        private void txtCommand_KeyDown(object sender, KeyEventArgs e)
        {
            /**
             * 有効なキーは
             * Ctrl, Shift, Alt
             * A ～ Z
             * F1 ～ F12
             * のみとする
             */
            textboxKeyDown(e, ref txtCommand);
        }

        private void txtCommand_KeyUp(object sender, KeyEventArgs e)
        {
            textboxKeyUp(ref txtCommand);
        }

        private void txtCommand_Enter(object sender, EventArgs e)
        {
            disableHotKey();
        }

        private void txtCommand_Leave(object sender, EventArgs e)
        {
            enableHotKey();
        }

        private void txtTask_KeyDown(object sender, KeyEventArgs e)
        {
            /**
             * 有効なキーは
             * Ctrl, Shift, Alt
             * A ～ Z
             * F1 ～ F12
             * のみとする
             */
            textboxKeyDown(e, ref txtTask);
        }

        private void txtTask_KeyUp(object sender, KeyEventArgs e)
        {
            textboxKeyUp(ref txtTask);
        }

        private void txtTask_Enter(object sender, EventArgs e)
        {
            disableHotKey();
        }

        private void txtTask_Leave(object sender, EventArgs e)
        {
            enableHotKey();
        }

        private void txtNewStickie_Enter(object sender, EventArgs e)
        {
            disableHotKey();
        }

        private void txtNewStickie_KeyDown(object sender, KeyEventArgs e)
        {
            /**
             * 有効なキーは
             * Ctrl, Shift, Alt
             * A ～ Z
             * F1 ～ F12
             * のみとする
             */
            textboxKeyDown(e, ref txtNewStickie);
        }

        private void txtNewStickie_KeyUp(object sender, KeyEventArgs e)
        {
            textboxKeyUp(ref txtNewStickie);
        }

        private void txtNewStickie_Leave(object sender, EventArgs e)
        {
            enableHotKey();
        }

        private void txtNewTask_Enter(object sender, EventArgs e)
        {
            disableHotKey();
        }

        private void txtNewTask_KeyDown(object sender, KeyEventArgs e)
        {
            /**
             * 有効なキーは
             * Ctrl, Shift, Alt
             * A ～ Z
             * F1 ～ F12
             * のみとする
             */
            textboxKeyDown(e, ref txtNewTask);
        }

        private void txtNewTask_KeyUp(object sender, KeyEventArgs e)
        {
            textboxKeyUp(ref txtNewTask);
        }

        private void txtNewTask_Leave(object sender, EventArgs e)
        {
            enableHotKey();
        }

        private void txtCommandlineLauncher_Enter(object sender, EventArgs e)
        {
            disableHotKey();
        }

        private void txtCommandlineLauncher_KeyDown(object sender, KeyEventArgs e)
        {
            /**
             * 有効なキーは
             * Ctrl, Shift, Alt
             * A ～ Z
             * F1 ～ F12
             * のみとする
             */
            textboxKeyDown(e, ref txtCommandlineLauncher);
        }

        private void txtCommandlineLauncher_KeyUp(object sender, KeyEventArgs e)
        {
            textboxKeyUp(ref txtCommandlineLauncher);
        }

        private void txtCommandlineLauncher_Leave(object sender, EventArgs e)
        {
            enableHotKey();
        }
    }
}
