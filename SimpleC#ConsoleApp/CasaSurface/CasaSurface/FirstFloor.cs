using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CasaSurface
{
    public class FirstFloor
    {
        string m_strGroupID; //RoomGroup Dictionary Key 
        List<Room> m_lFirstFloorRooms;//RoomGroup Dictionary Value

        public FirstFloor()
        {
            m_strGroupID = "1st Floor";
            InitializeRooms();
        }

        private void SetRooms(List<Room> lFirstFloorRooms)
        {
            this.m_lFirstFloorRooms = lFirstFloorRooms;
        }

        public List<Room> GetRooms()
        {
            return this.m_lFirstFloorRooms;
        }

        private void InitializeRooms()
        {
            List<Room> lRooms = new List<Room>();//creates room objects with only the room number explicitly set
            Room room101 = new Room("101");
            Room room102 = new Room("102");
            Room room103 = new Room("103");
            Room room104 = new Room("104");
            Room room105 = new Room("105");
            Room room106 = new Room("106");
            Room room110 = new Room("110");
            Room room111 = new Room("111");
            Room room112 = new Room("112");
            lRooms.Add(room101);
            lRooms.Add(room102);
            lRooms.Add(room103);
            lRooms.Add(room104);
            lRooms.Add(room105);
            lRooms.Add(room106);
            lRooms.Add(room110);
            lRooms.Add(room111);
            lRooms.Add(room112);
            SetRooms(lRooms);
        }

        public string GetGroupID()
        {
            return this.m_strGroupID;
        }

    }
}
