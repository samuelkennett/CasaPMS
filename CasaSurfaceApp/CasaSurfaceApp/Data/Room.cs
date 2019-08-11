using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaSurface
{
    public class Room
    {
        public string   m_strRoomNumber;  
        public string   m_strHouseKeepingStatus;
        int             m_nCleaningInProgress;
        int             m_nRoomCleanStatus;
        string          m_strHskprName;
        DateTime        m_dtTimeIn;
        DateTime        m_dtTimeOut;

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

        public Room(string strRoomNumber)
        {
            this.m_strRoomNumber = strRoomNumber;
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
            DateTime strCurrentDate = CurrentDate;//formats DateTime object CurrentDate for SQL Query
            string strCurrentDateAsString = CurrentDate.ToString("HH:mm: tt");

            Console.WriteLine(strCurrentDate);
            Console.WriteLine("Cleaning is In progress");

            //Database Connection String
            string strCasaDatabaseConnectionString = "Data Source=Sam-PC; Initial Catalog = CasaDatabase; Integrated Security=SSPI";
            //SQL Queries
            string strUpdateCleaningInProgressFlagQuery = "UPDATE Rooms SET CleaningInProgressFlag=" + this.m_nCleaningInProgress + " WHERE RoomNumber=" + strRoomNumber;
            string strUpdateHousekeepingStatusQuery = "UPDATE Rooms SET HousekeepingStatus=" + this.m_strHouseKeepingStatus + " WHERE RoomNumber=" + strRoomNumber;
            string strUpdateInspectionFlagQuery = "UPDATE Rooms SET InspectionFlag = 0 WHERE RoomNumber = " + strRoomNumber;
            string strUpdateTimeInQuery = "UPDATE RoomCleaning SET TimeIn= CONVERT(datetime,'" + strCurrentDate + "', 0) WHERE RmNmbr=" + strRoomNumber;
            string strUpdateTimeInAsStringQuery = "UPDATE RoomCleaning SET TimeInAsStr = '" + strCurrentDateAsString + "' WHERE RmNmbr = " + strRoomNumber;
            string strUpdateHouseKeeper = "UPDATE Rooms SET HskprName = " + this.m_strHskprName + " WHERE RoomNumber = " + strRoomNumber;
            //Database calls (refactored)
            SQLCommands.Update(strUpdateCleaningInProgressFlagQuery, strCasaDatabaseConnectionString);//Updates CleaningInProgressFlag in DB: CasaDatabase --> Table: Rooms
            SQLCommands.Update(strUpdateHousekeepingStatusQuery, strCasaDatabaseConnectionString);//Updates HouskeepingStatus in DB: CasaDatabase --> Table: Rooms
            SQLCommands.Update(strUpdateInspectionFlagQuery, strCasaDatabaseConnectionString);//Updates InspectionFlag in DB: CasaDatabase --> Table: Rooms
            SQLCommands.Update(strUpdateTimeInQuery, strCasaDatabaseConnectionString);//Updates TimeIn in DB: CasaDatabase --> Table: RoomCleaning
            SQLCommands.Update(strUpdateTimeInAsStringQuery, strCasaDatabaseConnectionString);//Updates TimeInAsStr in DB: CasaDatabase --> Table: RoomCleaning
            SQLCommands.Update(strUpdateHouseKeeper, strCasaDatabaseConnectionString);//Updates HskprName in DB: CasaDatabase --> Table: Rooms
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

            //Database Connection String
            string strCasaDatabaseConnectionString = "Data Source=Sam-PC; Initial Catalog = CasaDatabase; Integrated Security=SSPI";

            //SQL Queries
            string strUpdateCleaningInProgressFlagQuery = "UPDATE Rooms SET CleaningInProgressFlag=" + this.m_nCleaningInProgress + " WHERE RoomNumber=" + strRoomNumber;
            string strUpdateHousekeepingStatusQuery = "UPDATE Rooms SET HousekeepingStatus=" + this.m_strHouseKeepingStatus + " WHERE RoomNumber=" + strRoomNumber;
            string strUpdateInspectionFlagQuery = "UPDATE Rooms SET InspectionFlag = 1 WHERE RoomNumber = " + strRoomNumber;
            string strUpdateTimeOutQuery = "UPDATE RoomCleaning SET TimeOut= CONVERT(datetime,'" + strCurrentDate + "', 0) WHERE RmNmbr=" + strRoomNumber;
            string strUpdateTimeOutAsStringQuery = "UPDATE RoomCleaning SET TimeOutAsStr = '" + strCurrentDateAsString + "' WHERE RmNmbr = " + strRoomNumber;
            string strUpdateHouseKeeper = "UPDATE Rooms SET HskprName = " + this.m_strHskprName + " WHERE RoomNumber = " + strRoomNumber;

            //Database Calls (refactored)
            SQLCommands.Update(strUpdateCleaningInProgressFlagQuery, strCasaDatabaseConnectionString);//Updates CleaningInProgressFlag in DB: CasaDatabase --> Table: Rooms
            SQLCommands.Update(strUpdateHousekeepingStatusQuery, strCasaDatabaseConnectionString);//Updates HouskeepingStatus in DB: CasaDatabase --> Table: Rooms
            SQLCommands.Update(strUpdateInspectionFlagQuery, strCasaDatabaseConnectionString);//Updates InspectionFlag in DB: CasaDatabase --> Table: Rooms
            SQLCommands.Update(strUpdateTimeOutQuery, strCasaDatabaseConnectionString);//Updates TimeOut in DB: CasaDatabase --> Table: RoomCleaning
            SQLCommands.Update(strUpdateTimeOutAsStringQuery, strCasaDatabaseConnectionString);//Updates TimOutAsStr in DB: CasaDatabase --> Table: RoomCleaning
            //Database Calls (NOT refactored)
            SQLCommands.UpdateElapsedTime(strRoomNumber, this.m_dtTimeIn, this.m_dtTimeOut);//Updates the ElapsedTime field in DB: Casadatabase --> Table: RoomCleaning
            SQLCommands.SetRoomAttendant(strRoomNumber);//Updates RoomAttendant in DB: CasaDatabase --> Table: RoomCleaning

            //Imposibility Check: Checks to see if both CleaningInProgressFlag and InspectionFlag == 1. If so Reverts both values back to 0.
            if (SQLCommands.SQLImposibilityCheck(strRoomNumber) == 1)
            {
                SQLCommands.SQLRevert(strRoomNumber);
            }
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
