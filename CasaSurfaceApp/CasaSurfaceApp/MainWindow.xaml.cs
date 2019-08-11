using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CasaSurfaceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            GenerateControls();
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)//What does this do????
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GenerateControls()
        {          
            cbHousekeepers.ItemsSource = GenerateHouseKeepers();//needs to be taken from the backend not generated here
        }

        public ObservableCollection<string> GenerateHouseKeepers()
        {
            RoomAttendantViewModel raViewModel = new RoomAttendantViewModel();
            ObservableCollection<string> ocNames = new ObservableCollection<string>();
            foreach(var item in raViewModel.GetRoomAttendantObservableCollection())
            {
                ocNames.Add(item.GetFirstName());
            }

            return ocNames;
            
            
        }

        private ObservableCollection<string> HouseKeepersList()
        {
            ObservableCollection<string> houseKeepers = new ObservableCollection<string>();
            houseKeepers.Add("Casa1");
            houseKeepers.Add("Casa2");
            houseKeepers.Add("Casa3");
            return houseKeepers;
        }


        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
