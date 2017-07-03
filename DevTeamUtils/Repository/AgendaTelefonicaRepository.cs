using DevTeamUtils.Models;
using DevTeamUtils.Models.EqualityComparer;
using DevTeamUtils.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUtils.Repository
{
    public class AgendaTelefonicaRepository
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

        public static int Delete(AgendaTelefonica agendaTelefonica)
        {
            int resultado = -1;
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "DELETE FROM AgendaTelefonica WHERE Id = @Id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Id", agendaTelefonica.Id);
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

        public static int Insert(AgendaTelefonica agendaTelefonica)
        {
            int resultado = -1;
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "INSERT INTO AgendaTelefonica(nome,telefone,cargo,local,observacao) " +
                                      " VALUES (@nome,@telefone,@cargo,@local,@observacao)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@nome", agendaTelefonica.Nome);
                    cmd.Parameters.AddWithValue("@telefone", agendaTelefonica.Telefone);
                    cmd.Parameters.AddWithValue("@cargo", agendaTelefonica.Cargo);
                    cmd.Parameters.AddWithValue("@local", agendaTelefonica.Local);
                    cmd.Parameters.AddWithValue("@observacao", agendaTelefonica.Observacao);
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

        public static int Update(AgendaTelefonica agendaTelefonica)
        {
            int resultado = -1;
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "UPDATE alunos SET nome=@nome, telefone=@telefone, cargo=@cargo, " +
                        " local=@local,  observacao=@observacao " + 
                        " WHERE Id = @id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Id", agendaTelefonica.Id);
                    cmd.Parameters.AddWithValue("@nome", agendaTelefonica.Nome);
                    cmd.Parameters.AddWithValue("@telefone", agendaTelefonica.Telefone);
                    cmd.Parameters.AddWithValue("@cargo", agendaTelefonica.Cargo);
                    cmd.Parameters.AddWithValue("@local", agendaTelefonica.Local);
                    cmd.Parameters.AddWithValue("@observacao", agendaTelefonica.Observacao);
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
