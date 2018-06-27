Aplicação Web para Facilitar o dia a dia da equipe de desenvolvimento

A aplicação possui 3 módulos:
1) Cadastro de Contatos Telefônicos
2) Cadastro de Conexões de banco de Dados com opção de geração de arquivo INI para ser usado pela aplicação
3) Cadastro de Scripts por Projetos e geração de arquivo Passo para ser rodado no servidor 



INFORMAÇÕES TÉCNICAS

1) Para iniciar o mongoDb na pasta padrão do mongoDB (\Data\Db):
	mongod

2) Para iniciar o mongoDb em outra pasta:
	mongod.exe --dbpath C:\wwwDevTeam\DevTeamUtils.Data

3) Criar arquivo mongod.conf na pasta bin do mongoDB com o seguinte conteúdo:
	dbpath = c:/C:\wwwDevTeam
	logpath = C:\wwwDevTeamlogs/mongo.log log
	logappend = true 

4) Para criar o serviço no windows e startar:
	mongod.exe --config "C:\wwwDevTeam\mongod.conf" --install
        net start MongoDB

5) Criar serviço .Net Core (web API)
        sc create DevTeamUtils binpath = C:\wwwDevTeam\api\RunServer.bat start = auto depend = MongoDB
        sc create DevTeamUtils binPath = C:\wwwDevTeam\api\RunServer.bat DisplayName= "Developer Team Utils" start = auto depend = MongoDB

6) Restaurar backup
	mongorestore pastaBackupAserRestaurada/


7) Para importar os dados rodar o importaDados_mongo.bat

8) Executar os seguintes comandos para atualizar os dados dos campos Ativo e DataCriacao:
	mongo

9) use DevTeamUtils

db.Conexao.update( {}, { $set : { "DataCriacao" : new ISODate("2017-09-22T08:51:04Z") } }, true, true);
db.Conexao.update( {}, { $set : { "DataAlteracao" : new ISODate("2017-09-22T08:51:04Z") } }, true, true);
db.Conexao.update( {}, { $set : { "DataStatus" : new ISODate("2017-09-22T08:51:04Z") } }, true, true);
db.Conexao.update( {}, { $set : { "Ativo" : true } }, true, true);

db.Contato.update( {}, { $set : { "DataCriacao" : new ISODate("2017-09-22T08:51:04Z") } }, true, true);
db.Contato.update( {}, { $set : { "DataAlteracao" : new ISODate("2017-09-22T08:51:04Z") } }, true, true);
db.Contato.update( {}, { $set : { "Ativo" : true } }, true, true);