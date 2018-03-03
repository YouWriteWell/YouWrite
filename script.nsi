; -------------------------------
; Start
 
 
  !define MUI_PRODUCT "YouWrite"
  !define MUI_FILE "savefile"
  !define MUI_VERSION "1.0.0.1"
  !define MUI_BRANDINGTEXT "YouWrite Ver. 1.0.0.1"
  !define MUI_PUBLISHER "YouWrite"
  !define MUI_WEB_SITE "http://www.YouWrite.CC"
  !define MUI_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\YouWrite.exe"
  !define MUI_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${MUI_PRODUCT}"
  !define MUI_UNINST_ROOT_KEY "HKLM"
  
  
  CRCCheck On
 
  ; Bij deze moeten we waarschijnlijk een absoluut pad gaan gebruiken
  ; dit moet effe uitgetest worden.
  !include "${NSISDIR}\Contrib\Modern UI\System.nsh"
 
 ;Include Modern UI
  !include "MUI.nsh"
;--------------------------------
;Ã‰nclude MUI_EXTRAPAGES header
  !include "MUI_EXTRAPAGES.nsh"

  
 
 
 
;---------------------------------
;General
  Name "${MUI_PRODUCT}"
  OutFile "dist\YouWrite.exe"
  Caption "${MUI_PRODUCT}"
  ShowInstDetails "nevershow"
  ShowUninstDetails "nevershow"
  SetCompressor "bzip2"
	
  !define MUI_ICON "Youwrite\bin\Debug\favicon.ico"
  !define MUI_UNICON "Youwrite\bin\Debug\favicon.ico"
  !define MUI_SPECIALBITMAP "Bitmap.bmp"
 
 
;--------------------------------
;Folder selection page
 
  InstallDir "$PROGRAMFILES\${MUI_PRODUCT}"
 
 
;--------------------------------
;Modern UI Configuration
 
  !define MUI_WELCOMEPAGE  
  !define MUI_LICENSEPAGE
  !define MUI_DIRECTORYPAGE
  !define MUI_ABORTWARNING
  !define MUI_UNINSTALLER
  !define MUI_UNCONFIRMPAGE
  !define MUI_FINISHPAGE  
 
 

 
 
;-------------------------------- 
;Modern UI System
 
; !insertmacro MUI_SYSTEM 
 
 
;--------------------------------
;Data
 
  ;LicenseData "license.txt"
 ;!insertmacro MUI_PAGE_LICENSE "license.txt"
 
 
 
;Installer Pages
  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "license.txt"
 
;Add the install read me page
  !insertmacro MUI_PAGE_README "Changelog.txt"
 
  !insertmacro MUI_PAGE_COMPONENTS
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  !define MUI_FINISHPAGE_RUN_FUNCTION "LaunchLink"
  !insertmacro MUI_PAGE_FINISH
 
;--------------------------------
;Uninstaller Pages

#!define MUI_WELCOMEPAGE_TEXT "New text goes here"


!insertmacro MUI_UNPAGE_WELCOME 
!define MUI_PAGE_CUSTOMFUNCTION_SHOW un.MyWelcomeShowCallback
;Add the uninstall read me page
  !insertmacro MUI_UNPAGE_README "License.txt"
 
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  ;!insertmacro MUI_UNPAGE_FINISH
  
 

 
;--------------------------------
;Language
 
  !insertmacro MUI_LANGUAGE "English" 
;-------------------------------- 


;  Installer properties
VIProductVersion "4.7.0.1"  
VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductName" "${MUI_PRODUCT}" 
VIAddVersionKey /LANG=${LANG_ENGLISH} "Comments" "YouWrite. Copyright (c) 2018. http://www.YouWrite.cc/"  
VIAddVersionKey /LANG=${LANG_ENGLISH} "CompanyName" "${MUI_PRODUCT}"  
VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalCopyright" "Copyright (c) 2018 ${MUI_PRODUCT}" 
VIAddVersionKey /LANG=${LANG_ENGLISH} "FileDescription" "${MUI_PRODUCT}" 
VIAddVersionKey /LANG=${LANG_ENGLISH} "FileVersion" "${MUI_VERSION}" 
VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductVersion" "${MUI_VERSION}" 
VIAddVersionKey /LANG=${LANG_ENGLISH} "InternalName" "YouWrite.exe"
VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalTrademarks" "Copyright (c) 2018 ${MUI_PRODUCT}" 
VIAddVersionKey /LANG=${LANG_ENGLISH} "OriginalFilename" "YouWrite.exe"



;Installer Sections     
Section "YouWrite" Section01
 
