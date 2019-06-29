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

        public static void UpdateBeginCleaning(string strRoomNumber, int intCleaningInProgressFlag, int nRoomCleanStatus)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Rooms SET CleaningInProgressFlag="+ intCleaningInProgressFlag + "WHERE roomNumber =" + strRoomNumber, con); //updates the CleaningInProgress field to 1 in database
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("UPDATE Rooms SET HousekeepingStatus =" + nRoomCleanStatus + "WHERE roomNumber =" + strRoomNumber, con); //updates the HouseKeepingStatus field to 0 in database
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("UPDATE Rooms SET InspectionFlag = 0 WHERE RoomNumber =" + strRoomNumber, con);
                cmd3.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!");
                Console.WriteLine(ex.ToString());
            }
        }

        public static void UpdateFinishCleaning(string strRoomNumber, int intCleaningInProgressFlag, int strRoomCleanStatus)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd2 = new SqlCommand("UPDATE Rooms SET CleaningInProgressFlag =" + intCleaningInProgressFlag + "WHERE RoomNumber =" + strRoomNumber, con); //Updates the CleaningInProgress field to 0 in DB: CasaDatabase --> Table: Rooms
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("UPDATE Rooms SET InspectionFlag = 1 WHERE RoomNumber = " + strRoomNumber, con);//Updates the InspectionFlag field to 1 in DB: Casadatabase --> Table: Rooms
                cmd3.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UpdateFinishCleaning");
                Console.WriteLine(ex.ToString());
               
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

        
        public static void UpdateHouseKeepingStatus(string strRoomNumber, string strStatus)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Rooms SET HousekeepingStatus =" + strStatus + " WHERE roomNumber=" + strRoomNumber, con); //Updates the HousekeepingStatus field in DB: CasaDatabase --> Table: Rooms
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating Status!");
                Console.WriteLine(ex.ToString());
            }
        }
        //use time strings
        public static void UpdateTimeIn(string strRoomNumber, string strTimeIn)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE RoomCleaning SET TimeIn= '" + strTimeIn +  "' WHERE RmNmbr=" + strRoomNumber, con);//Updates the TimeIn field to the current time in DB: CasaDatabase --> Table: RoomCleaning
                cmd.ExecuteNonQuery();
                con.Close();
            }
            
            catch(Exception ex)
            {
                Console.WriteLine("Error Updating Time In");
                Console.WriteLine(ex.ToString());
            }
        }

        public static void UpdateTimeOut(string strRoomNumber, string strTimeOut)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE RoomCleaning SET TimeOut= '"+ strTimeOut + "' WHERE RmNmbr=" + strRoomNumber, con); //Updates the TimeOut field to the current time in DB: CasaDatabase --> Table: RoomCleaning
                cmd.ExecuteNonQuery();
                con.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error Updating Time In");
                Console.WriteLine(ex.ToString());
            }
        }

        public static void UpdateHouseKeeper(string strRoomNumber, string strHskprName)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Rooms SET HskprName = " + strHskprName + " WHERE RoomNumber = " + strRoomNumber, con);//Updates the field HskprName in DB: CasaDatabase --> Table: Rooms
                cmd.ExecuteNonQuery();
                con.Close();
            }
            
            catch(Exception ex)
            {
                Console.WriteLine("Error in UpdateHouseKeeper");
                Console.WriteLine(ex.ToString());
            }
        }

        public static void UpdateTimeInAsStr(string strRoomNumber, string strTimeInAsString){
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE RoomCleaning SET TimeInAsStr= '" + strTimeInAsString + "' WHERE RmNmbr=" + strRoomNumber, con);//Updates the TimeOutAsStr field to the current time in DB: CasaDatabase --> Table: RoomCleaning
                cmd.ExecuteNonQuery();
                con.Close();
            }

            catch(Exception ex)
            {
                Console.WriteLine("Error in UpdatingTimeInAsStr");
                Console.WriteLine(ex.ToString());
            }
        }

        public static void UpdateTimeOutAsStr(string strRoomNumber, string strTimeOutAsStr)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE RoomCleaning SET TimeOutAsStr= '" + strTimeOutAsStr + "' WHERE RmNmbr=" + strRoomNumber, con);//Updates the TimeInAsStr field to the current time in DB: CasaDatabase --> Table: RoomCleaning
                cmd.ExecuteNonQuery();
                con.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error in UpdatingTimeInAsStr");
                Console.WriteLine(ex.ToString());
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

        /*public static List<UserName> SQLCreateUserNames()
        {
            try
            {
                string strFirstName = null;
                string strLastName = null;
                int nEmployeeID = -1;
                List<UserName> lUserList = new List<UserName>();

                SqlConnection con = new SqlConnection(m_strDbConnectionStringToMgmtSysConfig);
                con.Open();
                SqlCommand loop = new SqlCommand("SELECT * FROM RoomAttendants", con);
                SqlCommand cmd = new SqlCommand("SELECT FirstName FROM RoomAttendants WHERE Active = 1 AND FloorAttendant = 1", con);
                SqlCommand cmd2 = new SqlCommand("SELECT LastName FROM RoomAttendants WHERE Active = 1 AND FloorAttendant = 1", con);
                SqlCommand cmd3 = new SqlCommand("SELECT EmployeeID FROM RoomAttendants WHERE Active = 1 AND FloorAttendant = 1", con);

             
                  using (SqlDataReader reader = cmd.ExecuteReader())
                  {
                        if (reader.Read())
                        {
                             strFirstName = reader.GetString(0);
                            reader.Close();
                        }
                        else
                        {
                             Console.WriteLine("Nothing to reader when trying to get FirstName from RoomAttendants.");
                            reader.Close();
                        }
                        reader.Close();
                  }
                  using (SqlDataReader reader = cmd2.ExecuteReader())
                  {
                        if (reader.Read())
                        {
                            strLastName = reader.GetString(0);
                            reader.Close();
                        }
                        else
                        {
                             Console.WriteLine("Nothing to read when trying LastName name from RoomAttendants.");
                             reader.Close();
                        }
                  }
                  using (SqlDataReader reader = cmd3.ExecuteReader())
                  {
                        if (reader.Read())
                        {
                                nEmployeeID = reader.GetInt16(0);
                                reader.Close();
                            }
                            else
                            {
                                Console.WriteLine("Nothing to read when trying to get EmployeeID from RoomAttendants.");
                                reader.Close();
                            }
                            reader.Close();
                  }                      
                        UserName user = new UserName(strFirstName, strLastName, nEmployeeID);
                        lUserList.Add(user);
                        return lUserList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in SQLCreateUserNames");
                Console.WriteLine(ex.ToString());
                return null;
            }
        }*/

        public static List<UserName> SQLCreateUsers()//Clones the Database table RoomAttendants and creates a new UserName object for each Row where Active = 1 and FloorAttendant = 1.
        {
            SqlConnection con = new SqlConnection(m_strDbConnectionStringToMgmtSysConfig);
            con.Open();
            List<UserName> lUserList = new List<UserName>(); //creates an empty list if UserName objects.
            SqlCommand cmd = new SqlCommand("SELECT * FROM RoomAttendants WHERE Active = 1 AND FloorAttendant = 1", con);// Queries all rows and columns in RoomAttendants.
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();//Creates a new DataTable 
            da.Fill(dt);//Fills it with the data that is specified in the query. In This case its the entire table.
            foreach(DataRow dr in dt.Rows)
            {
                if(dr["Active"].ToString() == "1" && dr["FloorAttendant"].ToString() == "1")//specifies the paramters for which rows should be pulled
                {
                    UserName user = new UserName(dr["FirstName"].ToString(), dr["LastName"].ToString(), dr["EmployeeId"].ToString());//Creates a new user object.
                    lUserList.Add(user);//adds the UserName object to the list.
                }
            }
            return lUserList;//returns the list.
        }
    }
}

