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
                throw new Exception("O parâmetro campo não pode ser nulo [classe CampoAssertion]");
            }

            if (!canIdNull)
            {
                if (campo.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (string.IsNullOrEmpty(campo.Nome))
                SetNofication("500", "Informe o Nome");
        }
    }
}
