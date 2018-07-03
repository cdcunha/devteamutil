using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Assertions
{
    public class ProjetoAssertion : BaseAssertion
    {
        public ProjetoAssertion(Models.Projeto projeto, bool canIdNull = false) : base()
        {
            if (projeto == null)
            {
                throw new Exception("O parâmetro projeto não pode ser nulo [classe ProjetoAssertion]");
            }

            if (!canIdNull)
            {
                if (projeto.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (string.IsNullOrEmpty(projeto.Nome))
                SetNofication("500", "Informe o Nome do Projeto");

            if (string.IsNullOrEmpty(projeto.Codigo))
                SetNofication("500", "Informe o Código do Passo");

            if (string.IsNullOrEmpty(projeto.Codigo))
                SetNofication("500", "Informe o Autor do Passo");

            if (projeto.Tarefa == 0)
                SetNofication("500", "Informe o código da Tarefa (Jira/ALM)");

            if (string.IsNullOrEmpty(projeto.Descricao))
                SetNofication("500", "Informe a Descrição do Projeto");
        }
    }
}
