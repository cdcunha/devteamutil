using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Repository
{
    public interface IProjetoRepository
    {
        void Add(Projeto projeto);
        IEnumerable<Projeto> GetAll();
        Projeto Find(Guid id);
        void Remove(Guid id);
        void Update(Projeto projeto);
        System.IO.Stream DownloadArquivoPasso(string passo);
    }
}
