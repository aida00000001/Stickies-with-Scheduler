using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SQLite;
using System.Drawing;

namespace SwS
{
    /// <summary>
    /// S.w.S. データベースマネージャ
    /// </summary>
    public static class DBManager
    {
        /// <summary>
        /// データベースアクセスパス - 起動時に設定しておくこと
        /// </summary>
        public static string dbPath = "";

        /// <summary>
        /// S.w.S.設定データ管理クラス
        /// </summary>
        public static class Setting
        {
            /// <summary>
            /// バージョン表記
            /// </summary>
            public static string version { get; set; }

            /// <summary>
            /// カレンダ画面起動位置X
            /// </summary>
            public static Int32 main_startUp_position_x { get; set; }

            /// <summary>
            /// カレンダ画面起動位置Y
            /// </summary>
            public static Int32 main_startUp_position_y { get; set; }

            /// <summary>
            /// 時間表記フォーマット
            /// </summary>
            public static string main_time_format { get; set; }

            /// <summary>
            /// 日付表記フォーマット
            /// </summary>
            public static string main_date_format { get; set; }

            /// <summary>
            /// 非透過率
            /// </summary>
            public static int main_alpha { get; set; }

            /// <summary>
            /// [S.w.S.]開始時時刻同期有無
            /// </summary>
            public static bool main_time_adjust_startup { get; set; }

            /// <summary>
            /// 指定間隔時刻同期有無
            /// </summary>
            public static bool main_time_adjust_interval { get; set; }

            /// <summary>
            /// 時刻同期種別(0:月毎 1:日毎 2:時間毎)
            /// </summary>
            public static int main_time_adjust_interval_type { get; set; }

            /// <summary>
            /// 時刻同期間隔
            /// </summary>
            public static int main_time_adjust_interval_count { get; set; }

            /// <summary>
            /// SNTPサーバ名
            /// </summary>
            public static string main_time_adjust_ntp_server { get; set; }

            /// <summary>
            /// [S.w.S.]開始時当日予定アリ時入力画面同時表示有無
            /// </summary>
            public static bool main_inputform_startup { get; set; }

            /// <summary>
            /// カレンダ現在時刻非表示設定
            /// </summary>
            public static bool main_no_timedisplay { get; set; }

            /// <summary>
            /// デスクトップ操作設定
            /// </summary>
            public static bool main_desktop_access { get; set; }

            /// <summary>
            /// デスクトップ末端吸着設定
            /// </summary>
            public static bool main_desktop_snapping { get; set; }

            /// <summary>
            /// デスクトップ末端吸着感度設定
            /// </summary>
            public static Int32 main_desktop_snapping_band { get; set; }

            /// <summary>
            /// クエリ検索文字列
            /// </summary>
            public static string main_query_string { get; set; }

            /// <summary>
            /// クエリ検索対象
            /// </summary>
            public static string main_query_object { get; set; }

            /// <summary>
            /// クエリ検索方法
            /// </summary>
            public static Int32 main_query_expr { get; set; }

            /// <summary>
            /// メール新着通知のキャンセル
            /// </summary>
            public static bool main_mailchecker_blinkcancel { get; set; }

            /// <summary>
            /// ホットキー０
            /// </summary>
            public static string main_hotkey_000 { get; set; }

            /// <summary>
            /// ホットキー１
            /// </summary>
            public static string main_hotkey_001 { get; set; }

            /// <summary>
            /// ホットキー２
            /// </summary>
            public static string main_hotkey_002 { get; set; }

            /// <summary>
            /// ホットキー３
            /// </summary>
            public static string main_hotkey_003 { get; set; }

            /// <summary>
            /// ホットキー４
            /// </summary>
            public static string main_hotkey_004 { get; set; }
            
            /// <summary>
            /// タスク登録ウィンドウ表示対象(0,0,0,0,0,0 : 完了,未完了,スケジュール,ToDo,メモ,アラーム)
            /// </summary>
            public static string main_task_display_target { get; set; }

            /// <summary>
            /// ホットキー５
            /// </summary>
            public static string main_hotkey_005 { get; set; }

            /// <summary>
            /// ホットキー６
            /// </summary>
            public static string main_hotkey_006 { get; set; }

            /// <summary>
            /// ホットキー７
            /// </summary>
            public static string main_hotkey_007 { get; set; }

            /// <summary>
            /// ホットキー８
            /// </summary>
            public static string main_hotkey_008 { get; set; }

            /// <summary>
            /// ホットキー９
            /// </summary>
            public static string main_hotkey_009 { get; set; }

            /// <summary>
            /// 付箋の初期背景表示色
            /// </summary>
            public static string main_default_backcolor_stickies { get; set; }


            //////////////////////////////
            // 以下はDBにない一時的項目 //
            //////////////////////////////

            /// <summary>
            /// ラーメンタイマー実行フラグ
            /// </summary>
            public static bool bCountdownTimer { get; set; }

            /// <summary>
            /// ラーメンタイマー(終了時刻を保持)
            /// </summary>
            public static DateTime countdownTermTime { get; set; }

            /// <summary>
            /// メールチェッカー最終動作時刻
            /// </summary>
            public static DateTime mailCheckerExecuteTime { get; set; }

            /// <summary>
            /// [S.w.S.]開始時時刻同期有無
            /// </summary>
            /// <returns></returns>
            public static bool isMain_time_adjust_startup()
            {
                return main_time_adjust_startup;
            }

            /// <summary>
            /// 指定間隔時刻同期有無
            /// </summary>
            /// <returns></returns>
            public static bool isMain_time_adjust_interval()
            {
                return main_time_adjust_interval;
            }

