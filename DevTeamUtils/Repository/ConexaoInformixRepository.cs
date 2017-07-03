using DevTeamUtils.Models;
using System;
using System.Data;
using System.Data.SQLite;

namespace DevTeamUtils.Repository
{
    public class ConexaoInformixRepository
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

        public static int Delete(ConexaoInformix conexaoInformix)
        {
            int resultado = -1;
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "DELETE FROM ConexaoInformix WHERE Id = @Id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Id", conexaoInformix.Id);
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

        public static int Insert(ConexaoInformix conexaoInformix)
        {
            int resultado = -1;
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "INSERT INTO ConexaoInformix(NomeServidor,Ip,Porta,Usuario,Senha) " +
                                      " VALUES (@NomeServidor,@Ip,@Porta,@Usuario,@Senha)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@NomeServidor", conexaoInformix.NomeServidor);
                    cmd.Parameters.AddWithValue("@Ip", conexaoInformix.Ip);
                    cmd.Parameters.AddWithValue("@Porta", conexaoInformix.Porta);
                    cmd.Parameters.AddWithValue("@Usuario", conexaoInformix.Usuario);
                    cmd.Parameters.AddWithValue("@Senha", conexaoInformix.Senha);
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

        public static int Update(ConexaoInformix conexaoInformix)
        {
            int resultado = -1;
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "UPDATE ConexaoInformix SET NomeServidor=@NomeServidor, Ip=@Ip, Porta=@Porta, " +
                        " Usuario=@Usuario,  Senha=@Senha " + 
                        " WHERE Id = @id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Id", conexaoInformix.Id);
                    cmd.Parameters.AddWithValue("@NomeServidor", conexaoInformix.NomeServidor);
                    cmd.Parameters.AddWithValue("@Ip", conexaoInformix.Ip);
                    cmd.Parameters.AddWithValue("@Porta", conexaoInformix.Porta);
                    cmd.Parameters.AddWithValue("@Usuario", conexaoInformix.Usuario);
                    cmd.Parameters.AddWithValue("@Senha", conexaoInformix.Senha);
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
    }
}
