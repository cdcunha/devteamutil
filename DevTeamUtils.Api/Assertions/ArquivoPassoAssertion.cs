using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Assertions
{
    public class ArquivoPassoAssertion : BaseAssertion
    {
        public ArquivoPassoAssertion(string passo, bool validado) : base()
        {
            if (!validado)
                SetNofication("500", "O passo deve estar validado. Valide o passo");
            if (string.IsNullOrEmpty(passo.Trim()))
                SetNofication("500", "O passo não pode ser vazio. Crie os scripts e gere o passo");
        }
    }
}
