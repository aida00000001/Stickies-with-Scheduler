drop table setting;
drop table holiday_master;
drop table holiday_list;
drop table contents_list;
drop table mail_list;
drop table launcher_list;

CREATE TABLE [setting] (
[id] INTEGER PRIMARY KEY AUTOINCREMENT,
[version] TEXT DEFAULT '0',--バージョン表記
[main_startUp_position_x] INTEGER DEFAULT '0',
[main_startUp_position_y] INTEGER DEFAULT '0',
[main_time_format] TEXT,
[main_date_format] TEXT,
[main_alpha] INTEGER DEFAULT '100',--1～100
[main_time_adjust_startup] BOOLEAN DEFAULT '0',--スタートアップ時時刻同期の有無
[main_time_adjust_interval] BOOLEAN DEFAULT '0',--時刻同期の有無
[main_time_adjust_interval_type] INTEGER DEFAULT '0',--時刻同期の時間間隔種別
[main_time_adjust_interval_count] INTEGER DEFAULT '0',--時刻同期の時間間隔
[main_time_adjust_ntp_server] TEXT,--NTPサーバ名
[main_inputform_startup] BOOLEAN DEFAULT '0',--当日予定アリ時入力画面同時表示有無
[main_no_timedisplay] BOOLEAN DEFAULT '0',--カレンダ現在時刻非表示設定
[main_desktop_access] BOOLEAN DEFAULT '0',--デスクトップ操作設定
[main_desktop_snapping] BOOLEAN DEFAULT '0',--デスクトップ末端吸着設定
[main_desktop_snapping_band] INTEGER DEFAULT '32',--デスクトップ末端吸着感度設定
[main_query_string] TEXT DEFAULT '',--クエリ検索文字列
[main_query_object] TEXT DEFAULT '1,1,1,1,1',--クエリ検索対象(0,0,0,0,0 : スケジュール,ToDo,メモ,付箋,アラーム)
[main_query_expr] INTEGER NOT NULL DEFAULT '0',--クエリ検索方法(0:完全一致,1:すべて含む,2:いずれか含む)
[main_mailchecker_blinkcancel] BOOLEAN DEFAULT '0',--メール新着通知を次回新着確認時に初期化するかどうか
[main_hotkey_000] TEXT DEFAULT '',--ホットキー０(ランチャメニュー用)
[main_hotkey_001] TEXT DEFAULT '',--ホットキー１(コマンドメニュー用)
[main_hotkey_002] TEXT DEFAULT '',--ホットキー２(タスク登録用)
[main_hotkey_003] TEXT DEFAULT '',--ホットキー３
[main_hotkey_004] TEXT DEFAULT '',--ホットキー４
[main_task_display_target] TEXT DEFAULT '1,1,1,1,1,1',--タスク登録ウィンドウ表示対象(0,0,0,0,0,0 : 完了,未完了,スケジュール,ToDo,メモ,アラーム)
[main_hotkey_005] TEXT DEFAULT '',--ホットキー５
[main_hotkey_006] TEXT DEFAULT '',--ホットキー６
[main_hotkey_007] TEXT DEFAULT '',--ホットキー７
[main_hotkey_008] TEXT DEFAULT '',--ホットキー８
[main_hotkey_009] TEXT DEFAULT '',--ホットキー９
[main_default_backcolor_stickies] TEXT NOT NULL DEFAULT '230, 245, 230' --付箋の初期背景表示色
);

insert into setting (
version,
main_time_format,
main_date_format,
main_time_adjust_ntp_server
) values (
'0.12 β',
'HH:mm:ss',
'yyyy/MM/dd',
''
);

CREATE TABLE [contents_list] (
[id] INTEGER PRIMARY KEY AUTOINCREMENT,--プライマリキー
[effective] BOOLEAN NOT NULL DEFAULT '1',--データの有効無効
[data_type] INTEGER NOT NULL DEFAULT '0',--登録データの種類。0:schedule, 1:ToDo, 2:Memo, 3:Stickies, 4:Alarm
[date_time] DATETIME NOT NULL,--データ登録日時
[contents] TEXT,--データ内容
[show_stickies] BOOLEAN NOT NULL DEFAULT '0',--付箋の表示可否
[backcolor_stickies] TEXT NOT NULL,--付箋の背景表示色
[point_stickies] TEXT,--付箋表示位置及び幅と高さ。X, Y, Width, Height の４項目
[time_trigger] BOOLEAN NOT NULL DEFAULT '0',--時限起動で表示可否
[time_trigger_type] INTEGER NOT NULL DEFAULT '0',--時限起動の種類。0：日付指定、1:毎時, 2:日付, 3:毎週、4：毎月
[time_trigger_datetime] DATETIME,--時限起動の日付
[time_trigger_dayofweek] INTEGER NOT NULL DEFAULT '0',--時限起動の曜日
[attach_trigger] BOOLEAN NOT NULL DEFAULT '0',--特定のウィンドウが起動されたとき(連想起動)の表示可否
[attach_trigger_title] TEXT,--特定のウィンドウが起動されたとき(連想起動)に表示するためのトリガウィンドウタイトル
[display_backimage_stickies] BOOLEAN NOT NULL DEFAULT '0',--付箋の背景画像の表示可否
[display_backimage_type_stickies] INTEGER NOT NULL DEFAULT '0',--付箋の背景画像の表示方法。0:なし 1:並べる, 2:中央, 3:フィット, 4:拡大縮小
[display_backimage_path_stickies] TEXT,--付箋の背景画像のパス
[display_backimage_region_stickies] BOOLEAN NOT NULL DEFAULT '0',--付箋の背景画像のリージョン指定
[alpha_stickies] INTEGER NOT NULL DEFAULT '100',--付箋の透過率
[display_fontname_stickies] TEXT,--付箋のフォント名
[display_fontsize_stickies] INTEGER,--付箋のフォントサイズ
[display_fontcolor_stickies] TEXT,--付箋のフォントカラー
[display_font_stickies] TEXT,--付箋のフォント構造体
[task_to_calendar] BOOLEAN NOT NULL DEFAULT '0',--タスク(4:Alarm)の繰り返し表示可否(alarmのカレンダへ表示は当フィールドの設定が必要)
[task_trigger_type] INTEGER NOT NULL DEFAULT '0',--タスク繰り返し表示の種類。0：日付指定、1:毎時, 2:日付, 3:毎週、4：毎月
[task_trigger_datetime] DATETIME,--タスク繰り返し表示の日付
[task_trigger_dayofweek] INTEGER NOT NULL DEFAULT '0',--タスク繰り返し表示の曜日
[task_trigger_span] INTEGER NOT NULL DEFAULT '0',--タスク繰り返し表示の時間間隔。0:一回のみ, 1:5分間隔, 2:10分間隔, 3:15分間隔, 4:30分間隔
[task_range] BOOLEAN NOT NULL DEFAULT '0',--タスク繰り返し表示期間設定有無
[task_range_startdate] DATETIME,--タスク繰り返し表示期間開始日
[task_range_enddate] DATETIME,--タスク繰り返し表示期間終了日
[task_type] INTEGER NOT NULL DEFAULT '0'--タスク(4:alarm)の種類。0:message, 1:command
);

