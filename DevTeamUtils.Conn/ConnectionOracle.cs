using Oracle.ManagedDataAccess.Client;
using System;
using System.Threading.Tasks;

namespace DevTeamUtils.Conn
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
                return $"{server}\nOracle Versão: {version}\n\nConectado com sucesso!";
            }
            catch (OracleException ex)
            {
                if (ex.InnerException == null)
                    return $"{conn.ServiceName}\nErro ao tentar conectar: \n{ex.Message}";
                else
                {
                    if (ex.Message != ex.InnerException.Message)
                        return $"{conn.ServiceName}\nErro ao tentar conectar: \n{ex.Message}\nDetalhe: {ex.InnerException.Message}";
                    else
                        return $"{conn.ServiceName}\nErro ao tentar conectar: \n{ex.Message}";
                }
            }
        }
    }
}
