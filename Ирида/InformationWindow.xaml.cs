using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ирида
{
    /// <summary>
    /// Логика взаимодействия для InformationWindow.xaml
    /// </summary>
    public partial class InformationWindow : Window
    {
        Clients client { get; set; }
        Supp.IM im { get; set; }
        public InformationWindow(Clients clients)
        {
            InitializeComponent();
            client = clients;
            im = new Supp.IM();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Name.Text = client.Name;
            LastName.Text = client.LastName;
            Patronomic.Text = client.Patronomic;
            Pasport.Text = client.Pasport;
            Phone.Text = client.PhoneNumber;
            BirthDate.SelectedDate = client.BirthDate;
            PasportCopy.Text = client.PasportCopy;
        }

        private void Exit_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                MessageBox.Show("У данного клиента нет копии паспорта");
            }
        }
    }
}
