using DevTeamUtils.Api.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Assertions
{
    public class ConexaoSishospAssertion : BaseAssertion
    {
        public ConexaoSishospAssertion(Models.ConexaoSishosp conexaoSishosp, bool canIdNull = false) : base()
        {
            if (conexaoSishosp == null)
            {
                throw new Exception("O parâmetro apoiado não pode ser nulo [classe VoluntarioAssertion]");
            }

            if (!canIdNull)
            {
                if (conexaoSishosp.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (string.IsNullOrEmpty(conexaoSishosp.NomeServidor))
                SetNofication("500", "Informe o Nome");

            /*if (!apoiado.DataNascimento.HasValue)
                SetNofication("500", "Informe a Data de Nascimento");
                */
        }
    }
}
