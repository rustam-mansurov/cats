echo off
set LOCALHOST=%COMPUTERNAME%
set KILL_CMD="C:\PROGRA~1\ANSYSI~1\v221\fluent/ntbin/win64/winkill.exe"

"C:\PROGRA~1\ANSYSI~1\v221\fluent\ntbin\win64\tell.exe" I2 64280 CLEANUP_EXITING
if /i "%LOCALHOST%"=="I2" (%KILL_CMD% 21196) 
if /i "%LOCALHOST%"=="I2" (%KILL_CMD% 12884) 
if /i "%LOCALHOST%"=="I2" (%KILL_CMD% 6648) 
if /i "%LOCALHOST%"=="I2" (%KILL_CMD% 16592) 
if /i "%LOCALHOST%"=="I2" (%KILL_CMD% 11132) 
if /i "%LOCALHOST%"=="I2" (%KILL_CMD% 19168)
del "C:\Users\Admin\Desktop\Git\cats\cleanup-fluent-I2-11132.bat"
