using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using static WpfApp1.BMI;

namespace WpfApp1
{
    interface Ibmi
    {
        private void BMI_Calc(object sender, RoutedEventArgs e) { } //Obliczanie wartosci BMI dla użytkownika oraz wyświetlanie komunikatów odpowiednich do wyniku
        public string Sugestia(string BMI); //Sugestia dla użytkownika po podaniu BMI i zatwierdzeniu go 
        public void Zapis(string czas, string wynik); //Zapisywanie wartości końcowej do pliku txt
        public void ArrayCreate(); //Tworzenie tablicy zmiennej z pliku txt do wczytania przez metode UpdateGrid()
        public void UpdateGrid(Vars vars); //Przepisywanie tablicy programowej na DataGrid graficzny
    }
    public partial class BMI : UserControl, Ibmi
    {
        public ObservableCollection<BMIRecord> DataGridItems { get; set; }
        public BMI()
        {
            InitializeComponent();
            //DataGridItems = new ObservableCollection<BMIRecord>();
            DataContext = this;
            if (DataGridItems == null)
            {
                MessageBox.Show("DataGridItems is null after initialization!");
            }
            if (DataContext == null)
            {
                MessageBox.Show("DataContext is null after initialization!");
            }
        }
        public class BMIRecord
        {
            public string Data { get; set; } = "";
            public string Wynik { get; set; } = "";
            public string Sugerowane_dzial { get; set; } = "";
        }
        public class BMI_Calc_Vars 
        {
            public double masa = 0;
            public double wzrost = 0;
            public double wynik = 0;
            public string czas = "";
            public string tekst = "Twoje BMI wynosi: ";
            public string tekst_koncowy_1 = ", jest prawidłowe.";
            public string tekst_koncowy_2 = ", jest za wysokie.";
            public string tekst_koncowy_3 = ", jest za niskie.";
        }
        public class Vars
        {
            public string file_path = "";
            public string data = "";
            public string[] dane_BMI = { };
            public int counter = 0;
            public int pozycja_startowa = 0;
            public int pozycja_koncowa = 0;
            public int tab_size = 0;
            public string num_chold = "";
            public int index = 0;
            public string sugerowane = "";
        }
        private void BMI_Calc(object sender, RoutedEventArgs e) //Obliczenie BMI,
        {
            BMI_Calc_Vars vars = new BMI_Calc_Vars();
            try
            {
                vars.masa = double.Parse(Input_Masa.Text);
                vars.wzrost = double.Parse(Input_Wzrost.Text);
                vars.wynik = 0;
                vars.wynik = vars.masa / Math.Pow(vars.wzrost, 2);
                vars.wynik = vars.wynik * 10000;
                vars.wynik = Math.Round(vars.wynik, 2);
                vars.czas = DateTime.Now.ToString("dd.MM.yyyy");
                if (vars.wynik >= 18.5 && vars.wynik <= 25)
                {
                    Text_BMI.Text = "";
                    Text_BMI.Inlines.Add(new Run(vars.tekst));
                    Text_BMI.Inlines.Add(new Run(vars.wynik.ToString()) { Foreground = Brushes.Beige });
                    Text_BMI.Inlines.Add(new Run(vars.tekst_koncowy_1));
                }
                else if (vars.wynik > 25)
                {
                    Text_BMI.Text = "";
                    Text_BMI.Inlines.Add(new Run(vars.tekst));
                    Text_BMI.Inlines.Add(new Run(vars.wynik.ToString()) { Foreground = Brushes.Red });
                    Text_BMI.Inlines.Add(new Run(vars.tekst_koncowy_2));
                }
                else
                {
                    Text_BMI.Text = "";
                    Text_BMI.Inlines.Add(new Run(vars.tekst));
                    Text_BMI.Inlines.Add(new Run(vars.wynik.ToString()) { Foreground = Brushes.Red });
                    Text_BMI.Inlines.Add(new Run(vars.tekst_koncowy_3));
                }
                DataGridItems.Add(new BMIRecord
                {
                    Data = vars.czas,
                    Wynik = vars.wynik.ToString(),
                    Sugerowane_dzial = Sugestia(vars.wynik.ToString())
                });
                Zapis(vars.czas, vars.wynik.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Coś poszło nie tak, spróbuj ponownie - " + ex.Message);
            }
        }
        public void Zapis(string czas,string wynik)
        {
            Vars vars = new Vars();
            vars.file_path = Directory.GetCurrentDirectory();
            vars.data = File.ReadAllText(vars.file_path + @"\Resources_2\data.txt");
            string result = " " + wynik + " " + czas;
            for (int i = 0; i < vars.data.Length; i++) //szukanie pozycji końcowej |
            {
                if (vars.data[i] == '|')
                { vars.counter++; }
                if (vars.counter == 2)
                {
                    vars.pozycja_koncowa = i;
                    break;
                }
            }
            vars.data = vars.data.Insert(vars.pozycja_koncowa - 1, result);
            File.WriteAllText(vars.file_path + @"\Resources_2\data.txt", vars.data);
        }
        private void DataGridRowStyle1_Initialized(object sender, EventArgs e)
        {
            ArrayCreate();
        }
        public void ArrayCreate()
        {
            DataGridItems = new ObservableCollection<BMIRecord>();
            Vars vars = new Vars();
            vars.file_path = Directory.GetCurrentDirectory();
            vars.data = File.ReadAllText(vars.file_path + @"\Resources_2\data.txt");
            for (int i = 0; i < vars.data.Length; i++) //szukanie pozycji |
            {
                char znak = vars.data[i];
                if (znak == '|')
                {
                    vars.pozycja_startowa = i;
                    break;
                }
            }
            for (int i = 0; i < vars.data.Length; i++) //szukanie pozycji końcowej |
            {
                char znak = vars.data[i];
                if (znak == '|')
                { vars.counter++; }
                if (vars.counter == 2)
                {
                    vars.pozycja_koncowa = i;
                    break;
                }
            }
            for (int i = vars.pozycja_startowa; i <= vars.pozycja_koncowa; i++)
            {
                char znak = vars.data[i];
                if (znak == ' ')
                {
                    vars.tab_size++;
                }
            }
            vars.dane_BMI = new string[vars.tab_size - 1];
            for (int g = vars.pozycja_startowa + 2; g < vars.pozycja_koncowa; g++)
            {
                char znak = vars.data[g];
                if (znak != ' ' && znak != '|')
                {
                    vars.num_chold = vars.num_chold + znak;
                }
                else
                {
                    vars.dane_BMI[vars.index] = vars.num_chold;
                    vars.index++;
                    vars.num_chold = "";
                }
            }
            UpdateGrid(vars);
        }
        public void UpdateGrid(Vars vars) //Tworzenie grida
        {
            for (int i = 0; i < vars.index; i = i + 2)
            {
                vars.sugerowane = Sugestia(vars.dane_BMI[i]);
                DataGridItems.Add(new BMIRecord
                {
                    Data = vars.dane_BMI[i + 1],
                    Wynik = vars.dane_BMI[i],
                    Sugerowane_dzial = vars.sugerowane
                });
            } 
        }
        public string Sugestia(string BMI)
        {
            if (double.Parse(BMI) > 25)
            {
                return "Powinieneś schudnąć.";
            }
            else if (double.Parse(BMI) < 18.5)
            {
                return "Powinieneś zjeść coś.";
            }
            else
            {
                return "Twoje BMI jest w normie.";
            }
        }
        //Inputy stylowanie do latwiejszego wpisywania
        private void Input_Masa_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Input_Masa.Text == "Masa ciała")
            {
                Input_Masa.Text = "";
            }
        }
        private void Input_Wzrost_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Input_Wzrost.Text == "Wzrost")
            {
                Input_Wzrost.Text = "";
            }
        }

        private void Input_Masa_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Input_Masa.Text == "")
            {
                Input_Masa.Text = "Masa ciała";
            }
        }

        private void Input_Wzrost_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Input_Wzrost.Text == "")
            {
                Input_Wzrost.Text = "Wzrost";
            }
        }
    }
}
