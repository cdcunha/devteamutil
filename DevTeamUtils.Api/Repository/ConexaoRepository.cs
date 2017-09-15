using MongoDB.Driver;
using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTeamUtils.Api.Repository
{
    public class ConexaoRepository : IConexaoRepository
    {
        private readonly MongoDbContext _context;

        public ConexaoRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.Conexoes.Find(FilterDefinition<DevTeamUtils.Api.Models.Conexao>.Empty).Skip(1);
            if (!resultado.Any())
                Add(new Conexao());
        }

        public void Add(Conexao conexao)
        {
            _context.Conexoes.InsertOne(conexao);

            //_context.Conexoes.Add(conexao);
            //_context.SaveChanges();
        }

        public Conexao Find(Guid id)
        {
            var resultado = _context.Conexoes.Find(Builders<Conexao>.Filter.Eq("_id", id)).FirstOrDefault();
            return resultado;
        }

        public IEnumerable<Conexao> GetAll()
        {
            var resultado = _context.Conexoes.Find(FilterDefinition<Conexao>.Empty).SortBy(it => it.NomeServidor);//.Skip(0).Limit(50)
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.Conexoes.DeleteOne(Builders<Conexao>.Filter.Eq(p => p.Id, id));

            //var conexao = _context.Conexoes.First(t => t.Id == id);
            //_context.Conexoes.Remove(conexao);
            // _context.SaveChanges();
        }

        public void Update(Conexao conexao)
        {
            _context.Conexoes.ReplaceOne(Builders<Conexao>.Filter.Eq(p => p.Id, conexao.Id), conexao);
            //_context.Conexoes.Update(conexao);
            //_context.SaveChanges();
        }
    }
}
