using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaSurface
{
    class Room
    {
        public string m_roomNumber;  //currently unused
        string m_status;
        bool m_cleaningInProgress;
        bool m_roomCleanStatus;

        public Room()
        {

            string roomNumber = this.m_roomNumber;
            string status = this.m_status;
            bool cleaningInProgress = this.m_cleaningInProgress;
            bool roomCleanStatus = this.m_roomCleanStatus;
        }
        public void beginCleaning()
        {
            //roomNumber must be passed for SQLCommand to know which Room field to update in the Database
            this.m_cleaningInProgress = true;
            this.m_roomCleanStatus = false;
            this.m_status = "Not Clean";
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Cleaning is In progress");
            
            // data will updated here
            //SQL function to be put here
            SQLCommands.UpdateBeginCleaning("101", BoolToBin(m_cleaningInProgress), BoolToBin(m_roomCleanStatus)); //converts boolean value to string for sql command

        }

        public void finishCleaning()
        {
            //roomNumber must be passed for SQLCommand to know which Room field to update in the Database
            this.m_cleaningInProgress = false;
            this.m_roomCleanStatus = true;
            this.m_status = "Clean";
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Room has finished being cleaned.");

            //Database Call
            SQLCommands.UpdateFinishCleaning("101", BoolToBin(m_roomCleanStatus), BoolToBin(m_cleaningInProgress));


        }

        public bool GetCleaningInProgress()
        {
            return this.m_cleaningInProgress;
        }

        public string GetStatus()
        {
            return this.m_status;
        }

        public string BoolToBin(bool someString) // takes a boolean value and returns a string of either 1 or 0.
        {
            string bin = null;
            if(someString == true)
            {
                bin = "1";
            }
            else
            {
                bin = "0";
            }
            return bin;
        }

    }
}
