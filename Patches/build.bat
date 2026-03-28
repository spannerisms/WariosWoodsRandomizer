@echo off

for %%i in (Source/*.asm) do (
	echo(
	echo %%~i

	copy "./woods.sfc" %%~ni.sfc
	asar.exe --fix-checksum=off -Dsrcfile="Source/%%~i" "base.asm" %%~ni.sfc
	flips --create --ips "woods.sfc" "%%~ni.sfc" "IPS/%%~ni.ips"
	del "%%~ni.sfc"
)

pause