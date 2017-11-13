using System;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Utils
{
    public static class IniTextHelper
    {
        public static string CreateIniText(string userName, string password)
        {
            StringBuilder iniText = new StringBuilder();
            iniText.AppendLine("[Connection]");
            iniText.AppendLine("Username=" + userName);
            iniText.AppendLine("Password=" + CryptographyHelper.EncryptPassword(password));

            return iniText.ToString();
        }
    }
}
