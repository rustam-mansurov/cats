echo off
set LOCALHOST=%COMPUTERNAME%
set KILL_CMD="C:\PROGRA~1\ANSYSI~1\v221\fluent/ntbin/win64/winkill.exe"

"C:\PROGRA~1\ANSYSI~1\v221\fluent\ntbin\win64\tell.exe" I2 59746 CLEANUP_EXITING
if /i "%LOCALHOST%"=="I2" (%KILL_CMD% 3100) 
if /i "%LOCALHOST%"=="I2" (%KILL_CMD% 1308) 
if /i "%LOCALHOST%"=="I2" (%KILL_CMD% 8436) 
if /i "%LOCALHOST%"=="I2" (%KILL_CMD% 12404) 
if /i "%LOCALHOST%"=="I2" (%KILL_CMD% 13260) 
if /i "%LOCALHOST%"=="I2" (%KILL_CMD% 7852)
del "C:\Users\Admin\Desktop\Git\cats\cleanup-fluent-I2-13260.bat"
