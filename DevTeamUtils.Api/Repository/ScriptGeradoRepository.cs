using DevTeamUtils.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTeamUtils.Api.Repository
{
    public class ScriptGeradoRepository : IScriptGeradoRepository
    {
        private readonly MongoDbContext _context;

        public ScriptGeradoRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.ScriptGerados.Find(FilterDefinition<ScriptGerado>.Empty).Skip(1);
        }

        public void Add(ScriptGerado scriptGerado)
        {
            _context.ScriptGerados.InsertOne(scriptGerado);
        }

        public ScriptGerado Find(Guid id)
        {
            //var resultado = _context.ScriptGerados.Find(Builders<ScriptGerado>.Filter.Eq("_id", id)).FirstOrDefault();
            var resultado = _context.ScriptGerados.AsQueryable().Where(p => p.Id == id).SingleOrDefault();
            return resultado;
        }

        public IEnumerable<ScriptGerado> GetAllByScript(Guid scriptId)
        {
            //var resultado = _context.ScriptGerados.Find(FilterDefinition<ScriptGerado>.Empty).SortBy(it => it.NomeScriptGerado);//.Skip(0).Limit(50)
            var resultado = _context.ScriptGerados.AsQueryable().Where(p => p.ScriptId == scriptId).OrderBy(t => new { t.bancoDados, t.TipoInstrucaoSql });
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.ScriptGerados.DeleteOne(Builders<ScriptGerado>.Filter.Eq(p => p.Id, id));
        }

        public void Update(ScriptGerado scriptGerado)
        {
            _context.ScriptGerados.ReplaceOne(Builders<ScriptGerado>.Filter.Eq(p => p.Id, scriptGerado.Id), scriptGerado);
        }
    }
}
