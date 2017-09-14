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
            Voluntarios = _mongoDatabase.GetCollection<Voluntario>("Voluntario");
        }

        public IApoiadoRepository GetConexaoSishospRepository()
        {
            return new ConexaoSishospRepository(this);
        }

        public IVoluntarioRepository GetVoluntarioRepository()
        {
            return new VoluntarioRepository(this);
        }

        public IMongoCollection<ConexaoSishosp> ConexaoSishosps { get; set; }
        public IMongoCollection<Voluntario> Voluntarios { get; set; }
        
    }
}
