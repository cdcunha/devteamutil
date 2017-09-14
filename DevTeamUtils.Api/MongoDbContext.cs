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

            ConexaoSishosps = _mongoDatabase.GetCollection<ConexaoSishosp>("ConexaoSishosp");
            AgendaTelefonicas = _mongoDatabase.GetCollection<AgendaTelefonica>("AgendaTelefonica");
        }

        public IConexaoSishospRepository GetConexaoSishospRepository()
        {
            return new ConexaoSishospRepository(this);
        }

        public IAgendaTelefonicaRepository GetVoluntarioRepository()
        {
            return new AgendaTelefonicaRepository(this);
        }

        public IMongoCollection<ConexaoSishosp> ConexaoSishosps { get; set; }
        public IMongoCollection<AgendaTelefonica> AgendaTelefonicas { get; set; }
        
    }
}
