using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Repository
{
    public interface ITabelaRepository
    {
        void Add(Tabela tabela);
        IEnumerable<Tabela> GetAllByProject(Guid projetoId);
        Tabela Find(Guid id);
        void Remove(Guid id);
        void Update(Tabela tabela);
    }
}
