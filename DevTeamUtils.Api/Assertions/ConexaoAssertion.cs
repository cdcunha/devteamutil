using DevTeamUtils.Api.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Assertions
{
    public class ConexaoAssertion : BaseAssertion
    {
        public ConexaoAssertion(Models.Conexao conexao, bool canIdNull = false) : base()
        {
            if (conexao == null)
            {
                throw new Exception("O parâmetro conexao não pode ser nulo [classe ConexaoAssertion]");
            }

            if (!canIdNull)
            {
                if (conexao.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (string.IsNullOrEmpty(conexao.NomeServidor))
                SetNofication("500", "Informe o Nome");

            /*if (!conexao.DataNascimento.HasValue)
                SetNofication("500", "Informe a Data de Nascimento");
                */
        }
    }
}
