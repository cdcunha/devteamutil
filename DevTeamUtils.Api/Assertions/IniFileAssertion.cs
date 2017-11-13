namespace DevTeamUtils.Api.Assertions
{
    public class IniFileAssertion : BaseAssertion
    {
        public IniFileAssertion(string usuario, string password) : base()
        {
            if (string.IsNullOrEmpty(usuario.Trim()))
                SetNofication("500", "Usuário não pode ser vazio. Atualize o registro");
            if (string.IsNullOrEmpty(password.Trim()))
                SetNofication("500", "Senha não pode ser vazia. Atualize o registro");
        }
    }
}
