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
using System.Windows.Shapes;

namespace Ирида
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Button_auth_Click(object sender, RoutedEventArgs e)
        {
            string login = loginbox.Text.Trim();
            string pass = passbox.Password.Trim();

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Проверьте корректность ввода логина и пароля.");
            }
            else
            {
                var user = DB.db.UsersSet.Where(d=>d.login == login && d.password == pass).FirstOrDefault();
                if (user != null)
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Проверьте корректность ввода логина и пароля.");
                }
            }
        }

        private void Button_Click_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
