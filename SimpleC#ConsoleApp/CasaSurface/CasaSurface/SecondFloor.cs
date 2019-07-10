using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaSurface
{
    public class SecondFloor
    {
        string m_strGroupID; //RoomGroup Dictionary Key 
        List<Room> m_lFirstFloorRooms;//RoomGroup Dictionary Value

        public SecondFloor()
        {
            m_strGroupID = "2nd Floor";
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

        private void InitializeRooms()//creates room objects with only the room number explicitly set
        {
            List<Room> lRooms = new List<Room>();
            Room room201 = new Room("201");
            Room room202 = new Room("202");
            Room room203 = new Room("203");
            Room room204 = new Room("204");
            Room room205 = new Room("205");
            Room room206 = new Room("206");
            Room room207 = new Room("207");
            Room room208 = new Room("208");
            Room room209 = new Room("209");
            Room room210 = new Room("210");
            Room room211 = new Room("211");
            Room room212 = new Room("212");
            lRooms.Add(room201);
            lRooms.Add(room202);
            lRooms.Add(room203);
            lRooms.Add(room204);
            lRooms.Add(room205);
            lRooms.Add(room206);
            lRooms.Add(room207);
            lRooms.Add(room208);
            lRooms.Add(room209);
            lRooms.Add(room210);
            lRooms.Add(room211);
            lRooms.Add(room212);
            SetRooms(lRooms);
        }

        public string GetGroupID()
        {
            return this.m_strGroupID;
        }
    }
}
