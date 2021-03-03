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

        public Driver GetDriver(int id)
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

        public Driver GetDriverName(string name)
        {
            Driver retVal = null;
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                String sql = "SELECT * FROM driver WHERE name='" + name + "';";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = 0;
                            int number = reader.GetInt32(1);
                            string name_driver = reader.GetString(2);
                            DateTime date = reader.GetDateTime(3);
                            byte[] HelmetImage = reader["HelmetImage"] as byte[];
                            byte[] Image = reader["Image"] as byte[];
                            int TeamID = reader.GetInt32(6);
                            int podiums = reader.GetInt32(7);
                            string countryCode = reader.GetString(8);
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", id, number, name_driver, date, HelmetImage, Image, TeamID, podiums, countryCode);
                            retVal = new Driver(id, number, name_driver, date, HelmetImage, Image, TeamID, podiums, countryCode);
                        }
                    }
                }
            }
            return retVal;
        }

        public List<DriverListDTO> GetDriversList()
        {
            List<DriverListDTO> retVal = new List<DriverListDTO>();
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                String sql = "SELECT d.Number,d.Name,d.Image,t.TeamName,c.countryCode FROM driver d,team t,country c WHERE d.TeamID=t.ID AND d.CountryCode=c.countryCode;";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int number = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            byte[] Image = reader["Image"] as byte[];
                            string TeamName = reader.GetString(3);
                            string countryCode = reader.GetString(4);
                            Console.WriteLine("{0} {1} {2} {3} {4}", number, name, Image, TeamName, countryCode);
                            retVal.Add(new DriverListDTO(number, name, Image, TeamName, countryCode));
                        }
                    }
                }
            }
            return retVal;
        }

        public DriverDetailsDTO getDriverDetails(int number)
        {
            DriverDetailsDTO retVal = null;
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                string sql = "SELECT d.Number,d.Name,d.Image,t.TeamName,c.countryName,d.Podiums,d.DOB FROM driver d,team t,country c WHERE d.TeamID=t.ID AND d.CountryCode=c.countryCode AND d.Number='" + number + "';";
                using (SqlCommand command = new SqlCommand(sql, dbConn))
                {
                    dbConn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Number = reader.GetInt32(0);
                            string Name = reader.GetString(1);
                            byte[] Image = reader["Image"] as byte[];
                            string Team = reader.GetString(3);
                            string CountryName = reader.GetString(4);
                            int Podiums = reader.GetInt32(5);
                            DateTime DOB = reader.GetDateTime(6);
                            retVal = new DriverDetailsDTO(Number, Name, Image, Team, CountryName, Podiums, DOB);
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

        public Team GetTeam(int id)
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

        public Team GetTeamName(string name)
        {
            Team retVal = null;
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                String sql = "SELECT * FROM team WHERE TeamName='" + name + "';";
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
                            retVal = new Team(id, teamName, teamLogo, baseT, teamChief, technicalChief, powerUnit, carImage, countryID, worldChampionships, polePositions);
                        }
                    }
                }
            }
            return retVal;
        }

        public List<TeamListDTO> GetTeamsList()
        {
            List<TeamListDTO> retVal = new List<TeamListDTO>();
            string tName = "";
            byte[] tLogo = new byte[0];
            string[] dNames = new string[2];
            List<byte[]> dImages = new List<byte[]>();
            byte[] carImage = new byte[0];
            int i = 0;
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                dbConn.Open();
                string sql = "SELECT t.TeamName, t.TeamLogo, d.Name, d.Image, t.CarImage FROM Team t, Driver d WHERE d.TeamID = t.ID ORDER BY t.TeamName";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (i % 2 == 0)
                    {
                        tName = reader.GetString(0);
                        tLogo = reader["TeamLogo"] as byte[];
                        dNames = new string[2];
                        dNames[0] = reader.GetString(2);
                        dImages = new List<byte[]>();
                        dImages.Add(reader["Image"] as byte[]);
                        carImage = reader["CarImage"] as byte[];
                    }
                    else
                    {
                        dNames[1] = reader.GetString(2);
                        dImages.Add(reader["Image"] as byte[]);
                        TeamListDTO team = new TeamListDTO(tName, tLogo, dNames, dImages, carImage);
                        retVal.Add(team);
                    }
                    i++;
                }
            }
            return retVal;
        }

        public TeamDetailsDTO GetTeamsDetails(int id)
        {
            TeamDetailsDTO retVal = null;
            int id_team = 0;
            string TeamName = string.Empty;
            byte[] teamLogo = null;
            string baseT = string.Empty;
            string teamChief = string.Empty;
            string technicalChief = string.Empty;
            string powerUnit = string.Empty;
            byte[] CarImage = null;
            string countryID = string.Empty;
            int worldChampionships = 0;
            int polePositions = 0;
            int[] numbers = new int[2];
            string[] dNames = new string[2];
            List<byte[]> dImages = new List<byte[]>();
            int i = 0;
            using (SqlConnection dbConn = new SqlConnection())
            {
                dbConn.ConnectionString = CONNECTION_STRING;
                dbConn.Open();
                string sql = "SELECT t.id,t.TeamName,t.TeamLogo,t.Base,t.TeamChief,t.TechnicalChief,t.PowerUnit,t.CarImage,t.countryID,t.worldChampionships,t.polePositions,d.number,d.Name,d.Image FROM driver d, team t WHERE d.TeamID = t.ID AND t.ID ='" + id + "';";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (i % 2 == 0)
                    {
                        id_team = reader.GetInt32(0);
                        TeamName = reader.GetString(1); ;
                        teamLogo = reader["TeamLogo"] as byte[]; ;
                        baseT = reader.GetString(3); ;
                        teamChief = reader.GetString(4); ;
                        technicalChief = reader.GetString(5); ;
                        powerUnit = reader.GetString(6); ;
                        CarImage = reader["CarImage"] as byte[]; ;
                        countryID = reader.GetString(8); ;
                        worldChampionships = reader.GetInt32(9);
                        polePositions = reader.GetInt32(10);
                        numbers[0] = reader.GetInt32(11);
                        dNames[0] = reader.GetString(12);
                        dImages.Add(reader["Image"] as byte[]);
                    }
                    else
                    {
                        numbers[1] = reader.GetInt32(11);
                        dNames[1] = reader.GetString(12);
                        dImages.Add(reader["Image"] as byte[]);
                        retVal = new TeamDetailsDTO(id_team, TeamName, teamLogo, baseT, teamChief, technicalChief, powerUnit, CarImage, countryID, worldChampionships, polePositions, numbers, dNames, dImages);
                    }
                    i++;
                }
            }
            return retVal;
        }
    }
}