using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Repository
{
    public interface ICampoRepository
    {
        void Add(Campo campo);
        IEnumerable<Campo> GetAllByScript(Guid scriptId);
        Campo Find(Guid id);
        void Remove(Guid id);
        void Update(Campo campo);
    }
}
