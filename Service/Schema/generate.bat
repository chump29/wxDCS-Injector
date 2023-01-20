@echo off
cls
echo Generating METAR classes...
echo.
xsd metar1_2.xsd /c /n:Schema /nologo
echo.
echo Renaming file...
if exist metar.cs del metar.cs
ren metar1_2.cs metar.cs
echo.
echo Done!