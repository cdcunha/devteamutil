using DevTeamUtils.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Repository
{
    public class CampoRepository : ICampoRepository
    {
        private readonly MongoDbContext _context;

        public CampoRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.Campos.Find(FilterDefinition<Campo>.Empty).Skip(1);
        }

        public void Add(Campo campo)
        {
            _context.Campos.InsertOne(campo);
        }

        public Campo Find(Guid id)
        {
            var resultado = _context.Campos.Find(Builders<Campo>.Filter.Eq("_id", id)).FirstOrDefault();
            return resultado;
        }

        public IEnumerable<Campo> GetAll()
        {
            var resultado = _context.Campos.Find(FilterDefinition<Campo>.Empty).SortBy(it => it.Nome);//.Skip(0).Limit(50)
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.Campos.DeleteOne(Builders<Campo>.Filter.Eq(p => p.Id, id));
        }

        public void Update(Campo campo)
        {
            _context.Campos.ReplaceOne(Builders<Campo>.Filter.Eq(p => p.Id, campo.Id), campo);
        }
    }
}
