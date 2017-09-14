using MongoDB.Bson;
using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Repository
{
    public interface IConexaoSishospRepository
    {
        void Add(ConexaoSishosp conexaoSishosp);
        IEnumerable<ConexaoSishosp> GetAll();
        ConexaoSishosp Find(Guid id);
        void Remove(Guid id);
        void Update(ConexaoSishosp conexaoSishosp);
    }
}
