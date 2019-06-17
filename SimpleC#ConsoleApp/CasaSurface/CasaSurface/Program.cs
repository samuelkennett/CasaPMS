using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int  roomNumber;  //unused in this program
        string      status;
        bool        cleaningInProgress;
        bool        roomCleanStatus;

        public Room()
        {
            // roomNumber needs to be string: strRoomNumber
            int roomNumber = this.roomNumber;
            string status = this.status;
            bool cleaningInProgress = this.cleaningInProgress;
            bool roomCleanStatus = this.roomCleanStatus;
        }
        public void beginCleaning()
        {
            
            this.cleaningInProgress = true;
            this.status = "Not Clean";
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Cleaning is In progress");
           // data will updated here
        }

        public void finishCleaning()
        {
            this.cleaningInProgress = false;
            this.status = "Clean";
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Room has finished being cleaned.");
            
        }

        public bool GetCleaningInProgress()
        {
            return this.cleaningInProgress;
        }

        public string GetStatus()
        {
            return this.status;
        }
    }
}
