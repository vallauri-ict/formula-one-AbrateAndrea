using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace FormulaOneConsole
{
    class Program
    {
        public const string WORKINGPATH = @"C:\data\formulaone\";
        public const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + WORKINGPATH + @"formulaone.mdf;Integrated Security=True";

        static void Main(string[] args)
        {
            char scelta = ' ';
            do
            {
                Console.WriteLine("\n*** FORMULA ONE - BATCH SCRIPTS ***\n");
                Console.WriteLine("1 - Create Countries");
                Console.WriteLine("2 - Create Teams");
                Console.WriteLine("3 - Create Drivers");
                Console.WriteLine("------------------");
                Console.WriteLine("R - Reset");
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
                    case 'R':
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
            DropTable("Team");
            DropTable("Driver");
            DropTable("Country");
            ExecuteSqlScript("Countries.sql");
            ExecuteSqlScript("Drivers.sql");
            ExecuteSqlScript("Teams.sql");
            Console.WriteLine("RESET AVVENUTO CON SUCCESSO");
        }
        public static bool ExecuteSqlScript(string scriptName)
        {
            try
            {
                var fileContent = File.ReadAllText(WORKINGPATH + scriptName);
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
    }
}
