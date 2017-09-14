using MongoDB.Driver;
using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTeamUtils.Api.Repository
{
    public class ConexaoSishospRepository : IConexaoSishospRepository
    {
        private readonly MongoDbContext _context;

        public ConexaoSishospRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.ConexaoSishosps.Find(FilterDefinition<DevTeamUtils.Api.Models.ConexaoSishosp>.Empty).Skip(1);
            if (!resultado.Any())
                Add(new ConexaoSishosp());
        }

        public void Add(ConexaoSishosp conexaoSishosp)
        {
            _context.ConexaoSishosps.InsertOne(conexaoSishosp);

            //_context.Apoiados.Add(conexaoSishosp);
            //_context.SaveChanges();
        }

        public ConexaoSishosp Find(Guid id)
        {
            var resultado = _context.ConexaoSishosps.Find(Builders<ConexaoSishosp>.Filter.Eq("_id", id)).FirstOrDefault();
            return resultado;
        }

        public IEnumerable<ConexaoSishosp> GetAll()
        {
            var resultado = _context.ConexaoSishosps.Find(FilterDefinition<ConexaoSishosp>.Empty).SortBy(it => it.NomeServidor);//.Skip(0).Limit(50)
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.ConexaoSishosps.DeleteOne(Builders<ConexaoSishosp>.Filter.Eq(p => p.Id, id));

            //var conexaoSishosp = _context.ConexaoSishosps.First(t => t.Id == id);
            //_context.Apoiados.Remove(conexaoSishosp);
            // _context.SaveChanges();
        }

        public void Update(ConexaoSishosp conexaoSishosp)
        {
            _context.ConexaoSishosps.ReplaceOne(Builders<ConexaoSishosp>.Filter.Eq(p => p.Id, conexaoSishosp.Id), conexaoSishosp);
            //_context.ConexaoSishosps.Update(conexaoSishosp);
            //_context.SaveChanges();
        }
    }
}
