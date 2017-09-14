using DevTeamUtils.Api.Notifications;
using System;

namespace DevTeamUtils.Api.Assertions
{
    public class AgendaTelefonicaAssertion : BaseAssertion
    {   
        public AgendaTelefonicaAssertion(Models.AgendaTelefonica agendaTelefonica, bool canIdNull = false) : base()
        {
            if (agendaTelefonica == null)
            {
                throw new Exception("O parâmetro voluntario não pode ser nulo [classe VoluntarioAssertion]");
            }

            if (!canIdNull)
            {
                if (agendaTelefonica.Id == Guid.Empty)
                    SetNofication("500", "O ID não pode ser nulo");
            }

            if (string.IsNullOrEmpty(agendaTelefonica.Nome))
                SetNofication("500", "Informe o Nome");
            
        }
    }
}
