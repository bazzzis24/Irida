using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Ирида
{
    /// <summary>
    /// Логика взаимодействия для RoomsPage.xaml
    /// </summary>
    public partial class RoomsPage : Page
    {
        public RoomsPage()
        {
            InitializeComponent();
        }

        private void Reset_button_Click(object sender, RoutedEventArgs e)
        {
            Page_Loaded(sender, e);
        }

        private void Free_rooms_button_Click(object sender, RoutedEventArgs e)
        {
            var filteredlist = DB.RoomsList.Where(x=> DB.AccommodationsList.Where(c=> x.IdRooms==c.Rooms.IdRooms).FirstOrDefault(v=>v.DateInA.IsInRange((DateTime)DateIn.SelectedDate, (DateTime)DateOut.SelectedDate, v.DateOutA)) == null).Where(x => x.Reservations.Contains(x.Reservations.FirstOrDefault(c => c.DateInR.IsInRange((DateTime)DateIn.SelectedDate, (DateTime)DateOut.SelectedDate, c.DateOutR))).Invert());
            RoomsGrid.ItemsSource = filteredlist;
            CountFreeRooms.Text = DB.RoomsList.Where(x => DB.AccommodationsList.Where(c => x.IdRooms == c.Rooms.IdRooms).FirstOrDefault(v => v.DateInA.IsInRange((DateTime)DateIn.SelectedDate, (DateTime)DateOut.SelectedDate, v.DateOutA)) == null).Where(x => x.Reservations.Contains(x.Reservations.FirstOrDefault(c => c.DateInR.IsInRange((DateTime)DateIn.SelectedDate, (DateTime)DateOut.SelectedDate, c.DateOutR))).Invert()).Count().ToString();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RoomsGrid.ItemsSource = DB.GetRooms();
            DateIn.SelectedDate = DateTime.Today;
            DateOut.SelectedDate = DateTime.Today.AddDays(1);
            CountFreeRooms.Text = "";
            Number_search.Text = "";
        }

        private void Number_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;
            if(tbx.Text != "")
            {
                var filteredlist = DB.RoomsList.Where(x => x.Number.Contains(tbx.Text));
                RoomsGrid.ItemsSource = filteredlist;
            }
            else
            {
                RoomsGrid.ItemsSource= DB.GetRooms();
            }
        }
    }
}
