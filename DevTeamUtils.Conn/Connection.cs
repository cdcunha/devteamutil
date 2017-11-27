using System;
using System.Threading.Tasks;

namespace DevTeamUtils.Conn
{
    public static partial class Connection
    {
        static string _connectionString;
        
        public static async Task<string> TestConnection(string connectionString)
        {
            _connectionString = connectionString;
            if (connectionString.Contains("CONNECT_DATA=(SERVICE_NAME"))
                return await TestConnectionOracle();
            else
                return await TestConnectionInformix();
        }

    }
}
