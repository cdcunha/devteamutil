using DevTeamUtils.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTeamUtils.Api.Repository
{
    public class PassoRepository : IPassoRepository
    {
        private readonly MongoDbContext _context;

        public PassoRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.Passos.Find(FilterDefinition<Passo>.Empty).Skip(1);
        }

        public void Add(Passo passo)
        {
            _context.Passos.InsertOne(passo);
        }

        public Passo Find(Guid id)
        {
            //var resultado = _context.Passos.Find(Builders<Passo>.Filter.Eq("_id", id)).FirstOrDefault();
            var resultado = _context.Passos.AsQueryable().Where(p => p.Id == id).SingleOrDefault();
            return resultado;
        }

        public IEnumerable<Passo> GetAll()
        {
            //var resultado = _context.Passos.Find(FilterDefinition<Passo>.Empty).SortBy(it => it.Nome);//.Skip(0).Limit(50)
            var resultado = _context.Passos.AsQueryable().OrderBy(o => o.Nome);
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.Passos.DeleteOne(Builders<Passo>.Filter.Eq(p => p.Id, id));
        }

        public void Update(Passo passo)
        {
            _context.Passos.ReplaceOne(Builders<Passo>.Filter.Eq(p => p.Id, passo.Id), passo);
        }

        public System.IO.Stream DownloadArquivoPasso(string passo)
        {
            return Utils.FileHelper.CriarArquivoPasso(passo);
        }
    }
}
