using MongoDB.Driver;
using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTeamUtils.Api.Repository
{
    public class ConexaoSishospRepository : IApoiadoRepository
    {
        private readonly MongoDbContext _context;

        public ConexaoSishospRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.ConexaoSishosps.Find(FilterDefinition<DevTeamUtils.Api.Models.ConexaoSishosp>.Empty).Skip(1);
            if (!resultado.Any())
                Add(new ConexaoSishosp());
        }

        public void Add(ConexaoSishosp conexaoDB)
        {
            _context.Apoiados.InsertOne(conexaoDB);

            //_context.Apoiados.Add(apoiado);
            //_context.SaveChanges();
        }

        public ConexaoSishosp Find(Guid id)
        {
            var resultado = _context.Apoiados.Find(Builders<ConexaoSishosp>.Filter.Eq("_id", id)).FirstOrDefault();
            return resultado;
        }

        public IEnumerable<ConexaoSishosp> GetAll()
        {
            var resultado = _context.ConexaoSishosps.Find(FilterDefinition<ConexaoSishosp>.Empty).SortBy(it => it.NomeServidor);//.Skip(0).Limit(50)
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.Apoiados.DeleteOne(Builders<ConexaoSishosp>.Filter.Eq(p => p.Id, id));

            //var apoiado = _context.Apoiados.First(t => t.Id == id);
            //_context.Apoiados.Remove(apoiado);
           // _context.SaveChanges();
        }

        public void Update(ConexaoSishosp conexaoDB)
        {
            _context.Apoiados.ReplaceOne(Builders<ConexaoSishosp>.Filter.Eq(p => p.Id, conexaoDB.Id), conexaoDB);
            //_context.Apoiados.Update(apoiado);
            //_context.SaveChanges();
        }
    }
}
