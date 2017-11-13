using System;

namespace DevTeamUtils.Api.Utils
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

        public static string GetConnectionString(Models.Conexao conexao)
        {
            if (conexao.BancoDados == "Informix")
            {
                string database = "wpdhosp";
                return $"Host={conexao.Ip}; " +
                       $"Service={conexao.Porta}; " +
                       $"Server={conexao.NomeServidor}; " +
                       $"Database={database}; " +
                       $"User Id={conexao.Usuario}; " +
                       $"Password={conexao.Senha}; ";
            }
            else
            {
                //"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=100.100.100.100)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=myservice.com)));User Id=scott;Password=tiger;"
                return "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)" +
                       $"(HOST={conexao.Ip})(PORT={conexao.Porta}))(CONNECT_DATA=(SERVICE_NAME={conexao.NomeServidor})));" +
                       $"User Id= {conexao.Usuario};Password= {conexao.Senha}; ";
            }
        }
    }
}
