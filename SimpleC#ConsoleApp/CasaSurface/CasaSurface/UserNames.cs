using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CasaSurface
{
    public class UserNames
    {
        List<UserName> m_unUserNames;
        
         public UserNames()
        {
            List<UserName> userNames = this.m_unUserNames;
            CreateUserNames();
        }
        
        private void SetUserNames(List<UserName> unUserNames)
        {
            this.m_unUserNames = unUserNames;
        }
        private void CreateUserNames()
        {
            List<UserName> lUserName = SQLCommands.SQLCreateUsers();//Creates a list from what is returned from the SQLCreateUsers method.
            SetUserNames(lUserName);//Sets it to the class member.
        }

         public List<UserName> GetUserNames()
        {
            return this.m_unUserNames;
        }
    }
}
