@ECHO off
 
ECHO "Starting CrystalReports Installation" >> log.txt
msiexec.exe /I "CRRuntime_32bit_13_0_12.msi" /qn
ECHO "Completed CrystalReports Installation" >> log.txt