SetOutPath "$INSTDIR"
  SetOverwrite try
  File "Youwrite\bin\Debug\AutocompleteMenu.dll"
  File "Youwrite\bin\Debug\AutoUpdater.NET.dll"
  File "Youwrite\bin\Debug\AutoUpdater.NET.pdb"
  File "Youwrite\bin\Debug\AutoUpdater.NET.xml"
  File "Youwrite\bin\Debug\bcmail-jdk15-1.44.dll"
  File "Youwrite\bin\Debug\bcprov-jdk15-1.44.dll"
  File "Youwrite\bin\Debug\categories.db"
  File "Youwrite\bin\Debug\commons-logging.dll"
  SetOutPath "$INSTDIR\da"
  File "Youwrite\bin\Debug\da\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR\de"
  File "Youwrite\bin\Debug\de\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR"
  File "Youwrite\bin\Debug\EnglishSD.nbin"
  File "Youwrite\bin\Debug\EnglishTok.nbin"
  SetOutPath "$INSTDIR\es"
  File "Youwrite\bin\Debug\es\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR"
  File "Youwrite\bin\Debug\favicon.ico"
  File "Youwrite\bin\Debug\fontbox-1.7.0.dll"
  File "Youwrite\bin\Debug\fontbox-1.8.4.dll"
  File "Youwrite\bin\Debug\fontbox-1.8.9.dll"
  SetOutPath "$INSTDIR\fr"
  File "Youwrite\bin\Debug\fr\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR"
  File "Youwrite\bin\Debug\ICSharpCode.SharpZipLib.dll"
  File "Youwrite\bin\Debug\ICU4NET.dll"
  File "Youwrite\bin\Debug\ICU4NETExtension.dll"
  File "Youwrite\bin\Debug\icuio42.dll"
  File "Youwrite\bin\Debug\icule42.dll"
  File "Youwrite\bin\Debug\iculx42.dll"
  File "Youwrite\bin\Debug\ikvm-native-win32-x64.dll"
  File "Youwrite\bin\Debug\ikvm-native-win32-x86.dll"
  File "Youwrite\bin\Debug\IKVM.AWT.WinForms.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Beans.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Charsets.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Corba.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Core.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Jdbc.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Management.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Media.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Misc.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Naming.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Remoting.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Security.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.SwingAWT.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Text.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Tools.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.Util.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.XML.API.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.XML.Bind.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.XML.Crypto.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.XML.Parse.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.XML.Transform.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.XML.WebServices.dll"
  File "Youwrite\bin\Debug\IKVM.OpenJDK.XML.XPath.dll"
  File "Youwrite\bin\Debug\IKVM.Reflection.dll"
  File "Youwrite\bin\Debug\IKVM.Runtime.dll"
  File "Youwrite\bin\Debug\IKVM.Runtime.JNI.dll"
  File "Youwrite\bin\Debug\images.jpeg"
  File "Youwrite\bin\Debug\images.jpg"
  File "Youwrite\bin\Debug\index.jpeg"
  SetOutPath "$INSTDIR\it"
  File "Youwrite\bin\Debug\it\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR\ja-JP"
  File "Youwrite\bin\Debug\ja-JP\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR"
  File "Youwrite\bin\Debug\jempbox-1.8.9.dll"
  File "Youwrite\bin\Debug\junit.dll"
  SetOutPath "$INSTDIR\ko"
  File "Youwrite\bin\Debug\ko\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR"
  File "Youwrite\bin\Debug\model.db"
  SetOutPath "$INSTDIR\nl"
  File "Youwrite\bin\Debug\nl\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR"
  File "Youwrite\bin\Debug\OpenNLP.dll"
  File "Youwrite\bin\Debug\pdf.jpg"
  File "Youwrite\bin\Debug\Pdf2Text.vshost.exe.manifest"
  File "Youwrite\bin\Debug\pdfbox-1.8.9.dll"
  SetOutPath "$INSTDIR\pl"
  File "Youwrite\bin\Debug\pl\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR\pt"
  File "Youwrite\bin\Debug\pt\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR\ru"
  File "Youwrite\bin\Debug\ru\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR"
  File "Youwrite\bin\Debug\SharpEntropy.dll"
  File "Youwrite\bin\Debug\SharpEntropySqlite.dll"
  File "Youwrite\bin\Debug\SQLite.Designer.dll"
  File "Youwrite\bin\Debug\SQLite.Interop.dll"
  File "Youwrite\bin\Debug\sqlite3.dll"
  File "Youwrite\bin\Debug\StringParser.dll"
  SetOutPath "$INSTDIR\sv"
  File "Youwrite\bin\Debug\sv\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR"
  File "Youwrite\bin\Debug\System.Data.SQLite.dll"
  File "Youwrite\bin\Debug\System.Data.SQLite.Linq.dll"
  File "Youwrite\bin\Debug\TextEditor.cs"
  File "Youwrite\bin\Debug\TextEditor.Designer.cs"
  File "Youwrite\bin\Debug\TextEditor.resx"
  File "Youwrite\bin\Debug\TextRuler.suo"
  SetOutPath "$INSTDIR\tr"
  File "Youwrite\bin\Debug\tr\AutoUpdater.NET.resources.dll"
  SetOutPath "$INSTDIR"
  File "Youwrite\bin\Debug\WebResourceProvider.dll"
  File "Youwrite\bin\Debug\YouWrite.application"
  SetOverwrite ifnewer
  File "Youwrite\bin\Debug\YouWrite.exe"
  CreateDirectory "$SMPROGRAMS\YouWrite"
  CreateShortCut "$SMPROGRAMS\YouWrite\YouWrite.lnk" "$INSTDIR\YouWrite.exe"
  CreateShortCut "$DESKTOP\YouWrite.lnk" "$INSTDIR\YouWrite.exe"
  SetOverwrite try
  SetOutPath "$INSTDIR\zh"
  File "Youwrite\bin\Debug\zh\AutoUpdater.NET.resources.dll"
 
