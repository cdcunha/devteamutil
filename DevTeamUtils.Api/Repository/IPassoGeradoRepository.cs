using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Repository
{
    public interface IPassoGeradoRepository
    {
        void Add(PassoGerado passoGerado);
        IEnumerable<PassoGerado> GetAllByPasso(Guid passoId);
        PassoGerado Find(Guid id);
        void Remove(Guid id);
        void Update(PassoGerado passoGerado);
    }
}
