using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_rooms(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RoomsPage());
        }

        private void Button_Click_reservation(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReservationPage(new Clients(),new Reservations()));
        }

        private void Button_Click_Clients(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ClientsPage(new Clients()));
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Close_page_button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(null);
        }

        private void Accommodation_Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AccommodationPage(new Clients(), new Accommodations()));
        }
    }
}