SectionEnd
 
Section -AdditionalIcons
  SetOutPath $INSTDIR
  WriteIniStr "$INSTDIR\${MUI_PRODUCT}.url" "InternetShortcut" "URL" "${MUI_WEB_SITE}"
  CreateShortCut "$SMPROGRAMS\YouWrite\Website.lnk" "$INSTDIR\${MUI_PRODUCT}.url"
  CreateShortCut "$SMPROGRAMS\YouWrite\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${MUI_DIR_REGKEY}" "" "$INSTDIR\YouWrite.exe"
  WriteRegStr ${MUI_UNINST_ROOT_KEY} "${MUI_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${MUI_UNINST_ROOT_KEY} "${MUI_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${MUI_UNINST_ROOT_KEY} "${MUI_UNINST_KEY}" "DisplayIcon" "$INSTDIR\YouWrite.exe"
  WriteRegStr ${MUI_UNINST_ROOT_KEY} "${MUI_UNINST_KEY}" "DisplayVersion" "${MUI_VERSION}"
  WriteRegStr ${MUI_UNINST_ROOT_KEY} "${MUI_UNINST_KEY}" "URLInfoAbout" "${MUI_WEB_SITE}"
  WriteRegStr ${MUI_UNINST_ROOT_KEY} "${MUI_UNINST_KEY}" "Publisher" "${MUI_PUBLISHER}"
SectionEnd

 
;--------------------------------    
;Uninstaller Section  
 
Section Uninstall
; Initialization of Uninstall page
  Delete "$INSTDIR\${MUI_PRODUCT}.url"
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\zh\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\YouWrite.exe"
  Delete "$INSTDIR\YouWrite.application"
  Delete "$INSTDIR\WebResourceProvider.dll"
  Delete "$INSTDIR\tr\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\TextRuler.suo"
  Delete "$INSTDIR\TextEditor.resx"
  Delete "$INSTDIR\TextEditor.Designer.cs"
  Delete "$INSTDIR\TextEditor.cs"
  Delete "$INSTDIR\System.Data.SQLite.Linq.dll"
  Delete "$INSTDIR\System.Data.SQLite.dll"
  Delete "$INSTDIR\sv\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\StringParser.dll"
  Delete "$INSTDIR\sqlite3.dll"
  Delete "$INSTDIR\SQLite.Interop.dll"
  Delete "$INSTDIR\SQLite.Designer.dll"
  Delete "$INSTDIR\SharpEntropySqlite.dll"
  Delete "$INSTDIR\SharpEntropy.dll"
  Delete "$INSTDIR\ru\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\pt\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\pl\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\pdfbox-1.8.9.dll"
  Delete "$INSTDIR\Pdf2Text.vshost.exe.manifest"
  Delete "$INSTDIR\pdf.jpg"
  Delete "$INSTDIR\OpenNLP.dll"
  Delete "$INSTDIR\nl\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\model.db"
  Delete "$INSTDIR\ko\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\junit.dll"
  Delete "$INSTDIR\jempbox-1.8.9.dll"
  Delete "$INSTDIR\ja-JP\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\it\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\index.jpeg"
  Delete "$INSTDIR\images.jpg"
  Delete "$INSTDIR\images.jpeg"
  Delete "$INSTDIR\IKVM.Runtime.JNI.dll"
  Delete "$INSTDIR\IKVM.Runtime.dll"
  Delete "$INSTDIR\IKVM.Reflection.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.XML.XPath.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.XML.WebServices.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.XML.Transform.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.XML.Parse.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.XML.Crypto.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.XML.Bind.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.XML.API.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Util.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Tools.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Text.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.SwingAWT.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Security.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Remoting.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Naming.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Misc.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Media.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Management.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Jdbc.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Core.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Corba.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Charsets.dll"
  Delete "$INSTDIR\IKVM.OpenJDK.Beans.dll"
  Delete "$INSTDIR\IKVM.AWT.WinForms.dll"
  Delete "$INSTDIR\ikvm-native-win32-x86.dll"
  Delete "$INSTDIR\ikvm-native-win32-x64.dll"
  Delete "$INSTDIR\iculx42.dll"
  Delete "$INSTDIR\icule42.dll"
  Delete "$INSTDIR\icuio42.dll"
  Delete "$INSTDIR\ICU4NETExtension.dll"
  Delete "$INSTDIR\ICU4NET.dll"
  Delete "$INSTDIR\ICSharpCode.SharpZipLib.dll"
  Delete "$INSTDIR\fr\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\fontbox-1.8.9.dll"
  Delete "$INSTDIR\fontbox-1.8.4.dll"
  Delete "$INSTDIR\fontbox-1.7.0.dll"
  Delete "$INSTDIR\favicon.ico"
  Delete "$INSTDIR\es\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\EnglishTok.nbin"
  Delete "$INSTDIR\EnglishSD.nbin"
  Delete "$INSTDIR\de\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\da\AutoUpdater.NET.resources.dll"
  Delete "$INSTDIR\commons-logging.dll"
  Delete "$INSTDIR\categories.db"
  Delete "$INSTDIR\bcprov-jdk15-1.44.dll"
  Delete "$INSTDIR\bcmail-jdk15-1.44.dll"
  Delete "$INSTDIR\AutoUpdater.NET.xml"
  Delete "$INSTDIR\AutoUpdater.NET.pdb"
  Delete "$INSTDIR\AutoUpdater.NET.dll"
  Delete "$INSTDIR\AutocompleteMenu.dll"

  Delete "$SMPROGRAMS\YouWrite\Uninstall.lnk"
  Delete "$SMPROGRAMS\YouWrite\Website.lnk"
  Delete "$DESKTOP\YouWrite.lnk"
  Delete "$SMPROGRAMS\YouWrite\YouWrite.lnk"

  RMDir "$SMPROGRAMS\YouWrite"
  RMDir "$INSTDIR\zh"
  RMDir "$INSTDIR\tr"
  RMDir "$INSTDIR\sv"
  RMDir "$INSTDIR\ru"
  RMDir "$INSTDIR\pt"
  RMDir "$INSTDIR\pl"
  RMDir "$INSTDIR\nl"
  RMDir "$INSTDIR\ko"
  RMDir "$INSTDIR\ja-JP"
  RMDir "$INSTDIR\it"
  RMDir "$INSTDIR\fr"
  RMDir "$INSTDIR\es"
  RMDir "$INSTDIR\de"
  RMDir "$INSTDIR\da"
  RMDir "$INSTDIR"

 ;Delete Start Menu Shortcuts
  Delete "$DESKTOP\${MUI_PRODUCT}.lnk"
  Delete "$SMPROGRAMS\${MUI_PRODUCT}\*.*"
  RmDir  "$SMPROGRAMS\${MUI_PRODUCT}"
 

  DeleteRegKey ${MUI_UNINST_ROOT_KEY} "${MUI_UNINST_KEY}"
  DeleteRegKey HKLM "${MUI_DIR_REGKEY}"
  SetAutoClose true
  SetOutPath "$INSTDIR"
  
  
;Delete databases files 
  RMDir /r "$APPDATA\${MUI_PRODUCT}"
 
SectionEnd 
 
;--------------------------------    
;MessageBox Section
 
 Function un.MyWelcomeShowCallback
SendMessage $mui.WelcomePage.Text ${WM_SETTEXT} 0 "STR:$(MUI_TEXT_WELCOME_INFO_TEXT)$\n$\nVersion: foo.bar"
FunctionEnd
 
;Function that calls a messagebox when installation finished correctly
Function .onInstSuccess
  MessageBox MB_OK "You have successfully installed ${MUI_PRODUCT}. Use the desktop icon to start the program."
  ExecShell "" "$DESKTOP\${MUI_PRODUCT}.lnk"
FunctionEnd
 
 
Function un.onUninstSuccess
  MessageBox MB_OK "You have successfully uninstalled ${MUI_PRODUCT}."
FunctionEnd
 
Function .onInit
  InitPluginsDir
  File "/oname=splash.bmp" "splash.bmp"

; optional
; File /oname=$PluginsDir\spltmp.wav "my_splashsound.wav"

  advsplash::show 1000 600 400 0xFF01FF splash

  Pop $0 ; $0 has '1' if the user closed the splash screen early,
         ; '0' if everything closed normally, and '-1' if some error occurred.

FunctionEnd
 

;eof