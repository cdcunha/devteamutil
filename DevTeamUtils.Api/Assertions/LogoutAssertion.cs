using System;

namespace DevTeamUtils.Api.Assertions
{
    public class LogoutAssertion : BaseAssertion
    {   
        public LogoutAssertion(DTO.LogoutDTO login, bool canIdNull = false) : base()
        {
            if (login == null)
            {
                throw new Exception("O parâmetro contato não pode ser nulo [classe ContatoAssertion]");
            }

            if (string.IsNullOrEmpty(login.Apelido))
                SetNofication("500", "Informe o Apelido");

            if (string.IsNullOrEmpty(login.ConnectionId))
                SetNofication("500", "Informe o Id da Conecção");
        }
    }
}
