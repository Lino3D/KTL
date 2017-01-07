using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WPF.Classes;

namespace WPF.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        public Settings Settings;
        public SettingsWindow()
        {
            InitializeComponent();
            DifficulityComboBox.ItemsSource = Enum.GetValues(typeof(AIDifficultyEnum)).Cast<AIDifficultyEnum>();
            DifficulityComboBox.SelectedIndex = 1;
        }


        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (InitializeSettings())
                this.Close();
            else
                MessageBox.Show("Proszę poprawić parametry");

        }

        private bool InitializeSettings()
        {
            int allColors = 0;
            int roundColors = 0;
            int boardLength = 0;
            int seriesLength = 0;
            AIDifficultyEnum difficulty = AIDifficultyEnum.Medium;

            if (!int.TryParse(AllColorsTextBox.Text, out allColors)) return false;
            if (!int.TryParse(RoundColorsTextBox.Text, out roundColors)) return false;
            if (!int.TryParse(BoardLengthTextBox.Text, out boardLength)) return false;
            if (!int.TryParse(SeriesLengthTextBox.Text, out seriesLength)) return false;
            if (boardLength <= 0 || seriesLength <= 0 || roundColors <= 0 || allColors <= 0) return false;
            if (seriesLength > boardLength || roundColors > allColors) return false;

            difficulty = (AIDifficultyEnum)DifficulityComboBox.SelectedItem;


            allColors = 2;
            roundColors = 2;
            boardLength = 10;
            seriesLength = 3;
            Settings = new Settings(allColors, roundColors, boardLength, seriesLength, difficulty);
            return true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void AllColorsTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void RoundColorsTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void BoardLengthTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void SeriesLengthTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
