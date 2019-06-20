using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaSurface
{
    class Room
    {
        public string m_strRoomNumber;  //m_strRroomNumber;
        public string m_strHouseKeepingStatus;
        int m_nCleaningInProgress;//m_bCleaningInProgress;
        int m_nRoomCleanStatus;
        string m_strHskprName;
        DateTime m_dtTimeIn;
        DateTime m_dtTimeOut;

        public Room()
        {
            string strRoomNumber = this.m_strRoomNumber;
            string strStatus = this.m_strHouseKeepingStatus;
            int nCleaningInProgress = this.m_nCleaningInProgress;
            int nRoomCleanStatus = this.m_nRoomCleanStatus;
            string m_strHskprName = this.m_strHskprName;
            DateTime dtTimeIn = this.m_dtTimeIn;
            DateTime dtTimeOut = this.m_dtTimeOut;
        }
        public void beginCleaning(string strRoomNumber)
        {
            DateTime CurrentDate;
            CurrentDate = DateTime.Now;

            this.m_dtTimeIn = CurrentDate;
            this.m_nCleaningInProgress = 1;
            this.m_nRoomCleanStatus = 0;
            this.m_strHouseKeepingStatus = "'NotClean'";
            this.m_dtTimeIn = CurrentDate;
            Console.WriteLine(CurrentDate);
            Console.WriteLine("Cleaning is In progress");
            
            //Database calls
            SQLCommands.UpdateBeginCleaning(strRoomNumber, m_nCleaningInProgress, m_nRoomCleanStatus); //converts boolean value to string for sql command
            SQLCommands.UpdateRoomStatus(strRoomNumber, this.m_strHouseKeepingStatus);//updates the database field of Status to "NotClean" given a specific room number
            SQLCommands.UpdateTimeIn(strRoomNumber);
            SQLCommands.UpdateHouseKeeper(m_strRoomNumber, m_strHskprName);

        }


        public void finishCleaning(string strRoomNumber)
        {
            DateTime CurrentDate;
            CurrentDate = DateTime.Now;

            this.m_dtTimeOut = CurrentDate; 
            this.m_nCleaningInProgress = 0;
            this.m_nRoomCleanStatus = 1;
            this.m_strHouseKeepingStatus = "'Clean'";
            this.m_dtTimeOut = CurrentDate;
            Console.WriteLine(CurrentDate);
            Console.WriteLine("Room has finished being cleaned.");

            //Database Call
            SQLCommands.UpdateFinishCleaning(strRoomNumber, m_nCleaningInProgress, m_nRoomCleanStatus);
            SQLCommands.UpdateRoomStatus(strRoomNumber, this.m_strHouseKeepingStatus);//updates the database field of Status to "Clean" given a specific room number.
            SQLCommands.UpdateTimeOut(strRoomNumber);

        }

        public string GetRoomNumber()
        {
            return this.m_strRoomNumber;
        }

        public void SetRoomNumber(string strRoomNumber)
        {
            this.m_strRoomNumber = strRoomNumber;
        }
        public int GetCleaningInProgress()
        {

            return this.m_nCleaningInProgress;
        }

        public void SetCleaningInProgresss(int nCleaningInProgress)
        {
            this.m_nCleaningInProgress = nCleaningInProgress;
        }

        public string GetHouseKeepingStatus()
        {
            return this.m_strHouseKeepingStatus;
        }

        public void SetStatus(string strHouseKeepingStatus)
        {
            this.m_strHouseKeepingStatus = strHouseKeepingStatus;
        }

        public int GetRoomCleanStatus()
        {
            return this.m_nRoomCleanStatus;
        }

        public void SetRoomCleanStatus(int nRoomCleanStatus)
        {
            this.m_nRoomCleanStatus = nRoomCleanStatus;

        }

        public void SetHskprName(string strHskprName)
        {
            this.m_strHskprName = strHskprName;
        }
        public int BoolToInt(bool boolSomeBool) // takes a boolean value and returns a string of either 1 or 0.
        {
            int bin;
            if(boolSomeBool == true)
            {
                bin = 1;
            }
            else
            {
                bin = 1;
            }
            return bin;
        }

    }
}
