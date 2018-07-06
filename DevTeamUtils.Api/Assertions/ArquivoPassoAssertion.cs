using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Assertions
{
    public class ArquivoPassoAssertion : BaseAssertion
    {   
        public ArquivoPassoAssertion() : base(){}

        public ArquivoPassoAssertion(string passo) : base()
        {
            CheckPassoAssertion(passo);
        }

        public void CheckPassoAssertion(string passo)
        {
            if (string.IsNullOrEmpty(passo.Trim()))
                SetNofication("500", "O passo não pode ser vazio. Crie os scripts e gere o passo");
        }

        public void CheckPassoValidadoAssertion(bool validado)
        {
            if (!validado)
                SetNofication("500", "O passo não foi validado. Valide o passo primeiro");
        }
    }
}
