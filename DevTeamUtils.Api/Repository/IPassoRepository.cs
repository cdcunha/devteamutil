using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Repository
{
    public interface IPassoRepository
    {
        void Add(Passo passo);
        IEnumerable<Passo> GetAll();
        Passo Find(Guid id);
        void Remove(Guid id);
        void Update(Passo passo);
        System.IO.Stream DownloadArquivoPasso(string passo);
    }
}
