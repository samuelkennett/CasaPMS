using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CasaSurface
{
    public class SQLCommands
    {
        //database connection string for Sam-PC, this is my local server, Initial Catalog is the Database, Integrated Security is supposed to require permission when altering a database but does nothing right now.
        public const string m_strDbConnectionString = "Data Source=Sam-PC; Initial Catalog = CasaDatabase; Integrated Security=SSPI"; 
                                                                                                                                
        public const SqlDataReader rdr = null;

        public static void SelectAllRooms() //Just a test funtion to ensure that data is properly pulled from the Database
        {
            try
            {

                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("select roomNumber from Room", con);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0]);
                }
                con.Close();

            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
            
            

        }

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
                        return -1;
                    }
                    

                }
                con.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("SQL Command Error!");
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

    }

}

