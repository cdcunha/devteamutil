using System;

namespace DevTeamUtils.Api.Assertions
{
    public class ScriptGeradoAssertion : BaseAssertion
    {
        public ScriptGeradoAssertion(Models.ScriptGerado scriptGerado, bool canIdNull = false) : base()
        {
            if (scriptGerado == null)
            {
                throw new Exception("O parâmetro conexao não pode ser nulo [classe ScriptGeradoAssertion]");
            }

            if (!canIdNull)
            {
                if (scriptGerado.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (scriptGerado.ScriptId == Guid.Empty)
                SetNofication("500", "O ID do Passo não pode ser nulo");

            if (string.IsNullOrEmpty(scriptGerado.bancoDados))
                SetNofication("500", "Informe o nome o banco de dados");

            if (string.IsNullOrEmpty(scriptGerado.TipoInstrucaoSql))
                SetNofication("500", "Informe o tipo de instrução SQL");

            if (string.IsNullOrEmpty(scriptGerado.Script))
                SetNofication("500", "Passo não foi gerado");

            /*if (!conexao.DataNascimento.HasValue)
                SetNofication("500", "Informe a Data de Nascimento");
                */
        }
    }
}
