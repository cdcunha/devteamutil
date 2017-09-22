using System;

namespace DevTeamUtils.Api.Assertions
{
    public class ContatoAssertion : BaseAssertion
    {   
        public ContatoAssertion(Models.Contato contato, bool canIdNull = false) : base()
        {
            if (contato == null)
            {
                throw new Exception("O parâmetro contato não pode ser nulo [classe ContatoAssertion]");
            }

            if (!canIdNull)
            {
                if (contato.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (string.IsNullOrEmpty(contato.Nome))
                SetNofication("500", "Informe o Nome");
            
        }
    }
}
