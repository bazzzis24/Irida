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
    /// Логика взаимодействия для Photo.xaml
    /// </summary>
    public partial class Photo : Window
    {
        public Photo(Supp.IM im)
        {
            InitializeComponent();
            BitmapImage tt = new BitmapImage();
            tt.BeginInit();
            tt.UriSource = new Uri(im.Img);
            tt.EndInit();
            Image1.Source = tt;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
