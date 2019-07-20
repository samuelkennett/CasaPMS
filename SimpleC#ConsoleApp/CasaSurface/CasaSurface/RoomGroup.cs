using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CasaSurface
{
    public class RoomGroup
    {
        string                              m_strGroupID;
        List<string>                        m_lRmNmbrs;

        public RoomGroup(string strGroupID)
        {
            InitializeRoomGroup(strGroupID);
        }

        public void InitializeRoomGroup(string strGoupID)
        {
            string strConnectionString = "Data Source=Sam-PC; Initial Catalog = MgmtSysConfig; Integrated Security=SSPI; MultipleActiveResultSets=true";
            string strQuery = "SELECT RmNmbr FROM RmGrpsDtl WHERE GroupID=" + "'" + strGoupID + "'";//creates sql query based on which GroupID is passed.
            List<string> strRmGrps;
            strRmGrps = SQLCommands.GetRmGrpDtl(strQuery, strConnectionString, strGoupID);//Gets a list of room numbers (string).          
            SetRmNmbr(strRmGrps);//sets the list of room numbers to the class variable.
            SetGroupID(strGoupID);//sets the GroupID to the class variable.
        }

        public void SetRmNmbr(List<string> strRmNmbr)
        {
            this.m_lRmNmbrs = strRmNmbr;
        }
        public List<string> GetRmNmbrs()
        {
            return this.m_lRmNmbrs;
        }

        public void SetGroupID(string strGroupID)
        {
            this.m_strGroupID = strGroupID;
        }

        public string GetGroupID()
        {
            return this.m_strGroupID;
        }
    } 
}
