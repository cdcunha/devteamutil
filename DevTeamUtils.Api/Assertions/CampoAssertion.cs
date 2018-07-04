using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Assertions
{
    public class CampoAssertion : BaseAssertion
    {
        public CampoAssertion(Models.Campo campo, bool canIdNull = false) : base()
        {
            if (campo == null)
            {
                throw new Exception("O parâmetro passo não pode ser nulo [classe CampoAssertion]");
            }

            if (!canIdNull)
            {
                if (campo.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (campo.ScriptId == Guid.Empty)
            {
                SetNofication("500", "O ID da Script não pode ser nulo");
            }

            if (string.IsNullOrEmpty(campo.NomeCampo))
            {
                SetNofication("500", "Informe a Nome do Campo");
            }

            if (string.IsNullOrEmpty(campo.DescricaoCampo))
            {
                SetNofication("500", "Informe a Descrição da script");
            }

            if (!Enum.IsDefined(typeof(Enums.AtributoCampo), campo.Atributo))
            {
                SetNofication("500", "Atributo de campo Inválido");
            }

            if (!Enum.IsDefined(typeof(Enums.TipoCampo), campo.TipoCampo))
            {
                SetNofication("500", "Tipo de campo Inválido");
            }
        }
    }
}
