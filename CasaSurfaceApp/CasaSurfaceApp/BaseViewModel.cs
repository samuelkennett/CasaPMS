using System.ComponentModel;

namespace CasaSurfaceApp
{
    //Base view model that fires Property Changed events   
    public class BaseViewModel : INotifyPropertyChanged
    {
        //The event that is fired when any child property changes its value
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};
    }
}
