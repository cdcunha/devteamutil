using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Assertions
{
    public class ScriptAssertion : BaseAssertion
    {
        public ScriptAssertion(Models.Script script, bool canIdNull = false) : base()
        {
            if (script == null)
            {
                throw new Exception("O parâmetro passo não pode ser nulo [classe ScriptAssertion]");
            }

            if (!canIdNull)
            {
                if (script.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (script.PassoId == Guid.Empty)
            {
                SetNofication("500", "O ID do Passo não pode ser nulo");
            }

            if (string.IsNullOrEmpty(script.NomeScript))
            {
                SetNofication("500", "Informe a Nome");
            }

            if (string.IsNullOrEmpty(script.DescricaoScript))
            {
                SetNofication("500", "Informe a Descrição");
            }

            if (!Enum.IsDefined(typeof(Enums.EnumTipoScript), script.TipoScript))
            {
                SetNofication("500", "Tipo de script");
            }

            if (string.IsNullOrEmpty(script.Mnemonico))
            {
                SetNofication("500", "Informe o Mnemônico");
            }

            if (string.IsNullOrEmpty(script.TxtScript))
            {
                SetNofication("500", "Informe o Script");
            }
        }
    }
}
