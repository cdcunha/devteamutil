using DevTeamUtils.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTeamUtils.Api.Repository
{
    public class TabelaRepository : ITabelaRepository
    {
        private readonly MongoDbContext _context;

        public TabelaRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.Tabelas.Find(FilterDefinition<Tabela>.Empty).Skip(1);
        }

        public void Add(Tabela tabela)
        {
            _context.Tabelas.InsertOne(tabela);
        }

        public Tabela Find(Guid id)
        {
            //var resultado = _context.Tabelas.Find(Builders<Tabela>.Filter.Eq("_id", id)).FirstOrDefault();
            var resultado = _context.Tabelas.AsQueryable().Where(p => p.Id == id).SingleOrDefault();
            return resultado;
        }

        public IEnumerable<Tabela> GetAllByProject(Guid projetoId)
        {
            //var resultado = _context.Tabelas.Find(FilterDefinition<Tabela>.Empty).SortBy(it => it.Nome);//.Skip(0).Limit(50)
            var resultado = _context.Tabelas.AsQueryable().Where(t => t.ProjetoId == projetoId).OrderBy(t => t.NomeTabela);
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.Tabelas.DeleteOne(Builders<Tabela>.Filter.Eq(p => p.Id, id));
        }

        public void Update(Tabela tabela)
        {
            _context.Tabelas.ReplaceOne(Builders<Tabela>.Filter.Eq(p => p.Id, tabela.Id), tabela);
        }
    }
}
