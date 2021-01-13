
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOneConsole
{
    class Program
    {
        public const string QUERYPATH = @"C:\data\formulaone\queries\";
        public const string DBPATH = @"C:\data\formulaone\";
        private const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + DBPATH + @"FormulaOne.mdf;Integrated Security=True";
        private static string[] tableNames = { "Country", "Driver", "Team" };

        static void Main(string[] args)
        {
            char scelta = ' ';
            do
            {
                Console.WriteLine("\n*** FORMULA ONE - SCRIPTS ***\n");
                Console.WriteLine("1 - Create Countries");
                Console.WriteLine("2 - Create Teams");
                Console.WriteLine("3 - Create Drivers");
                Console.WriteLine("------------------");
                Console.WriteLine("B - Backup all");
                Console.WriteLine("R - Restore");
                Console.WriteLine("------------------");
                Console.WriteLine("X - EXIT\n");
                scelta = Console.ReadKey(true).KeyChar;
                switch (scelta)
                {
                    case '1':
                        ExecuteSqlScript("Countries.sql");
                        break;
                    case '2':
                        ExecuteSqlScript("Teams.sql");
                        break;
                    case '3':
                        ExecuteSqlScript("Drivers.sql");
                        break;
                    case 'B':
                        RestoreDb();
                        break;
                    case 'R':
                        resetDB();
                        break;
                    case 'r':
                        resetDB();
                        break;
                    default:
                        if (scelta != 'X' && scelta != 'x') Console.WriteLine("\nUncorrect Choice - Try Again\n");
                        break;
                }
            } while (scelta != 'X' && scelta != 'x');
        }

        public static void resetDB()
        {
            bool OK;
            BackupDb();
            OK = DropTable("Country");
            if (OK) OK = DropTable("Driver");
            if (OK) OK = DropTable("Team");
            if (OK) OK = ExecuteSqlScript("Countries.sql");
            if (OK) OK = ExecuteSqlScript("Drivers.sql");
            if (OK) OK = ExecuteSqlScript("Teams.sql");
            if (OK)
            {
                Console.WriteLine("OK\n");
            }
            else
            {
                RestoreDb();
            }
            Console.WriteLine("\nRESET AVVENUTO CON SUCCESSO");
        }

        public static bool ExecuteSqlScript(string scriptName)
        {
            try
            {
                var fileContent = File.ReadAllText(QUERYPATH + scriptName);
                fileContent = fileContent.Replace("\r\n", "");
                fileContent = fileContent.Replace("\r", "");
                fileContent = fileContent.Replace("\n", "");
                fileContent = fileContent.Replace("\t", "");
                var sqlqueries = fileContent.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                var con = new SqlConnection(CONNECTION_STRING);
                var cmd = new SqlCommand("query", con);
                con.Open();
                int i = 0;
                foreach (var query in sqlqueries)
                {
                    cmd.CommandText = query; i++;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException err)
                    {
                        Console.WriteLine("Errore in esecuzione della query numero: " + i);
                        Console.WriteLine("\tErrore SQL: " + err.Number + " - " + err.Message);
                    }
                }
                con.Close();
                Console.WriteLine("\nCreate " + scriptName + " - SUCCESS\n");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nCreate " + scriptName + " - ERROR: " + ex.Message + "\n");
                return false;
            }
        }

        public static bool DropTable(string tableName)
        {
            try
            {
                var con = new SqlConnection(CONNECTION_STRING);
                var cmd = new SqlCommand("Drop Table If exists " + tableName + ";", con);
                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException err)
                {
                    Console.WriteLine("\tErrore SQL: " + err.Number + " - " + err.Message);
                }
                con.Close();
                Console.WriteLine("\nDROP " + tableName + " - SUCCESS\n");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nDROP " + tableName + " - ERROR: " + ex.Message + "\n");
                return false;
            }
        }

        public static void BackupDb()
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection())
                {
                    dbConn.ConnectionString = CONNECTION_STRING;
                    dbConn.Open();

                    using (SqlCommand multiuser_rollback_dbcomm = new SqlCommand())
                    {
                        multiuser_rollback_dbcomm.Connection = dbConn;
                        multiuser_rollback_dbcomm.CommandText = @"ALTER DATABASE [" + DBPATH + "formulaone.mdf" + "] SET MULTI_USER WITH ROLLBACK IMMEDIATE";

                        multiuser_rollback_dbcomm.ExecuteNonQuery();
                    }
                    dbConn.Close();
                }

                SqlConnection.ClearAllPools();

                using (SqlConnection backupConn = new SqlConnection())
                {
                    backupConn.ConnectionString = CONNECTION_STRING;
                    backupConn.Open();

                    using (SqlCommand backupcomm = new SqlCommand())
                    {
                        backupcomm.Connection = backupConn;
                        backupcomm.CommandText = @"BACKUP DATABASE [" + DBPATH + "formulaone.mdf" + "] TO DISK='" + DBPATH + @"\prova.bak'";
                        backupcomm.ExecuteNonQuery();
                    }
                    backupConn.Close();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void RestoreDb()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
                {
                    string sqlStmt = "";
                    foreach (string table in tableNames)
                    {
                        sqlStmt += "TRUNCATE TABLE " + table + ";";
                        sqlStmt += "SELECT * INTO " + table + "_bck FROM " + table + ";";
                    }
                    sqlStmt = string.Format("RESTORE database FormulaOne.mdf FROM disk='{0}'", DBPATH + "FormulaOneBackup.mdf");
                    using (SqlCommand bu2 = new SqlCommand(sqlStmt, conn))
                    {
                        conn.Open();
                        bu2.ExecuteNonQuery();
                        conn.Close();
                        Console.WriteLine("\nDatabase restore successfully\n");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\nDatabase restore successfully\n");
            }
        }
    }
}
