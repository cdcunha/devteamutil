using DevTeamUtils.Models;
using System;
using System.Data;
using System.Data.SQLite;
using System.Globalization;

namespace DevTeamUtils.Repository
{
    public class ConexaoBDRepository
    {
        private static string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DevTeamUtilsDB"].ConnectionString;
        }

        public static DataTable GetDataTable<S, T>(string query) where S : IDbConnection, new()
                                           where T : IDbDataAdapter, IDisposable, new()
        {
            using (var conn = new S())
            {
                using (var da = new T())
                {
                    using (da.SelectCommand = conn.CreateCommand())
                    {
                        da.SelectCommand.CommandText = query;
                        da.SelectCommand.Connection.ConnectionString = GetConnectionString();
                        DataSet ds = new DataSet(); //conn é aberto pelo dataadapter
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
            }
        }

        public static int Delete(ConexaoBD conexaoBD)
        {
            int resultado = -1;
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "DELETE FROM ConexaoBD WHERE Id = @Id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Id", conexaoBD.Id);
                    try
                    {
                        resultado = cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return resultado;
        }

        public static int Insert(ConexaoBD conexaoBD)
        {
            int resultado = -1;
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "INSERT INTO ConexaoBD(Tipo, NomeServidor, Ip, Porta, Usuario, Senha) " +
                                      " VALUES (@Tipo, @NomeServidor, @Ip, @Porta, @Usuario, @Senha)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Tipo", conexaoBD.Tipo);
                    cmd.Parameters.AddWithValue("@NomeServidor", conexaoBD.NomeServidor);
                    cmd.Parameters.AddWithValue("@Ip", conexaoBD.Ip);
                    cmd.Parameters.AddWithValue("@Porta", conexaoBD.Porta);
                    cmd.Parameters.AddWithValue("@Usuario", conexaoBD.Usuario);
                    cmd.Parameters.AddWithValue("@Senha", conexaoBD.Senha);
                    try
                    {
                        resultado = cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return resultado;
        }

        public static int Update(ConexaoBD conexaoBD)
        {
            int resultado = -1;
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "UPDATE ConexaoBD SET Tipo=@Tipo, NomeServidor=@NomeServidor, " +
                        " Ip=@Ip, Porta=@Porta, Usuario=@Usuario,  Senha=@Senha " + 
                        " WHERE Id = @id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Id", conexaoBD.Id);
                    cmd.Parameters.AddWithValue("@Tipo", conexaoBD.Tipo);
                    cmd.Parameters.AddWithValue("@NomeServidor", conexaoBD.NomeServidor);
                    cmd.Parameters.AddWithValue("@Ip", conexaoBD.Ip);
                    cmd.Parameters.AddWithValue("@Porta", conexaoBD.Porta);
                    cmd.Parameters.AddWithValue("@Usuario", conexaoBD.Usuario);
                    cmd.Parameters.AddWithValue("@Senha", conexaoBD.Senha);
                    try
                    {
                        resultado = cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return resultado;
        }

        public static string GetConnectionString(ConexaoBD conexaoBD)
        {
            if (conexaoBD.Tipo == "Informix")
            {
                string database = "wpdhosp";
                return $"Host={conexaoBD.Ip}; " +
                       $"Service={conexaoBD.Porta}; " +
                       $"Server={conexaoBD.NomeServidor}; " +
                       $"Database={database}; " +
                       $"User Id={conexaoBD.Usuario}; " +
                       $"Password={conexaoBD.Senha}; ";
            }
            else
            {
                    //"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=100.100.100.100)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=myservice.com)));User Id=scott;Password=tiger;"
                return "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)" +
                       $"(HOST={conexaoBD.Ip})(PORT={conexaoBD.Porta}))(CONNECT_DATA=(SERVICE_NAME={conexaoBD.NomeServidor})));" +
                       $"User Id= {conexaoBD.Usuario};Password= {conexaoBD.Senha}; ";
            }
        }



        public static void UpdateStatus(long id, string msg)
        {
            string status = "";
            string dataStatus = DateTime.Now.ToString(new CultureInfo("PT-br"));
            if (msg.Contains("Conectado com sucesso!"))
            {
                status = "Conectado com sucesso!";
            }
            else
            {
                if (msg.Substring(0, 1) == "\n")
                    msg = msg.Substring(1, msg.Length - 1);
                string[] arr = msg.Split('\n');
                
                status = arr[arr.Length - 1];
            }
            int resultado = -1;
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "UPDATE ConexaoBD SET Status=@Status, DataStatus=@DataStatus " +
                        " WHERE Id = @id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@DataStatus", dataStatus);
                    try
                    {
                        resultado = cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}
