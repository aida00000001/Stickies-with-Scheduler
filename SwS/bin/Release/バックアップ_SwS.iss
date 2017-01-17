; -- SwS.iss --
; Stickies with Scheduler �Z�b�g�A�b�v�t�@�C���쐬�p�X�N���v�g

#define AppName "Stickies with Scheduler"
#define ExeName "SwS.exe"
#define AppVer  "0.08 ��"

[Setup]
AppName={#AppName}
AppId=http://aida.kei.no-ip.com/applictions/SwS/
AppMutex={#AppName}
AppPublisher=Aida
AppPublisherURL=http://aida.kei.no-ip.com/
AppVersion={#AppVer}
AppVerName=S.w.S. {#AppVer}
DefaultDirName={pf}\{#AppName}
DefaultGroupName={#AppName}
UninstallDisplayIcon={app}\{#ExeName}
OutputBaseFilename=SwSSetup
OutputDir=.
ShowLanguageDialog=no

[Tasks]
Name: startupicon; Description: �X�^�[�g�A�b�v�ɃV���[�g�J�b�g���쐬; GroupDescription: �X�^�[�g�A�b�v; Flags: 

[Files]
Source: .\SwS.exe; DestDir: {app}; Flags: ignoreversion
Source: .\Readme.txt; DestDir: {app}; Flags: ignoreversion
Source: .\ReleaseNote.txt; DestDir: {app}; Flags: ignoreversion
Source: .\SwS.db; DestDir: {app}; Flags: onlyifdoesntexist
Source: .\System.Data.SQLite.DLL; DestDir: {app}; Flags: ignoreversion
Source: .\UCLabel.dll; DestDir: {app}; Flags: ignoreversion

[Icons]
Name: {group}\{#AppName}; Filename: {app}\{#ExeName}; WorkingDir: {app}
Name: {group}\{#AppName}�̃A���C���X�g�[��; Filename: {uninstallexe}
Name: {userstartup}\{#AppName}; Filename: {app}\{#ExeName}; WorkingDir: {app}; Tasks: startupicon

[Languages]
Name: jp; MessagesFile: compiler:Japanese.isl
Name: en; MessagesFile: compiler:Default.isl

[Registry]
Root: HKCU; Subkey: Software\{#AppName}; Flags: uninsdeletekey

[Run]
Filename: {app}\Readme.txt; Description: README��\������; Flags: postinstall shellexec skipifsilent unchecked
Filename: {app}\{#ExeName}; Description: �A�v���P�[�V�������N������; Flags: postinstall shellexec skipifsilent
