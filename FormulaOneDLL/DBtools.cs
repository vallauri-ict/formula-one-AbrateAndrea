﻿using System;
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

        // Countries

        public List<string> GetCountries()
        {
            List<string> retVal = new List<string>();
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                String sql = "SELECT * FROM country";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string countryCode = reader.GetString(0);
                            string countryName = reader.GetString(1);
                            Console.WriteLine("{0} {1} ", countryCode, countryName);
                            retVal.Add(countryCode + " - " + countryName);
                        }
                    }
                }
            }
            return retVal;
        }

        public List<Country> GetCountriesObj()
        {
            List<Country> retVal = new List<Country>();
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                String sql = "SELECT * FROM country";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string countryCode = reader.GetString(0);
                            string countryName = reader.GetString(1);
                            Console.WriteLine("{0} {1} ", countryCode, countryName);
                            retVal.Add(new Country(countryCode, countryName));
                        }
                    }
                }
            }
            return retVal;
        }

        public Country GetCountry(string isoCode)
        {
            Country retVal = null;
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                String sql = "SELECT * FROM country WHERE countryCode='" + isoCode + "';";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string countryCode = reader.GetString(0);
                            string countryName = reader.GetString(1);
                            Console.WriteLine("{0} {1} ", countryCode, countryName);
                            retVal = new Country(countryCode, countryName);
                        }
                    }
                }
            }
            return retVal;
        }

        // Drivers

        public List<Driver> GetDriversObj()
        {
            List<Driver> retVal = new List<Driver>();
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                String sql = "SELECT * FROM driver";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int number = reader.GetInt32(1);
                            string name = reader.GetString(2);
                            DateTime date = reader.GetDateTime(3);
                            byte[] HelmetImage = reader["HelmetImage"] as byte[];
                            byte[] Image = reader["Image"] as byte[];
                            int TeamID = reader.GetInt32(6);
                            int podiums = reader.GetInt32(7);
                            string countryCode = reader.GetString(8);
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", id, number, name, date, HelmetImage, Image, TeamID, podiums, countryCode);
                            retVal.Add(new Driver(id, number, name, date, HelmetImage, Image, TeamID, podiums, countryCode));
                        }
                    }
                }
            }
            return retVal;
        }

        public Driver GetDriver(string id)
        {
            Driver retVal = null;
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                String sql = "SELECT * FROM driver WHERE id='" + id + "';";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id_driver = reader.GetInt32(0);
                            int number = reader.GetInt32(1);
                            string name = reader.GetString(2);
                            DateTime date = reader.GetDateTime(3);
                            byte[] HelmetImage = reader["HelmetImage"] as byte[];
                            byte[] Image = reader["Image"] as byte[];
                            int TeamID = reader.GetInt32(6);
                            int podiums = reader.GetInt32(7);
                            string countryCode = reader.GetString(8);
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", id_driver, number, name, date, HelmetImage, Image, TeamID, podiums, countryCode);
                            retVal= new Driver(id_driver, number, name, date, HelmetImage, Image, TeamID, podiums, countryCode);
                        }
                    }
                }
            }
            return retVal;
        }

        public Driver GetDriverNumber(int number)
        {
            Driver retVal = null;
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                String sql = "SELECT * FROM driver WHERE number='" + number + "';";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = 0;
                            int number_driver = reader.GetInt32(1);
                            string name = reader.GetString(2);
                            DateTime date = reader.GetDateTime(3);
                            byte[] HelmetImage = reader["HelmetImage"] as byte[];
                            byte[] Image = reader["Image"] as byte[];
                            int TeamID = reader.GetInt32(6);
                            int podiums = reader.GetInt32(7);
                            string countryCode = reader.GetString(8);
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", id, number, name, date, HelmetImage, Image, TeamID, podiums, countryCode);
                            retVal = new Driver(id, number, name, date, HelmetImage, Image, TeamID, podiums, countryCode);
                        }
                    }
                }
            }
            return retVal;
        }
        // Team

        public List<Team> GetTeamsObj()
        {
            List<Team> retVal = new List<Team>();
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                String sql = "SELECT * FROM team";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string teamName = reader.GetString(1);
                            byte[] teamLogo = reader["teamLogo"] as byte[];
                            string baseT = reader.GetString(3);
                            string teamChief = reader.GetString(4);
                            string technicalChief = reader.GetString(5);
                            string powerUnit = reader.GetString(6);
                            byte[] carImage = reader["carImage"] as byte[];
                            string countryID = reader.GetString(8);
                            int worldChampionships = reader.GetInt32(9);
                            int polePositions = reader.GetInt32(10);
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}", id, teamName, teamLogo, baseT, teamChief, technicalChief, powerUnit, carImage, countryID, worldChampionships, polePositions);
                            retVal.Add(new Team(id, teamName, teamLogo, baseT, teamChief, technicalChief, powerUnit, carImage, countryID, worldChampionships, polePositions));
                        }
                    }
                }
            }
            return retVal;
        }

        public Team GetTeam(string id)
        {
            Team retVal = null;
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                String sql = "SELECT * FROM team WHERE id='" + id + "';";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id_team = reader.GetInt32(0);
                            string teamName = reader.GetString(1);
                            byte[] teamLogo = reader["teamLogo"] as byte[];
                            string baseT = reader.GetString(3);
                            string teamChief = reader.GetString(4);
                            string technicalChief = reader.GetString(5);
                            string powerUnit = reader.GetString(6);
                            byte[] carImage = reader["carImage"] as byte[];
                            string countryID = reader.GetString(8);
                            int worldChampionships = reader.GetInt32(9);
                            int polePositions = reader.GetInt32(10);
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}", id_team, teamName, teamLogo, baseT, teamChief, technicalChief, powerUnit, carImage, countryID, worldChampionships, polePositions);
                            retVal= new Team(id_team, teamName, teamLogo, baseT, teamChief, technicalChief, powerUnit, carImage, countryID, worldChampionships, polePositions);
                        }
                    }
                }
            }
            return retVal;
        }
    }
}