using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using DevTeamUtils.Api.Controllers;
using DevTeamUtils.Api.Models;
using DevTeamUtils.Api.Repository;

namespace DevTeamUtils.Api
{
    public class MongoDbContext : DbContext
    {
        //private IMongoDatabase _mongoDatabase;
        public MongoDbContext(MongoClient mongoClient)
        {
            IMongoDatabase _mongoDatabase = ControllersUtils.GetDatabase(mongoClient);

            Conexoes = _mongoDatabase.GetCollection<Conexao>("Conexao");
            Contatos = _mongoDatabase.GetCollection<Contato>("Contato");
        }

        public IConexaoRepository GetConexaoRepository()
        {
            return new ConexaoRepository(this);
        }

        public IContatoRepository GetVoluntarioRepository()
        {
            return new ContatoRepository(this);
        }

        public IMongoCollection<Conexao> Conexoes { get; set; }
        public IMongoCollection<Contato> Contatos { get; set; }
        
    }
}
