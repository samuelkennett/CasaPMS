using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CasaSurface
{
    public class RoomAttendantNames
    {
        List<RoomAttendantName>     m_unUserNames;
        
         public RoomAttendantNames()
        {
            List<RoomAttendantName> userNames = this.m_unUserNames;
            CreateUserNames();
        }
        
        private void SetUserNames(List<RoomAttendantName> unUserNames)
        {
            this.m_unUserNames = unUserNames;
        }
        private void CreateUserNames()
        {
            List<RoomAttendantName> lUserName = SQLCommands.SQLCreateUsers();//Creates a list from what is returned from the SQLCreateUsers method.
            SetUserNames(lUserName);//Sets it to the class member.
        }

         public List<RoomAttendantName> GetUserNames()
        {
            return this.m_unUserNames;
        }
    }
}
