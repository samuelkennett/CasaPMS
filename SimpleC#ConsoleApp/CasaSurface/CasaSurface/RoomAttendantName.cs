namespace CasaSurface
{
    public class RoomAttendantName
    {
        string      m_strFirstName;
        string      m_strLastName;
        string      m_nEmployeeID;
        string      m_strUserName;

        public RoomAttendantName()
        {
            string strFirstName = this.m_strFirstName;
            string strLastName = this.m_strLastName;
            string nEmployeeID = this.m_nEmployeeID;
            string strUserName =  this.m_strUserName;
        }

        public RoomAttendantName(string strFirstName, string strLastName, string nEmployeeID, string strUserName)//Explicit Constructor for testing.
        {
            m_strFirstName = strFirstName;
            m_strLastName = strLastName;
            m_nEmployeeID = nEmployeeID;
            m_strUserName = strUserName;
        }
        public string GetFirstName()
        {
            return this.m_strFirstName;
        }

        public void SetFirstName(string strFirstName)
        {
            this.m_strFirstName = strFirstName;
        }

        public string GetLastName()
        {
            return this.m_strLastName;
        }

        public void SetLastName(string strLastName)
        {
            this.m_strLastName = strLastName;
        }

        public string GetEmployeeID()
        {
            return this.m_nEmployeeID;
        }

        public void SetEmployeeID(string nEmployeeID)
        {
            this.m_nEmployeeID = nEmployeeID;
        }

        public string GetUserName()
        {
            return this.m_strUserName;
        }

        public void SetUserName(string strUserName)
        {
            this.m_strUserName = strUserName;
        }
    }
}
