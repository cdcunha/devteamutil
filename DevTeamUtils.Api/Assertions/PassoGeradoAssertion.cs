using System;

namespace DevTeamUtils.Api.Assertions
{
    public class PassoGeradoAssertion : BaseAssertion
    {
        public PassoGeradoAssertion(Models.PassoGerado passoGerado, bool canIdNull = false) : base()
        {
            if (passoGerado == null)
            {
                throw new Exception("O parâmetro passoGerado não pode ser nulo [classe PassoGeradoAssertion]");
            }

            if (!canIdNull)
            {
                if (passoGerado.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (passoGerado.PassoId == Guid.Empty)
                SetNofication("500", "O ID do Passo não pode ser nulo");

            if (string.IsNullOrEmpty(passoGerado.NomeArquivo))
                SetNofication("500", "Informe o nome do arquivo");

            if (string.IsNullOrEmpty(passoGerado.bancoDados))
                SetNofication("500", "Informe o nome o banco de dados");

            if (string.IsNullOrEmpty(passoGerado.TipoInstrucaoSql))
                SetNofication("500", "Informe o tipo de instrução SQL");

            if (string.IsNullOrEmpty(passoGerado.Passo))
                SetNofication("500", "Passo não foi gerado");

            /*if (!passoGerado.DataNascimento.HasValue)
                SetNofication("500", "Informe a Data de Nascimento");
                */
        }
    }
}
