@echo off
REM change dir to location of script
SET mypath=%~dp0
CD %mypath%\deploy\bin_win32

start PhotonControl.exe 