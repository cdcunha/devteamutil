using System;

namespace DevTeamUtils.Utils
{
    public static partial class Connection
    {
        static string _connectionString;
        public static string TestConnection(string connectionString)
        {
            _connectionString = connectionString;
            if (connectionString.Contains("CONNECT_DATA=(SERVICE_NAME"))
                return TestConnectionOracle();
            else
                return TestConnectionInformix();
        }
    }
}
