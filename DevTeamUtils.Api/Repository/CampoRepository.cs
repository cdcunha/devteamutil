using DevTeamUtils.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

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
            //var resultado = _context.Campos.Find(Builders<Campo>.Filter.Eq("_id", id)).FirstOrDefault();
            var resultado = _context.Campos.AsQueryable().Where(p => p.Id == id).SingleOrDefault();
            return resultado;
        }

        public IEnumerable<Campo> GetAllByScript(Guid scriptId)
        {
            //var resultado = _context.Campos.Find(FilterDefinition<Campo>.Empty).SortBy(it => it.NomeCampo);//.Skip(0).Limit(50)
            var resultado = _context.Campos.AsQueryable().Where(p => p.ScriptId == scriptId).OrderBy(t => t.NomeCampo);
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
