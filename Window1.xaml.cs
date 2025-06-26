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

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            Container.Content = new Start();
        }
        private void Start_Content(object sender, RoutedEventArgs e)
        {
            Container.Content = new Start();
        }

        private void BMI_Content(object sender, RoutedEventArgs e)
        {
            Container.Content = new BMI();
        }
        private void Kroki_Content(object sender, RoutedEventArgs e)
        {
            Container.Content = new kroki();
        }

        private void Cwiczenia_Content(object sender, RoutedEventArgs e)
        {
            Container.Content = new Cwiczenia();
        }

        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow test = new MainWindow();
            test.Show();
        }
    }
}
