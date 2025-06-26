using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Metrics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static WpfApp1.BMI;

namespace WpfApp1
{
    interface Ikroki
    {
        public void Plik_conn(); //Połączenie sie z plikiem txt, wyciagniecie danych
        public void Zapis(string czas, string wynik); //Zapis dodanych kroków w pliku txt
    }
    public partial class kroki : UserControl, Ikroki
    {
        public kroki()
        {
            InitializeComponent();
            KrokiList = new ObservableCollection<KrokiData>();
            dataGrid.ItemsSource = KrokiList;
            Plik_conn();
        }
        public ObservableCollection<KrokiData> KrokiList { get; set; }
        public class KrokiData
        {
            public int IloscKrokow { get; set; }
            public string Data { get; set; }

        }
        public class Vars
        {
            public string file_path = "";
            public string file_name = "data.txt";
            public string data = "";
            public string[] tablica = { };
            public int pozycja_startowa = 0;
            public int pozycja_koncowa = 0;
            public int counter = 0;
            public int tab_size;
            public int index = 0;
        }
        public void Plik_conn()
        {
            Vars vars = new Vars();
            vars.file_path = Directory.GetCurrentDirectory() + @"\Resources_2\data.txt";
            vars.data = File.ReadAllText(vars.file_path);
            for (int i = 0; i < vars.data.Length; i++) //pozycja startowa
            {
                if (vars.counter == 2)
                {
                    vars.pozycja_startowa = i;
                    break;
                }
                if (vars.data[i] == '|')
                {
                    vars.counter++;
                }
            };
            vars.pozycja_koncowa = vars.data.LastIndexOf("|");
            //pozycja koncowa - uwaga niebezpieczne jezeli pojawia sie nowe dane to wskaze zly punkt koncowy dla sekcji danych dla krokow
            //ustalenie wielkosci tablicy
            for (int i = vars.pozycja_startowa; i + 1 < vars.pozycja_koncowa; i++)
            {
                if (vars.data[i] == ' ')
                {
                    vars.tab_size++;
                }
            }
            vars.tablica = new string[vars.tab_size];
            for (int i = vars.pozycja_startowa+1; i < vars.pozycja_koncowa; i++)
            {
                if(vars.data[i] != ' ')
                {
                    vars.tablica[vars.index] = vars.tablica[vars.index] + vars.data[i];
                } else
                {
                    vars.index++;
                }
            }
            UpdateGrid(vars);
        }
        public void UpdateGrid(Vars vars) //Tworzenie grida
        {
            for (int i = 0; i < vars.index; i = i + 2)
            {
                KrokiList.Add(new KrokiData
                {
                    Data = vars.tablica[i + 1],
                    IloscKrokow = int.Parse(vars.tablica[i])
                });
            }
        }
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ilosc.Text) && int.TryParse(ilosc.Text, out int kroki) && kroki >= 0)
            {
                string czas = DateTime.Now.ToString("dd.MM.yyyy");
                KrokiList.Add(new KrokiData
                {
                    IloscKrokow = kroki,
                    Data = czas
                });
                Zapis(czas,kroki.ToString()); //zapis w pliku txt
                ilosc.Clear();
            }
            else
            {
                MessageBox.Show("Wprowadź poprawną liczbę kroków.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Zapis(string czas,string wynik)
        {
            Vars vars = new Vars();
            vars.file_path = Directory.GetCurrentDirectory();
            vars.data = File.ReadAllText(vars.file_path + @"\Resources_2\data.txt");
            string result = " " + wynik + " " + czas;
            vars.pozycja_koncowa = vars.data.LastIndexOf("|");
            vars.data = vars.data.Insert(vars.pozycja_koncowa - 1, result);
            File.WriteAllText(vars.file_path + @"\Resources_2\data.txt", vars.data);
        }
        private void ilosc_GotFocus(object sender, RoutedEventArgs e)
        {
            ilosc.Clear();
        }
    }
}
