using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Repository
{
    public interface ICampoRepository
    {
        void Add(Campo campo);
        IEnumerable<Campo> GetAllByTable(Guid tabelaId);
        Campo Find(Guid id);
        void Remove(Guid id);
        void Update(Campo campo);
    }
}
