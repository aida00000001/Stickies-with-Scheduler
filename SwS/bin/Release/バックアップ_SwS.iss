; -- SwS.iss --
; Stickies with Scheduler セットアップファイル作成用スクリプト

#define AppName "Stickies with Scheduler"
#define ExeName "SwS.exe"
#define AppVer  "0.08 β"

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
Name: startupicon; Description: スタートアップにショートカットを作成; GroupDescription: スタートアップ; Flags: 

[Files]
Source: .\SwS.exe; DestDir: {app}; Flags: ignoreversion
Source: .\Readme.txt; DestDir: {app}; Flags: ignoreversion
Source: .\ReleaseNote.txt; DestDir: {app}; Flags: ignoreversion
Source: .\SwS.db; DestDir: {app}; Flags: onlyifdoesntexist
Source: .\System.Data.SQLite.DLL; DestDir: {app}; Flags: ignoreversion
Source: .\UCLabel.dll; DestDir: {app}; Flags: ignoreversion

[Icons]
Name: {group}\{#AppName}; Filename: {app}\{#ExeName}; WorkingDir: {app}
Name: {group}\{#AppName}のアンインストール; Filename: {uninstallexe}
Name: {userstartup}\{#AppName}; Filename: {app}\{#ExeName}; WorkingDir: {app}; Tasks: startupicon

[Languages]
Name: jp; MessagesFile: compiler:Japanese.isl
Name: en; MessagesFile: compiler:Default.isl

[Registry]
Root: HKCU; Subkey: Software\{#AppName}; Flags: uninsdeletekey

[Run]
Filename: {app}\Readme.txt; Description: READMEを表示する; Flags: postinstall shellexec skipifsilent unchecked
Filename: {app}\{#ExeName}; Description: アプリケーションを起動する; Flags: postinstall shellexec skipifsilent
