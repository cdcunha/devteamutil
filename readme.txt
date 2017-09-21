Para importar os dados rodar o importaDados_mongo.bat

Executar os seguintes comandos para atualizar os dados dos campos Ativo e DataCriacao:
mongo

use DevTeamUtils

db.Conexao.update( {}, { $set : { "DataCriacao" : new ISODate("2017-09-21T15:37:04Z") } }, true, true);
db.Conexao.update( {}, { $set : { "Ativo" : true } }, true, true);
db.Contato.update( {}, { $set : { "DataCriacao" : new ISODate("2017-09-21T15:37:04Z") } }, true, true);
db.Contato.update( {}, { $set : { "Ativo" : true } }, true, true);