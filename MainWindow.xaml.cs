using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string haslo = Input_Haslo.Password;
                string login = Input_Login.Text;
                string file_path = Directory.GetCurrentDirectory();
                string data = File.ReadAllText(file_path + @"\Resources_2\data.txt");
                int data_breaks = 0;
                int start = 0;
                int end = 0;
                for (int i = 0; i < data.Length; i++) //okreslenie ilosci zmiennych
                {
                    
                    char c = data[i];
                    if (c == '|') {
                        
                        break; 
                    }
                    if (c == ' ') { data_breaks++; }
                }
                string[] arr = new string[data_breaks];
                for (int i = 0; i < arr.Length; i++) //tworzy tablice z pliku txt (kazde dane nalezy konczyc znakiem spacji w pliku)
                {
                    end = data.IndexOf(" ");
                    if (end == -1) { break; }
                        arr[i] = data.Substring(start, end);
                        data = data.Remove(start, arr[i].Length + 1);
                }
                for (int i = 0; i < arr.Length; i++)
                {
                    if (login == arr[i] && haslo == arr[i + 1])
                    {
                        Window1 main_menu = new Window1();
                        main_menu.Owner = this;
                        this.Hide();
                        main_menu.ShowDialog();
                        return;
                    }
                }
                MessageBox.Show("Logowanie zakończone niepowodzeniem");
            } catch(Exception ex)
            {
                MessageBox.Show("Coś poszło nie tak "+ ex.Message);
            }
        }
        private void Input_Login_GotFocus(object sender, RoutedEventArgs e) //animacja która usuwa zawartość pola
        {
            if (Input_Login.Text == "Login")
            {
                Input_Login.Text = "";
            }
        }
        private void Input_Haslo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Input_Haslo.Password == "Hasło")
            {
                Input_Haslo.Password = "";
            }
        }
        private void Input_Login_LostFocus(object sender, RoutedEventArgs e) //animacja, gdy user nic nie wpisze, ma pozostać nazwa "login"
        {
            if (Input_Login.Text == "")
            {
                Input_Login.Text = "Login";
            }
        }
        private void Input_Haslo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Input_Haslo.Password == "")
            {
                Input_Haslo.Password = "Hasło";
            }
        }
    }
}
