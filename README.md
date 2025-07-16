# FITIT – Aplikacja do monitorowania aktywności i BMI

FITIT to aplikacja desktopowa stworzona w technologii WPF (Windows Presentation Foundation) w języku C#, która korzysta z interfejsu graficznego oraz prostego systemu plików tekstowych do przechowywania danych.
Pozwala użytkownikowi:
- logować się przy użyciu loginu i hasła,
- obliczyć wskaźnik BMI i zapisać go do historii,
- tworzyć indywidualny plan treningowy,
- ręcznie zapisywać ilość kroków,
- przeglądać podsumowanie aktywności,

# Technologie

Projekt wykorzystuje:
- .NET 6.0 (Windows Desktop) – platforma uruchomieniowa,
- WPF (Windows Presentation Foundation) – framework do tworzenia interfejsu graficznego,

Dane przechowywane są lokalnie w plikach tekstowych (`data.txt`), bez użycia bazy danych.


# Instalacja:

Projekt można uruchomić w środowisku Visual Studio 2022 lub nowszym.  
Wymagane jest zainstalowane SDK **.NET 6.0** oraz wsparcie dla aplikacji desktopowych (WPF).
W celu zalogowania się do aplikacji, należy podać dane zapisane w pliku `data.txt` na początku - login **"Kuba"** oraz hasło **"wsb"**.


# Struktura projektu i paradygmaty

 ## BMI.xaml
Opis: strona z obliczaniem BMI na podstawie podanej masy oraz wzrostu. Po obliczeniu aplikacja poda stan BMI na podstawie z góry podanych w kodzie zakresów wartości, czy BMI jest prawidłowe, za wysokie bądź za niskie.

  **Paradygmaty:**
- Enkapsulacja:
  - Dane użytkownika (waga, wzrost, BMI) są przechowywane w prywatnych polach lub lokalnych zmiennych.
  - Klasy pomocnicze i metody działają na tych danych wewnątrz klasy.
- Abstrakcja:
  - Klasa BMI ukrywa szczegóły logiki obliczania BMI – użytkownik widzi tylko wynik, nie sposób jego uzyskania.
- Dziedziczenie:
  - `BMI : UserControl` – dziedziczy z kontrolki WPF.

## `Cwiczenia.xaml`
Opis: strona z planowaniem i generowaniem planu treningowego na wydarzenia kliknięcia przycisku. Tworzy plan na podstawie wybranych dni wolnych od ćwiczeń, możliwości treningowych oraz liczby dni treningowych. Zależnie od wartości BMI oraz poziomu trudności, oblicza przewidywaną liczbę spalonych kalorii podczas treningu.

  **Paradygmaty:**
- Enkapsulacja:
  - Klasa wewnętrzna `TrainingPlanItem` przechowuje dane ćwiczeń.
- Abstrakcja:
  - Interfejs `Icwiczenia` – deklaruje logikę generowania planu.
- Dziedziczenie:
  - `Cwiczenia` : `UserControl`.
- **Polimorfizm**:
  - Implementacja interfejsu `Icwiczenia`.

## `kroki.xaml` 
Opis: strona z wprowadzaniem oraz zapisywaniem podanej liczby zrobionych kroków. Zapisuje w systemie tą wartość wraz z datą danego dnia, w którym liczba została wprowadzona za pomocą przycisku 'DODAJ'.

  **Paradygmaty:**
- Enkapsulacja:
  - `Vars` przechowuje ścieżkę do pliku, dane, indeksy itd.
  - `KrokiData` – model danych dla widoku (ilość kroków i data).
- Abstrakcja:
  - Interfejs `Ikroki` z metodami `Plik_conn()` i `Zapis()`.
  - *Dziedziczenie*:
  - `kroki : UserControl` – kontrolka WPF.
- Polimorfizm:
  - Można użyć instancji klasy przez interfejs `Ikroki`.

## `MainWindow.xaml` 
Opis: strona główna z logowaniem z walidacją. Żeby zalogować się, trzeba podać poprawny login oraz hasło (Kuba  ,  wsb).

  **Paradygmaty:**
- Enkapsulacja:
  - Obsługuje logikę logowania (np. pobieranie nazwy użytkownika), zapisuje dane tylko lokalnie.
- Abstrakcja:
  - Metody jak `Zaloguj_Click` czy `SprawdzLogin()` ukrywają szczegóły działania za prostym przyciskiem.
- Dziedziczenie:
  - `MainWindow : Window` – główne okno aplikacji dziedziczy z Window.

## `Start.xaml` 
Opis: main z podsumowaniem ostatnich wprowadzonych danych wraz z aktualizowaniem liczby dni od czasu ostatniej aktualizacji.

  **Paradygmaty:**
- Enkapsulacja:
  - Klasa wewnętrzna `Vars` przechowuje dane lokalne jak `nazwa_uzytkownika`, `data`, `BMI_tab`, `kroki_tab`.
- Abstrakcja:
  - Interfejs Istart definiuje metody jak Wypisanie, ReverseString, pozycja_koncowa, oddzielając „co robi” od „jak”.
- Dziedziczenie:
  - `Start : UserControl` – korzysta z funkcjonalności WPF-owego UserControl.
- Polimorfizm:
  - Klasa implementuje Istart, może być użyta przez typ interfejsowy.

## `Window1.xaml` 
Opis: nawigadcja boczna do 'kroki', 'bmi', 'main', 'plan'.

  **Paradygmaty:**
- Enkapsulacja:
  - Klasa `Window1` zawiera `TabControl` i ładuje inne widoki (`Start`, `Cwiczenia`, `kroki`, `BMI`), nie udostępniając ich wewnętrznych danych.
- Abstrakcja:
  - Działa jako kontener widoków – użytkownik widzi tylko przełączanie zakładek, nie logikę ich ładowania.
- Dziedziczenie:
  - `Window1 : Window` – własne okno kontenera.

## `Resources_2`  
Opis: pliki z grafikami oraz `DataGrid` i `data.txt` (służy do zapisywania i odczytywania danych użytkownika).


# Licencja

Projekt stworzony w celach edukacyjnych.


# Autorzy:
- **Kuba Dąbek**
- **Mateusz Bywalec**
