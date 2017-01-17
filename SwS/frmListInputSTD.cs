using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SQLite;
using System.IO;

namespace SwS
{
    public partial class frmListInputSTD : Form
    {
        private SQLiteAccess sqliteAccess = new SQLiteAccess();
        private Hashtable holiday = new Hashtable();
        private Hashtable dataType = new Hashtable();
        private DBManager.Contents.ContentsList contentsList = null;
        private DBManager.Contents.data selectedData = new DBManager.Contents.data();
        private DateTime selectedDate = DateTime.Today;
        private frmCalendar parentForm;
        private Hashtable dayOfWeekToNumber = new Hashtable();
        private ListViewItem selectedListViewItem = null;
        private Color selectedListViewItemColor = new Color();

        private bool bNewEntry = true;
        private Int32 id = -1;

        public frmListInputSTD()
        {
            InitializeComponent();
        }

        public void inputData(DateTime selectDate, ref DBManager.Contents.ContentsList list, frmCalendar frm)
        {
            this.SuspendLayout();

            contentsList = list;
            parentForm = frm;

            columnHeader5.Width = listView1.Width - columnHeader4.Width - 20;
            columnHeader2.Width = listView2.Width - columnHeader1.Width - 20;

            listView1.Height = splitContainer1.Height - label1.Height;
            listView2.Height = splitContainer2.Panel1.Height - label2.Height - 20;

            panel1.Height = 92;
            textBox1.Top = splitContainer2.Panel2.Top + panel1.Height;
            textBox1.Height = splitContainer2.Panel2.Height - panel1.Height;

            dataType.Clear();
            dataType.Add(0, "スケジュール");
            dataType.Add(1, "ToDo");
            dataType.Add(2, "メモ");
            dataType.Add(3, "付箋");
            dataType.Add(4, "アラーム");
            dataType.Add("スケジュール", 0);
            dataType.Add("ToDo", 1);
            dataType.Add("メモ", 2);
            dataType.Add("付箋", 3);
            dataType.Add("アラーム", 4);

            dayOfWeekToNumber.Clear();
            dayOfWeekToNumber.Add(DayOfWeek.Sunday, 0);
            dayOfWeekToNumber.Add(DayOfWeek.Monday, 1);
            dayOfWeekToNumber.Add(DayOfWeek.Tuesday, 2);
            dayOfWeekToNumber.Add(DayOfWeek.Wednesday, 3);
            dayOfWeekToNumber.Add(DayOfWeek.Thursday, 4);
            dayOfWeekToNumber.Add(DayOfWeek.Friday, 5);
            dayOfWeekToNumber.Add(DayOfWeek.Saturday, 6);

            ttTips.SetToolTip(btnNew, "新規作成");
            ttTips.SetToolTip(btnCancel, "キャンセル");
            ttTips.SetToolTip(btnEntry, "登録・更新");
            ttTips.SetToolTip(btnDelete, "削除");

            // 一旦、スケジュール・ToDo・メモのみ許可とする
            cmbDataType.Items.Clear();
            cmbDataType.Items.Add(dataType[0]);
            cmbDataType.Items.Add(dataType[1]);
            cmbDataType.Items.Add(dataType[2]);
            cmbDataType.Items.Add(dataType[4]);

            ucCommandTrigger.InputControl = textBox1;

            // SQL発行前の環境設定
            sqliteAccess.setEnviroment(DBManager.dbPath, "SwS.db");

            DBManager.Setting.save(sqliteAccess);
            char[] sep = { ',' };
            String[] target = DBManager.Setting.main_task_display_target.Split(sep,  StringSplitOptions.None);
            if ("0".Equals(target[0]))
            {
                cbDispUnfinished.Checked = false;
            }
            else
            {
                cbDispUnfinished.Checked = true;
            }
            if ("0".Equals(target[1]))
            {
                cbDispFinished.Checked = false;
            }
            else
            {
                cbDispFinished.Checked = true;
            }
            if ("0".Equals(target[2]))
            {
                cbDispSchedule.Checked = false;
            }
            else
            {
                cbDispSchedule.Checked = true;
            }
            if ("0".Equals(target[3]))
            {
                cbDispTodo.Checked = false;
            }
            else
            {
                cbDispTodo.Checked = true;
            }
            if ("0".Equals(target[4]))
            {
                cbDispMemo.Checked = false;
            }
            else
            {
                cbDispMemo.Checked = true;
            }
            if ("0".Equals(target[5]))
            {
                cbDispAlarm.Checked = false;
            }
            else
            {
                cbDispAlarm.Checked = true;
            }

            selectedDate = selectDate;

            readMonthlyContentsData(selectedDate);

            // 日毎コンテンツロード
            loadDairyContents();
            // 編集領域非活性化
            resetInputFields();

            dtpSelecter.Value = selectedDate;

            this.Show();
            this.Activate();
        }

