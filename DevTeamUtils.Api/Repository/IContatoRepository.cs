using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Repository
{
    public interface IContatoRepository
    {
        void Add(Contato contato);
        IEnumerable<Contato> GetAll();
        Contato Find(Guid id);
        void Remove(Guid id);
        void Update(Contato contato);
        void Import(string pathAndFile);
    }
}
