using DevTeamUtils.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTeamUtils.Api.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly MongoDbContext _context;

        public ContatoRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.Contatos.Find(FilterDefinition<Contato>.Empty).Skip(1);
            //if (!resultado.Any())
            //    Add(new Contato());
        }

        public void Add(Contato contato)
        {
            _context.Contatos.InsertOne(contato);

            //_context.Contatos.Add(apoiado);
            //_context.SaveChanges();
        }

        public Contato Find(Guid id)
        {   
            var resultado = _context.Contatos.Find(Builders<Contato>.Filter.Eq("_id", id)).FirstOrDefault();
            return resultado;
        }

        public IEnumerable<Contato> GetAll()
        {
            var resultado = _context.Contatos.Find(FilterDefinition<Contato>.Empty).SortBy(it => it.Nome);//.Skip(0).Limit(50)
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.Contatos.DeleteOne(Builders<Contato>.Filter.Eq(p => p.Id, id));

            //var apoiado = _context.Contatos.First(t => t.Id == id);
            //_context.Contatos.Remove(apoiado);
            //_context.SaveChanges();
        }

        public void Update(Contato contato)
        {
            _context.Contatos.ReplaceOne(Builders<Contato>.Filter.Eq(p => p.Id, contato.Id), contato);
            //_context.Contatos.Update(apoiado);
            //_context.SaveChanges();
        }

        private Contato CsvToContato(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Contato contato = new Contato();
            
            contato.Nome = values[0];
            contato.Telefone = values[1];
            contato.Cargo = values[2];
            contato.Local = values[3];
            contato.Observacao = values[4];
            //conexao.DataStatus = values[8];

            return contato;
        }


        public void Import(string pathAndFile)
        {
            List<Contato> contatos = System.IO.File.ReadAllLines(pathAndFile)
                                           .Skip(1)
                                           .Select(v => CsvToContato(v))
                                           .ToList();
            foreach (Contato item in contatos)
            {
                Add(item);
            }
        }
    }
}
