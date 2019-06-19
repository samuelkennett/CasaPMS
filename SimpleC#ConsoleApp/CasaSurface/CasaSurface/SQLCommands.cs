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
        public const string m_dbConnectionString = "Data Source=Sam-PC; Initial Catalog = TestDB; Integrated Security=SSPI"; 
                                                                                                                                
        public const SqlDataReader rdr = null;

        public static void SelectAllRooms() //Just a test funtion to ensure that data is properly pulled from the Database
        {
            try
            {

                SqlConnection con = new SqlConnection(m_dbConnectionString);
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

        public static void UpdateBeginCleaning(string roomNumber, string cleaningInProgress, string roomCleanStatus)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_dbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Room SET CleaningInProgress="+ cleaningInProgress + "WHERE roomNumber =" + roomNumber, con); //updates the CleaningInProgress field to TRUE in database
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("UPDATE Room SET RoomCleanStatus =" + roomCleanStatus + "WHERE roomNumber =" + roomNumber, con); //updates the RoomCleanStatus field to FALSE in database
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
            }
        }

        public static void UpdateFinishCleaning(string roomNumber, string roomCleanStatus, string cleaningInProgress)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_dbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Room SET RoomCleanStatus =" + roomCleanStatus + "WHERE roomNumber =" + roomNumber, con); //Updates the RoomCleanStatus to TRUE in the database
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("UPDATE Room SET CleaningInProgress =" + cleaningInProgress + "WHERE roomNumber =" + roomNumber, con); //Updates the CleaningInProgress field to FALSE in the database
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
               
            }
        }

        public static string SQLGetCleaningInProgress(string roomNumber) //Retrieves a RoomCleanStatus value from the database given a room number. Then converts the TRUE or FALSE value to a String.
        {
            try
            {
                SqlConnection con = new SqlConnection(m_dbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT CleaningInProgress FROM Room WHERE roomNumber =" + roomNumber, con);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return String.Format("{0}", reader["CleaningInProgress"]);//converts bool to string

                    }
                    else
                    {
                        return "Null Field";
                    }
                    
                }
                con.Close();
            }

            catch (Exception)
            {
                Console.WriteLine("SQL Command Error!");
                return "-1";
            }

        }

        //UpdateStatus
        public static void UpdateRoomStatus(string roomNumber, string status)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_dbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Room SET Status =" + status + " WHERE roomNumber=" + roomNumber, con); //Updates the Status field in the database given a specific room number 
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating Status!");
                Console.WriteLine(ex.ToString());
            }
        }

    }
        
}

