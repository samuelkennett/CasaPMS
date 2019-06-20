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
        public const string m_strDbConnectionString = "Data Source=Sam-PC; Initial Catalog = TestDB; Integrated Security=SSPI"; 
                                                                                                                                
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

        public static void UpdateBeginCleaning(string strRoomNumber, int intCleaningInProgress, int nRoomCleanStatus)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Room SET CleaningInProgress="+ intCleaningInProgress + "WHERE roomNumber =" + strRoomNumber, con); //updates the CleaningInProgress field to TRUE in database
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("UPDATE Room SET RoomCleanStatus =" + nRoomCleanStatus + "WHERE roomNumber =" + strRoomNumber, con); //updates the HouseKeepingStatus field to FALSE in database
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("UPDATE Room SET InspectionFlag = 0 WHERE RoomNumber =" + strRoomNumber, con);
                cmd3.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!");
                Console.WriteLine(ex.ToString());
            }
        }

        public static void UpdateFinishCleaning(string strRoomNumber, int intCleaningInProgress, int strRoomCleanStatus)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Room SET RoomCleanStatus =" + strRoomCleanStatus + "WHERE RoomNumber =" + strRoomNumber, con); //Updates the RoomCleanStatus to TRUE in the database
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("UPDATE Room SET CleaningInProgress =" + intCleaningInProgress + "WHERE RoomNumber =" + strRoomNumber, con); //Updates the CleaningInProgress field to FALSE in the database
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("UPDATE Room SET InspectionFlag = 1 WHERE RoomNumber = " + strRoomNumber, con);
                cmd3.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UpdateFinishCleaning");
                Console.WriteLine(ex.ToString());
               
            }
        }

        public static int SQLGetCleaningInProgress(string strRoomNumber) //Retrieves a RoomCleanStatus value from the database given a room number. Then converts the TRUE or FALSE value to a String.
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT CleaningInProgress FROM Room WHERE roomNumber =" + strRoomNumber, con);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int result = reader.GetInt32(0);
                        reader.Close();
                        return result;//This will return "1" or "0" as string
                        

                    }
                    else
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

        //UpdateStatus
        public static void UpdateRoomStatus(string strRoomNumber, string strStatus)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Room SET HouseKeepingStatus =" + strStatus + " WHERE roomNumber=" + strRoomNumber, con); //Updates the Status field in the database given a specific room number 
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
        public static void UpdateTimeIn(string strRoomNumber)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Room SET TimeIn= GETDATE() WHERE RoomNumber=" + strRoomNumber, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            
            catch(Exception ex)
            {
                Console.WriteLine("Error Updating Time In");
                Console.WriteLine(ex.ToString());
            }
        }

        public static void UpdateTimeOut(string strRoomNumber)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_strDbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Room SET TimeOut= GETDATE() WHERE RoomNumber=" + strRoomNumber, con);
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
                SqlCommand cmd = new SqlCommand("UPDATE Room SET HskprName = " + strHskprName + " WHERE RoomNumber = " + strRoomNumber, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            
            catch(Exception ex)
            {
                Console.WriteLine("Error in UpdateHouseKeeper");
                Console.WriteLine(ex.ToString());
            }
        }

    

    }
        
}

