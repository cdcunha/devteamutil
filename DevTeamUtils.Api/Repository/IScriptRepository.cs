using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Repository
{
    public interface IScriptRepository
    {
        void Add(Script script);
        IEnumerable<Script> GetAllByPasso(Guid passoId);
        Script Find(Guid id);
        void Remove(Guid id);
        void Update(Script script);
    }
}
