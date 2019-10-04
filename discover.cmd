@echo off
cd "src\DiscoverDotnet"
dotnet run -- %*
set exitcode=%errorlevel%
cd %~dp0
echo %exitcode%
exit /b %exitcode%