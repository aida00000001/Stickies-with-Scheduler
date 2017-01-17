drop table setting;
drop table holiday_master;
drop table holiday_list;
drop table contents_list;
drop table mail_list;
drop table launcher_list;

CREATE TABLE [setting] (
[id] INTEGER PRIMARY KEY AUTOINCREMENT,
[version] TEXT DEFAULT '0',--�o�[�W�����\�L
[main_startUp_position_x] INTEGER DEFAULT '0',
[main_startUp_position_y] INTEGER DEFAULT '0',
[main_time_format] TEXT,
[main_date_format] TEXT,
[main_alpha] INTEGER DEFAULT '100',--1�`100
[main_time_adjust_startup] BOOLEAN DEFAULT '0',--�X�^�[�g�A�b�v�����������̗L��
[main_time_adjust_interval] BOOLEAN DEFAULT '0',--���������̗L��
[main_time_adjust_interval_type] INTEGER DEFAULT '0',--���������̎��ԊԊu���
[main_time_adjust_interval_count] INTEGER DEFAULT '0',--���������̎��ԊԊu
[main_time_adjust_ntp_server] TEXT,--NTP�T�[�o��
[main_inputform_startup] BOOLEAN DEFAULT '0',--�����\��A�������͉�ʓ����\���L��
[main_no_timedisplay] BOOLEAN DEFAULT '0',--�J�����_���ݎ�����\���ݒ�
[main_desktop_access] BOOLEAN DEFAULT '0',--�f�X�N�g�b�v����ݒ�
[main_desktop_snapping] BOOLEAN DEFAULT '0',--�f�X�N�g�b�v���[�z���ݒ�
[main_desktop_snapping_band] INTEGER DEFAULT '32',--�f�X�N�g�b�v���[�z�����x�ݒ�
[main_query_string] TEXT DEFAULT '',--�N�G������������
[main_query_object] TEXT DEFAULT '1,1,1,1,1',--�N�G�������Ώ�(0,0,0,0,0 : �X�P�W���[��,ToDo,����,�t�,�A���[��)
[main_query_expr] INTEGER NOT NULL DEFAULT '0',--�N�G���������@(0:���S��v,1:���ׂĊ܂�,2:�����ꂩ�܂�)
[main_mailchecker_blinkcancel] BOOLEAN DEFAULT '0',--���[���V���ʒm������V���m�F���ɏ��������邩�ǂ���
[main_hotkey_000] TEXT DEFAULT '',--�z�b�g�L�[�O(�����`�����j���[�p)
[main_hotkey_001] TEXT DEFAULT '',--�z�b�g�L�[�P(�R�}���h���j���[�p)
[main_hotkey_002] TEXT DEFAULT '',--�z�b�g�L�[�Q(�^�X�N�o�^�p)
[main_hotkey_003] TEXT DEFAULT '',--�z�b�g�L�[�R
[main_hotkey_004] TEXT DEFAULT '',--�z�b�g�L�[�S
[main_task_display_target] TEXT DEFAULT '1,1,1,1,1,1',--�^�X�N�o�^�E�B���h�E�\���Ώ�(0,0,0,0,0,0 : ����,������,�X�P�W���[��,ToDo,����,�A���[��)
[main_hotkey_005] TEXT DEFAULT '',--�z�b�g�L�[�T
[main_hotkey_006] TEXT DEFAULT '',--�z�b�g�L�[�U
[main_hotkey_007] TEXT DEFAULT '',--�z�b�g�L�[�V
[main_hotkey_008] TEXT DEFAULT '',--�z�b�g�L�[�W
[main_hotkey_009] TEXT DEFAULT '',--�z�b�g�L�[�X
[main_default_backcolor_stickies] TEXT NOT NULL DEFAULT '230, 245, 230' --�tⳂ̏����w�i�\���F
);

insert into setting (
version,
main_time_format,
main_date_format,
main_time_adjust_ntp_server
) values (
'0.12 ��',
'HH:mm:ss',
'yyyy/MM/dd',
''
);

CREATE TABLE [contents_list] (
[id] INTEGER PRIMARY KEY AUTOINCREMENT,--�v���C�}���L�[
[effective] BOOLEAN NOT NULL DEFAULT '1',--�f�[�^�̗L������
[data_type] INTEGER NOT NULL DEFAULT '0',--�o�^�f�[�^�̎�ށB0:schedule, 1:ToDo, 2:Memo, 3:Stickies, 4:Alarm
[date_time] DATETIME NOT NULL,--�f�[�^�o�^����
[contents] TEXT,--�f�[�^���e
[show_stickies] BOOLEAN NOT NULL DEFAULT '0',--�tⳂ̕\����
[backcolor_stickies] TEXT NOT NULL,--�tⳂ̔w�i�\���F
[point_stickies] TEXT,--�tⳕ\���ʒu�y�ѕ��ƍ����BX, Y, Width, Height �̂S����
[time_trigger] BOOLEAN NOT NULL DEFAULT '0',--�����N���ŕ\����
[time_trigger_type] INTEGER NOT NULL DEFAULT '0',--�����N���̎�ށB0�F���t�w��A1:����, 2:���t, 3:���T�A4�F����
[time_trigger_datetime] DATETIME,--�����N���̓��t
[time_trigger_dayofweek] INTEGER NOT NULL DEFAULT '0',--�����N���̗j��
[attach_trigger] BOOLEAN NOT NULL DEFAULT '0',--����̃E�B���h�E���N�����ꂽ�Ƃ�(�A�z�N��)�̕\����
[attach_trigger_title] TEXT,--����̃E�B���h�E���N�����ꂽ�Ƃ�(�A�z�N��)�ɕ\�����邽�߂̃g���K�E�B���h�E�^�C�g��
[display_backimage_stickies] BOOLEAN NOT NULL DEFAULT '0',--�tⳂ̔w�i�摜�̕\����
[display_backimage_type_stickies] INTEGER NOT NULL DEFAULT '0',--�tⳂ̔w�i�摜�̕\�����@�B0:�Ȃ� 1:���ׂ�, 2:����, 3:�t�B�b�g, 4:�g��k��
[display_backimage_path_stickies] TEXT,--�tⳂ̔w�i�摜�̃p�X
[display_backimage_region_stickies] BOOLEAN NOT NULL DEFAULT '0',--�tⳂ̔w�i�摜�̃��[�W�����w��
[alpha_stickies] INTEGER NOT NULL DEFAULT '100',--�tⳂ̓��ߗ�
[display_fontname_stickies] TEXT,--�tⳂ̃t�H���g��
[display_fontsize_stickies] INTEGER,--�tⳂ̃t�H���g�T�C�Y
[display_fontcolor_stickies] TEXT,--�tⳂ̃t�H���g�J���[
[display_font_stickies] TEXT,--�tⳂ̃t�H���g�\����
[task_to_calendar] BOOLEAN NOT NULL DEFAULT '0',--�^�X�N(4:Alarm)�̌J��Ԃ��\����(alarm�̃J�����_�֕\���͓��t�B�[���h�̐ݒ肪�K�v)
[task_trigger_type] INTEGER NOT NULL DEFAULT '0',--�^�X�N�J��Ԃ��\���̎�ށB0�F���t�w��A1:����, 2:���t, 3:���T�A4�F����
[task_trigger_datetime] DATETIME,--�^�X�N�J��Ԃ��\���̓��t
[task_trigger_dayofweek] INTEGER NOT NULL DEFAULT '0',--�^�X�N�J��Ԃ��\���̗j��
[task_trigger_span] INTEGER NOT NULL DEFAULT '0',--�^�X�N�J��Ԃ��\���̎��ԊԊu�B0:���̂�, 1:5���Ԋu, 2:10���Ԋu, 3:15���Ԋu, 4:30���Ԋu
[task_range] BOOLEAN NOT NULL DEFAULT '0',--�^�X�N�J��Ԃ��\�����Ԑݒ�L��
[task_range_startdate] DATETIME,--�^�X�N�J��Ԃ��\�����ԊJ�n��
[task_range_enddate] DATETIME,--�^�X�N�J��Ԃ��\�����ԏI����
[task_type] INTEGER NOT NULL DEFAULT '0'--�^�X�N(4:alarm)�̎�ށB0:message, 1:command
);

CREATE TABLE [holiday_master] (
[id] INTEGER PRIMARY KEY AUTOINCREMENT,--�v���C�}���L�[
[holiday_type] INTEGER NOT NULL,--�x����ʁB0:fix(�Œ�Bfix,����,��,��), 1:week(xxx�Ɏw�肳�ꂽ�T��yyy�Ɏw�肳�ꂽ�j����L���N�ƗL������q�ɂ�蔻��Bweek,����,��,�T,�j��,�L���N,�L������q(<,>,<=,>=,==,><)), 2:equinox(xxx�͌���\���B���݂͏t���ƏH���̓����ΏہBequinox,����,��)
[holiday_name] TEXT,--�x����
[third] INTEGER,--��
[fourth] INTEGER,--fix:��, week:�T
[fifth] INTEGER,--week:�j��
[sixth] INTEGER,--week:�L���N
[seventh] INTEGER--week:�L������q�Bsixth(�L���N)��菬�����A�傫���A�ȂǂŔ��肷��B 0:<, 1:>,2:<=,3:>=,4:==,5:><
);

CREATE TABLE [holiday_list] (
[holiday_date] DATETIME NOT NULL UNIQUE,--�j�Փ����t
[holiday_type] INTEGER NOT NULL,--�j�Փ����
[holiday_name] TEXT--�j�Փ�����
);

insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("1","0","���U","0","0","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("2","0","���l�̓�","0","14","-1","2000","0");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("3","1","���l�̓�","0","1","1","2000","3");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("4","0","�����L�O�̓�","1","10","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("5","2","�t���̓�","2","-1","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("6","0","�݂ǂ�̓�","3","28","-1","2007","0");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("7","0","���a�̓�","3","28","-1","2007","3");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("8","0","���@�L�O��","4","2","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("9","0","�݂ǂ�̓�","4","3","-1","2007","3");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("10","0","���ǂ��̓�","4","4","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("11","1","�C�̓�","6","2","1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("12","1","�h�V�̓�","8","2","1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("13","2","�H���̓�","8","-1","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("14","0","�̈�̓�","9","9","-1","2000","0");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("15","1","�̈�̓�","9","1","1","2000","3");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("16","0","�����̓�","10","2","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("17","0","�ΘJ���ӂ̓�","10","22","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("18","0","�V�c�a����","11","22","-1","2009","-1");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("19","0","�c���q���m�e���̌����̋V","3","9","-1","1959","4");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("20","0","���a�V�c�̑�r�̗�","1","23","-1","1989","4");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("21","0","���ʗ琳�a�̋V","10","11","-1","1990","4");
insert into holiday_master (id,holiday_type,holiday_name,third,fourth,fifth,sixth,seventh)
 values("22","0","�c���q���m�e���̌����̋V","5","8","-1","1993","4");

CREATE TABLE [mail_list] (
[id] INTEGER PRIMARY KEY AUTOINCREMENT,--�v���C�}���L�[
[mail_connect_name] TEXT,--�ڑ���(���o��)
[mail_effective] BOOLEAN NOT NULL DEFAULT '1',--�f�[�^�̗L������
[mail_pop3] TEXT,--POP3�T�[�o
[mail_user] TEXT,--���[�U
[mail_pass] TEXT,--�p�X���[�h
[mail_check_span] INTEGER DEFAULT '5',--���[���`�F�b�N�Ԋu(��)
[mail_last_uidl] TEXT--�ŏI�`�F�b�N��UIDL(���[���̃��j�[�NID)
);

CREATE TABLE [launcher_list] (
[id] INTEGER PRIMARY KEY AUTOINCREMENT,--�v���C�}���L�[
[launcher_order] INTEGER NOT NULL DEFAULT '0',--�\������
[launcher_name] TEXT,--�\����(���o��)
[launcher_path] TEXT,--�N���p�X
[launcher_parameter] TEXT,--�N���p�����[�^
[launcher_type] BOOLEAN DEFAULT '0',--�����`���̎�ށB0(false)�F�t�H���_�A1(true) :���s�Ώ�
[launcher_parent_node] INTEGER NOT NULL DEFAULT '0'--�e�m�[�h��ID�B0�̓��[�g�m�[�h
);