        /// <summary>
        /// SQLから指定月のコンテンツデータ読込
        /// </summary>
        private void readMonthlyContentsData(DateTime selectDate)
        {
            Hashtable contentsTable = new Hashtable();
            string dateFormat = "yyyy/MM/dd";

            this.SuspendLayout();

            // SELECT文の作成
            DateTime fromDate = DateTime.Parse(selectDate.ToString("yyyy/MM/01"));
            DateTime toDate = DateTime.Parse(selectDate.AddMonths(1).ToString("yyyy/MM/01")).AddDays(-1);

            lock (contentsList)
            {
                // 左ペイン用コンテンツデータ作成
                foreach (DBManager.Contents.data schedule in contentsList.SelectDataTypeAllEffective(0).SelectDateRange(fromDate, toDate))
                {
                    string contents = schedule.contents;
                    string[] date = new[] { "" };

                    // スケジュールデータで期間設定
                    if (schedule.task_range == true)
                    {
                        // 日付ワークエリア
                        ArrayList dateString = new ArrayList();

                        // タスク繰り返し表示期間設定がある場合は必要な繰り返しデータ生成
                        Int32 timeTriggerType = schedule.time_trigger_type;
                        DateTime rangeStartDate = (fromDate > schedule.task_range_startdate) ? fromDate : schedule.task_range_startdate;
                        DateTime judgeDate = schedule.task_range_enddate;

                        while (DateTime.Parse(rangeStartDate.ToString("yyyy/MM/dd 00:00:00")).CompareTo(judgeDate) <= 0)
                        {
                            if (schedule.DateJudgeByTaskTrigger(rangeStartDate) == true)
                            {
                                dateString.Add(rangeStartDate.ToString("yyyy/MM/dd"));
                            }
                            rangeStartDate = calcNextDate(timeTriggerType, rangeStartDate);
                        }
                        date = new string[dateString.Count];
                        for (int i = 0; i < dateString.Count; i++)
                        {
                            date[i] = dateString[i].ToString();
                        }
                    }
                    else
                    {
                        date = new string[] { schedule.date_time.ToString("yyyy/MM/dd") };
                    }

                    for (int i = 0; i < date.Length; i++)
                    {
                        // 当該月データ収集
                        if (contentsTable.Contains(date[i]))
                        {
                            contents = contentsTable[date[i]] + "\r\n" + schedule.contents;
                            contentsTable[date[i]] = contents;
                        }
                        else
                        {
                            contentsTable.Add(date[i], schedule.contents);
                        }
                    }
                }
            }

            // 指定月の休日データ取得
            getHoliday(selectDate);

            // コンテンツをリストビューに登録
            DateTime startDate = fromDate;
            listView1.Items.Clear();
            for (int i = 0; i < 32; i++)
            {
                string strSelectDate = startDate.ToString(dateFormat);
                string[] item1 = { strSelectDate, "" };
                if (contentsTable.Contains(strSelectDate))
                {
                    item1[1] = (string)contentsTable[strSelectDate];
                }

                ListViewItem lvi = new ListViewItem(item1);
                lvi.Tag = startDate;
                lvi.ToolTipText = "";

                // 休日データに一致したら
                if (holiday.Contains(int.Parse(startDate.ToString("dd"))))
                {
                    lvi.ToolTipText = (string)holiday[int.Parse(startDate.ToString("dd"))];
                    lvi.BackColor = Color.FromArgb(255, 230, 230);
                }
                // 土曜日だったら
                else if (startDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    lvi.BackColor = Color.FromArgb(255, 230, 230);
                }
                // 日曜日だったら
                else if (startDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    lvi.BackColor = Color.FromArgb(230, 230, 255);
                }
                listView1.Items.Add(lvi);

                // カレンダ選択日を選択状態へ
                if (selectedDate == startDate)
                {
                    // 今回選択行を取得
                    selectedListViewItem = listView1.Items[i];
                    selectedListViewItemColor = selectedListViewItem.BackColor;

                    listView1.Items[i].Selected = true;
                }

                startDate = startDate.AddDays(1);
                if (startDate > toDate)
                {
                    break;
                }
            }
            this.ResumeLayout();
        }


        private static DateTime calcNextDate(Int32 timeTriggerType, DateTime timeTriggerDatetime)
        {
            // 時限起動の種別判定
            switch (timeTriggerType)
            {
                case 0:     // 日付
                    timeTriggerDatetime = timeTriggerDatetime.AddDays(1);
                    break;
                case 1:     // 毎時
                    timeTriggerDatetime = timeTriggerDatetime.AddHours(1);
                    break;
                case 2:     // 毎日
                    timeTriggerDatetime = timeTriggerDatetime.AddDays(1);
                    break;
                case 3:     // 毎週
                    timeTriggerDatetime = timeTriggerDatetime.AddDays(7);
                    break;
                case 4:     // 毎月
                    timeTriggerDatetime = timeTriggerDatetime.AddMonths(1);
                    break;
            }
            return timeTriggerDatetime;
        }

        private void frmListInputSTD_Load(object sender, EventArgs e)
        {
        }

        private void frmListInputSTD_Activated(object sender, EventArgs e)
        {
            this.ResumeLayout();
        }

