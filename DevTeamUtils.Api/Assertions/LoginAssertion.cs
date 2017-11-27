using System;

namespace DevTeamUtils.Api.Assertions
{
    public class LoginAssertion : BaseAssertion
    {   
        public LoginAssertion(DTO.LoginDTO login, bool canIdNull = false) : base()
        {
            if (login == null)
            {
                throw new Exception("O parâmetro contato não pode ser nulo [classe ContatoAssertion]");
            }

            if (string.IsNullOrEmpty(login.Apelido))
                SetNofication("500", "Informe o Apelido");

            if (string.IsNullOrEmpty(login.Senha))
                SetNofication("500", "Informe a Senha");
        }
    }
}
