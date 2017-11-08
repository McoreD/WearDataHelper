#define MyAppName "WearDataHelper"
#define MyAppParentDir "WearDataHelper\bin\Debug"
#define MyAppPath MyAppParentDir + "\WearDataHelper.exe"
#dim Version[4]
#expr ParseVersion(MyAppPath, Version[0], Version[1], Version[2], Version[3])
#define MyAppVersion Str(Version[0]) + "." + Str(Version[1]) + "." + Str(Version[2])
#define MyAppPublisher "Malith de Silva"
#define MyAppMyAppName "WearDataHelper.exe"

[Setup]
AllowNoIcons=true
AppMutex=Global\2178a79c-fce2-4e31-ace2-f095c95871b0
AppId={#MyAppName}
AppName={#MyAppName}
AppPublisher={#MyAppPublisher}
; AppPublisherURL=https://github.com/McoreD/WearDataHelper
; AppSupportURL=https://github.com/McoreD/WearDataHelper/issues
; AppUpdatesURL=https://github.com/McoreD/WearDataHelper/releases
AppVerName={#MyAppName} {#MyAppVersion}
AppVersion={#MyAppVersion}
ArchitecturesAllowed=x86 x64 ia64
ArchitecturesInstallIn64BitMode=x64 ia64
Compression=lzma/ultra64
CreateAppDir=true
DefaultDirName={localappdata}\{#MyAppName}
DefaultGroupName={#MyAppName}
DirExistsWarning=no
InternalCompressLevel=ultra64
LanguageDetectionMethod=uilanguage
MinVersion=6
OutputBaseFilename={#MyAppName}-{#MyAppVersion}-setup
OutputDir=Output\
PrivilegesRequired=lowest
ShowLanguageDialog=auto
ShowUndisplayableLanguages=false
SignedUninstaller=false
SolidCompression=true
Uninstallable=true
UninstallDisplayIcon={app}\{#MyAppName}.exe
UsePreviousAppDir=yes
UsePreviousGroup=yes
VersionInfoCompany={#MyAppName}
VersionInfoTextVersion={#MyAppVersion}
VersionInfoVersion={#MyAppVersion}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: WearDataHelper\bin\Debug\*.exe; Excludes: *.vshost.exe; DestDir: {app}; Flags: ignoreversion
; Source: WearDataHelper\bin\Debug\*.dll; DestDir: {app}; Flags: ignoreversion
Source: WearDataHelper\bin\Debug\*.pdb; DestDir: {app}; Flags: ignoreversion

; Source: "Lib\32-bit\MediaInfo.dll"; DestDir: "{app}"; Flags: ignoreversion; Check: Not IsWin64
; Source: "Lib\64-bit\MediaInfo.dll"; DestDir: "{app}"; Flags: ignoreversion; Check: IsWin64

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppMyAppName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppMyAppName}"; Tasks: desktopicon

[Run]
Filename: {app}\{#MyAppName}.exe.; Description: {cm:LaunchProgram,WearDataHelper}; Flags: nowait postinstall skipifsilent