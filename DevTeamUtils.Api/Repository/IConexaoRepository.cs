using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Repository
{
    public interface IConexaoRepository
    {
        void Add(Conexao conexao);
        IEnumerable<Conexao> GetAll();
        Conexao Find(Guid id);
        void Remove(Guid id);
        void Update(Conexao conexao);
        void Import(string pathAndFile);
        System.IO.Stream DownloadIniFile(string userName, string password);
    }
}
