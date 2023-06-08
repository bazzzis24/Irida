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
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.IO;


namespace Ирида
{
    /// <summary>
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        Clients client { get; set; }
        Supp.IM im { get; set; }
        public ClientsPage(Clients clients)
        {
            InitializeComponent();
            client = clients;
            im = new Supp.IM();
            List<Clients> clients1 = DB.GetClients();
            Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"));

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClientsGrid.ItemsSource = DB.GetClients();
            Name.Text = client.Name;
            LastName.Text = client.LastName;
            Patronomic.Text = client.Patronomic;
            Pasport.Text = client.Pasport;
            Phone.Text = client.PhoneNumber;
            if (client.IdClients == 0)
            {
                BirthDate.SelectedDate = DateTime.Today;
            }
            else
            {
                BirthDate.SelectedDate = client.BirthDate;
            }
            PasportCopy.Text = client.PasportCopy;
        }

        private void Reset_search_button_Click(object sender, RoutedEventArgs e)
        {
            ClientsGrid.ItemsSource = DB.GetClients();
            Pasport_search.Text = "";
            LastName_search.Text = "";
        }

        private void Pasport_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = LastName_search;
            var tbxx = Pasport_search;
            if (tbx.Text != "" || tbxx.Text != "")
            {
                if (tbx.Text != "" && tbxx.Text != "")
                {
                    var filteredlist = DB.ClientsList.Where(x => x.LastName.ToLower().Contains(tbx.Text.ToLower())).Where(x => x.Pasport.ToLower().Contains(tbxx.Text.ToLower()));
                    ClientsGrid.ItemsSource = filteredlist;
                }
                else
                {
                    if (tbx.Text != "")
                    {
                        var filteredlist = DB.ClientsList.Where(x => x.LastName.ToLower().Contains(tbx.Text.ToLower()));
                        ClientsGrid.ItemsSource = filteredlist;
                    }
                    else
                    {
                        if (tbxx.Text != "")
                        {
                            var filteredlist = DB.ClientsList.Where(x => x.Pasport.ToLower().Contains(tbxx.Text.ToLower()));
                            ClientsGrid.ItemsSource = filteredlist;
                        }
                    }
                }
            }
            else
            {
                ClientsGrid.ItemsSource = DB.GetClients();
            }
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text != "" && LastName.Text != "" && Patronomic.Text != "" && Pasport.Text != "" && Phone.Text != "" && BirthDate.SelectedDate != null)
            {
                Regex regex = new Regex(@".png$", RegexOptions.IgnoreCase);
                Regex regex1 = new Regex(@".jpg$", RegexOptions.IgnoreCase);
                MatchCollection matches = regex1.Matches(PasportCopy.Text);
                MatchCollection matches2 = regex.Matches(PasportCopy.Text);
                bool issucces = File.Exists(PasportCopy.Text);
                bool issucces2 = false;
                if (matches.Count > 0 || matches2.Count > 0||PasportCopy.Text=="")
                {
                    if (issucces || PasportCopy.Text == "")
                    {
                        if (PasportCopy.Text!="")
                        {


                            try
                            {
                                BitmapImage tt = new BitmapImage();
                                tt.BeginInit();
                                tt.UriSource = new Uri(PasportCopy.Text);
                                tt.EndInit();
                                issucces2 = true;
                            }
                            catch
                            {
                                MessageBox.Show("Некорректный файл");
                                issucces2 = false;
                            } 
                        }
                        if (issucces2 || PasportCopy.Text == "")
                        {
                            client.Name = Name.Text.ToLower();
                            client.LastName = LastName.Text.ToLower();
                            client.Patronomic = Patronomic.Text.ToLower();
                            client.Pasport = Pasport.Text.ToLower();
                            client.PhoneNumber = Phone.Text.ToLower();
                            client.BirthDate = (DateTime)BirthDate.SelectedDate;
                            client.PasportCopy = PasportCopy.Text;
                            if (client.IdClients == 0)
                            {
                                if (DB.ClientsList.FirstOrDefault(x => x.Name == client.Name && x.LastName == client.LastName && x.Patronomic == client.Patronomic && x.Pasport == client.Pasport && x.BirthDate == client.BirthDate) != null)
                                {
                                    MessageBox.Show("Данный клиент уже зарегестрирован");
                                }
                                else
                                {
                                    if (PasportCopy.Text != "")
                                    {
                                        bool issucces3;
                                        try
                                        {
                                            File.Copy(PasportCopy.Text, System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"), client.LastName + client.Pasport + ".png"), true);
                                            client.PasportCopy = System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"), client.LastName + client.Pasport + ".png");
                                            issucces3 = true;
                                        }
                                        catch
                                        {
                                            issucces3 = false;
                                        }
                                        if (issucces3 == false)
                                        {
                                            File.Copy(PasportCopy.Text, System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"), client.LastName + client.Pasport + "(1)" + ".png"), true);
                                            client.PasportCopy = System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"), client.LastName + client.Pasport + "(1)" + ".png");
                                        }
                                        else
                                        {
                                            File.Copy(PasportCopy.Text, System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"), client.LastName + client.Pasport + ".png"), true);
                                            client.PasportCopy = System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"), client.LastName + client.Pasport + ".png");
                                        }
                                    }
                                    DB.AddClient(client);
                                    MessageBox.Show("Сохранение успешно");
                                }
                            }
                            else
                            {
                                if (PasportCopy.Text != "")
                                {
                                    bool issucces3;
                                    try
                                    {
                                        File.Copy(PasportCopy.Text, System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"), client.LastName + client.Pasport + ".png"), true);
                                        client.PasportCopy = System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"), client.LastName + client.Pasport + ".png");
                                        issucces3 = true;
                                    }
                                    catch
                                    {
                                        issucces3 = false;
                                    }
                                    Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"));
                                    if (issucces3 == false)
                                    {
                                        
                                        File.Copy(PasportCopy.Text, System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"), client.LastName + client.Pasport + "(1)" + ".png"), true);
                                        client.PasportCopy = System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"), client.LastName + client.Pasport + "(1)" + ".png");
                                    }
                                    else
                                    {
                                        File.Copy(PasportCopy.Text, System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"), client.LastName + client.Pasport + ".png"), true);
                                        client.PasportCopy = System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IMG"), client.LastName + client.Pasport + ".png");
                                    }
                                }
                                DB.Save();
                                MessageBox.Show("Сохранение успешно");
                            }
                            ClientsGrid.ItemsSource = DB.GetClients();
                            client = new Clients();
                            Page_Loaded(sender, e);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нет доступа к данному файлу или файл не существует");
                    }
                }
                else
                {
                    MessageBox.Show("Данный файл не в формате png или jpg");
                }
                
            }
            else
            {
                MessageBox.Show("Не все данные введены!");
            }
        }

        public void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите изменить?", "Изменение", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                client = ClientsGrid.SelectedItem as Clients;
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
                var ss = ClientsGrid.SelectedItem as Clients;
                if (ss.Accommodations.Any() || ss.Reservations.Any())
                {
                    MessageBox.Show("Данного клиента нельзя удалить, у него есть бронирование или заселение.", "Удаление");
                }
                else
                {
                    DB.DeleteClient(ss);
                    Page_Loaded(sender, e);
                }
            }
            else
            {

            }
        }

        private void Reset_button_Click(object sender, RoutedEventArgs e)
        {
            client = new Clients();
            im = new Supp.IM();
            Page_Loaded(sender, e);
        }

        private void ReservationClient_button_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsGrid.SelectedItem != null)
            {
                Clients clients = ClientsGrid.SelectedItem as Clients;
                NavigationService.Navigate(new ReservationPage(clients,new Reservations()));
            }
            else
            {
                MessageBox.Show("Выберите клиента", "Бронирование по клиенту");
            }
        }

        private void AccommodationsClient_button_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsGrid.SelectedItem != null)
            {
                Clients clients = ClientsGrid.SelectedItem as Clients;
                NavigationService.Navigate(new AccommodationPage(clients, new Accommodations()));
            }
            else
            {
                MessageBox.Show("Выберите клиента", "Поселение по клиенту");
            }
        }

        private void PasportCopyOpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasportCopy.Text != "")
            {
                Regex regex = new Regex(@".png$", RegexOptions.IgnoreCase);
                Regex regex1 = new Regex(@".jpg$", RegexOptions.IgnoreCase);
                MatchCollection matches = regex1.Matches(PasportCopy.Text);
                MatchCollection matches2 = regex.Matches(PasportCopy.Text);
                bool issucces = File.Exists(PasportCopy.Text);
                bool issucces2;
                if (matches.Count > 0 || matches2.Count > 0)
                {
                    if (issucces)
                    {
                        try
                        {
                            BitmapImage tt = new BitmapImage();
                            tt.BeginInit();
                            tt.UriSource = new Uri(PasportCopy.Text);
                            tt.EndInit();
                            issucces2 = true;
                        }
                        catch
                        {
                            MessageBox.Show("Некорректный файл");
                            issucces2 = false;
                        }
                        if (issucces2)
                        {
                            im.Img = PasportCopy.Text;
                            Photo photo = new Photo(im);
                            photo.Show();
                            im = new Supp.IM();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нет доступа к данному файлу или файл не существует");
                    }
                }
                else
                {
                    MessageBox.Show("Данный файл не в формате png или jpg");
                }
            }
            else
            {
                MessageBox.Show("Файл не выбран");
            }
        }

        private void PasportCopyEditButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            if (openFileDialog.ShowDialog() == true)
            {
                PasportCopy.Text = openFileDialog.FileName;
            }
        }

        private void PasportCopyClearButton_Click(object sender, RoutedEventArgs e)
        {
            PasportCopy.Text = "";
        }

        private void PasportCopyOpenButton2_Click(object sender, RoutedEventArgs e)
        {
            var ss = ClientsGrid.SelectedItem as Clients;
            if (ss.PasportCopy != "")
            {
                Regex regex = new Regex(@".png$", RegexOptions.IgnoreCase);
                Regex regex1 = new Regex(@".jpg$", RegexOptions.IgnoreCase);
                MatchCollection matches = regex1.Matches(ss.PasportCopy);
                MatchCollection matches2 = regex.Matches(ss.PasportCopy);
                bool issucces = File.Exists(ss.PasportCopy);
                bool issucces2;
                if (matches.Count > 0 || matches2.Count > 0)
                {
                    if (issucces)
                    {
                        try
                        {
                            BitmapImage tt = new BitmapImage();
                            tt.BeginInit();
                            tt.UriSource = new Uri(ss.PasportCopy);
                            tt.EndInit();
                            issucces2 = true;
                        }
                        catch
                        {
                            MessageBox.Show("Некорректный файл");
                            issucces2 = false;
                        }
                        if (issucces2)
                        {
                            im.Img = ss.PasportCopy;
                            Photo photo = new Photo(im);
                            photo.Show();
                            im = new Supp.IM();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нет доступа к данному файлу или файл не существует");
                    }
                }
                else
                {
                    MessageBox.Show("Данный файл не в формате png или jpg");
                }
            }
            else
            {
                MessageBox.Show("Данный клиент не имеет копии паспорта");
            }
        }
    }
}
