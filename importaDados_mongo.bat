@echo off
choice /M "Importar os dados do arquivo contato.csv"
@echo .
goto contato%ERRORLEVEL%

:contato1
mongoimport -d DevTeam -c Contato --type csv --file contato.csv --headerline


:contato2
@echo .
choice /M "Importar os dados do arquivo conexao.csv"
@echo .
goto conexao%ERRORLEVEL%

:conexao1
mongoimport -d DevTeam -c Conexao --type csv --file conexao.csv --headerline

:conexao2
@echo .
@echo "Concluido"