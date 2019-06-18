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
                SqlCommand cmd = new SqlCommand("UPDATE Room SET CleaningInProgress="+ cleaningInProgress + "WHERE roomNumber =" + roomNumber, con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("UPDATE Room SET RoomCleanStatus =" + roomCleanStatus + "WHERE roomNumber =" + roomNumber, con);
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
                SqlCommand cmd = new SqlCommand("UPDATE Room SET RoomCleanStatus =" + roomCleanStatus + "WHERE roomNumber =" + roomNumber, con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("UPDATE Room SET CleaningInProgress =" + cleaningInProgress + "WHERE roomNumber =" + roomNumber, con);
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
               
            }
        }
    }
}
