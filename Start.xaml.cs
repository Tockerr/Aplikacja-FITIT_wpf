using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    interface Istart
    {
        private void Powitanie_Initialized(object sender, EventArgs e) { } //Po wejsciu w widok, pobiera dane z nazwa uzytkownika zeby go przywitac 
        private void Kroki_zawartosc_Initialized(object sender, EventArgs e) { } //Po inicjalizacji textblocku kroki wyswietla zawartosc
        private void BMI_zawartosc_Initialized(object sender, EventArgs e) { } //Po inicjalizacji textblocku BMI wyswietla zawartosc
        public int pozycja_koncowa(string data); //szukanie pozycji ostatniego znaku w pliku txt
        public void Wypisanie(string target, string[] tab); //Wypisanie wartosci, wraz z policzeniem daty wprowadzenia ostatnich rekordow do pliku
        public string[] ReverseString(string[] tab); //obrocenie wynikow branych z pliku txt

    }
    public partial class Start : UserControl, Istart
    {
        public Start()
        {
            InitializeComponent();
        }
        public class Vars
        {
            public string data = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Resources_2\data.txt");
            public string nazwa_uzytkownika = "";
            public string[] kroki_tab = { };
            public string[] BMI_tab = { };
            public string target = "";
            public int pozycja_koncowa = 0;
        }
        private void Powitanie_Initialized(object sender, EventArgs e)
        {
            Vars vars = new Vars();
            for (int i = 0; i < vars.data.Length; i++) //Wyciagniecie nazwy uzytkownika aktualnie korzystajacego z aplikacji
            {
                if (vars.data[i] != ' ')
                {
                    vars.nazwa_uzytkownika = vars.nazwa_uzytkownika + vars.data[i];
                } else
                {
                    break;
                }
            }
            Powitanie.Content = "Witaj, "+ vars.nazwa_uzytkownika + " oto Twoje ostatnie osiągnięcia:";
        }
        private void Kroki_zawartosc_Initialized(object sender, EventArgs e)
        {
            Vars vars = new Vars();
            vars.pozycja_koncowa = vars.data.LastIndexOf("|");
            vars.kroki_tab = new string[2];
            vars.kroki_tab = Uzupelnianie(vars.kroki_tab,vars.data, vars.pozycja_koncowa);
            vars.kroki_tab = ReverseString(vars.kroki_tab); //odwrócenie znaków w tablicy
            Wypisanie(vars.target = "Kroki",vars.kroki_tab);
        }
        private void BMI_zawartosc_Initialized(object sender, EventArgs e)
        {
            Vars vars = new Vars();
            vars.pozycja_koncowa = pozycja_koncowa(vars.data);
            vars.BMI_tab = new string[2];
            vars.BMI_tab = Uzupelnianie(vars.BMI_tab, vars.data, vars.pozycja_koncowa);
            vars.BMI_tab = ReverseString(vars.BMI_tab); //odwrócenie znaków w tablicy
            Wypisanie(vars.target = "BMI", vars.BMI_tab);
        }
        public int pozycja_koncowa(string data)
        {
            int pozycja_koncowa = 0;
            int counter = 0;
            for (int i = 0; i < data.Length; i++) //szukanie pozycji końcowej |
            {
                if (data[i] == '|')
                { counter++; }
                if (counter == 2)
                {
                    pozycja_koncowa = i;
                    break;
                }
            }
            return pozycja_koncowa;
        }
        public void Wypisanie(string target, string[] tab)
        {
            if (target == "Kroki")
            {
                DateTime teraz = DateTime.Now;
                DateTime wtedy = DateTime.Parse(tab[0]);
                TimeSpan diff = teraz - wtedy;
                Kroki_zawartosc.Text = tab[1] + " kroków,";
                Kroki_zawartosc_2.Text = diff.Days + " dni minęło od ostatniej aktualizacji";
            } else if (target == "BMI")
            {
                DateTime teraz = DateTime.Now;
                DateTime wtedy = DateTime.Parse(tab[0]);
                TimeSpan diff = teraz - wtedy;
                BMI_zawartosc.Text = tab[1] + " BMI,";
                BMI_zawartosc_2.Text = diff.Days + " dni minęło od ostatniej aktualizacji";
            }
        }
        public string[] Uzupelnianie(string[] tab,string data,int pozycja_koncowa)
        {
            int index = 0;
            for (int i = pozycja_koncowa - 2; i >= 0; i--)
            {
                if (data[i] != ' ')
                {
                    tab[index] = tab[index] + data[i];
                }
                else
                {
                    index++;
                    if (index >= tab.Length)
                    {
                        break;
                    }
                }
            }
            return tab;
        }
        public string[] ReverseString(string[] tab)
        {
            string[] tab_reversed = { "", "", "" };
            for(int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab[i].Length; j++)
                {
                    tab_reversed[i] = tab_reversed[i] + tab[i][(tab[i].Length-1) - j];
                }
            }
            return tab_reversed;
        }
        //Kroki_zawartosc, BMI_zawartosc
    }
}
