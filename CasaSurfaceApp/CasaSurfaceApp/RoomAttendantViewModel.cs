using CasaSurface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CasaSurfaceApp
{
    public class RoomAttendantViewModel : BaseViewModel
    {
       public ObservableCollection<RoomAttendantName> m_RoomAttendantNames;

        

       public RoomAttendantViewModel()
        {
            RoomAttendantNames rmNames = new RoomAttendantNames();
            List<RoomAttendantName> raNamesList = rmNames.GetUserNames();
            ObservableCollection<RoomAttendantName> housekeeperNames = new ObservableCollection<RoomAttendantName>(raNamesList);
            SetRoomAttendantObservableCollection(housekeeperNames);
        }

        public void SetRoomAttendantObservableCollection(ObservableCollection<RoomAttendantName> raNames)
        {
            this.m_RoomAttendantNames = raNames;
        }

        public ObservableCollection<RoomAttendantName> GetRoomAttendantObservableCollection()
        {
            return this.m_RoomAttendantNames;
        }
        
    }
}
