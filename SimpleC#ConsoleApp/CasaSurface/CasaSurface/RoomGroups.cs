using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CasaSurface
{
    public class RoomGroups
    {
        Dictionary<string, List<string>> m_dicRoomGroupsDictionary;

        public RoomGroups()
        {
            Dictionary<string, List<string>> dicRoomGroupsDictionary = new Dictionary<string, List<string>>();//Creates new Dictionary that contains all GroupID's as a key and a List<string> of Room Numbers to be the object returned.
            this.m_dicRoomGroupsDictionary = dicRoomGroupsDictionary;
        }

        public void AddToRoomGroupDictionary(string strGroupID, List<string> strRmNmbr)
        {
            // need to get this to work.
            this.m_dicRoomGroupsDictionary.Add(strGroupID, strRmNmbr);
            SetRoomGroupsDictionary(m_dicRoomGroupsDictionary);
        }

        public void SetRoomGroupsDictionary(Dictionary<string, List<string>> dicRoomGroupsDictionary)
        {
            this.m_dicRoomGroupsDictionary = dicRoomGroupsDictionary;
        }

        public Dictionary<string, List<string>> GetRoomGroupsDictionary()
        {
            return this.m_dicRoomGroupsDictionary;
        }
    }
}