        private void frmListInputSTD_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 終了理由が×ボタン押下のとき(正しくはユーザ操作による終了処理)
            // (タスクトレイアイコン右クリックでの終了処理の場合は通常処理を行う)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // 本来の終了処理ではなくフォーム非表示とする
                e.Cancel = true;                // 終了処理キャンセル
                this.Visible = false;           // フォーム非表示
            }
        }

        /// <summary>
        /// SQLから指定月の休日データ読込
        /// </summary>
        private SQLiteDataReader readMonthlyHolidayData(DateTime selectDate)
        {
            ArrayList result = new ArrayList();

            // SELECT文の作成
            string fromDate = DateTime.Parse(selectDate.ToString("yyyy-MM-01")).ToShortDateString().Replace('/', '-');
            string toDate = DateTime.Parse(selectDate.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString().Replace('/', '-');

            string strSQL = "select * from holiday_list where "
                + "holiday_date between '" + fromDate + "' and '" + toDate + "'";

            return sqliteAccess.select(strSQL);
        }

        /// <summary>
        /// 祝祭日取得
        /// </summary>
        private void getHoliday(DateTime selectDate)
        {

            SQLiteDataReader result = readMonthlyHolidayData(selectDate);
            holiday.Clear();
            while (result.Read())
            {
                holiday.Add(int.Parse(result.GetDateTime(0).ToString("dd")), result.GetString(2));
            }
            // リーダクローズ
            sqliteAccess.readerClose();
        }

        /// <summary>
        /// アイテム選択時(年月日取得により右上ペインへアイテム表示)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            this.SuspendLayout();

            ListView lv = (sender as ListView);
            ListViewItem lvi = lv.SelectedItems[0];

            // 前回選択していたアイテムの色を戻す
            selectedListViewItem.BackColor = selectedListViewItemColor;
            // 今回選択アイテムを取得する
            selectedListViewItem = lv.SelectedItems[0];
            // 今回選択アイテムの色を選択色にする
            selectedListViewItemColor = selectedListViewItem.BackColor;
            selectedListViewItem.BackColor = Color.FromArgb(192, 255, 192);

            selectedDate = DateTime.Parse(lvi.Tag.ToString());

            dtpSelecter.Value = selectedDate;

            // 日毎コンテンツロード
            loadDairyContents();
            resetInputFields();

            this.ResumeLayout();
        }

        /// <summary>
        /// 日毎コンテンツロード
        /// </summary>
        private void loadDairyContents()
        {
            this.SuspendLayout();

            listView2.Items.Clear();

            // スケジュール
            lock (contentsList)
            {
                // 右上ペイン用コンテンツデータ作成
                foreach (DBManager.Contents.data schedule in contentsList.SelectDataTypeAllEffective(0).SelectDateRange(selectedDate, selectedDate))
                {
                    if (schedule.DateJudgeByTaskTrigger(selectedDate) == true)
                    {
                        // データ一件設定
                        setDailyContents(schedule);
                    }
                }
            }

            // To Do
            lock (contentsList)
            {
                // 右上ペイン用コンテンツデータ作成
                foreach (DBManager.Contents.data todo in contentsList.SelectDataTypeAllEffective(1))
                {
                    // データ一件設定
                    setDailyContents(todo);
                }
            }

            // メモ
            lock (contentsList)
            {
                // 右上ペイン用コンテンツデータ作成
                foreach (DBManager.Contents.data memo in contentsList.SelectDataTypeAllEffective(2))
                {
                    // データ一件設定
                    setDailyContents(memo);
                }
            }

            // アラーム
            lock (contentsList)
            {
                // 右上ペイン用コンテンツデータ作成
                foreach (DBManager.Contents.data alarm in contentsList.SelectDataTypeAllEffective(4).SelectDateRange(selectedDate, selectedDate))
                {
                    // データ一件設定
                    setDailyContents(alarm);

                }
            }
            this.ResumeLayout();
        }

        /// <summary>
        /// データ一件設定
        /// </summary>
        /// <param name="data"></param>
        private void setDailyContents(DBManager.Contents.data data)
        {
            string[] item1 = { (string)dataType[(int)data.data_type], data.contents };

            // 有効無効フラグが有効のとき未完了表示フラグが立っていなければ
            if (data.effective == true && cbDispUnfinished.Checked == false)
            {
                return;
            }
            // 有効無効フラグが無効のとき完了表示フラグが立っていなければ
            if (data.effective == false && cbDispFinished.Checked == false)
            {
                return;
            }
            // データ種別がスケジュールのときスケジュール表示フラグが立っていなければ
            if (data.data_type == 0 && cbDispSchedule.Checked == false)
            {
                return;
            }
            // データ種別がToDoのときToDo表示フラグが立っていなければ
            if (data.data_type == 1 && cbDispTodo.Checked == false)
            {
                return;
            }
            // データ種別がメモのときメモ表示フラグが立っていなければ
            if (data.data_type == 2 && cbDispMemo.Checked == false)
            {
                return;
            }
            // データ種別がアラームのときアラーム表示フラグが立っていなければ
            if (data.data_type == 4 && cbDispAlarm.Checked == false)
            {
                return;
            }

            ListViewItem lvi2 = new ListViewItem(item1);
            lvi2.ToolTipText = "";
            lvi2.Tag = data.id.ToString();
            listView2.Items.Add(lvi2);
            if (data.effective == false)
            {
                Font font = new Font(lvi2.SubItems[1].Font, this.Font.Style | FontStyle.Strikeout);
                lvi2.Font = font;
            }
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            columnHeader5.Width = listView1.Width - columnHeader4.Width - 20;
        }

        private void listView2_Resize(object sender, EventArgs e)
        {
            columnHeader2.Width = listView2.Width - columnHeader1.Width - 20;
        }

        private void splitContainer2_Resize(object sender, EventArgs e)
        {
            textBox1.Top = splitContainer2.Top + splitContainer2.Height + panel1.Height;
        }

        private void splitContainer2_Panel2_Resize(object sender, EventArgs e)
        {
            textBox1.Top = splitContainer2.Panel2.Top + panel1.Height;
            textBox1.Height = splitContainer2.Panel2.Height - panel1.Height;
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            listView1.Height = splitContainer1.Height - label1.Height;
        }

        private void splitContainer2_Panel1_Resize(object sender, EventArgs e)
        {
            listView2.Height = splitContainer2.Panel1.Height - label2.Height - 20;
        }

        /// <summary>
        /// 個別予定データ編集へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            ListView lv = (sender as ListView);
            ListViewItem lvi = lv.SelectedItems[0];

            Int32 id = Int32.Parse(lvi.Tag.ToString());

            string type = lvi.SubItems[0].Text.ToString();

            lock (contentsList)
            {
                // 対象のコンテンツ一覧から検索取得
                selectedData = contentsList.SelectId(id);
                if (selectedData != null)
                {
                    DBManager.Contents.data contents = new DBManager.Contents.data();
                    contents = selectedData;
                    setInputFields(selectedData);
                }
            }

        }

        /// <summary>
        /// 指定されたコンテンツデータより編集領域へ(編集状態へ)
        /// </summary>
        /// <param name="data"></param>
        private void setInputFields(DBManager.Contents.data data)
        {
            btnNew.Enabled = false;
            btnCancel.Enabled = true;
            btnEntry.Enabled = true;
            btnDelete.Enabled = true;

            textBox1.Enabled = true;
            cmbDataType.Enabled = true;
            cbSendStickies.Enabled = true;

            bNewEntry = false;
            id = data.id;
            textBox1.Text = data.contents;
            if (data.effective == false)
            {
                Font font = new Font(textBox1.Font, this.Font.Style | FontStyle.Strikeout);
                textBox1.Font = font;
                label5.Visible = true;
            }
            else
            {
                Font font = new Font(textBox1.Font, this.Font.Style);
                textBox1.Font = font;
                label5.Visible = false;
            }
            cmbDataType.SelectedItem = dataType[data.data_type].ToString();
            cbSendStickies.Checked = data.show_stickies;
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 0;

            ucTimeTrigger.init();
            ucRangeTrigger.init();
            ucCommandTrigger.init();
            if (data.data_type != 4)
            {
                cbSendStickies.Checked = data.show_stickies;
                ucTimeTrigger.DayDivide = false;
            }
            else
            {
                // アラームのときだけカレンダ表示可否のチェックボックスとなる
                cbSendStickies.Checked = data.task_to_calendar;
                // アラームのときだけ日付指定と毎時を有効とする
                ucTimeTrigger.DayDivide = true;
            }
            cbRange.Checked = data.task_range;
            ucRangeTrigger.StartDate = data.task_range_startdate;
            ucRangeTrigger.EndDate = data.task_range_enddate;
            ucTimeTrigger.TriggerType = data.task_trigger_type;
            ucTimeTrigger.Date = data.task_trigger_datetime;
            ucTimeTrigger.Time = data.task_trigger_datetime;
            ucTimeTrigger.DayOfWeek = data.task_trigger_dayofweek;
            ucTimeTrigger.Span = data.task_trigger_span;
            ucCommandTrigger.CommandType = data.task_type;
            this.ActiveControl = textBox1;
        }

        /// <summary>
        /// 編集領域初期化(新規作成)
        /// </summary>
        /// <param name="data"></param>
        private void initInputFields()
        {
            btnNew.Enabled = false;
            btnCancel.Enabled = true;
            btnEntry.Enabled = true;
            btnDelete.Enabled = false;

            Font font = new Font(textBox1.Font, this.Font.Style);
            textBox1.Font = font;
            textBox1.Enabled = true;
            cmbDataType.Enabled = true;
            cbSendStickies.Enabled = true;
            label5.Visible = false;

            selectedData = new DBManager.Contents.data();

            bNewEntry = true;
            id = -1;
            textBox1.Text = "";
            cmbDataType.SelectedIndex = 0;
            cbSendStickies.Checked = false;
            ucTimeTrigger.init();
            ucRangeTrigger.init();
            ucCommandTrigger.init();
            ucTimeTrigger.DayDivide = false;
            ucTimeTrigger.TriggerType = 2;
            ucTimeTrigger.Date = selectedDate;
            ucRangeTrigger.StartDate = dtpSelecter.Value;
            ucRangeTrigger.EndDate = dtpSelecter.Value;
            
            this.ActiveControl = textBox1;
        }

        /// <summary>
        /// 編集領域非活性化(キャンセル、削除、起動直後など)
        /// </summary>
        private void resetInputFields()
        {
            btnNew.Enabled = true;
            btnCancel.Enabled = false;
            btnEntry.Enabled = false;
            btnDelete.Enabled = false;

            Font font = new Font(textBox1.Font, this.Font.Style);
            textBox1.Font = font;
            textBox1.Enabled = false;
            textBox1.Text = "";

            selectedData = new DBManager.Contents.data();

            ucTimeTrigger.init();
            ucRangeTrigger.init();
            ucCommandTrigger.init();

            ucTimeTrigger.DayDivide = false;
            cmbDataType.SelectedIndex = -1;

            cmbDataType.Enabled = false;
            cbSendStickies.Enabled = false;
            ucTimeTrigger.Enabled = false;
            ucRangeTrigger.Enabled = false;
            ucCommandTrigger.Enabled = false;
            label5.Visible = false;

            panel1.Height = 48;
            textBox1.Top = splitContainer2.Panel2.Top + panel1.Height;
            textBox1.Height = splitContainer2.Panel2.Height - panel1.Height;
        }

        /// <summary>
        /// コンテンツへ保存
        /// </summary>
        /// <param name="contents"></param>
        private void setToContents(DBManager.Contents.data contents)
        {
            contents.data_type = int.Parse(dataType[cmbDataType.SelectedItem.ToString()].ToString());
            contents.date_time = selectedDate;
            contents.contents = textBox1.Text;
            // 「付箋送り」チェックボックスの制御
            if (contents.data_type != 4)
            {
                contents.show_stickies = cbSendStickies.Checked;
            }
            else
            {
                // アラームのときだけカレンダ表示可否のチェックボックスとなる
                contents.task_to_calendar = cbSendStickies.Checked;
            }
            contents.task_trigger_type = ucTimeTrigger.TriggerType;

            // contents.task_trigger_datetime に設定
            contents.task_trigger_dayofweek = ucTimeTrigger.DayOfWeek;
            DateTime tempDate = ucTimeTrigger.Date;

            // スケジュールまたはアラームの場合対象日時の正当性チェック＆自動再定義
            if (contents.data_type == 0 || contents.data_type == 4)
            {
                // タスク種別ごとの処理
                switch (contents.task_trigger_type)
                {
                    case 0: // 日付指定
                    case 1: // 毎時
                    case 2: // 毎日
                    case 4: // 毎月
                        if (contents.task_range == true)
                        {
                            if (tempDate < contents.task_range_startdate)
                            {
                                tempDate = contents.task_range_startdate;
                            }
                        }
                        break;
                    case 3: // 毎週
                        if (contents.task_range == true)
                        {
                            if (tempDate < contents.task_range_startdate)
                            {
                                tempDate = contents.task_range_startdate;
                            }
                        }
                        while (true)
                        {
                            if ((int)dayOfWeekToNumber[tempDate.DayOfWeek] == contents.task_trigger_dayofweek)
                            {
                                if ((contents.task_range == false && tempDate >= DateTime.Now)
                                    || (contents.task_range == true && tempDate >= contents.task_range_startdate))
                                {
                                    break;
                                }
                            }
                            tempDate = tempDate.AddDays(1);
                        }
                        break;
                }
            }
#if false
            if (contents.task_trigger_type == 3
                && ((contents.task_range == false && tempDate < DateTime.Now)
                    || (contents.task_range == true && tempDate < contents.task_range_startdate)))
            {
                while (true)
                {
                    if ((int)dayOfWeekToNumber[tempDate.DayOfWeek] == contents.task_trigger_dayofweek)
                    {
                        if ((contents.task_range == false && tempDate >= DateTime.Now)
                            || (contents.task_range == true && tempDate >= contents.task_range_startdate))
                        {
                            break;
                        }
                    }
                    tempDate = tempDate.AddDays(1);
                }
            }
#endif
            contents.task_trigger_datetime = DateTime.Parse(tempDate.ToShortDateString() + " " + ucTimeTrigger.Time.ToShortTimeString());
            // 指定日時が現在日時より前なら
            if (contents.task_trigger_datetime.CompareTo(DateTime.Now) <= 0)
            {
                contents.task_trigger_datetime.AddDays(7);
            }
            contents.temp_task_trigger_datetime = contents.task_trigger_datetime;

            contents.task_trigger_span = ucTimeTrigger.Span;
            contents.setNextSpan();
            contents.task_range = cbRange.Checked;
            contents.task_range_startdate = ucRangeTrigger.StartDate;
            contents.task_range_enddate = ucRangeTrigger.EndDate;
            contents.task_type = ucCommandTrigger.CommandType;
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
            contentsSaveAndDisplay();
        }

        private void contentsSaveAndDisplay()
        {
            DBManager.Contents.data contents = new DBManager.Contents.data();

            // 新規登録
            if (bNewEntry)
            {
                setToContents(contents);
                insert(contents);
                contentsList.Add(contents);
            }
            // 更新
            else
            {
                lock (contentsList)
                {
                    // 対象のコンテンツ一覧から検索取得
                    DBManager.Contents.data data = contentsList.SelectId(id);
                    if (data != null)
                    {
                        contents = data;
                    }
                    setToContents(contents);
                }
                update(contents);
            }
            resetInputFields();

            // オーナフォーム(カレンダ)再表示
            parentForm.setCalendar();
            // 月間
            readMonthlyContentsData(selectedDate);
            // 日毎コンテンツロード
            loadDairyContents();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            contentsDeleteAndDisplay();
        }

        private void contentsDeleteAndDisplay()
        {
            DBManager.Contents.data contents = new DBManager.Contents.data();
            lock (contentsList)
            {
                // 対象のコンテンツ一覧から検索取得
                DBManager.Contents.data data = contentsList.SelectId(id);
                if (data != null)
                {
                    contents = data;
                }
            }

            destroy(contents);
            contentsList.Remove(contents);
            resetInputFields();

            // オーナフォーム(カレンダ)再表示
            parentForm.setCalendar();
            // 月間
            readMonthlyContentsData(selectedDate);
            // 日毎コンテンツロード
            loadDairyContents();
        }

        /// <summary>
        /// SQL接続(insert)
        /// </summary>
        private void insert(DBManager.Contents.data contents)
        {
            string strSQL = "insert into contents_list ("
                + "data_type"
                + ", date_time"
                + ", contents"
                + ", show_stickies"
                + ", backcolor_stickies"
                + ", point_stickies"
                + ", time_trigger"
                + ", time_trigger_type"
                + ", time_trigger_datetime"
                + ", time_trigger_dayofweek"
                + ", attach_trigger"
                + ", attach_trigger_title"
                + ", display_backimage_stickies"
                + ", display_backimage_type_stickies"
                + ", display_backimage_path_stickies"
                + ", display_backimage_region_stickies"
                + ", display_font_stickies"
                + ", task_to_calendar"
                + ", task_trigger_type"
                + ", task_trigger_datetime"
                + ", task_trigger_dayofweek"
                + ", task_trigger_span"
                + ", task_range"
                + ", task_range_startdate"
                + ", task_range_enddate"
                + ", task_type"
                + ", alpha_stickies"
                + ") values ("
                + contents.data_type
                + ", datetime('" + contents.date_time.ToString("yyyy-MM-dd 00:00:00") + "')"
                + ", '" + contents.contents + "'"
                + ", " + ((contents.show_stickies == true) ? 1 : 0)
                + ", '" + contents.backcolor_stickies + "'"
                + ", '" + contents.point_stickies + "'"
                + ", " + (contents.time_trigger == true ? 1 : 0)
                + ", " + contents.time_trigger_type
                + ", datetime('" + contents.time_trigger_datetime.ToString("yyyy-MM-dd HH:mm:ss") + "')"
                + ", " + contents.time_trigger_dayofweek
                + ", " + (contents.attach_trigger == true ? 1 : 0)
                + ", '" + contents.attach_trigger_title + "'"
                + ", " + (contents.display_backimage_stickies == true ? 1 : 0)
                + ", " + contents.display_backimage_type_stickies
                + ", '" + contents.display_backimage_path_stickies + "'"
                + ", " + (contents.display_backimage_region_stickies == true ? 1 : 0)
                + ", ''"
                + ", " + ((contents.task_to_calendar == true) ? 1 : 0)
                + ", " + contents.task_trigger_type
                + ", datetime('" + contents.task_trigger_datetime.ToString("yyyy-MM-dd HH:mm:ss") + "')"
                + ", " + contents.task_trigger_dayofweek
                + ", " + contents.task_trigger_span
                + ", " + ((contents.task_range == true) ? 1 : 0)
                + ", datetime('" + contents.task_range_startdate.ToString("yyyy-MM-dd 00:00:00") + "')"
                + ", datetime('" + contents.task_range_enddate.ToString("yyyy-MM-dd 00:00:00") + "')"
                + ", " + contents.task_type
                + ", " + contents.alpha_stickies + ""
                + ")";

            contents.id = sqliteAccess.insert(strSQL, "contents_list");
        }

        /// <summary>
        /// SQL接続(update)
        /// </summary>
        private void update(DBManager.Contents.data contents)
        {
            string strSQL = "update contents_list set "
                + "data_type = " + contents.data_type
                + ", contents = '" + contents.contents + "'"
                + ", show_stickies = " + ((contents.show_stickies == true) ? 1 : 0)
                + ", backcolor_stickies = '" + contents.backcolor_stickies + "'"
                + ", point_stickies = '" + contents.point_stickies + "'"
                + ", time_trigger = " + (contents.time_trigger == true ? 1 : 0)
                + ", time_trigger_type = " + contents.time_trigger_type
                + ", time_trigger_datetime = datetime('" + contents.time_trigger_datetime.ToString("yyyy-MM-dd HH:mm:ss") + "')"
                + ", time_trigger_dayofweek = " + contents.time_trigger_dayofweek
                + ", attach_trigger = " + (contents.attach_trigger == true ? 1 : 0)
                + ", attach_trigger_title = '" + contents.attach_trigger_title + "'"
                + ", display_backimage_stickies = " + (contents.display_backimage_stickies == true ? 1 : 0)
                + ", display_backimage_type_stickies = " + contents.display_backimage_type_stickies
                + ", display_backimage_path_stickies = '" + contents.display_backimage_path_stickies + "'"
                + ", display_backimage_region_stickies = " + (contents.display_backimage_region_stickies == true ? 1 : 0)
                + ", display_font_stickies = ''"
                + ", task_to_calendar = " + ((contents.task_to_calendar == true) ? 1 : 0)
                + ", task_trigger_type = " + contents.task_trigger_type
                + ", task_trigger_datetime = datetime('" + contents.task_trigger_datetime.ToString("yyyy-MM-dd HH:mm:ss") + "')"
                + ", task_trigger_dayofweek = " + contents.task_trigger_dayofweek
                + ", task_trigger_span = " + contents.task_trigger_span
                + ", task_range = " + ((contents.task_range == true) ? 1 : 0)
                + ", task_range_startdate = datetime('" + contents.task_range_startdate.ToString("yyyy-MM-dd 00:00:00") + "')"
                + ", task_range_enddate = datetime('" + contents.task_range_enddate.ToString("yyyy-MM-dd 00:00:00") + "')"
                + ", task_type = " + contents.task_type
                + ", alpha_stickies = " + contents.alpha_stickies
                + " where id = '" + contents.id + "' ";

            sqliteAccess.update(strSQL);
        }

        /// <summary>
        /// SQL接続(finished)
        /// </summary>
        private void finished(DBManager.Contents.data contents)
        {
            string strSQL = "update contents_list set "
                + "effective = 0"
                + " where id = '" + contents.id + "' ";

            sqliteAccess.update(strSQL);
        }

        /// <summary>
        /// SQL接続(unfinished)
        /// </summary>
        private void unfinished(DBManager.Contents.data contents)
        {
            string strSQL = "update contents_list set "
                + "effective = 1"
                + " where id = '" + contents.id + "' ";

            sqliteAccess.update(strSQL);
        }

        /// <summary>
        /// SQL接続(destroy)
        /// </summary>
        private void destroy(DBManager.Contents.data contents)
        {
            string strSQL = "delete from contents_list"
                + " where id = '" + contents.id + "' ";

            sqliteAccess.update(strSQL);
        }

        private void finishedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = listView2.FocusedItem;
            DBManager.Contents.data contents = new DBManager.Contents.data();

            Int32 id = Int32.Parse(lvi.Tag.ToString());

            lock (contentsList)
            {
                // 対象のコンテンツ一覧から検索取得
                DBManager.Contents.data data = contentsList.SelectId(id);
                if (data != null)
                {
                    contents = data;
                    contents.effective = false;
                    finished(contents);
                    setInputFields(data);
                    Font font = new Font(lvi.SubItems[1].Font, this.Font.Style | FontStyle.Strikeout);
                    lvi.Font = font;

                    // オーナフォーム(カレンダ)再表示
                    parentForm.setCalendar();
                }
            }
        }

        private void unfinishedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = listView2.FocusedItem;
            DBManager.Contents.data contents = new DBManager.Contents.data();

            Int32 id = Int32.Parse(lvi.Tag.ToString());

            lock (contentsList)
            {
                // 対象のコンテンツ一覧から検索取得
                DBManager.Contents.data data = contentsList.SelectId(id);
                if (data != null)
                {
                    contents = data;
                    contents.effective = true;
                    unfinished(contents);
                    setInputFields(data);
                    Font font = new Font(lvi.SubItems[1].Font, this.Font.Style);
                    lvi.Font = font;

                    // オーナフォーム(カレンダ)再表示
                    parentForm.setCalendar();
                }
            }
        }

        private void destroyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = listView2.FocusedItem;

            id = Int32.Parse(lvi.Tag.ToString());

            contentsDeleteAndDisplay();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // ctrl+s 実現
            if (e.Control && e.KeyCode.ToString().Equals("S"))
            {
                contentsSaveAndDisplay();
            }
            // ctrl+a 実現
            if (e.Control && e.KeyCode.ToString().Equals("A"))
            {
                textBox1.SelectAll();
            }
        }

        private void dtpSelecter_ValueChanged(object sender, EventArgs e)
        {
            int dtpYear = dtpSelecter.Value.Year;
            int dtpMonth = dtpSelecter.Value.Month;

            selectedDate = dtpSelecter.Value;
            DateTime lviDate = DateTime.Parse(listView1.Items[0].Tag.ToString());
            int lviYear = lviDate.Year;
            int lviMonth = lviDate.Month;

            // 年月が変化なし(同月)なら
            if (dtpYear == lviYear && dtpMonth == lviMonth)
            {
                listView1.Items[dtpSelecter.Value.Day - 1].Selected = true;

                // 前回選択していたアイテムの色を戻す
                selectedListViewItem.BackColor = selectedListViewItemColor;
                // 今回選択アイテムを取得する
                selectedListViewItem = listView1.Items[dtpSelecter.Value.Day - 1];
                // 今回選択アイテムの色を選択色にする
                selectedListViewItemColor = selectedListViewItem.BackColor;
                selectedListViewItem.BackColor = Color.FromArgb(192, 255, 192);
            }
            else
            {
                if (selectedListViewItem != null)
                {
                    // 前回選択していたアイテムの色を戻す
                    selectedListViewItem.BackColor = selectedListViewItemColor;
                    selectedListViewItem = null;
                    selectedListViewItemColor = new Color();
                }
                readMonthlyContentsData(selectedDate);
            }
            // 日毎コンテンツロード
            loadDairyContents();
            // 編集領域非活性化
            resetInputFields();
        }

        /// <summary>
        /// データ種類選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();

            int iDataType = -1;

            // データ種類が選択状態なら取得
            if (cmbDataType.SelectedItem != null)
            {
                iDataType = int.Parse(dataType[cmbDataType.SelectedItem.ToString()].ToString());
            }
            // スケジュールかアラームなら
            if (iDataType == 0 || iDataType == 4)
            {
                if (iDataType != 4)
                {
                    if (cbRange.Checked == true)
                    {
                        ucRangeTrigger.Enabled = true;
                        ucTimeTrigger.Enabled = true;
                    }
                    else
                    {
                        ucRangeTrigger.Enabled = false;
                        ucTimeTrigger.Enabled = false;
                    }
                    ucTimeTrigger.DayDivide = false;
                    ucTimeTrigger.TriggerType = 2;
                    ucCommandTrigger.Enabled = false;
                    ucCommandTrigger.Visible = false;
                    cbSendStickies.Text = "付箋送り";
                }
                else
                {
                    if (cbRange.Checked == true)
                    {
                        ucRangeTrigger.Enabled = true;
                    }
                    else
                    {
                        ucRangeTrigger.Enabled = false;
                    }
                    ucTimeTrigger.Enabled = true;
                    ucTimeTrigger.DayDivide = true;
                    ucTimeTrigger.TriggerType = 0;
                    ucCommandTrigger.Enabled = true;
                    ucCommandTrigger.Visible = true;
                    // アラームのときだけカレンダ表示可否のチェックボックスとなる
                    cbSendStickies.Text = "カレンダ";
                }
                panel1.Height = 92;
            }
            else
            {
                // データ種類が未選択、あるいはToDo・メモ(付箋はタスク登録ウィンドウの処理対象外)
                ucRangeTrigger.Enabled = false;
                ucTimeTrigger.Enabled = false;
                ucTimeTrigger.DayDivide = false;
                ucTimeTrigger.TriggerType = 2;
                ucCommandTrigger.Enabled = false;
                ucCommandTrigger.Visible = true;
                cbSendStickies.Text = "付箋送り";
                panel1.Height = 48;
            }
            textBox1.Top = splitContainer2.Panel2.Top + panel1.Height;
            textBox1.Height = splitContainer2.Panel2.Height - panel1.Height;

            this.ResumeLayout();
        }

        /// <summary>
        /// 繰り返し表示期間設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbRange_CheckedChanged(object sender, EventArgs e)
        {
            int iDataType = -1;

            // データ種類が選択状態なら取得
            if (cmbDataType.SelectedItem != null)
            {
                iDataType = int.Parse(dataType[cmbDataType.SelectedItem.ToString()].ToString());
            }
            if (cbRange.Checked == true)
            {
                ucRangeTrigger.Enabled = true;
                if (iDataType != 4)
                {
                    ucTimeTrigger.Enabled = true;
                }
                ucTimeTrigger.DateStart = ucRangeTrigger.StartDate;
                ucTimeTrigger.DateEnd = ucRangeTrigger.EndDate;
            }
            else
            {
                ucRangeTrigger.Enabled = false;
                if (iDataType != 4)
                {
                    ucTimeTrigger.Enabled = false;
                }
                ucTimeTrigger.DateStart = DateTime.Parse("1753/01/01 00:00:00");
                ucTimeTrigger.DateEnd = DateTime.Parse("9998/12/31 00:00:00");
                ucTimeTrigger.init();
                ucTimeTrigger.Date = selectedDate;
            }
        }

        private void cbDispXXX_CheckedChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();

            // 日毎コンテンツロード
            loadDairyContents();

            string target = "";
            if (cbDispUnfinished.Checked) {
                target = target + "1,";
            }
            else {
                target = target + "0,";
            }
            if (cbDispFinished.Checked)
            {
                target = target + "1,";
            }
            else
            {
                target = target + "0,";
            }
            if (cbDispSchedule.Checked)
            {
                target = target + "1,";
            }
            else
            {
                target = target + "0,";
            }
            if (cbDispTodo.Checked)
            {
                target = target + "1,";
            }
            else
            {
                target = target + "0,";
            }
            if (cbDispMemo.Checked)
            {
                target = target + "1,";
            }
            else
            {
                target = target + "0,";
            }
            if (cbDispAlarm.Checked)
            {
                target = target + "1";
            }
            else
            {
                target = target + "0";
            }
            DBManager.Setting.main_task_display_target = target;
            DBManager.Setting.save(sqliteAccess);

            this.ResumeLayout();
        }

        /// <summary>
        /// リストビューの選択状態変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lv = (sender as ListView);
            if (lv.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lv.SelectedItems[0];

                // 前回選択していたアイテムの色を戻す
                if (selectedListViewItem != null)
                {
                    selectedListViewItem.BackColor = selectedListViewItemColor;
                }
                // 今回選択アイテムを取得する
                selectedListViewItem = lv.SelectedItems[0];
                // 今回選択アイテムの色を選択色にする
                selectedListViewItemColor = selectedListViewItem.BackColor;
                selectedListViewItem.BackColor = Color.FromArgb(192, 255, 192);
            }
        }

        /// <summary>
        /// テキストボックスへのドラッグドロップイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                String[] dropFiles;
                dropFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (String dragFile in dropFiles)
                {
                    textBox1.Text += dragFile + System.Environment.NewLine; ;
                }
            }
            else if (e.Data.GetDataPresent("UniformResourceLocator")
                || e.Data.GetDataPresent("UniformResourceLocatorW"))
            {
                String fileGroupDescriptor = "";
                String encodeType = "";

                // タイプ判別
                if (e.Data.GetDataPresent("UniformResourceLocator"))
                {
                    fileGroupDescriptor = "fileGroupDescriptor";
                    encodeType = "Shift_JIS";
                }
                else
                {
                    fileGroupDescriptor = "fileGroupDescriptorW";
                    encodeType = "utf-8";
                }

                //ドロップされたリンクのURLを取得する
                string url = e.Data.GetData(DataFormats.Text).ToString();

                //リンクの文字列
                string txt = null;
                if (url.IndexOf('\\') > -1)
                {
                    //(*3)によると、一部のブラウザは、URLと文字列が
                    //一緒に取得されるらしい
                    string[] ss = url.Split('\\');
                    url = ss[0];
                    txt = ss[1];
                }
                else
                {
                    //FILEGROUPDESCRIPTOR構造体のデータをMemoryStreamで取得
                    MemoryStream ms =
                        (MemoryStream)e.Data.GetData(fileGroupDescriptor);
                    //cFileNameの長さを取得
                    ms.Seek(76, SeekOrigin.Begin);
                    int len = 0;
                    while (ms.ReadByte() > 0)
                        len++;
                    //cFileNameの部分をbyte配列に取得
                    byte[] bs = new byte[len];
                    ms.Seek(76, SeekOrigin.Begin);
                    ms.Read(bs, 0, len);
                    ms.Close();
                    //Shift JISでデコードする
                    //(*1)によると、.NET Server 2003ではUnicodeとなるらしい
                    txt = System.Text.Encoding.GetEncoding(encodeType).GetString(bs);
                    //最後が.urlか確認し、.urlをとる
                    if (txt.ToLower().EndsWith(".url"))
                        txt = txt.Substring(0, txt.Length - 4);
                }

                //結果を表示
                textBox1.Text += txt + " - " + url;

            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                textBox1.Text += e.Data.GetData(DataFormats.Text).ToString();

            }

        }

        /// <summary>
        /// テキストボックスへのドラッグエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // ファイル
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent("UniformResourceLocator")
                || e.Data.GetDataPresent("UniformResourceLocatorW"))
            {
                // URL
                e.Effect = DragDropEffects.Link;
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
    }
}
