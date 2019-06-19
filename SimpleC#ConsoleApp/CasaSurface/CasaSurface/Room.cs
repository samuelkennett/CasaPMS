using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaSurface
{
    class Room
    {
        public string m_roomNumber;  
        public string m_status;
        bool m_cleaningInProgress;
        bool m_roomCleanStatus;

        public Room()
        {

            string roomNumber = this.m_roomNumber;
            string status = this.m_status;
            bool cleaningInProgress = this.m_cleaningInProgress;
            bool roomCleanStatus = this.m_roomCleanStatus;
        }
        public void beginCleaning(string roomNumber)
        {
            //roomNumber must be passed for SQLCommand to know which Room field to update in the Database
            this.m_cleaningInProgress = true;
            this.m_roomCleanStatus = false;
            this.m_status = "'NotClean'";
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Cleaning is In progress");
            
            //Database call
            SQLCommands.UpdateBeginCleaning(roomNumber, BoolToBin(m_cleaningInProgress), BoolToBin(m_roomCleanStatus)); //converts boolean value to string for sql command
            SQLCommands.UpdateRoomStatus(roomNumber, this.m_status);//updates the database field of Status to "NotClean" given a specific room number
        }

        public void finishCleaning(string roomNumber)
        {
            //roomNumber must be passed for SQLCommand to know which Room field to update in the Database
            this.m_cleaningInProgress = false;
            this.m_roomCleanStatus = true;
            this.m_status = "'Clean'";
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Room has finished being cleaned.");

            //Database Call
            SQLCommands.UpdateFinishCleaning(roomNumber, BoolToBin(m_roomCleanStatus), BoolToBin(m_cleaningInProgress));
            SQLCommands.UpdateRoomStatus(roomNumber, this.m_status);//updates the database field of Status to "Clean" given a specific room number.


        }

        public string GetRoomNumber()
        {
            return this.m_roomNumber;
        }

        public void SetRoomNumber(string roomNumber)
        {
            this.m_roomNumber = roomNumber;
        }
        public bool GetCleaningInProgress()
        {

            return this.m_cleaningInProgress;
        }

        public void SetCleaningInProgresss(bool cleaningInProgress)
        {
            this.m_cleaningInProgress = cleaningInProgress;
        }

        public string GetStatus()
        {
            return this.m_status;
        }

        public void SetStatus(string status)
        {
            this.m_status = status;
        }

        public bool GetRoomCleanStatus()
        {
            return this.m_roomCleanStatus;
        }

        public void SetRoomCleanStatus(bool roomCleanStatus)
        {
            this.m_roomCleanStatus = roomCleanStatus;

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
