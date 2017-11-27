using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUtils.Conn
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string connectionString = args[0];
                //string connectionString = "Host=10.104.37.238; Service=1530; Server=qa2; Database=wpdhosp; User Id=qa; Password=fn2BEvSOn6Oh; ";
                //string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.104.79.14)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=s_dboqnext)));User Id= desenv;Password= desenv; ";
                //string msg = Connection.TestConnection(connectionString);
                Console.WriteLine(Connection.TestConnection(connectionString));
            }
            else
            {
                Console.WriteLine("Não foi passado a ConnectionString");
            }
        }
    }
}
