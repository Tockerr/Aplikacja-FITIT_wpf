# FITIT – Aplikacja do monitorowania aktywności i BMI

FITIT to aplikacja desktopowa stworzona w technologii WPF (Windows Presentation Foundation) w języku C#, która korzysta z interfejsu graficznego oraz prostego systemu plików tekstowych do przechowywania danych.
Pozwala użytkownikowi:
-logować się przy użyciu loginu i hasła,
-obliczyć wskaźnik BMI i zapisać go do historii,
-tworzyć indywidualny plan treningowy,
-ręcznie zapisywać ilość kroków,
-przeglądać podsumowanie aktywności,

# Technologie

Projekt wykorzystuje:
- .NET 6.0 (Windows Desktop) – platforma uruchomieniowa,
- WPF (Windows Presentation Foundation)** – framework do tworzenia interfejsu graficznego,

Dane przechowywane są lokalnie w plikach tekstowych (`data.txt`), bez użycia bazy danych.


# Uruchamianie:

Projekt można uruchomić w środowisku Visual Studio 2022 lub nowszym.  
Wymagane jest zainstalowane SDK **.NET 6.0** oraz wsparcie dla aplikacji desktopowych (WPF).

W celu zalogowania się do aplikacji, należy podać login **"Kuba"** oraz hasło **"wsb"**.



# Struktura projektu

- `BMI.xaml` - strona z obliczaniem BMI na podstawie podanej masy oraz wzrostu. Po obliczeniu aplikacja poda stan BMI na podstawie z góry podanych zakresów wartości, czy BMI jest prawidłowe, za wysokie bądź na niskie.
- `Cwiczenia.xaml` - strona z planowaniem i generowaniem planu treningowego na wydarzenia kliknięcia przycisku. Tworzy plan na podstawie wybranych dni wolnych od ćwiczeń, możliwości treningowych oraz liczby dni treningowych. Zależnie od wartości BMI oraz poziomu trudności,
  oblicza przewidywaną liczbę spalonych kalorii podczas treningu.
- `kroki.xaml` - strona z wprowadzaniem oraz zapisywaniem podanej liczby zrobionych kroków. Zapisuje w systemie tą wartość wraz z datą danego dnia, w którym liczba została wprowadzona za pomocą przycisku 'DODAJ'.
- `MainWindow.xaml` - strona główna z logowaniem z walidacją. Żeby zalogować się, trzeba podać poprawny login oraz hasło (Kuba  ,  wsb).
- `Start.xaml` - main z podsumowaniem ostatnich wprowadzonych danych wraz z aktualizowaniem liczby dni od czasu ostatniej aktualizacji.
- `Window1.xaml` - nawigadcja boczna do 'kroki', 'bmi', 'main', 'plan'.
- `Resources_2` - pliki z grafikami oraz datagrid i data.txt w którym jest podany logim i hasło do aplikacji (Kuba  wsb) wraz z zapisanymi datami.


# Licencja

Projekt stworzony w celach edukacyjnych.


# Autorzy:
- **Kuba Dąbek**
- **Mateusz Bywalec**
