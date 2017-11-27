using System;

namespace DevTeamUtils.Api.Assertions
{
    public class UserAssertion : BaseAssertion
    {   
        public UserAssertion(Models.User user, bool canIdNull = false) : base()
        {
            if (user == null)
            {
                throw new Exception("O parâmetro contato não pode ser nulo [classe ContatoAssertion]");
            }

            if (!canIdNull)
            {
                if (user.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (string.IsNullOrEmpty(user.Nome))
                SetNofication("500", "Informe o Nome");
            
            if (string.IsNullOrEmpty(user.Apelido))
                SetNofication("500", "Informe o Apelido");

            if (string.IsNullOrEmpty(user.Senha))
                SetNofication("500", "Informe a Senha");
        }
    }
}
