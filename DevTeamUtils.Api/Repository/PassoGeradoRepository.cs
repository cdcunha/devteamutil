using DevTeamUtils.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTeamUtils.Api.Repository
{
    public class PassoGeradoRepository : IPassoGeradoRepository
    {
        private readonly MongoDbContext _context;

        public PassoGeradoRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.PassoGerados.Find(FilterDefinition<PassoGerado>.Empty).Skip(1);
        }

        public void Add(PassoGerado passoGerado)
        {
            _context.PassoGerados.InsertOne(passoGerado);
        }

        public PassoGerado Find(Guid id)
        {
            //var resultado = _context.PassoGerados.Find(Builders<PassoGerado>.Filter.Eq("_id", id)).FirstOrDefault();
            var resultado = _context.PassoGerados.AsQueryable().Where(p => p.Id == id).SingleOrDefault();
            return resultado;
        }

        public IEnumerable<PassoGerado> GetAllByPasso(Guid passoId)
        {
            //var resultado = _context.PassoGerados.Find(FilterDefinition<PassoGerado>.Empty).SortBy(it => it.NomePassoGerado);//.Skip(0).Limit(50)
            var resultado = _context.PassoGerados.AsQueryable().Where(p => p.PassoId == passoId).OrderBy(t => new { t.bancoDados, t.NomeArquivo });
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.PassoGerados.DeleteOne(Builders<PassoGerado>.Filter.Eq(p => p.Id, id));
        }

        public void Update(PassoGerado passoGerado)
        {
            _context.PassoGerados.ReplaceOne(Builders<PassoGerado>.Filter.Eq(p => p.Id, passoGerado.Id), passoGerado);
        }
    }
}
