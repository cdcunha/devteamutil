using MongoDB.Bson;
using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Repository
{
    public interface IConexaoRepository
    {
        void Add(Conexao conexao);
        IEnumerable<Conexao> GetAll();
        Conexao Find(Guid id);
        void Remove(Guid id);
        void Update(Conexao conexao);
    }
}
