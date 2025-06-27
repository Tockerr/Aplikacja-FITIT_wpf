using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    interface Icwiczenia
    {
        private void GeneratePlan_Click(object sender, RoutedEventArgs e) { } //generowanie planu treningowego na wydarzenie klikniecia przycisku
    }
    public partial class Cwiczenia : UserControl
    {
        public ObservableCollection<TrainingPlanItem> TrainingPlan { get; set; }

        public Cwiczenia()
        {
            InitializeComponent();
            TrainingPlan = new ObservableCollection<TrainingPlanItem>();
            TrainingPlanDataGrid.ItemsSource = TrainingPlan;
        }
        public class TrainingPlanItem
        {
            public string Dzien { get; set; }
            public string Trening { get; set; }
            public double KalorieSpalone { get; set; }

            public TrainingPlanItem()
            {
                Dzien = string.Empty;
                Trening = string.Empty;
                KalorieSpalone = 0.0;
            }
        }

        private void GeneratePlan_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(BmiTextBox.Text, out double bmi))
            {
                MessageBox.Show("Wprowadź prawidłowe BMI.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedDifficulty = DifficultyComboBox.SelectedItem as ComboBoxItem;
            string difficulty = selectedDifficulty?.Content.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(difficulty))
            {
                MessageBox.Show("Wybierz poziom trudności.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(DaysPerWeekTextBox.Text, out int daysPerWeek) || daysPerWeek <= 0 || daysPerWeek > 7)
            {
                MessageBox.Show("Wprowadź prawidłową liczbę dni treningowych (1-7).", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<string> preferences = new List<string>();
            if (GymCheckBox.IsChecked == true) preferences.Add("Siłownia");
            if (SwimmingCheckBox.IsChecked == true) preferences.Add("Basen");
            if (CyclingCheckBox.IsChecked == true) preferences.Add("Rower");
            if (RunningCheckBox.IsChecked == true) preferences.Add("Bieganie");

            List<string> freeDays = new List<string>();
            if (MondayCheckBox.IsChecked == true) freeDays.Add("Poniedziałek");
            if (TuesdayCheckBox.IsChecked == true) freeDays.Add("Wtorek");
            if (WednesdayCheckBox.IsChecked == true) freeDays.Add("Środa");
            if (ThursdayCheckBox.IsChecked == true) freeDays.Add("Czwartek");
            if (FridayCheckBox.IsChecked == true) freeDays.Add("Piątek");
            if (SaturdayCheckBox.IsChecked == true) freeDays.Add("Sobota");
            if (SundayCheckBox.IsChecked == true) freeDays.Add("Niedziela");

            var trainingPlan = GenerateTrainingPlan(bmi, difficulty, daysPerWeek, preferences, freeDays);
            TrainingPlan.Clear();
            foreach (var item in trainingPlan)
            {
                TrainingPlan.Add(item);
            }
        }
        private List<TrainingPlanItem> GenerateTrainingPlan(double bmi, string difficulty, int daysPerWeek, List<string> preferences, List<string> freeDays)
        {
            List<TrainingPlanItem> plan = new List<TrainingPlanItem>();

            List<string> allDays = new List<string> { "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };
            int daysToTrain = 0;
            foreach (var day in allDays)
            {
                if (!freeDays.Contains(day) && daysToTrain < daysPerWeek)
                {
                    string trening = preferences.Count > 0 ? preferences[daysToTrain % preferences.Count] : "Dowolny";
                    double kalorieSpalone = CalculateCalories(bmi, trening, difficulty);
                    plan.Add(new TrainingPlanItem { Dzien = day, Trening = trening, KalorieSpalone = kalorieSpalone });
                    daysToTrain++;
                }
            }
            return plan;
        }
        private double CalculateCalories(double bmi, string trening, string difficulty)
        {
            double baseCalories = 100;
            switch (trening)
            {
                case "Siłownia":
                    baseCalories = 200;
                    break;
                case "Basen":
                    baseCalories = 300;
                    break;
                case "Rower":
                    baseCalories = 250;
                    break;
                case "Bieganie":
                    baseCalories = 350;
                    break;
            }

            switch (difficulty)
            {
                case "Początkujący":
                    baseCalories *= 1.0;
                    break;
                case "Średniozaawansowany":
                    baseCalories *= 1.2;
                    break;
                case "Zaawansowany":
                    baseCalories *= 1.5;
                    break;
            }

            return Math.Round(baseCalories * (bmi / 25));//zaokrąglanie
        }
    }
}
