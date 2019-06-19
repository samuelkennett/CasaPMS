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
            Console.WriteLine();
            Console.WriteLine();
              

            
            
            while (begin == "0") {  //begins simple program loop 0 is start/continue and 9 is exit

                

                Console.WriteLine("Please Select a Room");
                Console.WriteLine("--------------------");
                Console.WriteLine("1. Room 101");
                Console.WriteLine("2. Room 102");
                Console.WriteLine("3. Room 103");
                Room room = new Room();


                string input = Console.ReadLine();
                int response = Convert.ToInt32(input);  //needs to convert string to int for switch statement
                switch (response)
                {
                    case 1:

                        Console.WriteLine("Room 101 Selected");
                        room.SetRoomNumber("101");                     
                        break;

                    case 2:

                        Console.WriteLine("Room 102 Selected");
                        room.SetRoomNumber("102");
                        break;

                    case 3:

                        Console.WriteLine("Room 103 Selected");
                        room.SetRoomNumber("103");
                        break;

                }


                Console.WriteLine("Press 1 to begin cleaning");
                Console.WriteLine("Press 2 to finish cleaning");
                Console.WriteLine();
                string input2 = Console.ReadLine();
                int response2 = Convert.ToInt32(input2);

                switch (response2)
                {
                    case 1:

                        Console.WriteLine("Begin room clean.");
                        if(SQLCommands.SQLGetCleaningInProgress(room.GetRoomNumber()) == "False")
                        {
                            room.beginCleaning(room.m_roomNumber);
                            Console.WriteLine("Current status: " + room.GetStatus());
                            Console.WriteLine("------------");
                        }
                        else
                        {
                            Console.WriteLine("Room has already begun to be cleaned.");
                            Console.WriteLine("Current status: " + room.GetStatus());
                            Console.WriteLine("------------");
                            break;
                        }
                        break;

                    case 2:
                        Console.WriteLine("Finish cleaning room.");
                        if(SQLCommands.SQLGetCleaningInProgress(room.GetRoomNumber()) == "True")
                        {
                            room.finishCleaning(room.m_roomNumber);
                            Console.WriteLine("Current status: " + room.GetStatus());
                            Console.WriteLine("------------");
                        }
                        else
                        {
                            Console.WriteLine("Room has already been cleaned");
                            Console.WriteLine("Current status: " + room.GetStatus());
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
}
