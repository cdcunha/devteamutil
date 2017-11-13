@echo off

rem ###################################################
echo var D = new Date() > tmp.js 
echo D = (D.getFullYear()*100+D.getMonth()+1)*100+D.getDate()+ "_" + ("0" + D.getHours()).slice(-2)+("0" + D.getMinutes()).slice(-2) >> tmp.js 
echo WScript.Echo( 'set YYYYMMDD='+D ) >> tmp.js 
echo @echo off > tmp.bat 
cscript //nologo tmp.js >> tmp.bat 
call tmp.bat
rem ###################################################

rem ###################################################
rem ## Gera backup
rem ###################################################
md "C:\Projects\DevTeamUtils\tmp\"
md "C:\Projects\DevTeamUtils\Data.Backups"
mongodump --out C:\Projects\DevTeamUtils\tmp\backup%YYYYMMDD%
rem mongorestore pastaBackupAserRestaurada/
rem ###################################################

rem ###################################################
rem # Compacta o backup
rem ###################################################
cd "C:\Projects\DevTeamUtils\tmp\"
"c:\Program Files\7-Zip\7z.exe" a C:\Projects\DevTeamUtils\Data.Backups\Backup_%YYYYMMDD%.zip -w"C:\Projects\DevTeamUtils\tmp\backup%YYYYMMDD%"
cd "C:\Projects\DevTeamUtils\"
rem ###################################################

rem ###################################################
rem ## Exclui arquivos temporários
rem ###################################################
del tmp.bat /q
del tmp.js /q
rd /s /q .\tmp 
rem ###################################################

pause