using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace FormulaOneDLL
{
    public class DbTools
    {
        public DbTools() { }
        public string sql;
        public const string QUERYPATH = @"C:\data\formulaone\queries";
        public const string DBPATH = @"C:\data\formulaone\";
        public const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+ DBPATH + "FormulaOne.mdf;Integrated Security=True";

        public List<string> GetTablesName()
        {
            List<string> names = new List<string>();
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                string sql = $"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataReader re = command.ExecuteReader())
                    {
                        while (re.Read())
                        {
                            names.Add(re.GetString(0));
                        }
                    }
                }
            }
            return names;
        }

        public DataTable GetTable(string Nametable)
        {
            DataTable dt =new DataTable();
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                string sql = $"SELECT * FROM {Nametable}";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }


    }
}