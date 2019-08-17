
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

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
            tbHouseKeepers.Text = cbHousekeepers.SelectedItem.ToString();
        }

        public void GenerateControls()
        {          
            cbHousekeepers.ItemsSource = GenerateHouseKeepers();
            cbHousekeepers.SelectedIndex = 0;//Sets the selected item in the combo box to default to the first item
        }

        //need to make a NofityPropertyChanged when a new combobox item is selected that then updates the text block
        

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

        private void CbHousekeepers_DropDownClosed(object sender, System.EventArgs e)
        {
            tbHouseKeepers.Text = cbHousekeepers.SelectedItem.ToString();
        }
    }
}
