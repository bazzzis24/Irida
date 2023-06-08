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
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Ирида
{
    /// <summary>
    /// Логика взаимодействия для ReservationPage.xaml
    /// </summary>
    public partial class ReservationPage : Page
    {
        Accommodations acc { get; set; }
        Reservations reservation { get; set; }
        public ReservationPage(Clients clients, Reservations reservations)
        {
            InitializeComponent();
            reservation = reservations;
            ClientBox.ItemsSource = DB.GetClients(); 
            RoomBox.ItemsSource = DB.GetRooms();
            reservation.Clients = clients;
            PriceAccommodations.Text = "0";
            acc = new Accommodations();
            Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Reports"));
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите сохранить?", "Сохранение", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                bool issucces = int.TryParse(PriceReservation.Text, out int result);
                if (ClientBox.SelectedItem != null && RoomBox.SelectedItem != null && DateReservation.SelectedDate != null && DateAccommodation.SelectedDate != null && DateOut.SelectedDate != null && PriceReservation.Text != "")
                {
                    if (issucces)
                    {
                        if (reservation.IdReservatons == 0)
                        {

                            var sad1 = RoomBox.SelectedItem as Rooms;
                            if (sad1.Reservations.Contains(sad1.Reservations.FirstOrDefault(c => c.DateInR.IsInRange((DateTime)DateAccommodation.SelectedDate, (DateTime)DateOut.SelectedDate, c.DateOutR))).Invert())
                            {
                                if (DB.AccommodationsList.Where(c => sad1.IdRooms == c.Rooms.IdRooms).FirstOrDefault(v => v.DateInA.IsInRange((DateTime)DateAccommodation.SelectedDate, (DateTime)DateOut.SelectedDate, v.DateOutA)) == null)
                                {
                                    reservation.Clients = ClientBox.SelectedItem as Clients;
                                    reservation.Rooms = RoomBox.SelectedItem as Rooms;
                                    reservation.DateReservation = (DateTime)DateReservation.SelectedDate;
                                    reservation.DateInR = (DateTime)DateAccommodation.SelectedDate;
                                    reservation.DateOutR = (DateTime)DateOut.SelectedDate;
                                    reservation.PriceR = Convert.ToInt32(PriceReservation.Text);
                                    Supp.ReportR(reservation);
                                    DB.AddReservation(reservation);
                                    reservation = new Reservations();
                                    Page_Loaded(sender, e);
                                    MessageBox.Show("Сохранение успешно", "Сохранение");
                                }
                                else
                                {
                                    MessageBox.Show("Данный номер занят на эту дату", "Сохранение");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Данный номер занят на эту дату!", "Сохранение");
                            }

                        }
                        else
                        {
                            var sad1 = RoomBox.SelectedItem as Rooms;
                            if (sad1.Reservations.Contains(sad1.Reservations.FirstOrDefault(c => c.DateInR.IsInRange((DateTime)DateAccommodation.SelectedDate, (DateTime)DateOut.SelectedDate, c.DateOutR) && c.IdReservatons != reservation.IdReservatons)).Invert())
                            {
                                if (DB.AccommodationsList.Where(c => sad1.IdRooms == c.Rooms.IdRooms).FirstOrDefault(v => v.DateInA.IsInRange((DateTime)DateAccommodation.SelectedDate, (DateTime)DateOut.SelectedDate, v.DateOutA)) == null)
                                {
                                    reservation.Clients = ClientBox.SelectedItem as Clients;
                                    reservation.Rooms = RoomBox.SelectedItem as Rooms;
                                    reservation.DateReservation = (DateTime)DateReservation.SelectedDate;
                                    reservation.DateInR = (DateTime)DateAccommodation.SelectedDate;
                                    reservation.DateOutR = (DateTime)DateOut.SelectedDate;
                                    reservation.PriceR = Convert.ToInt32(PriceReservation.Text);
                                    Supp.ReportR(reservation);
                                    DB.Save();
                                    reservation = new Reservations();
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
                        MessageBox.Show("Стоимость бронирования должна быть в формате числа!", "Сохранение");
                    }
                }
                else
                {
                    MessageBox.Show("Не все данные введены!", "Сохранение");
                }
            }
            else
            {
                MessageBox.Show("Сохранение отменено", "Сохранение");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReservationsGrid.ItemsSource = DB.GetReservations();
            ClientBox.SelectedItem = reservation.Clients;
            RoomBox.SelectedItem = reservation.Rooms;
            if (reservation.IdReservatons == 0)
            {
                DateReservation.SelectedDate = DateTime.Today;
                DateAccommodation.SelectedDate = DateTime.Today;
                DateOut.SelectedDate = DateTime.Today.AddDays(1);
            }
            else
            {
                DateReservation.SelectedDate = reservation.DateReservation;
                DateAccommodation.SelectedDate = reservation.DateInR;
                DateOut.SelectedDate = reservation.DateOutR;
            }
            PriceReservation.Text = Convert.ToString(reservation.PriceR);
        }

        private void ReservationsReset_button_Click(object sender, RoutedEventArgs e)
        {
            ReservationsGrid.ItemsSource = DB.GetReservations();
            ReservationsSearch.Text = "";
            ReservationsRoomSearch.Text = "";
            ReservationsPasportSearch.Text = "";
        }

        private void ReservationsSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = ReservationsSearch;
            var tbxx = ReservationsRoomSearch;
            var tbxxx = ReservationsPasportSearch;
            if (tbx.Text != "" || tbxx.Text != "" || tbxxx.Text != "")
            {
                if (tbx.Text != "" && tbxx.Text != "" && tbxxx.Text != "")
                {
                    var filteredlist = DB.ReservationsList.Where(x => x.Clients.LastName.ToLower().Contains(tbx.Text.ToLower())).Where(x => x.Rooms.Number.ToLower().Contains(tbxx.Text.ToLower())).Where(x => x.Clients.Pasport.ToLower().Contains(tbxxx.Text.ToLower()));
                    ReservationsGrid.ItemsSource = filteredlist;
                }
                else
                {
                    if (tbx.Text!=""&&tbxx.Text!="") 
                    {
                        var filteredlist = DB.ReservationsList.Where(x => x.Clients.LastName.ToLower().Contains(tbx.Text.ToLower())).Where(x => x.Rooms.Number.ToLower().Contains(tbxx.Text.ToLower()));
                        ReservationsGrid.ItemsSource = filteredlist;
                    }
                    else
                    {
                        if (tbx.Text != "" && tbxxx.Text != "")
                        {
                            var filteredlist = DB.ReservationsList.Where(x => x.Clients.LastName.ToLower().Contains(tbx.Text.ToLower())).Where(x => x.Clients.Pasport.ToLower().Contains(tbxxx.Text.ToLower()));
                            ReservationsGrid.ItemsSource = filteredlist;
                        }
                        else
                        {
                            if (tbxx.Text != "" && tbxxx.Text != "")
                            {
                                var filteredlist = DB.ReservationsList.Where(x => x.Rooms.Number.ToLower().Contains(tbxx.Text.ToLower())).Where(x => x.Clients.Pasport.ToLower().Contains(tbxxx.Text.ToLower()));
                                ReservationsGrid.ItemsSource = filteredlist;
                            }
                            else
                            {
                                if (tbx.Text != "")
                                {
                                    var filteredlist = DB.ReservationsList.Where(x => x.Clients.LastName.ToLower().Contains(tbx.Text.ToLower()));
                                    ReservationsGrid.ItemsSource = filteredlist;
                                }
                                else
                                {
                                    if (tbxx.Text != "")
                                    {
                                        var filteredlist = DB.ReservationsList.Where(x => x.Rooms.Number.ToLower().Contains(tbxx.Text.ToLower()));
                                        ReservationsGrid.ItemsSource = filteredlist;
                                    }
                                    else
                                    {
                                        if (tbxxx.Text!="")
                                        {
                                            var filteredlist = DB.ReservationsList.Where(x => x.Clients.Pasport.ToLower().Contains(tbxxx.Text.ToLower()));
                                            ReservationsGrid.ItemsSource = filteredlist;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ReservationsGrid.ItemsSource = DB.GetReservations();
            }
        }

        private void Reset_button_Click(object sender, RoutedEventArgs e)
        {
            reservation = new Reservations();
            PriceAccommodations.Text = "0";
            Page_Loaded(sender, e);
        }

        private void InformationClient_button_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationsGrid.SelectedItem != null)
            {
                var reservationss = ReservationsGrid.SelectedItem as Reservations;
                Clients clients = reservationss.Clients;
                InformationWindow p = new InformationWindow(clients);
                p.Show();
            }
            else
            {
                MessageBox.Show("Выберите бронь","Информация о клиенте");
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите изменить?", "Изменение", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                reservation = ReservationsGrid.SelectedItem as Reservations;
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
                Reservations reservations = ReservationsGrid.SelectedItem as Reservations;
                DB.Save();
                DB.DeleteReservation(reservations);
                Page_Loaded(sender, e);
            }
            else
            {

            }
        }

        private void AccommodationsClient_button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите поселить?", "Посление по брони", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                if (ReservationsGrid.SelectedItem != null)
                {
                    var res = ReservationsGrid.SelectedItem as Reservations;
                    if (res.DateInR == DateTime.Today)
                    {
                        var prrr = Convert.ToInt32((res.DateOutR - res.DateInR).TotalDays);
                        acc.Clients = res.Clients;
                        acc.Rooms = res.Rooms;
                        acc.DateInA = res.DateInR;
                        acc.DateOutA = res.DateOutR;
                        acc.PriceA = res.Rooms.Price * prrr;
                        DB.AddAccommodation(acc);
                        Supp.ReportA(acc);
                        DB.DeleteReservation(res);
                        acc = new Accommodations();
                        Page_Loaded(sender, e);
                        MessageBox.Show("Поселение прошло успешно","Поселение по брони");
                        NavigationService.Navigate(new AccommodationPage(new Clients(), new Accommodations()));
                    }
                    else
                    {
                        MessageBox.Show("Дата поселения этой брони не совпадает с сегодняшней", "Поселение по брони");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите бронь", "Поселение по брони");
                }
            }
        }
        private void RoomBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateAccommodation.SelectedDate != null && RoomBox.SelectedItem != null && DateOut.SelectedDate != null && DateReservation.SelectedDate != null)
            {
                var pr = Convert.ToInt32(((DateTime)DateOut.SelectedDate - (DateTime)DateAccommodation.SelectedDate).TotalDays);
                var prr = RoomBox.SelectedItem as Rooms;
                PriceAccommodations.Text = Convert.ToString(prr.Price * pr);
                PriceReservation.Text = Convert.ToString(prr.Price * pr / 10);
            }
        }

        private void ReservationsToday_button_Click(object sender, RoutedEventArgs e)
        {
            var filteredlist = DB.ReservationsList.Where(x => x.DateInR==DateTime.Today);
            ReservationsGrid.ItemsSource = filteredlist;
        }

        
    }
}
