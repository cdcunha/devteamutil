using MongoDB.Driver;
using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTeamUtils.Api.Repository
{
    public class AgendaTelefonicaRepository : IAgendaTelefonicaRepository
    {
        private readonly MongoDbContext _context;

        public AgendaTelefonicaRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.AgendaTelefonicas.Find(FilterDefinition<AgendaTelefonica>.Empty).Skip(1);
            if (!resultado.Any())
                Add(new AgendaTelefonica());
        }

        public void Add(AgendaTelefonica agendaTelefonica)
        {
            _context.AgendaTelefonicas.InsertOne(agendaTelefonica);

            //_context.Apoiados.Add(apoiado);
            //_context.SaveChanges();
        }

        public AgendaTelefonica Find(Guid id)
        {   
            var resultado = _context.AgendaTelefonicas.Find(Builders<AgendaTelefonica>.Filter.Eq("_id", id)).FirstOrDefault();
            return resultado;
        }

        public IEnumerable<AgendaTelefonica> GetAll()
        {
            var resultado = _context.AgendaTelefonicas.Find(FilterDefinition<AgendaTelefonica>.Empty).SortBy(it => it.Nome);//.Skip(0).Limit(50)
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.AgendaTelefonicas.DeleteOne(Builders<AgendaTelefonica>.Filter.Eq(p => p.Id, id));

            //var apoiado = _context.Apoiados.First(t => t.Id == id);
            //_context.Apoiados.Remove(apoiado);
            //_context.SaveChanges();
        }

        public void Update(AgendaTelefonica agendaTelefonica)
        {
            _context.AgendaTelefonicas.ReplaceOne(Builders<AgendaTelefonica>.Filter.Eq(p => p.Id, agendaTelefonica.Id), agendaTelefonica);
            //_context.Apoiados.Update(apoiado);
            //_context.SaveChanges();
        }
    }
}
