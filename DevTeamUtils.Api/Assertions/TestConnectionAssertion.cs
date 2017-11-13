namespace DevTeamUtils.Api.Assertions
{
    public class TestConnectionAssertion : BaseAssertion
    {
        public TestConnectionAssertion(string ip, int porta, string nomeServidor, string usuario, string password) : base()
        {
            if (string.IsNullOrEmpty(ip.Trim()))
                SetNofication("500", "O IP não pode ser vazio. Atualize o registro");
            if (porta == 0)
                SetNofication("500", "A Porta não pode ser zero. Atualize o registro");
            if (string.IsNullOrEmpty(nomeServidor.Trim()))
                SetNofication("500", "O Nome do Servidor não pode ser vazio. Atualize o registro");
            if (string.IsNullOrEmpty(usuario.Trim()))
                SetNofication("500", "O Usuário não pode ser vazio. Atualize o registro");
            if (string.IsNullOrEmpty(password.Trim()))
                SetNofication("500", "A Senha não pode ser vazia. Atualize o registro");
        }
    }
}
