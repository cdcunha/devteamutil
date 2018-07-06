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
            Passos = (_mongoDatabase.GetCollection<Passo>("Passo")) as MongoCollectionBase<Passo>;
            Scripts = (_mongoDatabase.GetCollection<Script>("Script")) as MongoCollectionBase<Script>;
            Campos = (_mongoDatabase.GetCollection<Campo>("Campo")) as MongoCollectionBase<Campo>;
            ScriptGerados = (_mongoDatabase.GetCollection<ScriptGerado>("ScriptGerado")) as MongoCollectionBase<ScriptGerado>;
            PassoGerados = (_mongoDatabase.GetCollection<PassoGerado>("PassoGerado")) as MongoCollectionBase<PassoGerado>;
        }

        ///<summary>
        ///Construtor usado somente para teste unitário
        ///</summary>
        ///<param name="options"></param>
        public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options)
        {   
            //Conexoes = new MongoCollectionBase<Conexao>;

        }

        public IConexaoRepository GetConexaoRepository()
        {
            return new ConexaoRepository(this);
        }

        public IContatoRepository GetContatoRepository()
        {
            return new ContatoRepository(this);
        }

        public IPassoRepository GetPassoRepository()
        {
            return new PassoRepository(this);
        }

        public IScriptRepository GetScriptRepository()
        {
            return new ScriptRepository(this);
        }

        public ICampoRepository GetCampoRepository()
        {
            return new CampoRepository(this);
        }

        public IScriptGeradoRepository GetScriptGeradoRepository()
        {
            return new ScriptGeradoRepository(this);
        }

        public IPassoGeradoRepository GetPassoGeradoRepository()
        {
            return new PassoGeradoRepository(this);
        }

        public MongoCollectionBase<Conexao> Conexoes { get; set; }
        public MongoCollectionBase<Contato> Contatos { get; set; }
        public MongoCollectionBase<Passo> Passos { get; set; }
        public MongoCollectionBase<Script> Scripts { get; set; }
        public MongoCollectionBase<Campo> Campos { get; set; }
        public MongoCollectionBase<ScriptGerado> ScriptGerados { get; set; }
        public MongoCollectionBase<PassoGerado> PassoGerados { get; set; }
    }
}
