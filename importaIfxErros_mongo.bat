@echo off
choice /M "Importar os dados do arquivo IfxCodeErros.json"
@echo .
goto contato%ERRORLEVEL%

:contato1
mongoimport -d DevTeamUtils -c IfxError --file IfxCodeErros.json --jsonArray

:conexao2
@echo .
@echo Fim
pause