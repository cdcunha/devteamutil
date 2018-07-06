using DevTeamUtils.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTeamUtils.Api.Repository
{
    public class ScriptRepository : IScriptRepository
    {
        private readonly MongoDbContext _context;

        public ScriptRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.Scripts.Find(FilterDefinition<Script>.Empty).Skip(1);
        }

        public void Add(Script script)
        {
            _context.Scripts.InsertOne(script);
        }

        public Script Find(Guid id)
        {
            //var resultado = _context.Scripts.Find(Builders<Script>.Filter.Eq("_id", id)).FirstOrDefault();
            var resultado = _context.Scripts.AsQueryable().Where(p => p.Id == id).SingleOrDefault();
            return resultado;
        }

        public IEnumerable<Script> GetAllByPasso(Guid passoId)
        {
            //var resultado = _context.Scripts.Find(FilterDefinition<Script>.Empty).SortBy(it => it.Nome);//.Skip(0).Limit(50)
            var resultado = _context.Scripts.AsQueryable().Where(t => t.PassoId == passoId).OrderBy(t => t.NomeScript);
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.Scripts.DeleteOne(Builders<Script>.Filter.Eq(p => p.Id, id));
        }

        public void Update(Script script)
        {
            _context.Scripts.ReplaceOne(Builders<Script>.Filter.Eq(p => p.Id, script.Id), script);
        }
    }
}
