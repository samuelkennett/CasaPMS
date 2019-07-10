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

        Dictionary<string, List<Room>> m_dRoomGroup;

        public RoomGroup()
        {
            InitializeRoomGroup();
        }

        public void InitializeRoomGroup()
        {
            Dictionary<string, List<Room>> dRoomGroup = new Dictionary<string, List<Room>>();
            FirstFloor flFirstFloor = new FirstFloor();//Initializes First Floor rooms
            SecondFloor flSecondFloor = new SecondFloor();//Initializes Second Floor rooms
            dRoomGroup.Add(flFirstFloor.GetGroupID(), flFirstFloor.GetRooms());//maps GroupID "1st Floor" to the list of rooms in FirstFloor class
            dRoomGroup.Add(flSecondFloor.GetGroupID(), flSecondFloor.GetRooms());//maps GroupID "2nd Floor" to the list of rooms in FirstFloor class
            SetRoomGroup(dRoomGroup);
        }

        public void SetRoomGroup(Dictionary<string, List<Room>> dRoomGroup)
        {
            this.m_dRoomGroup = dRoomGroup;
        }

        public Dictionary<string, List<Room>> GetRoomGroupDictionary()//Dictionary that is accessible from the main class.
        {
            return this.m_dRoomGroup;
        }
    } 
}
