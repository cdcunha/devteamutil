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
            Projetos = (_mongoDatabase.GetCollection<Projeto>("Projeto")) as MongoCollectionBase<Projeto>;
            Tabelas = (_mongoDatabase.GetCollection<Tabela>("Tabela")) as MongoCollectionBase<Tabela>;
            Campos = (_mongoDatabase.GetCollection<Campo>("Campo")) as MongoCollectionBase<Campo>;
        }

        public IConexaoRepository GetConexaoRepository()
        {
            return new ConexaoRepository(this);
        }

        public IContatoRepository GetContatoRepository()
        {
            return new ContatoRepository(this);
        }

        public IProjetoRepository GetProjetoRepository()
        {
            return new ProjetoRepository(this);
        }

        public ITabelaRepository GetTabelaRepository()
        {
            return new TabelaRepository(this);
        }

        public ICampoRepository GetCampoRepository()
        {
            return new CampoRepository(this);
        }

        public MongoCollectionBase<Conexao> Conexoes { get; set; }
        public MongoCollectionBase<Contato> Contatos { get; set; }
        public MongoCollectionBase<Projeto> Projetos { get; set; }
        public MongoCollectionBase<Tabela> Tabelas { get; set; }
        public MongoCollectionBase<Campo> Campos { get; set; }
    }
}
