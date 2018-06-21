Para iniciar o mongoDb na pasta padrão do mongoDB (\Data\Db):
	mongod

Para iniciar o mongoDb em outra pasta:
	mongod.exe --dbpath C:\wwwDevTeam\DevTeamUtils.Data

Criar arquivo mongod.conf na pasta bin do mongoDB com o seguinte conteúdo:
	dbpath = c:/C:\wwwDevTeam
	logpath = C:\wwwDevTeamlogs/mongo.log log
	logappend = true 

Para criar o serviço no windows e startar:
	mongod.exe --config "C:\wwwDevTeam\mongod.conf" --install
        net start MongoDB

Criar serviço .Net Core (web API)
        sc create DevTeamUtils binpath = C:\wwwDevTeam\api\RunServer.bat start = auto depend = MongoDB
        sc create DevTeamUtils binPath = C:\wwwDevTeam\api\RunServer.bat DisplayName= "Developer Team Utils" start = auto depend = MongoDB

Restaurar backup
	mongorestore pastaBackupAserRestaurada/


Para importar os dados rodar o importaDados_mongo.bat

Executar os seguintes comandos para atualizar os dados dos campos Ativo e DataCriacao:
mongo

use DevTeamUtils

db.Conexao.update( {}, { $set : { "DataCriacao" : new ISODate("2017-09-22T08:51:04Z") } }, true, true);
db.Conexao.update( {}, { $set : { "DataAlteracao" : new ISODate("2017-09-22T08:51:04Z") } }, true, true);
db.Conexao.update( {}, { $set : { "DataStatus" : new ISODate("2017-09-22T08:51:04Z") } }, true, true);
db.Conexao.update( {}, { $set : { "Ativo" : true } }, true, true);

db.Contato.update( {}, { $set : { "DataCriacao" : new ISODate("2017-09-22T08:51:04Z") } }, true, true);
db.Contato.update( {}, { $set : { "DataAlteracao" : new ISODate("2017-09-22T08:51:04Z") } }, true, true);
db.Contato.update( {}, { $set : { "Ativo" : true } }, true, true);