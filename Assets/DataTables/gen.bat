set WORKSPACE=..
set LUBAN_DLL=%WORKSPACE%\Tools\Luban\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t all ^
    -c cs-simple-json ^
    -d json ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputDataDir=../Data ^
    -x outputCodeDir=../Scripts/Data

pause