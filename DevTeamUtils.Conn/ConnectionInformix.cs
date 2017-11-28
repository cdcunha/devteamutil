﻿using IBM.Data.Informix;
using System.Threading.Tasks;

namespace DevTeamUtils.Conn
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
                
                return $"{server}\n{type} versão {version}\n\nConectado com sucesso!";
            }
            catch (IfxException ex)
            {
                if (ex.InnerException == null)
                    return $"{conn.Server}\nErro ao tentar conectar: \n{ex.Message}";
                else
                    return $"{conn.Server}\nErro ao tentar conectar: \n{ex.Message}\nDetalhe: {ex.InnerException.Message}";
            }
        }
    }
}