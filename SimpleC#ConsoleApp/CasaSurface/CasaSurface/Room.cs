using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaSurface
{
    class Room
    {
        public string m_strRoomNumber;  
        public string m_strHouseKeepingStatus;
        int m_nCleaningInProgress;
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

            this.m_dtTimeIn = CurrentDate;//Stores the DateTime in this Room object instance
            this.m_nCleaningInProgress = 1;
            this.m_nRoomCleanStatus = 0;
            this.m_strHouseKeepingStatus = "'NotClean'";
            string strCurrentDate = CurrentDate.ToString("yyyy-MM-dd HH:mm:ss.fff");//formats DateTime object CurrentDate for SQL Query
            string strCurrentDateAsString = CurrentDate.ToString("HH:mm: tt");

            Console.WriteLine(strCurrentDate);
            Console.WriteLine("Cleaning is In progress");
            
            //Database calls
            SQLCommands.UpdateBeginCleaning(strRoomNumber, m_nCleaningInProgress, m_nRoomCleanStatus); //updates the database field of CleaningInProgress to 1 and RoomCLeanStatus to 0 in DB: CasaDatabase --> Table: Rooms
            SQLCommands.UpdateHouseKeepingStatus(strRoomNumber, this.m_strHouseKeepingStatus);//updates HousekeepingStatus field to 'NotClean' in DB: Casadatabase --> Table: Rooms
            SQLCommands.UpdateTimeIn(strRoomNumber, strCurrentDate);////updates TimeIn field to current time including Day/Month/Year in DB: CasaDatabase --> Table: RoomCleaning
            SQLCommands.UpdateHouseKeeper(m_strRoomNumber, m_strHskprName);//updates the HskprName field to what is selected in the beginning of the program in DB: CasaDatabase --> Table: Rooms
            SQLCommands.UpdateTimeInAsStr(strRoomNumber, strCurrentDateAsString);//updates TimInAsStr field to current time NOT including Day/Month/Year in DB: CasaDatabase --> Table: RoomCleaning
        }


        public void finishCleaning(string strRoomNumber)
        {
            DateTime CurrentDate;
            CurrentDate = DateTime.Now;

            this.m_dtTimeIn = SQLCommands.SQLGetTimeIn(this.m_strRoomNumber);//Probably Not the best way of doing this! It's pulling the TimeIn value from the database, not from the room object itself. This is due to the way the while loop in the main class works.
            this.m_dtTimeOut = CurrentDate; //Stores the DateTime in this Room object instance
            this.m_nCleaningInProgress = 0;
            this.m_nRoomCleanStatus = 1;
            this.m_strHouseKeepingStatus = "'Clean'";
            string strCurrentDate = CurrentDate.ToString("yyyy-MM-dd HH:mm:ss.fff");//formats DateTime object CurrentDate for SQL Query
            string strCurrentDateAsString = CurrentDate.ToString("HH:mm: tt");

            Console.WriteLine(strCurrentDate);
            Console.WriteLine("Room has finished being cleaned.");

            //Database Call
            SQLCommands.UpdateFinishCleaning(strRoomNumber, m_nCleaningInProgress, m_nRoomCleanStatus);
            SQLCommands.UpdateHouseKeepingStatus(strRoomNumber, this.m_strHouseKeepingStatus);//updates HousekeepingStatus field to 'Clean' in DB: Casadatabase --> Table: Rooms
            SQLCommands.UpdateTimeOut(strRoomNumber, strCurrentDate);//updates TimeOut field to current time including Day/Month/Year in DB: CasaDatabase --> Table: RoomCleaning
            SQLCommands.UpdateTimeOutAsStr(strRoomNumber, strCurrentDateAsString);//updates TimeOutAsStr field to current time NOT including Day/Month/Year in DB: CasaDatabase --> Table: RoomCleaning
            SQLCommands.UpdateElapsedTime(strRoomNumber, this.m_dtTimeIn, this.m_dtTimeOut);//Updates the ElapsedTime field in DB: Casadatabase --> Table: RoomCleaning

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
    }
}
