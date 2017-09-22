using DevTeamUtils.Api.Controllers;
using DevTeamUtils.Api.Models;
using DevTeamUtils.Api.Repository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace DevTeamUtils.Api
{
    public class MongoDbContext : DbContext
    {
        //private IMongoDatabase _mongoDatabase;
        public MongoDbContext(MongoClient mongoClient)
        {
            MongoDatabaseBase _mongoDatabase = (ControllersUtils.GetDatabase(mongoClient)) as MongoDatabaseBase;
            
            Conexoes = (_mongoDatabase.GetCollection<Conexao>("Conexao")) as MongoCollectionBase<Conexao>;
            Contatos = (_mongoDatabase.GetCollection<Contato>("Contato")) as MongoCollectionBase<Contato>;
        }

        public IConexaoRepository GetConexaoRepository()
        {
            return new ConexaoRepository(this);
        }

        public IContatoRepository GetVoluntarioRepository()
        {
            return new ContatoRepository(this);
        }

        public MongoCollectionBase<Conexao> Conexoes { get; set; }
        public MongoCollectionBase<Contato> Contatos { get; set; }
        
    }
}
