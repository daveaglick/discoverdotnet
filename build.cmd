@echo off
cd "src\DiscoverDotnet"
dotnet publish
if %errorlevel% == 0 (
  bin\Debug\netcoreapp3.0\publish\DiscoverDotnet.exe %*
)
set exitcode=%errorlevel%
cd %~dp0
exit /b %exitcode%