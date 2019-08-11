using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace CasaSurface
{
    public class SQLCommands
    {
        //database connection string for Sam-PC, this is my local server, Initial Catalog is the Database, Integrated Security is supposed to require permission when altering a database but does nothing right now.
        public const string m_strDbConnectionString = "Data Source=Sam-PC; Initial Catalog = CasaDatabase; Integrated Security=SSPI"; 
        public const string m_strDbConnectionStringToMgmtSysConfig = "Data Source=Sam-PC; Initial Catalog = MgmtSysConfig; Integrated Security=SSPI; MultipleActiveResultSets=true";

        public static void Update(string strQuery, string strConnectionString)//Generic Update 
        {
            try
            {
                SqlConnection con = new SqlConnection(strConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error In SQLCommands.Update");
                Console.WriteLine(ex.ToString());
            }
        }

        public static int GetInt(string strQuery, string strConnectionString)//Generic Get that retrives a smallint.
        {
            try
            {
                SqlConnection con = new SqlConnection(strConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(strQuery);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int result = reader.GetInt16(0);
                        reader.Close();
                        return result;
                    }
                    else 
                    {
                        reader.Close();
                        Console.WriteLine("No value to read when trying SQLCommands.GetInt!");
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in SQLCommands.GetInt!");
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
        public static int SQLGetCleaningInProgress(string strRoomNumber) //Retrieves a CleaningInProgressFlag value from DB: CasaDatabase --> Table: Rooms
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT CleaningInProgressFlag FROM Rooms WHERE roomNumber =" + strRoomNumber, con);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int result = reader.GetInt16(0);// reads a smallint value, i.e. GetInt16 instead of GetInt32
                        reader.Close();
                        return result;
                        

                    }
                    else //Error Handling Required HERE!
                    {
                        reader.Close();
                        Console.WriteLine("No value to read: SQLGetCleaningInProgress");
                        return -1;
                    }
                    

                }
                con.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("SQL Command Error in SQLGetCleaningInProgress!");
                Console.WriteLine(ex.ToString());
                return -1;
            }

        }

        public static int SQLGetInspectionFlag(string strRoomNumber) //Retrieves a InspectionFlag value from DB: CasaDatabase --> Table: Rooms
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT InspectionFlag FROM Rooms WHERE roomNumber =" + strRoomNumber, con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int result = reader.GetInt16(0);// reads a smallint value, i.e. GetInt16 instead of GetInt32
                        reader.Close();
                        return result;


                    }
                    else //Error Handling Required HERE!
                    {
                        reader.Close();
                        Console.WriteLine("No value to read: SQLGetInspectionFlag");
                        return -1;
                    }


                }
                con.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("SQL Command Error in SQLGetInspectionFlag!");
                Console.WriteLine(ex.ToString());
                return -1;
            }

        }

        public static DateTime SQLGetTimeIn(string strRoomNumber)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT TimeIn FROM RoomCleaning WHERE RmNmbr=" + strRoomNumber, con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime result = reader.GetDateTime(0);
                        reader.Close();
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("Nothing to read when running SQLGetTimeIn");
                        reader.Close();
                        return DateTime.Now;
                    }
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine("Error in SQLGetTimeIn");
                Console.WriteLine(ex.ToString());
                return DateTime.Now;
            }
        }
        
        public static void UpdateElapsedTime(string strRoomNumber, DateTime dtTimeIn, DateTime dtTimeOut) 
        {
            try
            {
                TimeSpan tsTimeDiff = dtTimeOut - dtTimeIn;
                double dlTimeDiff = tsTimeDiff.TotalMinutes;
                double dlResult = Math.Round(dlTimeDiff, 2);
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("Update RoomCleaning SET ElapsedTime=" + dlResult + " WHERE RmNmbr=" + strRoomNumber, con);
                Console.WriteLine(tsTimeDiff);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            catch(Exception ex)
            {
                Console.WriteLine("Error in UpdateTimeElapsed");
                Console.WriteLine(ex.ToString());        
            }
        }

        public static void SetRoomAttendant(string strRoomNumber)
        {
            SqlConnection con = new SqlConnection(m_strDbConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT HskprName FROM Rooms WHERE RoomNumber=" + strRoomNumber, con);
            using(SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    string strRoomAttendant = reader.GetString(0);
                    reader.Close();
                    SqlCommand cmd2 = new SqlCommand("UPDATE RoomCleaning SET RoomAttendant= '" + strRoomAttendant + "' WHERE RmNmbr=" + strRoomNumber, con);
                    cmd2.ExecuteNonQuery(); 
                }
                else
                {
                    Console.WriteLine("Nothing to read when trying SetRoomAttendant");
                    reader.Close();
                }
                
            }
        }

        public static int SQLImposibilityCheck(string roomNumber)//Checks to see if RoomCleaningFlag = 1 and InspectionFlag = 1. 
        {
            SqlConnection con = new SqlConnection(m_strDbConnectionString);
            con.Open();
            int nCleaningInProgressFlag = -1;
            int nInspectionFlag = -1;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT CleaningInProgressFlag FROM Rooms WHERE RoomNumber=" + roomNumber, con);
                SqlCommand cmd2 = new SqlCommand("SELECT InspectionFlag FROM Rooms WHERE RoomNumber=" + roomNumber, con);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        nCleaningInProgressFlag = reader.GetInt16(0);
                        reader.Close();
                    }
                    else
                    {
                        Console.WriteLine("Nothing to read in SQLImposibilityCheck --> CleaningInProgressFlag");
                        return -1;
                    }
                }
                using(SqlDataReader reader = cmd2.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        nInspectionFlag = reader.GetInt16(0);
                        reader.Close();
                    }
                    else
                    {
                        Console.WriteLine("Nothing to read in SQLImposibilityCheck --> InspectionFlag");
                        return -1;
                    }

                }

                if(nCleaningInProgressFlag == 1 && nInspectionFlag == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in SQLImposibilityCheck");
                Console.WriteLine(ex.ToString());
                return -1;
            }
            
        }
        public static void SQLRevert(string strRoomNumber)// Reverts the CleaningInProgressFlag and InspectionFlag to 0.
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Rooms SET CleaningInProgressFlag=0 WHERE RoomNumber =" + strRoomNumber, con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("UPDATE Rooms SET InspectionFlag = 0 WHERE RoomNumber = " + strRoomNumber, con);
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error at SQLRevert");
                Console.WriteLine(ex.ToString());
            }
        }

        public static List<RoomAttendantName> SQLCreateUsers()//Clones the Database table RoomAttendants and creates a new UserName object for each Row where Active = 1 and FloorAttendant = 1.
        {
            SqlConnection con = new SqlConnection(m_strDbConnectionStringToMgmtSysConfig);
            con.Open();
            List<RoomAttendantName> lUserList = new List<RoomAttendantName>(); //creates an empty list if UserName objects.
            SqlCommand cmd = new SqlCommand("SELECT * FROM RoomAttendants", con);// Queries all rows and columns in RoomAttendants.
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();//Creates a new DataTable 
            da.Fill(dt);//Fills it with the data that is specified in the query. In This case its the entire table.
            foreach(DataRow dr in dt.Rows)
            {
                if(dr["Active"].ToString() == "1" && dr["FloorAttendant"].ToString() == "1")//specifies the paramters for which rows should be pulled
                {
                    RoomAttendantName user = new RoomAttendantName(dr["FirstName"].ToString(), dr["LastName"].ToString(), dr["EmployeeId"].ToString(), dr["UserName"].ToString());//Creates a new user object.
                    lUserList.Add(user);//adds the UserName object to the list.
                }
            }
            return lUserList;//returns the list.
        }

        public static List<string> GetRmGrpDtl(string strQuery, string strConnectionString, string strRoomGroup)
        {
            SqlConnection con = new SqlConnection(strConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(strQuery,con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();//Creates a new DataTable 
            da.Fill(dt);
            List<string> lRooms = new List<string>();
            try
            {
                foreach(DataRow dr in dt.Rows)
                {
                    lRooms.Add(dr["RmNmbr"].ToString());
                }
                return lRooms;                                
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetRmGrpDtl");
                Console.WriteLine(ex.ToString());
                return lRooms;
            }
        }
    }
}

