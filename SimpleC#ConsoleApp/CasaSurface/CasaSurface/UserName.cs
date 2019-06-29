namespace CasaSurface
{
    public class UserName
    {
        string m_strFirstName;
        string m_strLastName;
        string m_nEmployeeID;

        public UserName()
        {
            string strFirstName = this.m_strFirstName;
            string strLastName = this.m_strLastName;
            string nEmployeeID = this.m_nEmployeeID;
        }

        public UserName(string strFirstName, string strLastName, string nEmployeeID)//Explicit Constructor for testing.
        {
            m_strFirstName = strFirstName;
            m_strLastName = strLastName;
            m_nEmployeeID = nEmployeeID;
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
    }
}
