using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace CasaSurface
{

    

    class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Begin");
            Console.WriteLine("-------------");
            Console.WriteLine("press 0 to begin");
     
            /*
            ** variable naming conventions
                - class members : begin with m_
                    - int       m_nCount
                    - string    m_strBegin;
                    - bool      m_bBegin;
                    - date      m_dtStart;
                    - time      m_tmStart
                    
                - local to function : same as class but without the m_
                    - int       nCount;
                    - string    strBegin;
                    - bool      bBegin;
                    - date      dtBegin;
                    - time      tmBegin
            */
            
            string begin = Console.ReadLine();

            Room room1 = new Room();    //initialzes a new room object

            //----------------------------------------------------
            //Testing Database Calls

            //Just a test funtion to ensure the data is properly pulled from the database.
            SQLCommands.SelectAllRooms();
            

            //----------------------------------------------------




            while (begin == "0") {  //begins simple program loop 0 is start/continue and 9 is exit

                

                Console.WriteLine("Press 1 to begin cleaning");
                Console.WriteLine("Press 2 to finish cleaing");
                Console.WriteLine();

                string input = Console.ReadLine();
                int response = Convert.ToInt32(input);  //needs to convert string to int for switch statement

                switch (response)
                {
                    case 1:

                        Console.WriteLine("Begin room clean.");
                        if(room1.GetCleaningInProgress() == false)
                        {
                            room1.beginCleaning();
                            Console.WriteLine("Current status: " + room1.GetStatus());
                            Console.WriteLine("------------");
                        }
                        else
                        {
                            Console.WriteLine("Room has already begun to be cleaned.");
                            Console.WriteLine("Current status: " + room1.GetStatus());
                            Console.WriteLine("------------");
                            break;
                        }
                        break;

                    case 2:
                        Console.WriteLine("Finish cleaning room.");
                        if(room1.GetCleaningInProgress() == true)
                        {
                            room1.finishCleaning();
                            Console.WriteLine("Current status: " + room1.GetStatus());
                            Console.WriteLine("------------");
                        }
                        else
                        {
                            Console.WriteLine("Room has already been cleaned");
                            Console.WriteLine("Current status: " + room1.GetStatus());
                            Console.WriteLine("------------");
                            break;
                        }
                        break;

                }

                Console.WriteLine("Continue? 0 for yes 9 for no.");

                begin = Console.ReadLine();
            }

        }
    }
    class Room
    {
        public string  m_roomNumber;  //currently unused
        string      m_status;
        bool        m_cleaningInProgress;
        bool        m_roomCleanStatus;

        public Room()
        {
            // roomNumber needs to be string: strRoomNumber
            string roomNumber = this.m_roomNumber;
            string status = this.m_status;
            bool cleaningInProgress = this.m_cleaningInProgress;
            bool roomCleanStatus = this.m_roomCleanStatus;
        }
        public void beginCleaning()
        {
            
            this.m_cleaningInProgress = true;
            this.m_status = "Not Clean";
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Cleaning is In progress");

            // data will updated here
            //SQL function to be put here
            SQLCommands.UpdateBeginCleaned("101");

        }

        public void finishCleaning()
        {
            this.m_cleaningInProgress = false;
            this.m_status = "Clean";
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Room has finished being cleaned.");

            
        }

        public bool GetCleaningInProgress()
        {
            return this.m_cleaningInProgress;
        }

        public string GetStatus()
        {
            return this.m_status;
        }

    }


    //-----------------------------------------------------------------------------------------------------------------------
    //This class will eventually go in its own C# file.
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
                SqlCommand cmd = new SqlCommand("select roomNumber from Room",con);
                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    Console.WriteLine(rdr[0]);
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
            
                
        }

        public static void UpdateBeginCleaned(string roomNumber)
        {
            try
            {
                SqlConnection con = new SqlConnection(m_dbConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Room SET CleaningInProgress = True WHERE roomNumber =" + roomNumber, con);//currently not able to update the table where roomNumber = 101
                cmd.BeginExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
            }
        }

        
    }
    //----------------------------------------------------------------------------------------------------------------------------------
    
}
