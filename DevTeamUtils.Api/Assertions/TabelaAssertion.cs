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
                throw new Exception("O parâmetro projeto não pode ser nulo [classe TabelaAssertion]");
            }

            if (!canIdNull)
            {
                if (tabela.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (tabela.ProjetoId == Guid.Empty)
            {
                SetNofication("500", "O ID do Projeto não pode ser nulo");
            }

            if (string.IsNullOrEmpty(tabela.NomeTabela))
            {
                SetNofication("500", "Informe a Nome da tabela");
            }

            if (string.IsNullOrEmpty(tabela.DescricaoTabela))
            {
                SetNofication("500", "Informe a Descrição da tabela");
            }

            if (!Enum.IsDefined(typeof(Enums.EnumTipoScript), tabela.TipoScript))
            {
                SetNofication("500", "Tipo de script Inválido");
            }

            if (string.IsNullOrEmpty(tabela.Mnemonico))
            {
                SetNofication("500", "Informe o Mnemônico da tabela");
            }

            if (string.IsNullOrEmpty(tabela.Script))
            {
                SetNofication("500", "Informe o Script da tabela");
            }
        }
    }
}
