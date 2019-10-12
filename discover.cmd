@echo off
cd "src\DiscoverDotnet"
dotnet run -- %*
set exitcode=%errorlevel%
cd %~dp0
exit /b %exitcode%