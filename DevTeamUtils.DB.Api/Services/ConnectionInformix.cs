using IBM.Data.Informix;
using System.Threading.Tasks;

namespace DevTeamUtils.DB.Api.Services
{
    static partial class Connection
    {
        private static async Task<string> TestConnectionInformix()
        {
            IfxConnection conn = new IfxConnection(_connectionString);
            //conn.ConnectionString = _connectionString;
            try
            {
                conn.Open();
                string server = conn.Server;
                string version = conn.ServerVersion;
                string type = conn.ServerType;
                conn.Close();
                        
                return $@"{{""error"": ""false"", server: ""{server}"" - {type},  ""version"": ""{version}"", ""status"": ""Conectado com sucesso!""}}";
            }
            catch (IfxException ex)
            {
                if (ex.InnerException == null)
                    return $@"{{""error"": ""true"", ""server"": ""{conn.Server}"", ""version"": null, ""status"": ""{ex.Message}""}}";
                else
                    return $@"{{""error"": ""true"", ""server"": ""{conn.Server}"", ""version"": null, ""status"": ""{ex.Message} - Detail: {ex.InnerException.Message}""}}";
            }
        }
    }
}
