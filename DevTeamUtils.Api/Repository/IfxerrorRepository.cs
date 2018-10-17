using DevTeamUtils.Api.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DevTeamUtils.Api.Repository
{
    public class IfxerrorRepository : IIfxerroRepository
    {
        private readonly MongoDbContext _context;

        public IfxerrorRepository(MongoDbContext context)
        {
            _context = context;
            var resultado = _context.Ifxerrors.Find(FilterDefinition<IfxError>.Empty).Skip(1);
        }

        public void Add(IfxError ifxError)
        {
            _context.Ifxerrors.InsertOne(ifxError);
        }

        public IfxError Find(System.Guid id)
        {
            var resultado = _context.Ifxerrors.AsQueryable().Where(p => p.Id == id).SingleOrDefault();
            return resultado;
        }

        public IfxError Find(int code)
        {
            var resultado = _context.Ifxerrors.AsQueryable().Where(p => p.Code == code).SingleOrDefault();
            return resultado;
        }

        public IEnumerable<IfxError> GetAll()
        {
            var resultado = _context.Ifxerrors.AsQueryable();//.OrderBy(o => o.Code);
            return resultado.ToList();
        }

        public void Import(string pathAndFile)
        {
            var ifxErrors = JsonConvert.DeserializeObject<List<IfxError>>(File.ReadAllText(pathAndFile));

            foreach (var item in ifxErrors)
            {
                Add(item);
            }
        }

        public void Remove(Guid id)
        {
            _context.Ifxerrors.DeleteOne(Builders<IfxError>.Filter.Eq(p => p.Id, id));
        }

        public void Update(IfxError ifxError)
        {
            _context.Ifxerrors.ReplaceOne(Builders<IfxError>.Filter.Eq(p => p.Id, ifxError.Id), ifxError);
        }
    }
}
