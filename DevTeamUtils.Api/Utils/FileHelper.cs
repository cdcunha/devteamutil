using System.Text;

namespace DevTeamUtils.Api.Utils
{
    public static class FileHelper
    {
        public static System.IO.MemoryStream CreateIniText(string userName, string password)
        {
            StringBuilder iniText = new StringBuilder();
            iniText.AppendLine("[Connection]");
            iniText.AppendLine("Username=" + userName);
            iniText.AppendLine("Password=" + CryptographyHelper.EncryptPassword(password));

            byte[] byteArray = Encoding.UTF8.GetBytes(iniText.ToString());
            return new System.IO.MemoryStream(byteArray);
        }

        public static System.IO.MemoryStream CriarArquivoPasso(string passo)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(passo);
            return new System.IO.MemoryStream(byteArray);
        }
    }
}
