using IBM.Data.Informix;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApplication1
{
    class Program
    {
        #region data members

        private IfxConnection _connection;
        private StreamWriter _outStream;
        private IfxCommand _command;
        private IfxDataReader _dataReader;

        #endregion

        #region internal functions

        /// <summary>
        /// Constructor
        /// </summary>
        public Program()
        {
            _connection = null;
            _outStream = new StreamWriter(Console.OpenStandardOutput());
            _outStream.AutoFlush = true;
        }

        /// <summary>
        /// Property to set the output stream for the application
        /// </summary>
        public StreamWriter OutStream
        {
            get { return _outStream; }
            set { _outStream = value; }
        }

        /// <summary>
        /// Establishes connection with the server
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>bool</returns>
        public bool EstablishConnection(String connectionString)
        {
            try
            {
                _connection = new IfxConnection(connectionString);
                _connection.Open();
            }
            catch (Exception excep)
            {
                WriteLine(String.Format("Connection Error: {0}", excep.Message));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Clean up
        /// </summary>
        public void CleanUp()
        {
            if (_connection != null)
                _connection.Close();

            _outStream.Close();
        }

        /// <summary>
        /// Write status messages to file/console
        /// </summary>
        /// <param name="logMessage"></param>
        public void WriteLine(string logMessage)
        {
            _outStream.WriteLine(logMessage);
        }

        #endregion

        #region Transaction related functions

        /// <summary>
        /// Create a table
        /// </summary>
        /// <returns>bool</returns>
        public bool CreateTable()
        {
            try
            {
                _command = _connection.CreateCommand();
                _command.CommandText = "create table transactiontable(id integer, name char(25));";
                _command.ExecuteNonQuery();

            }
            catch (Exception excep)
            {
                WriteLine(String.Format("The table cannot be created: {0}", excep.Message));
                return false;
            }
            return true;
        }

        /// <summary>
        /// Perform a simaple commit, rollback transaction operation
        /// </summary>
        private bool PerformTransaction()
        {
            bool bStatus = false;
            IfxTransaction tx;

            try
            {
                // ********************* Demo for Transaction Commit ********************
                tx = _connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                _command = _connection.CreateCommand();
                _command.Transaction = tx;
                _command.CommandText = "insert into transactiontable values(1, 'ORIGINAL NAME');";
                WriteLine("Inserting into table with the command: " + _command.CommandText);
                _command.ExecuteNonQuery();

                _command.CommandText = "update transactiontable set name='NAME UPDATED' where id='1';";
                WriteLine("Update table with the command: " + _command.CommandText);
                _command.ExecuteNonQuery();

                tx.Commit();

                WriteLine("Transaction Commited. Observe table contents");
                _command.CommandText = "select * from transactiontable";
                WriteLine("Results after executing the command: " + _command.CommandText);
                _dataReader = _command.ExecuteReader();
                while (_dataReader.Read())
                {
                    WriteLine(String.Format("{0} ----- {1}", _dataReader[0], _dataReader[1]));
                }
                _dataReader.Close();

                // ********************* Demo for Transaction Rollback ********************
                tx = _connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                _command = _connection.CreateCommand();
                _command.Transaction = tx;
                _command.CommandText = "update transactiontable set name='NAME ROLLEDBACK' where id='1';";
                WriteLine("Update table with the command: " + _command.CommandText);
                _command.ExecuteNonQuery();

                WriteLine("Observe table contents before rollback");
                _command.CommandText = "select * from transactiontable";
                WriteLine("Results after executing the command: " + _command.CommandText);
                _dataReader = _command.ExecuteReader();
                while (_dataReader.Read())
                {
                    WriteLine(String.Format("{0} ----- {1}", _dataReader[0], _dataReader[1]));
                }
                _dataReader.Close();

                tx.Rollback();

                WriteLine("Observe table contents after rollback");
                _command.CommandText = "select * from transactiontable";
                WriteLine("Results after executing the command: " + _command.CommandText);
                _dataReader = _command.ExecuteReader();
                while (_dataReader.Read())
                {
                    WriteLine(String.Format("{0} ----- {1}", _dataReader[0], _dataReader[1]));
                }
                _dataReader.Close();

                bStatus = true;
            }
            catch (Exception excep)
            {
                WriteLine(String.Format("Insert into table failed : {0}", excep.Message));
                bStatus = false;
            }

            return bStatus;
        }


        /// <summary>
        /// Delete existing table with the same name if present
        /// </summary>
        private void DropTable()
        {
            try
            {
                _command = _connection.CreateCommand();
                _command.CommandText = "drop table transactiontable";
                _command.ExecuteNonQuery();

            }
            catch (Exception excep)
            {
                WriteLine(String.Format("The particular table cannot be dropped. : {0}", excep.Message));
            }
        }

        #endregion

        /// <summary>
        /// The demo app needs to be invoked as  EXE -conn "connection string" -log "log filename"
        /// Eg: Transactions.exe //This will pick the connection string from ConnInfo.xml
        /// OR
        /// Transactions.exe -conn "Database=perf;Server=localhost:9092;User ID=informix;Password=informix123;"
        /// OR
        /// Transactions.exe -conn "Database=perf;Server=localhost:9092;User ID=informix;Password=informix123;" -log log.txt
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool bResult = false;
            String connString = String.Empty;
            String logFile = String.Empty;

            Program prg = new Program();

            if (args.Length == 0)
            {
                try
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ConformanceLevel = ConformanceLevel.Auto;
                    using (XmlReader reader = XmlReader.Create("ConnInfo.xml", settings))
                    {
                        try
                        {
                            connString = reader.ReadElementString("ConnectionString");
                            logFile = reader.ReadElementString("LogFile");
                        }
                        catch (Exception)
                        {
                            //Ignore this error
                        }
                    }
                }
                catch (System.Exception excep)
                {
                    Console.WriteLine(excep.Message);
                    Console.WriteLine("Problem reading ConnInfo.xml file, either use");
                    Console.WriteLine("Transactions.exe //This will pick the connection string from ConnInfo.xml");
                    Console.WriteLine("Transactions.exe -conn \"Database=perf;Server=localhost:9092;User ID=informix;Password=informix123;\" OR");
                    Console.WriteLine("Transactions.exe -conn \"Database=perf;Server=localhost:9092;User ID=informix;Password=informix123;\" -log log.txt");
                }
            }

            if (args.Length > 1)
                connString = args[1];

            if (args.Length == 4)
                logFile = args[3];

            if (logFile.Length != 0)
                prg.OutStream = new StreamWriter(logFile, false);

            if (connString.Length != 0)
                bResult = prg.EstablishConnection(connString);
            else
                prg.WriteLine("*****    Connection Failed!!!     *****");

            if (bResult == true)
            {
                prg.WriteLine("*****    Connection successful     *****");
                bResult = prg.CreateTable();
            }

            if (bResult == true)
            {
                prg.WriteLine("*****    Table creation successful     *****");
                bResult = prg.PerformTransaction();
            }

            prg.DropTable();

            if (bResult == false)
            {
                prg.WriteLine("There is an ERROR in the execution!");
            }
            else
            {
                prg.WriteLine("*****    Transactions demo has successfully executed!  *****");
            }

            if (logFile.Length != 0)
            {
                Console.WriteLine("Refer to results in the log file: {0}", logFile);
            }

            prg.CleanUp();
        }
    }
}
