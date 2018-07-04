using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Assertions
{
    public class PassoAssertion : BaseAssertion
    {
        public PassoAssertion(Models.Passo passo, bool canIdNull = false) : base()
        {
            if (passo == null)
            {
                throw new Exception("O parâmetro passo não pode ser nulo [classe PassoAssertion]");
            }

            if (!canIdNull)
            {
                if (passo.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (string.IsNullOrEmpty(passo.Nome))
                SetNofication("500", "Informe o Nome do Passo");

            if (string.IsNullOrEmpty(passo.Codigo))
                SetNofication("500", "Informe o Código do Passo");

            if (string.IsNullOrEmpty(passo.Codigo))
                SetNofication("500", "Informe o Autor do Passo");

            if (passo.Tarefa == 0)
                SetNofication("500", "Informe o código da Tarefa (Jira/ALM)");

            if (string.IsNullOrEmpty(passo.Descricao))
                SetNofication("500", "Informe a Descrição do Passo");
        }
    }
}