CREATE TABLE [holiday_master] (
[id] INTEGER PRIMARY KEY AUTOINCREMENT,--プライマリキー
[holiday_type] INTEGER NOT NULL,--休日種別。0:fix(固定。fix,名称,月,日), 1:week(xxxに指定された週とyyyに指定された曜日を有効年と有効判定子により判定。week,名称,月,週,曜日,有効年,有効判定子(<,>,<=,>=,==,><)), 2:equinox(xxxは月を表す。現在は春分と秋分の日が対象。equinox,名称,月)
[holiday_name] TEXT,--休日名
[third] INTEGER,--月
[fourth] INTEGER,--fix:日, week:週
[fifth] INTEGER,--week:曜日
[sixth] INTEGER,--week:有効年
[seventh] INTEGER--week:有効判定子。sixth(有効年)より小さい、大きい、などで判定する。 0:<, 1:>,2:<=,3:>=,4:==,5:><
);

CREATE TABLE [holiday_list] (
[holiday_date] DATETIME NOT NULL UNIQUE,--祝祭日日付
[holiday_type] INTEGER NOT NULL,--祝祭日種別
[holiday_name] TEXT--祝祭日名称
);

insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("1","0","元旦","0","0","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("2","0","成人の日","0","14","-1","2000","0");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("3","1","成人の日","0","1","1","2000","3");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("4","0","建国記念の日","1","10","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("5","2","春分の日","2","-1","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("6","0","みどりの日","3","28","-1","2007","0");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("7","0","昭和の日","3","28","-1","2007","3");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("8","0","憲法記念日","4","2","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("9","0","みどりの日","4","3","-1","2007","3");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("10","0","こどもの日","4","4","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("11","1","海の日","6","2","1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("12","1","敬老の日","8","2","1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("13","2","秋分の日","8","-1","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("14","0","体育の日","9","9","-1","2000","0");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("15","1","体育の日","9","1","1","2000","3");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("16","0","文化の日","10","2","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("17","0","勤労感謝の日","10","22","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("18","0","天皇誕生日","11","22","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("19","0","皇太子明仁親王の結婚の儀","3","9","-1","1959","4");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("20","0","昭和天皇の大喪の礼","1","23","-1","1989","4");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("21","0","即位礼正殿の儀","10","11","-1","1990","4");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("22","0","皇太子徳仁親王の結婚の儀","5","8","-1","1993","4");

CREATE TABLE [mail_list] (
[id] INTEGER PRIMARY KEY AUTOINCREMENT,--プライマリキー
[mail_connect_name] TEXT,--接続名(見出し)
[mail_effective] BOOLEAN NOT NULL DEFAULT '1',--データの有効無効
[mail_pop3] TEXT,--POP3サーバ
[mail_user] TEXT,--ユーザ
[mail_pass] TEXT,--パスワード
[mail_check_span] INTEGER DEFAULT '5',--メールチェック間隔(分)
[mail_last_uidl] TEXT--最終チェック時UIDL(メールのユニークID)
);

CREATE TABLE [launcher_list] (
[id] INTEGER PRIMARY KEY AUTOINCREMENT,--プライマリキー
[launcher_order] INTEGER NOT NULL DEFAULT '0',--表示順序
[launcher_name] TEXT,--表示名(見出し)
[launcher_path] TEXT,--起動パス
[launcher_parameter] TEXT,--起動パラメータ
[launcher_type] BOOLEAN DEFAULT '0',--ランチャの種類。0(false)：フォルダ、1(true) :実行対象
[launcher_parent_node] INTEGER NOT NULL DEFAULT '0'--親ノードのID。0はルートノード
);