            /// <summary>
            /// リザルトセットアクセス用enum
            /// </summary>
            public enum enum_setting
            {
                id,
                version,
                main_startUp_position_x,
                main_startUp_position_y,
                main_time_format,
                main_date_format,
                main_alpha,
                main_time_adjust_startup,
                main_time_adjust_interval,
                main_time_adjust_interval_type,
                main_time_adjust_interval_count,
                main_time_adjust_ntp_server,
                main_inputform_startup,
                main_no_timedisplay,
                main_desktop_access,
                main_desktop_snapping,
                main_desktop_snapping_band,
                main_query_string,
                main_query_object,
                main_query_expr,
                main_mailchecker_blinkcancel,
                main_hotkey_000,
                main_hotkey_001,
                main_hotkey_002,
                main_hotkey_003,
                main_hotkey_004,
                main_task_display_target,
                main_hotkey_005,
                main_hotkey_006,
                main_hotkey_007,
                main_hotkey_008,
                main_hotkey_009,
                main_default_backcolor_stickies,

                main_end
            }

            /// <summary>
            /// テーブル更新
            /// </summary>
            /// <param name="sqliteAccess"></param>
            public static void save(SQLiteAccess sqliteAccess)
            {
                string strSQL = "update setting set "
                    + "version = '" + version + "'"
                    + ", main_startUp_position_x = " + main_startUp_position_x
                    + ", main_startUp_position_y = " + main_startUp_position_y
                    + ", main_time_format = '" + main_time_format + "'"
                    + ", main_date_format = '" + main_date_format + "'"
                    + ", main_alpha = " + main_alpha
                    + ", main_time_adjust_startup = " + ((main_time_adjust_startup == true) ? 1 : 0)
                    + ", main_time_adjust_interval = " + ((main_time_adjust_interval == true) ? 1 : 0)
                    + ", main_time_adjust_interval_type = " + main_time_adjust_interval_type
                    + ", main_time_adjust_interval_count = " + main_time_adjust_interval_count
                    + ", main_time_adjust_ntp_server = '" + main_time_adjust_ntp_server + "'"
                    + ", main_inputform_startup = " + ((main_inputform_startup == true) ? 1 : 0)
                    + ", main_no_timedisplay = " + ((main_no_timedisplay == true) ? 1 : 0)
                    + ", main_desktop_access = " + ((main_desktop_access == true) ? 1 : 0)
                    + ", main_desktop_snapping = " + ((main_desktop_snapping == true) ? 1 : 0)
                    + ", main_desktop_snapping_band = " + main_desktop_snapping_band
                    + ", main_query_string = '" + main_query_string + "'"
                    + ", main_query_object = '" + main_query_object + "'"
                    + ", main_query_expr = " + main_query_expr
                    + ", main_mailchecker_blinkcancel = " + ((main_mailchecker_blinkcancel == true) ? 1 : 0)
                    + ", main_hotkey_000 = '" + main_hotkey_000 + "'"
                    + ", main_hotkey_001 = '" + main_hotkey_001 + "'"
                    + ", main_hotkey_002 = '" + main_hotkey_002 + "'"
                    + ", main_hotkey_003 = '" + main_hotkey_003 + "'"
                    + ", main_hotkey_004 = '" + main_hotkey_004 + "'"
                    + ", main_task_display_target = '" + main_task_display_target + "'"
                    + ", main_hotkey_005 = '" + main_hotkey_005 + "'"
                    + ", main_hotkey_006 = '" + main_hotkey_006 + "'"
                    + ", main_hotkey_007 = '" + main_hotkey_007 + "'"
                    + ", main_hotkey_008 = '" + main_hotkey_008 + "'"
                    + ", main_hotkey_009 = '" + main_hotkey_009 + "'"
                    + ", main_default_backcolor_stickies = '" + main_default_backcolor_stickies + "'"
                    ;

                sqliteAccess.update(strSQL);
            }
        }

        /// <summary>
        /// S.w.S.コンテンツデータ管理クラス
        /// </summary>
        public static class Contents
        {
            // コンテンツリスト保持クラス
            public class ContentsList : CollectionBase
            {
                public int Add(data d)
                {
                    return (List.Add(d));
                }

                public int IndexOf(data d)
                {
                    return (List.IndexOf(d));
                }

                public void Insert(int index, data d)
                {
                    List.Insert(index, d);
                }

                public void Remove(data d)
                {
                    List.Remove(d);
                }

                public bool Contains(data d)
                {
                    return (List.Contains(d));
                }

                public data this[int index]
                {
                    get
                    {
                        return ((data)List[index]);
                    }
                    set
                    {
                        List[index] = value;
                    }
                }

