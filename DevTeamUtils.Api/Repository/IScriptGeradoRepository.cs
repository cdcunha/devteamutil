using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Repository
{
    public interface IScriptGeradoRepository
    {
        void Add(ScriptGerado scriptGerado);
        IEnumerable<ScriptGerado> GetAllByScript(Guid scriptId);
        ScriptGerado Find(Guid id);
        void Remove(Guid id);
        void Update(ScriptGerado scriptGerado);
    }
}
