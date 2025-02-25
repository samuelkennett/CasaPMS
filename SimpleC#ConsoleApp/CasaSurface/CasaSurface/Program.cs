﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace CasaSurface
{

    class Program
    {
       
        static void Main(string[] args)
        {

            Console.WriteLine("Begin");
            Console.WriteLine("-------------");
            Console.WriteLine("Please select which housekeeper you are:");

            //Create RoomAttendentNames
            RoomAttendantNames unsUserNames = new RoomAttendantNames();

            //Create RoomGroups
            RoomGroups roomGroups = new RoomGroups();
            RoomGroup FirstFloor = new RoomGroup("1st Floor");
            RoomGroup SecondFloor = new RoomGroup("2nd Floor");
            RoomGroup ThirdFloor = new RoomGroup("3rd Floor");
            RoomGroup FourthFloor = new RoomGroup("4th Floor");
            RoomGroup FifthFloor = new RoomGroup("5th Floor");
            roomGroups.AddToRoomGroupDictionary(FirstFloor.GetGroupID(), FirstFloor.GetRmNmbrs());
            roomGroups.AddToRoomGroupDictionary(SecondFloor.GetGroupID(), SecondFloor.GetRmNmbrs());
            roomGroups.AddToRoomGroupDictionary(ThirdFloor.GetGroupID(), ThirdFloor.GetRmNmbrs());
            roomGroups.AddToRoomGroupDictionary(FourthFloor.GetGroupID(), FourthFloor.GetRmNmbrs());
            roomGroups.AddToRoomGroupDictionary(FifthFloor.GetGroupID(), FifthFloor.GetRmNmbrs());

            //prints out the key and the room number from the list of rooms in the FirstFloor and SecondFloor classes.
            foreach (KeyValuePair<string, List<string>> item in roomGroups.GetRoomGroupsDictionary())
            {
                foreach(var strRmNumbr in item.Value)
                {
                    Console.WriteLine("Key: {0}, Value: {1}", item.Key, strRmNumbr.ToString());
                }            
            }



            //prints out the First and Last names of RoomAttendents.
            foreach(var item in unsUserNames.GetUserNames())
            {
                Console.WriteLine(item.GetFirstName());
                Console.WriteLine(item.GetLastName());
            }
            
            
            string strCurrentHskpr = null ;
            string strHskprSelect = Console.ReadLine();
            int nHskprSelect = Convert.ToInt32(strHskprSelect);//convert string to int for switch statement

            switch (nHskprSelect)
            {
                case 1:
                    strCurrentHskpr = "'Sam'";
                    break;

                case 2:
                    strCurrentHskpr = "'Ed'";
                    break;

                case 3:
                    strCurrentHskpr = "'Wade'";
                    break;

            }

            Console.WriteLine("press 0 to begin");           
            string begin = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();
 
            while (begin == "0") {  //begins simple program loop 0 is start/continue and 9 is exit

                

                Console.WriteLine("Please Select a Room");
                Console.WriteLine("--------------------");
                Console.WriteLine("1. Room 101");
                Console.WriteLine("2. Room 102");
                Console.WriteLine("3. Room 103");

                //Create Room Object
                Room room = new Room();

                string input = Console.ReadLine();
                int response = Convert.ToInt32(input);  //convert string to int for switch statement
                switch (response)
                {
                    case 1:

                        Console.WriteLine("Room 101 Selected");
                        room.SetRoomNumber("101");
                        room.SetHskprName(strCurrentHskpr);
                        break;

                    case 2:

                        Console.WriteLine("Room 102 Selected");
                        room.SetRoomNumber("102");
                        room.SetHskprName(strCurrentHskpr);
                        break;

                    case 3:

                        Console.WriteLine("Room 103 Selected");
                        room.SetRoomNumber("103");
                        room.SetHskprName(strCurrentHskpr);
                        break;

                }


                Console.WriteLine("Press 1 to begin cleaning.");
                Console.WriteLine("Press 2 to finish cleaning.");
                Console.WriteLine("Press 3 to revert room back to Not Clean and No Cleaning In Progress.");
                Console.WriteLine();
                string input2 = Console.ReadLine();
                int response2 = Convert.ToInt32(input2);

                switch (response2)
                {
                    case 1:

                        Console.WriteLine("Begin room clean.");
                        if(SQLCommands.SQLGetCleaningInProgress(room.GetRoomNumber()) == 0 && SQLCommands.SQLGetInspectionFlag(room.GetRoomNumber()) == 0)
                        {
                            room.beginCleaning(room.m_strRoomNumber);
                            Console.WriteLine("Current status: " + room.GetHouseKeepingStatus());
                            Console.WriteLine("------------");
                        }
                        else if(SQLCommands.SQLGetCleaningInProgress(room.GetRoomNumber()) == 1 && SQLCommands.SQLGetInspectionFlag(room.GetRoomNumber()) == 0)
                        {
                            room.SetStatus("'NotClean'");
                            Console.WriteLine("Room has already begun to be cleaned.");
                            Console.WriteLine("Current status: " + room.GetHouseKeepingStatus());
                            Console.WriteLine("------------");                          
                        }
                        else if(SQLCommands.SQLGetCleaningInProgress(room.GetRoomNumber()) == 0 && SQLCommands.SQLGetInspectionFlag(room.GetRoomNumber()) == 1)
                        {
                            Console.WriteLine("Room has already been cleaned");
                            Console.WriteLine("Current status: " + room.GetHouseKeepingStatus());
                            Console.WriteLine("------------");
                        }
                        else if (SQLCommands.SQLImposibilityCheck(room.GetRoomNumber()) == 1)
                        {
                            Console.WriteLine("ERROR! CleaningInProgressFlag=1 and InspectionFLag=1");
                            Console.WriteLine("Reverting Status.");
                            SQLCommands.SQLRevert(room.GetRoomNumber());
                        }
                        else
                        {
                            Console.WriteLine("Unknown ERROR in Switch --> Begin Room Clean");
                            break;
                        }
                        break;

                    case 2:

                        Console.WriteLine("Finish cleaning room.");
                        Console.WriteLine("---------------------");
                        if(SQLCommands.SQLGetCleaningInProgress(room.GetRoomNumber()) == 1 && SQLCommands.SQLGetInspectionFlag(room.GetRoomNumber()) == 0)
                        {                           
                            room.finishCleaning(room.m_strRoomNumber);
                            Console.WriteLine("Room has finshed being cleaned.");
                            Console.WriteLine("Current status: " + room.GetHouseKeepingStatus());
                            Console.WriteLine("------------");
                        }
                        if (SQLCommands.SQLGetCleaningInProgress(room.GetRoomNumber()) == 0 && SQLCommands.SQLGetInspectionFlag(room.GetRoomNumber()) == 0)
                        {
                            Console.WriteLine("Room has not begun to be cleaned yet. Please begin to clean room.");
                            Console.WriteLine("Current status: " + room.GetHouseKeepingStatus());
                            Console.WriteLine("------------");
                        }
                        else if (SQLCommands.SQLGetCleaningInProgress(room.GetRoomNumber()) == 0 && SQLCommands.SQLGetInspectionFlag(room.GetRoomNumber()) == 1)
                        {
                            Console.WriteLine("Room has already been cleaned");
                        }
                        else if (SQLCommands.SQLImposibilityCheck(room.GetRoomNumber()) == 1)
                        {
                            Console.WriteLine("ERROR! CleaningInProgressFlag=1 and InspectionFLag=1");
                            Console.WriteLine("Reverting Status.");
                            SQLCommands.SQLRevert(room.GetRoomNumber());
                        }
                        else
                        {                          
                            Console.WriteLine("Unknown ERROR in Switch --> Finish Cleaning Room");
                            break;
                        }
                        break;

                    case 3:

                        Console.WriteLine("Reverting Room.");
                        SQLCommands.SQLRevert(room.GetRoomNumber());
                        break;
                }

                Console.WriteLine("Continue? 0 for yes 9 for no.");

                begin = Console.ReadLine();
            }

        }


    }
}