                /// <summary>
                /// 有効なリストを返却
                /// </summary>
                /// <returns></returns>
                public ContentsList SelectEffective()
                {
                    ContentsList select = new ContentsList();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.effective == true)
                            {
                                select.Add(cl);
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// 該当のコンテンツを返却
                /// </summary>
                /// <returns></returns>
                public Contents.data SelectId(int id)
                {
                    Contents.data select = new Contents.data();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.id == id)
                            {
                                select = cl;
                                break;
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// データ種別を限定した有効なリストを返却
                /// </summary>
                /// <param name="id"></param>
                /// <returns></returns>
                public ContentsList SelectDataType(int type)
                {
                    ContentsList select = new ContentsList();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.effective == true && cl.data_type == type)
                            {
                                select.Add(cl);
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// データ種別を限定したリストを返却
                /// </summary>
                /// <param name="id"></param>
                /// <returns></returns>
                public ContentsList SelectDataTypeAllEffective(int type)
                {
                    ContentsList select = new ContentsList();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.data_type == type)
                            {
                                select.Add(cl);
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// 付箋対象リストを返却
                /// </summary>
                /// <returns></returns>
                public ContentsList SelectStickies()
                {
                    ContentsList select = new ContentsList();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.effective == true && (cl.data_type == 3 || cl.show_stickies == true))
                            {
                                select.Add(cl);
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// 未表示のリストを返却
                /// </summary>
                /// <returns></returns>
                public ContentsList SelectUndisplayed()
                {
                    ContentsList select = new ContentsList();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.effective == true && cl.displayed == false)
                            {
                                select.Add(cl);
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// 表示済のリストを返却
                /// </summary>
                /// <returns></returns>
                public ContentsList SelectDisplayed()
                {
                    ContentsList select = new ContentsList();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.effective == true && cl.displayed == true)
                            {
                                select.Add(cl);
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// 時限起動のリストを返却
                /// </summary>
                /// <returns></returns>
                public ContentsList SelectTimeTrigger()
                {
                    ContentsList select = new ContentsList();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.effective == true && cl.time_trigger == true)
                            {
                                select.Add(cl);
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// 連想起動のリストを返却
                /// </summary>
                /// <returns></returns>
                public ContentsList SelectAttachTrigger()
                {
                    ContentsList select = new ContentsList();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.effective == true && cl.attach_trigger == true)
                            {
                                select.Add(cl);
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// ウィンドウ貼付のリストを返却
                /// </summary>
                /// <returns></returns>
                public ContentsList SelectSnapWindow()
                {
                    ContentsList select = new ContentsList();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.effective == true && cl.snap_window == true)
                            {
                                select.Add(cl);
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// 予定データ日付範囲限定のリストを返却
                /// </summary>
                /// <param name="id"></param>
                /// <returns></returns>
                public ContentsList SelectDateRange(DateTime fromDate, DateTime toDate)
                {
                    ContentsList select = new ContentsList();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            string datetime = cl.date_time.ToString("yyyy/MM/dd");
                            string rangeStart = cl.task_range_startdate.ToString("yyyy/MM/dd");
                            string rangeEnd = cl.task_range_enddate.ToString("yyyy/MM/dd");

                            // タスク表示範囲指定あり
                            if (cl.task_range == true)
                            {
                                // スケジュールまたはアラームなら
                                if (cl.data_type == 0 || cl.data_type == 4)
                                {
                                    if (rangeEnd.CompareTo(fromDate.ToString("yyyy/MM/dd")) < 0)
                                    {
                                        continue;
                                    }
                                    if (rangeStart.CompareTo(toDate.ToString("yyyy/MM/dd")) > 0)
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    if (datetime.CompareTo(fromDate.ToString("yyyy/MM/dd")) < 0)
                                    {
                                        continue;
                                    }
                                    if (datetime.CompareTo(toDate.ToString("yyyy/MM/dd")) > 0)
                                    {
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                // スケジュール以外なら
                                if (cl.data_type != 0)
                                {
                                    // 処理なし、常に表示対象となる
                                }
                                else
                                {
                                    if (datetime.CompareTo(fromDate.ToString("yyyy/MM/dd")) < 0)
                                    {
                                        continue;
                                    }
                                    if (datetime.CompareTo(toDate.ToString("yyyy/MM/dd")) > 0)
                                    {
                                        continue;
                                    }
                                }
                            }

                            select.Add(cl);
                        }
                    }

                    return select;
                }
            }

            // コンテンツ構造
            public class data
            {
                public Int32 id;				                //プライマリキー
                public bool effective;		                    //データの有効/無効(予定データでは未完了/完了として扱う)
                public Int32 data_type;		                    //登録データの種類。0:schedule, 1:ToDo, 2:Memo, 3:Stickies, 4:Alarm
                public DateTime date_time;		                //データ登録日時
                public string contents;		                    //データ内容
                public bool show_stickies;	                    //付箋の表示可否
                public string backcolor_stickies;               //付箋の背景表示色
                public string point_stickies;	                //付箋表示位置及び幅と高さ。X, Y, Width, Height の４項目
                public bool time_trigger;	                    //時間駆動で表示可否
                public Int32 time_trigger_type;	                //時限起動の種類。0:日付, 1:曜日, 2:月
                public DateTime time_trigger_datetime;	        //時間駆動の時間
                public Int32 time_trigger_dayofweek;	        //時間駆動の曜日
                public bool attach_trigger;	                    //特定のウィンドウが起動されたとき(連想起動)の表示可否
                public string attach_trigger_title;	            //特定のウィンドウが起動されたとき(連想起動)に表示するためのトリガウィンドウタイトル
                public bool snap_window;	                    //貼付対象ウィンドウが選択されているとき(ウィンドウ貼付)の表示可否
                public string snap_window_title;	            //貼付対象ウィンドウが選択されているとき(ウィンドウ貼付)に表示するためのトリガウィンドウタイトル
                public string point_snap_window;	            //貼付対象ウィンドウの表示位置及。X, Y の２項目
                public Boolean snap_window_to_top;	            //貼付対象ウィンドウのに対する最前面化制御
                public bool display_backimage_stickies;	        //付箋の背景画像の表示可否
                public Int32 display_backimage_type_stickies;	//付箋の背景画像の表示方法
                public string display_backimage_path_stickies;	//付箋の背景画像のパス
                public bool display_backimage_region_stickies;  //付箋の背景画像のリージョン指定
                public Int32 alpha_stickies;	                //付箋の透過率
                public string display_fontname_stickies;	    //付箋のフォント名
                public Int32 display_fontsize_stickies;	        //付箋のフォントサイズ
                public string display_fontcolor_stickies;	    //付箋のフォントカラー
                public Font display_font_stickies;	            //付箋のフォント構造体
                public bool task_to_calendar;	                //タスク(4:Alarm)の繰り返し表示可否(alarmのカレンダへ表示は当フィールドの設定が必要)
                public Int32 task_trigger_type;                 //タスク繰り返し表示の種類。0：日付指定、1:毎時, 2:日付, 3:毎週、4：毎月
                public DateTime task_trigger_datetime;          //タスク繰り返し表示の日付
                public Int32 task_trigger_dayofweek;            //タスク繰り返し表示の曜日
                public Int32 task_trigger_span;                 //タスク繰り返し表示の時間間隔
                public bool task_range;                         //タスク繰り返し表示期間設定有無
                public DateTime task_range_startdate;           //タスク繰り返し表示期間開始日
                public DateTime task_range_enddate;             //タスク繰り返し表示期間終了日
                public Int32 task_type;                         //タスク(4:alarm)の種類。0:message, 1:command


                //////////////////////////////
                // 以下はDBにない一時的項目 //
                //////////////////////////////

                public bool displayed;                          //付箋表示済みフラグ
                public Int32 nextSpan;                          //タスク繰り返し表示の時間間隔を用いた次の実行時間算出用(最大一時間後まで)
                public DateTime temp_task_trigger_datetime;     //タスク繰り返し表示の日付(分刻み繰り返し時一時使用)
                public DateTime backup_task_trigger_datetime;   //タスク繰り返し表示の日付(分刻み繰り返し時バックアップ使用)
                public frmStickies frm;                         //付箋ウィンドウオブジェクト(付箋のみ)

                public data()
                {
                    id = -1;
                    effective = true;
                    data_type = 0;
                    contents = "";
                    backcolor_stickies = "245, 230, 245";
                    point_stickies = "0, 0, 320, 240";
                    time_trigger = false;
                    time_trigger_datetime = DateTime.Now;
                    time_trigger_dayofweek = 0;
                    attach_trigger = false;
                    attach_trigger_title = "";
                    display_backimage_stickies = false;
                    display_backimage_type_stickies = 0;
                    display_backimage_path_stickies = "";
                    display_backimage_region_stickies = false;
                    snap_window = false;
                    snap_window_title = "";
                    point_snap_window = "0, 0, 320, 240";
                    alpha_stickies = 100;
                    task_to_calendar = false;
                    task_trigger_type = 0;
                    task_trigger_datetime = DateTime.Now;
                    task_trigger_dayofweek = 0;
                    task_trigger_span = 0;
                    task_range = false;
                    task_range_startdate = DateTime.Now;
                    task_range_enddate = DateTime.Now;
                    task_type = 0;
                    displayed = false;
                    setNextSpan();
                    temp_task_trigger_datetime = task_trigger_datetime;
                    backup_task_trigger_datetime = task_trigger_datetime;
                    frm = null;
                }

                /// <summary>
                /// アラームの繰り返し処理用時間間隔初期値算出
                /// </summary>
                public void setNextSpan()
                {
                    switch (task_trigger_span)
                    {
                        case 1:
                            nextSpan = 5;
                            break;
                        case 2:
                            nextSpan = 10;
                            break;
                        case 3:
                            nextSpan = 15;
                            break;
                        case 4:
                            nextSpan = 30;
                            break;
                        default:
                            nextSpan = 0;
                            break;
                    }
                }

                /// <summary>
                /// 時限起動の判定
                /// </summary>
                /// <param name="judgeDate"></param>
                /// <returns></returns>
                public bool TimeTriggerJudgment(DateTime judgeDate)
                {
                    // 時間が指定時刻を過ぎていたら
                    if (task_trigger_datetime <= judgeDate)
                    {
                        // アラーム起動の種別判定
                        switch (time_trigger_type)
                        {
                            case 0:     // 日付
                                displayed = true;
                                break;
                            case 1:     // 毎時
                                time_trigger_datetime = time_trigger_datetime.AddHours(1);
                                displayed = true;
                                break;
                            case 2:     // 毎日
                                time_trigger_datetime = time_trigger_datetime.AddDays(1);
                                displayed = true;
                                break;
                            case 3:     // 毎週
                                time_trigger_datetime = time_trigger_datetime.AddDays(7);
                                displayed = true;
                                break;
                            case 4:     // 毎月(日)
                                time_trigger_datetime = time_trigger_datetime.AddMonths(1);
                                displayed = true;
                                break;
                            case 5:     // 毎月(曜)
                                DayOfWeek dayofWeek = time_trigger_datetime.DayOfWeek;
                                int day = time_trigger_datetime.Day;
                                int around = (int)Math.Floor((decimal)day / 7);

                                DateTime firstDate = DateTime.Parse(time_trigger_datetime.AddMonths(1).ToString("yyyy-MM-01"));
                                for (int i = 0; i < 7; i++)
                                {
                                    if (dayofWeek == firstDate.DayOfWeek)
                                    {
                                        break;
                                    }
                                    firstDate = firstDate.AddDays(1);

                                }
                                if (firstDate.AddDays(around * 7).Month == firstDate.Month)
                                {
                                    time_trigger_datetime = firstDate.AddDays(around * 7);
                                    displayed = true;
                                }
                                break;
                        }
                    }
                    return displayed;
                }

                /// <summary>
                /// アラーム起動時間の判定
                /// </summary>
                /// <param name="judgeDate"></param>
                /// <returns></returns>
                public bool TaskTriggerJudgment(DateTime judgeDate)
                {
                    displayed = false;

                    // 時間が指定時刻を過ぎていたら
                    if (task_trigger_datetime <= judgeDate)
                    {
                        // アラーム起動の種別判定
                        switch (task_trigger_type)
                        {
                            case 0:     // 日付
                                break;
                            case 1:     // 毎時
                                temp_task_trigger_datetime = task_trigger_datetime;
                                backup_task_trigger_datetime = task_trigger_datetime;
                                task_trigger_datetime = task_trigger_datetime.AddHours(1);
                                break;
                            case 2:     // 毎日
                                task_trigger_datetime = task_trigger_datetime.AddDays(1);
                                temp_task_trigger_datetime = task_trigger_datetime;
                                backup_task_trigger_datetime = task_trigger_datetime;
                                break;
                            case 3:     // 毎週
                                temp_task_trigger_datetime = task_trigger_datetime;
                                backup_task_trigger_datetime = task_trigger_datetime;
                                task_trigger_datetime = task_trigger_datetime.AddDays(7);
                                break;
                            case 4:     // 毎月(日)
                                temp_task_trigger_datetime = task_trigger_datetime;
                                backup_task_trigger_datetime = task_trigger_datetime;
                                task_trigger_datetime = task_trigger_datetime.AddMonths(1);
                                break;
                            case 5:     // 毎月(曜)
                                temp_task_trigger_datetime = task_trigger_datetime;
                                backup_task_trigger_datetime = task_trigger_datetime;

                                DayOfWeek dayofWeek = task_trigger_datetime.DayOfWeek;
                                int day = task_trigger_datetime.Day;
                                int around = (int)Math.Floor((decimal)(day - 1) / 7);

                                while (true)
                                {
                                    DateTime firstDate = DateTime.Parse(task_trigger_datetime.AddMonths(1).ToString("yyyy-MM-01"));
                                    for (int i = 0; i < 7; i++)
                                    {
                                        if (dayofWeek == firstDate.DayOfWeek)
                                        {
                                            break;
                                        }
                                        firstDate = firstDate.AddDays(1);

                                    }
                                    if (firstDate.AddDays(around * 7).Month == firstDate.Month)
                                    {
                                        task_trigger_datetime = firstDate.AddDays(around * 7);
                                        break;
                                    }
                                }
                                break;
                        }
                        // 日時指定の場合
                        if (task_trigger_type == 0)
                        {
                            // 一回のみの場合は完了フラグを立てる
                            if (task_trigger_span == 0)
                            {
                                // 完了フラグを立てる(無効化。このフラグは失敗だったよなぁ。。)
                                effective = false;
                                displayed = true;
                            }
                            // 一回のみでなく繰り返し時間間隔の設定値より現在時刻が大きくなれば
                            else if (temp_task_trigger_datetime <= judgeDate)
                            {
                                // 時間間隔指定分ずらした時間を算出
                                temp_task_trigger_datetime = temp_task_trigger_datetime.AddMinutes(nextSpan);
                                displayed = true;
                                
                                // 算出結果が日時指定値＋1時間1分より大きければ
                                if (temp_task_trigger_datetime > task_trigger_datetime.AddHours(1).AddMinutes(1))
                                {
                                    // 完了フラグを立てる(無効化。このフラグは失敗だったよなぁ。。)
                                    effective = false;
                                }
                            }
                        }
                        // 日時指定以外の場合
                        else
                        {
                            displayed = true;

                            // 一回のみの場合
                            if (task_trigger_span == 0)
                            {
                                temp_task_trigger_datetime = task_trigger_datetime;
                            }
                            else
                            {
                                temp_task_trigger_datetime = temp_task_trigger_datetime.AddMinutes(nextSpan);
                            }
                        }
                    }
                    else if (task_trigger_span != 0 && temp_task_trigger_datetime <= judgeDate)
                    {
                        // 次回アラーム起動時間が繰り返し間隔あり時の１時間半以内であるかどうかの判定
                        if (temp_task_trigger_datetime >= backup_task_trigger_datetime
                            && temp_task_trigger_datetime < backup_task_trigger_datetime.AddHours(1).AddMinutes(1))
                        {
                            // 次の判定時間生成
                            temp_task_trigger_datetime = temp_task_trigger_datetime.AddMinutes(nextSpan);
                            displayed = true;
#if false
                            // 算出結果が日時指定値＋1時間1分より大きければ
                            if (temp_task_trigger_datetime > task_trigger_datetime.AddHours(1).AddMinutes(1))
                            {
                                temp_task_trigger_datetime = task_trigger_datetime;
                                backup_task_trigger_datetime = task_trigger_datetime;
                            }
#endif
                        }
                    }

                    // 現在時刻がタスク繰り返し表示範囲指定を超えたら
                    if (task_range == true && DateTime.Today > task_range_enddate)
                    {
                        // 完了フラグを立てる(無効化。このフラグは失敗だったよなぁ。。)
                        effective = false;
                    }

                    return displayed;
                }

                /// <summary>
                /// 当該日のタスク繰り返し表示設定による有効日判定(スケジュール用)
                /// </summary>
                /// <param name="judgeDate"></param>
                /// <returns></returns>
                public bool DateJudgeByTaskTrigger(DateTime judgeDate)
                {
                    if (task_range == true)
                    {
                        DateTime tempJudgeDate = DateTime.Parse(judgeDate.ToString("yyyy/MM/dd 00:00:00"));
                        DateTime tempTaskTriggerDatetime = DateTime.Parse(task_trigger_datetime.ToString("yyyy/MM/dd 00:00:00"));

                        // スケジュール/アラーム起動の種別判定
                        switch (task_trigger_type)
                        {
                            case 0:     // 日付
                                return true;
                            case 1:     // 毎時
                                return true;
                            case 2:     // 毎日
                                return true;
                            case 3:     // 毎週
                                if (tempJudgeDate.DayOfWeek == tempTaskTriggerDatetime.DayOfWeek)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            case 4:     // 毎月(日)
                                if (tempJudgeDate.Day == tempTaskTriggerDatetime.Day)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            case 5:     // 毎月(曜)
                                DayOfWeek dayofWeek = tempTaskTriggerDatetime.DayOfWeek;
                                int day = tempTaskTriggerDatetime.Day;
                                int around = (int)Math.Floor((decimal)(day - 1) / 7);

                                DayOfWeek todayDayofWeek = tempJudgeDate.DayOfWeek;
                                int todayDay = tempJudgeDate.Day;
                                int todayAround = (int)Math.Floor((decimal)(todayDay - 1) / 7);

                                if (dayofWeek == todayDayofWeek && around == todayAround)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                        }
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                /// <summary>
                /// 当該日のタスク繰り返し表示設定によるカレンダ表示判定(アラーム用)
                /// </summary>
                /// <param name="judgeDate"></param>
                /// <returns></returns>
                public bool DateJudgeByTaskTriggerForCalendar(DateTime judgeDate)
                {
                    DateTime tempJudgeDate = DateTime.Parse(judgeDate.ToString("yyyy/MM/dd 00:00:00"));
                    DateTime tempTaskTriggerDatetime = DateTime.Parse(task_trigger_datetime.ToString("yyyy/MM/dd 00:00:00"));

                    // アラーム起動の種別判定
                    switch (task_trigger_type)
                    {
                        case 0:     // 日付
                            // DateRange でアラームは全日表示対象としているため、ここで絞り込む
                            // タスク入力画面と同じDateRangeにしているのがある意味間違い。。。
                            if (tempJudgeDate.Date == tempTaskTriggerDatetime.Date)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case 1:     // 毎時
                            return true;
                        case 2:     // 毎日
                            return true;
                        case 3:     // 毎週
                            if (tempJudgeDate.DayOfWeek == tempTaskTriggerDatetime.DayOfWeek)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case 4:     // 毎月(日)
                            if (tempJudgeDate.Day == tempTaskTriggerDatetime.Day)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case 5:     // 毎月(曜)
                            DayOfWeek dayofWeek = tempTaskTriggerDatetime.DayOfWeek;
                            int day = tempTaskTriggerDatetime.Day;
                            int around = (int)Math.Floor((decimal)(day - 1) / 7);

                            DayOfWeek todayDayofWeek = tempJudgeDate.DayOfWeek;
                            int todayDay = tempJudgeDate.Day;
                            int todayAround = (int)Math.Floor((decimal)(todayDay - 1) / 7);

                            if (dayofWeek == todayDayofWeek && around == todayAround)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                    }
                    return false;
                }
            }

            public enum enum_data
            {
                id,
                effective,
                data_type,
                date_time,
                contents,
                show_stickies,
                backcolor_stickies,
                point_stickies,
                time_trigger,
                time_trigger_type,
                time_trigger_datetime,
                time_trigger_dayofweek,
                attach_trigger,
                attach_trigger_title,
                display_backimage_stickies,
                display_backimage_type_stickies,
                display_backimage_path_stickies,
                display_backimage_region_stickies,
                alpha_stickies,
                display_fontname_stickies,
                display_fontsize_stickies,
                display_fontcolor_stickies,
                display_font_stickies,
                task_to_calendar,
                task_trigger_type,
                task_trigger_datetime,
                task_trigger_dayofweek,
                task_trigger_span,
                task_range,
                task_range_startdate,
                task_range_enddate,
                task_type,

                displayed
            }
        }

        /// <summary>
        /// S.w.S.メールチェッカ管理クラス
        /// </summary>
        public static class MailChecker
        {
            // メールチェッカリスト保持クラス
            public class MailCheckerList : CollectionBase
            {
                public int Add(data d)
                {
                    return (List.Add(d));
                }

                public int IndexOf(data d)
                {
                    return (List.IndexOf(d));
                }

                public void Insert(int index, data d)
                {
                    List.Insert(index, d);
                }

                public void Remove(data d)
                {
                    List.Remove(d);
                }

                public bool Contains(data d)
                {
                    return (List.Contains(d));
                }

                public data this[int index]
                {
                    get
                    {
                        return ((data)List[index]);
                    }
                    set
                    {
                        List[index] = value;
                    }
                }

                /// <summary>
                /// 有効なリストを返却
                /// </summary>
                /// <returns></returns>
                public MailCheckerList SelectEffective()
                {
                    MailCheckerList select = new MailCheckerList();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.mail_effective == true)
                            {
                                select.Add(cl);
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// 該当のメールチェッカを返却
                /// </summary>
                /// <returns></returns>
                public MailChecker.data SelectId(int id)
                {
                    MailChecker.data select = new MailChecker.data();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.id == id)
                            {
                                select = cl;
                                break;
                            }
                        }
                    }

                    return select;
                }
            }

            // メールチェッカ構造
            public class data
            {
                /// <summary>
                /// プライマリキー
                /// </summary>
                public Int32 id;

                /// <summary>
                /// 接続名(見出し)
                /// </summary>
                public string mail_connect_name { get; set; }

                /// <summary>
                /// 有効無効
                /// </summary>
                public bool mail_effective { get; set; }
                
                /// <summary>
                /// POP3サーバ
                /// </summary>
                public string mail_pop3 { get; set; }

                /// <summary>
                /// ユーザ
                /// </summary>
                public string mail_user { get; set; }

                /// <summary>
                /// パスワード
                /// </summary>
                public string mail_pass { get; set; }

                /// <summary>
                /// チェック間隔
                /// </summary>
                public Int32 mail_check_span { get; set; }

                public void setMail_check_span(Int32 span)
                {
                    mail_check_span = span;
                    nextCheckTime = DateTime.Now.AddMinutes(mail_check_span);
                }

                /// <summary>
                /// UIDL
                /// </summary>
                public string mail_uidl { get; set; }


                //////////////////////////////
                // 以下はDBにない一時的項目 //
                //////////////////////////////

                /// <summary>
                /// チェック間隔
                /// </summary>
                public DateTime nextCheckTime { get; set; }

                /// <summary>
                /// 新着件数/エラーメッセージ
                /// </summary>
                public string mail_message { get; set; }


                /// <summary>
                /// コンストラクタ
                /// </summary>
                public data()
                {
                    id = -1;
                    mail_connect_name = "";
                    mail_effective = true;
                    mail_pop3 = "";
                    mail_user = "";
                    mail_pass = "";
                    mail_check_span = 5;
                    nextCheckTime = DateTime.Now;
                    mail_uidl = "";
                    mail_message = "";
                }
            }

            public enum enum_data
            {
                id,
                mail_connect_name,
                mail_effective,
                mail_pop3,
                mail_user,
                mail_pass,
                mail_check_span,
                mail_uidl,
                mail_end
            }
        }

        /// <summary>
        /// ランチャメニュー管理クラス
        /// </summary>
        public static class Launcher
        {
            // ランチャメニューリスト保持クラス
            public class LauncherList : CollectionBase
            {
                public int Add(data d)
                {
                    return (List.Add(d));
                }

                public int IndexOf(data d)
                {
                    return (List.IndexOf(d));
                }

                public void Insert(int index, data d)
                {
                    List.Insert(index, d);
                }

                public void Remove(data d)
                {
                    List.Remove(d);
                }

                public bool Contains(data d)
                {
                    return (List.Contains(d));
                }

                public data this[int index]
                {
                    get
                    {
                        return ((data)List[index]);
                    }
                    set
                    {
                        List[index] = value;
                    }
                }

                /// <summary>
                /// 該当のランチャメニューを返却
                /// </summary>
                /// <returns></returns>
                public Launcher.data SelectId(int id)
                {
                    Launcher.data select = new Launcher.data();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.id == id)
                            {
                                select = cl;
                                break;
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// 該当のランチャメニューを返却
                /// </summary>
                /// <returns></returns>
                public Launcher.data SelectName(String name)
                {
                    Launcher.data select = new Launcher.data();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.launcher_name.ToLower() == name.ToLower())
                            {
                                select = cl;
                                break;
                            }
                        }
                    }

                    return select;
                }

                /// <summary>
                /// 親ノードをもつ該当のランチャメニューリストを返却
                /// </summary>
                /// <returns></returns>
                public LauncherList SelectChild(int parentNode)
                {
                    LauncherList select = new LauncherList();
                    lock (this)
                    {
                        foreach (data cl in this)
                        {
                            if (cl.launcher_parent_node == parentNode)
                            {
                                select.Add(cl);
                            }
                        }
                    }

                    return select;
                }
            }

            // ランチャメニュー構造
            public class data
            {
               /// <summary>
                /// プライマリキー
                /// </summary>
                public Int32 id;

                /// <summary>
                /// 表示順序
                /// </summary>
                public Int32 launcher_order { get; set; }

                /// <summary>
                /// 表示名(見出し)
                /// </summary>
                public string launcher_name { get; set; }

                /// <summary>
                /// 起動パス
                /// </summary>
                public string launcher_path { get; set; }

                /// <summary>
                /// 起動パラメータ
                /// </summary>
                public string launcher_parameter { get; set; }

                /// <summary>
                /// ランチャの種類
                /// </summary>
                public bool launcher_type { get; set; }

                /// <summary>
                /// 親ノードのID。0はルートノード
                /// </summary>
                public Int32 launcher_parent_node { get; set; }
            }

            public enum enum_data
            {
                id,
                launcher_order,
                launcher_name,
                launcher_path,
                launcher_parameter,
                launcher_type,
                launcher_parent_node,
                launcher_end
            }
        }


        /// <summary>
        /// S.w.S.DB最新バージョンへの移行マネージャ
        /// </summary>
        public static class UpdateManager
        {
            /// <summary>
            /// DB更新処理
            /// </summary>
            /// <param name="sqliteAccess"></param>
            public static void Update(SQLiteAccess sqliteAccess)
            {
                string version = "";
                string[] vers = { "", "" };
                double dblVers = 0;
                char[] sep = { ' ' };
                bool bUpdate = false;

                // DBVersion読み出し
                string strSQL = "select version from setting";

                SQLiteDataReader result = sqliteAccess.select(strSQL);
                if (result.Read())
                {
                    // バージョン表記より数値部取出し
                    // α、βの表記は確定ではないという意味のみとする
                    version = result.GetString(0);
                    vers = version.Split(sep);
                    dblVers = double.Parse(vers[0]);
                }
                result.Close();

                // 更新チェック
                // プログラム全体のバージョンコントロールはここで行う
                // PG更新時にリビジョンが必ず上がる運用とする
                // 下記ロジックの最下部バージョン変更ロジックに記述される内容が
                // 　最新バージョンとなる
                //
                if (dblVers < 0.02)
                {
                    // version 0.02 β
                    version = "0.02 β";

                    // 設定テーブルカラム追加
                    strSQL = "alter table setting add main_no_timedisplay BOOLEAN DEFAULT '0'";
                    sqliteAccess.execDDL(strSQL);

                    bUpdate = true;
                }
                if (dblVers < 0.03)
                {
                    // version 0.03 β
                    version = "0.03 β";

                    // このアップデートではDB更新処理なし。

                    bUpdate = true;
                }
                if (dblVers < 0.04)
                {
                    // version 0.04 β
                    version = "0.04 β";

                    strSQL = "select * from contents_list ";
                    result = sqliteAccess.select(strSQL);
                    if (result.Read())
                    {
                        // DBのFIELD数に相違があった場合(ver 0.03 βのDBを新規で作成していない場合)のみ
                        // 本来は22必要
                        if (result.FieldCount == 19)
                        {
                            result.Close();
                            strSQL = "alter table contents_list add [display_fontname_stickies] TEXT";
                            sqliteAccess.execDDL(strSQL);
                            strSQL = "update contents_list set display_fontname_stickies = ''";
                            sqliteAccess.update(strSQL);
                            strSQL = "alter table contents_list add [display_fontsize_stickies] INTEGER";
                            sqliteAccess.execDDL(strSQL);
                            strSQL = "update contents_list set display_fontsize_stickies = 0";
                            sqliteAccess.update(strSQL);
                            strSQL = "alter table contents_list add [display_fontcolor_stickies] TEXT";
                            sqliteAccess.execDDL(strSQL);
                            strSQL = "update contents_list set display_fontcolor_stickies = ''";
                            sqliteAccess.update(strSQL);
                        }
                        else
                        {
                            result.Close();
                        }
                    }
                    else
                    {
                        result.Close();
                    }

                    // コンテンツテーブルカラム追加・削除
                    strSQL = "update contents_list set display_fontcolor_stickies = ''";
                    sqliteAccess.update(strSQL);
                    strSQL = "alter table contents_list add [display_font_stickies] TEXT";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "update contents_list set display_font_stickies = ''";
                    sqliteAccess.update(strSQL);
                    // 現在sqlite3 はテーブルの削除は出来ないとの事
                    //strSQL = "alter table contents_list drop display_fontname_stickies";
                    //sqliteAccess.execDDL(strSQL);
                    //strSQL = "alter table contents_list drop display_fontsize_stickies";
                    //sqliteAccess.execDDL(strSQL);

                    bUpdate = true;
                }
                if (dblVers < 0.05)
                {
                    // version 0.05 β
                    version = "0.05 β";

                    // 設定テーブルカラム追加
                    strSQL = "alter table setting add main_desktop_access BOOLEAN DEFAULT '0'";
                    sqliteAccess.execDDL(strSQL);

                    bUpdate = true;
                }
                if (dblVers < 0.06)
                {
                    // version 0.06 β
                    version = "0.06 β";

                    // メールチェック設定テーブル追加
                    strSQL = "create table mail_list ([id] INTEGER PRIMARY KEY AUTOINCREMENT,"
                        + "[mail_connect_name] TEXT,"
                        + "[mail_effective] BOOLEAN NOT NULL DEFAULT '1',"
                        + "[mail_pop3] TEXT,"
                        + "[mail_user] TEXT,"
                        + "[mail_pass] TEXT,"
                        + "[mail_check_span] INTEGER DEFAULT '5',"
                        + "[mail_last_uidl] TEXT"
                        + ")";
                    sqliteAccess.execDDL(strSQL);

                    // 設定テーブルカラム追加
                    strSQL = "alter table setting add [main_desktop_snapping] BOOLEAN DEFAULT '0'";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_desktop_snapping_band] INTEGER DEFAULT '32'";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_query_string] TEXT DEFAULT ''";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_query_object] TEXT DEFAULT '1,1,1,1,1'";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_query_expr] INTEGER NOT NULL DEFAULT '0'";
                    sqliteAccess.execDDL(strSQL);

                    bUpdate = true;
                }
                if (dblVers < 0.07)
                {
                    // version 0.07 β
                    version = "0.07 β";

                    // このアップデートではDB更新処理なし。

                    bUpdate = true;
                }
                if (dblVers < 0.08)
                {
                    // version 0.08 β
                    version = "0.08 β";

                    // コンテンツテーブルカラム追加・削除
                    strSQL = "alter table contents_list add [task_to_calendar] BOOLEAN NOT NULL DEFAULT '0'";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table contents_list add [task_trigger_type] INTEGER NOT NULL DEFAULT '0'";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table contents_list add [task_trigger_datetime] DATETIME";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table contents_list add [task_trigger_dayofweek] INTEGER NOT NULL DEFAULT '0'";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table contents_list add [task_trigger_span] INTEGER NOT NULL DEFAULT '0'";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table contents_list add [task_range] BOOLEAN NOT NULL DEFAULT '0'";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table contents_list add [task_range_startdate] DATETIME";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table contents_list add [task_range_enddate] DATETIME";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table contents_list add [task_type] INTEGER NOT NULL DEFAULT '0'";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "update contents_list set task_trigger_datetime = date_time";
                    sqliteAccess.update(strSQL);
                    strSQL = "update contents_list set task_range_startdate = date_time";
                    sqliteAccess.update(strSQL);
                    strSQL = "update contents_list set task_range_enddate = date_time";
                    sqliteAccess.update(strSQL);
                    strSQL = "update contents_list set task_trigger_type = 2 where data_type = 0 and task_trigger_type = 0";
                    sqliteAccess.update(strSQL);

                    bUpdate = true;
                }
                if (dblVers < 0.09)
                {
                    // version 0.09 β
                    version = "0.09 β";

                    strSQL = "update contents_list set task_trigger_datetime = date_time where task_trigger_datetime is null";
                    sqliteAccess.update(strSQL);
                    strSQL = "update contents_list set task_range_startdate = date_time where task_range_startdate is null";
                    sqliteAccess.update(strSQL);
                    strSQL = "update contents_list set task_range_enddate = date_time where task_range_enddate is null";
                    sqliteAccess.update(strSQL);

                    bUpdate = true;
                }
                if (dblVers < 0.10)
                {
                    // version 0.10 β
                    version = "0.10 β";

                    // 設定テーブルカラム追加
                    strSQL = "alter table setting add [main_mailchecker_blinkcancel] BOOLEAN DEFAULT '0'";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_hotkey_000] TEXT DEFAULT ''";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_hotkey_001] TEXT DEFAULT ''";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_hotkey_002] TEXT DEFAULT ''";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_hotkey_003] TEXT DEFAULT ''";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_hotkey_004] TEXT DEFAULT ''";
                    sqliteAccess.execDDL(strSQL);

                    // ランチャメニュー設定テーブル追加
                    strSQL = "CREATE TABLE [launcher_list] ([id] INTEGER PRIMARY KEY AUTOINCREMENT,"
                        + "[launcher_order] INTEGER NOT NULL DEFAULT '0',"
                        + "[launcher_name] TEXT,"
                        + "[launcher_path] TEXT,"
                        + "[launcher_parameter] TEXT,"
                        + "[launcher_type] BOOLEAN DEFAULT '0',"
                        + "[launcher_parent_node] INTEGER NOT NULL DEFAULT '0'"
                        + ")";
                    sqliteAccess.execDDL(strSQL);

                    bUpdate = true;
                }
                if (dblVers < 0.11)
                {
                    // version 0.11 β
                    version = "0.11 β";

                    // 設定テーブルカラム追加
                    strSQL = "alter table setting add [main_task_display_target] TEXT DEFAULT '1,1,1,1,1,1'";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "update setting set main_task_display_target = '1,1,1,1,1,1'";
                    sqliteAccess.update(strSQL);

                    bUpdate = true;
                }
                if (dblVers < 0.12)
                {
                    // version 0.12 β
                    version = "0.12 β";

                    // 設定テーブルカラム追加
                    strSQL = "alter table setting add [main_hotkey_005] TEXT DEFAULT ''";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_hotkey_006] TEXT DEFAULT ''";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_hotkey_007] TEXT DEFAULT ''";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_hotkey_008] TEXT DEFAULT ''";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_hotkey_009] TEXT DEFAULT ''";
                    sqliteAccess.execDDL(strSQL);
                    strSQL = "alter table setting add [main_default_backcolor_stickies] TEXT NOT NULL DEFAULT '230, 245, 230'";
                    sqliteAccess.execDDL(strSQL);

                    bUpdate = true;
                }

                // 更新アリならバージョン更新
                if (bUpdate == true)
                {
                    strSQL = "update setting set version = '" + version + "' ";
                    sqliteAccess.update(strSQL);
                }
            }
        }
    }
}
