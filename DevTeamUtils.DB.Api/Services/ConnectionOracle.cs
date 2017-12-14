using Oracle.ManagedDataAccess.Client;
using System;
using System.Threading.Tasks;

namespace DevTeamUtils.DB.Api.Services
{
    static partial class Connection
    {
        private static async Task<string> TestConnectionOracle()
        {
            OracleConnection conn = new OracleConnection(_connectionString);
            //conn.ConnectionString = _connectionString;
            try
            {
                conn.Open();
                string server = conn.ServiceName;
                string version = conn.ServerVersion;
                conn.Close();
                return $@"{{""error"": ""false"", ""server"": ""{server}"", ""version"": ""{version}"", ""status"": ""Conectado com sucesso!""}}";
            }
            catch (OracleException ex)
            {
                if (ex.InnerException == null)
                    return $@"{{""error"": ""true"", ""server"": ""{conn.ServiceName}"", ""version"": null, ""status"": ""{ex.Message}""}}";
                else
                {
                    if (ex.Message != ex.InnerException.Message)
                        return $@"{{""error"": ""true"", ""server"": ""{conn.ServiceName}"", ""version"": null, ""status"": ""{ex.Message}""}}";
                    else
                        return $@"{{""error"": ""true"", ""server"": ""{conn.ServiceName}"", ""version"": null, ""status"": ""{ex.Message} - Detail: {ex.InnerException.Message}""}}";
                }
            }
        }
    }
}
