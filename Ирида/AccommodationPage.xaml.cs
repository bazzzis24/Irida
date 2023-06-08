using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для AccommodationPage.xaml
    /// </summary>
    public partial class AccommodationPage : Page
    {
        Accommodations accommodationn { get; set; }
        Accommodations accommodation { get; set; }
        public AccommodationPage(Clients clients, Accommodations accommodations)
        {
            InitializeComponent();
            ClientBoxA.ItemsSource = DB.GetClients();
            RoomBoxA.ItemsSource = DB.GetRooms();
            accommodation = accommodations;
            accommodation.Clients = clients;
        }

        private void Save_A_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите сохранить?", "Сохранение", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                bool issucces = int.TryParse(PriceA.Text, out int result);
                if (ClientBoxA.SelectedItem != null && RoomBoxA.SelectedItem != null && DateAccommodationA.SelectedDate != null && DateOutA.SelectedDate != null && PriceA.Text != "")
                {
                    if (issucces)
                    {
                        if (accommodation.IdAccommodations == 0)
                        {
                            var sad1 = RoomBoxA.SelectedItem as Rooms;
                            if (sad1.Reservations.Contains(sad1.Reservations.FirstOrDefault(c => c.DateInR.IsInRange((DateTime)DateAccommodationA.SelectedDate, (DateTime)DateOutA.SelectedDate, c.DateOutR))).Invert())
                            {
                                if (DB.AccommodationsList.Where(c => sad1.IdRooms == c.Rooms.IdRooms).FirstOrDefault(v => v.DateInA.IsInRange((DateTime)DateAccommodationA.SelectedDate, (DateTime)DateOutA.SelectedDate, v.DateOutA)) == null)
                                {
                                    if (DB.AccommodationsList.FirstOrDefault(c => sad1.IdRooms == c.Rooms.IdRooms) == null)
                                    {
                                        accommodation.Clients = ClientBoxA.SelectedItem as Clients;
                                        accommodation.Rooms = RoomBoxA.SelectedItem as Rooms;
                                        accommodation.DateInA = (DateTime)DateAccommodationA.SelectedDate;
                                        accommodation.DateOutA = (DateTime)DateOutA.SelectedDate;
                                        accommodation.PriceA = Convert.ToInt32(PriceA.Text);
                                        Supp.ReportA(accommodation);
                                        DB.AddAccommodation(accommodation);
                                        accommodation = new Accommodations();
                                        Page_Loaded(sender, e);
                                        MessageBox.Show("Сохранение успешно", "Сохранение");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Перед поселением в этот номер удалите прошлое поселение", "Поселение");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Данный номер занят на эту дату!", "Поселение");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Данный номер занят на эту дату!", "Поселение");
                            }
                        }
                        else
                        {
                            var sad1 = RoomBoxA.SelectedItem as Rooms;
                            if (sad1.Reservations.Contains(sad1.Reservations.FirstOrDefault(c => c.DateInR.IsInRange((DateTime)DateAccommodationA.SelectedDate, (DateTime)DateOutA.SelectedDate, c.DateOutR))).Invert())
                            {
                                if (DB.AccommodationsList.Where(c => sad1.IdRooms == c.Rooms.IdRooms).FirstOrDefault(v => v.DateInA.IsInRange((DateTime)DateAccommodationA.SelectedDate, (DateTime)DateOutA.SelectedDate, v.DateOutA) && v.IdAccommodations != accommodation.IdAccommodations) == null)
                                {
                                    accommodation.Clients = ClientBoxA.SelectedItem as Clients;
                                    accommodation.Rooms = RoomBoxA.SelectedItem as Rooms;
                                    accommodation.DateInA = (DateTime)DateAccommodationA.SelectedDate;
                                    accommodation.DateOutA = (DateTime)DateOutA.SelectedDate;
                                    accommodation.PriceA = Convert.ToInt32(PriceA.Text);
                                    Supp.ReportA(accommodation);
                                    DB.Save();
                                    accommodation = new Accommodations();
                                    Page_Loaded(sender, e);
                                    MessageBox.Show("Сохранение успешно", "Сохранение");
                                }
                                else
                                {
                                    MessageBox.Show("Данный номер занят на эту дату!", "Сохранение");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Данный номер занят на эту дату!", "Сохранение");
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Стоимость поселения должна быть в формате числа!");
                    }
                }
                else
                {
                    MessageBox.Show("Не все данные введены!");
                }
            }
            else
            {
                MessageBox.Show("Сохранение отменено", "Сохранение");
            }
        }

        private void InformationClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccommodationsGrid.SelectedItem != null)
            {
                accommodationn = AccommodationsGrid.SelectedItem as Accommodations;
                Clients clients = accommodationn.Clients;
                InformationWindow p = new InformationWindow(clients);
                p.Show();
            }
            else
            {
                MessageBox.Show("Выберите поселение", "Информация о клиенте");
            }
        }

        private void Search_Reset_button_Click(object sender, RoutedEventArgs e)
        {
            AccommodationsGrid.ItemsSource = DB.GetAccommodations();
            SearchA.Text = "";
            SearchAPasport.Text = "";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            AccommodationsGrid.ItemsSource = DB.GetAccommodations();
            ClientBoxA.SelectedItem = accommodation.Clients;
            RoomBoxA.SelectedItem = accommodation.Rooms;
            if (accommodation.IdAccommodations == 0)
            {
                DateAccommodationA.SelectedDate = DateTime.Today;
                DateOutA.SelectedDate = DateTime.Today.AddDays(1);
            }
            else
            {
                DateAccommodationA.SelectedDate = accommodation.DateInA;
                DateOutA.SelectedDate = accommodation.DateOutA;
            }
            PriceA.Text = Convert.ToString(accommodation.PriceA);
        }

        private void SearchA_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = SearchA;
            var tbxx = SearchAPasport;
            if (tbx.Text != ""||tbxx.Text!="")
            {
                if (tbx.Text != "" && tbxx.Text != "")
                {
                    var filteredlist = DB.AccommodationsList.Where(x => x.Clients.LastName.ToLower().Contains(tbx.Text.ToLower())).Where(x => x.Clients.Pasport.ToLower().Contains(tbxx.Text.ToLower()));
                    AccommodationsGrid.ItemsSource = filteredlist;
                }
                else
                {
                    if (tbx.Text != "")
                    {
                        var filteredlist = DB.AccommodationsList.Where(x => x.Clients.LastName.ToLower().Contains(tbx.Text.ToLower()));
                        AccommodationsGrid.ItemsSource = filteredlist;
                    }
                    else
                    {
                        if (tbxx.Text != "")
                        {
                            var filteredlist = DB.AccommodationsList.Where(x => x.Clients.Pasport.ToLower().Contains(tbxx.Text.ToLower()));
                            AccommodationsGrid.ItemsSource = filteredlist;
                        }
                    }
                }
            }
            else
            {
                AccommodationsGrid.ItemsSource = DB.GetAccommodations();
            }
        }

        private void RoomBoxA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateAccommodationA.SelectedDate != null && RoomBoxA.SelectedItem != null && DateOutA.SelectedDate != null)
            {
                var pr = Convert.ToInt32(((DateTime)DateOutA.SelectedDate - (DateTime)DateAccommodationA.SelectedDate).TotalDays);
                var prr = RoomBoxA.SelectedItem as Rooms;
                PriceA.Text = Convert.ToString(prr.Price * pr);
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите изменить?", "Изменение", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                accommodation = AccommodationsGrid.SelectedItem as Accommodations;
                Page_Loaded(sender, e);
            }
            else
            {

            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить?", "Удаление", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Accommodations accommodations = AccommodationsGrid.SelectedItem as Accommodations;
                DB.Save();
                DB.DeleteAccommodation(accommodations);
                Page_Loaded(sender, e);
            }
            else
            {

            }
        }

        private void Reset_A_button_Click(object sender, RoutedEventArgs e)
        {
            accommodation = new Accommodations();
            Page_Loaded(sender, e);
        }

        private void DateOutA_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateAccommodationA.SelectedDate != null && RoomBoxA.SelectedItem != null && DateOutA.SelectedDate != null)
            {
                var pr = Convert.ToInt32(((DateTime)DateOutA.SelectedDate - (DateTime)DateAccommodationA.SelectedDate).TotalDays);
                var prr = RoomBoxA.SelectedItem as Rooms;
                PriceA.Text = Convert.ToString(prr.Price * pr);
            }
        }

        private void DateAccommodationA_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateAccommodationA.SelectedDate != null && RoomBoxA.SelectedItem != null && DateOutA.SelectedDate != null)
            {
                var pr = Convert.ToInt32(((DateTime)DateOutA.SelectedDate - (DateTime)DateAccommodationA.SelectedDate).TotalDays);
                var prr = RoomBoxA.SelectedItem as Rooms;
                PriceA.Text = Convert.ToString(prr.Price * pr);
            }
        }

        private void AccommodationsOutToday_button_Click(object sender, RoutedEventArgs e)
        {
            var filteredlist = DB.AccommodationsList.Where(x => x.DateOutA== DateTime.Today);
            AccommodationsGrid.ItemsSource = filteredlist;
        }
    }
}
