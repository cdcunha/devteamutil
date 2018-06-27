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
                SetNofication("500", "Informe o Nome");


           
            if (projeto.Tabelas.Count > 0)
            {
                for (int i = 0; i <= projeto.Tabelas.Count - 1; i++)
                {
                    #region TabelaAssertions
                    if (string.IsNullOrEmpty(projeto.Tabelas[i].Nome))
                    {
                        SetNofication("500", "Informe a Nome da tabela");
                    }

                    if (string.IsNullOrEmpty(projeto.Tabelas[i].Descricao))
                    {
                        SetNofication("500", "Informe a Descrição da tabela");
                    }

                    if (!Enum.IsDefined(typeof(Enums.EnumTipoScript), projeto.Tabelas[i].TipoScript))
                    {
                        SetNofication("500", "Tipo de script Inválido");
                    }

                    if (string.IsNullOrEmpty(projeto.Tabelas[i].Mnemonico))
                    {
                        SetNofication("500", "Informe o Mnemônico da tabela");
                    }

                    if (string.IsNullOrEmpty(projeto.Tabelas[i].Script))
                    {
                        SetNofication("500", "Informe o Script da tabela");
                    }
                    #endregion

                    #region CampoAssertion
                    if (projeto.Tabelas[i].Campos.Count > 1)
                    {
                        for (int j = 0; j <= projeto.Tabelas[i].Campos.Count - 1; j++)
                        {
                            if (string.IsNullOrEmpty(projeto.Tabelas[i].Campos[j].Nome))
                            {
                                SetNofication("500", "Informe a Nome do Campo");
                            }

                            if (string.IsNullOrEmpty(projeto.Tabelas[i].Campos[j].Descricao))
                            {
                                SetNofication("500", "Informe a Descrição da tabela");
                            }

                            if (!Enum.IsDefined(typeof(Enums.EnumAtributoCampo), projeto.Tabelas[i].Campos[j].Atributo))
                            {
                                SetNofication("500", "Atributo de campo Inválido");
                            }

                            if (!Enum.IsDefined(typeof(Enums.EnumTipoCampo), projeto.Tabelas[i].Campos[j].Tipo))
                            {
                                SetNofication("500", "Tipo de campo Inválido");
                            }
                        }
                    }
                    #endregion
                }
            }            
        }
    }
}
