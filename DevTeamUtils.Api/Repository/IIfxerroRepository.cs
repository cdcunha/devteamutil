using DevTeamUtils.Api.Models;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Repository
{
    public interface IIfxerroRepository
    {
        void Add(IfxError ifxError);
        IEnumerable<IfxError> GetAll();
        IfxError Find(System.Guid id);
        IfxError Find(int code);
        void Remove(System.Guid id);
        void Update(IfxError ifxError);
        void Import(string pathAndFile);
    }
}
