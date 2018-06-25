using DevTeamUtils.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Repository
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly MongoDbContext _context;

        public ProjetoRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.Projetos.Find(FilterDefinition<Projeto>.Empty).Skip(1);
        }

        public void Add(Projeto projeto)
        {
            _context.Projetos.InsertOne(projeto);
        }

        public Projeto Find(Guid id)
        {
            var resultado = _context.Projetos.Find(Builders<Projeto>.Filter.Eq("_id", id)).FirstOrDefault();
            return resultado;
        }

        public IEnumerable<Projeto> GetAll()
        {
            var resultado = _context.Projetos.Find(FilterDefinition<Projeto>.Empty).SortBy(it => it.Nome);//.Skip(0).Limit(50)
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.Projetos.DeleteOne(Builders<Projeto>.Filter.Eq(p => p.Id, id));
        }

        public void Update(Projeto projeto)
        {
            _context.Projetos.ReplaceOne(Builders<Projeto>.Filter.Eq(p => p.Id, projeto.Id), projeto);
        }

        public System.IO.Stream DownloadArquivoPasso(string passo)
        {
            return Utils.FileHelper.CriarArquivoPasso(passo);
        }
    }
}
