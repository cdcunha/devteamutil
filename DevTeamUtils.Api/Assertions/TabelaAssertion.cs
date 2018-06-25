using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Assertions
{
    public class TabelaAssertion : BaseAssertion
    {
        public TabelaAssertion(Models.Tabela tabela, bool canIdNull = false) : base()
        {
            if (tabela == null)
            {
                throw new Exception("O parâmetro tabela não pode ser nulo [classe TabelaAssertion]");
            }

            if (!canIdNull)
            {
                if (tabela.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (string.IsNullOrEmpty(tabela.Nome))
                SetNofication("500", "Informe o Nome");
        }
    }
}
