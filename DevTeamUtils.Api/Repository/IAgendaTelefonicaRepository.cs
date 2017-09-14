using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Repository
{
    public interface IAgendaTelefonicaRepository
    {
        void Add(AgendaTelefonica agendaTelefonica);
        IEnumerable<AgendaTelefonica> GetAll();
        AgendaTelefonica Find(Guid id);
        void Remove(Guid id);
        void Update(AgendaTelefonica agendaTelefonica);
    }
}
