using System.Text.RegularExpressions;
using System.Windows;
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
        }
        

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            InitializeSettings();
            this.Close();

        }

        private void InitializeSettings()
        {
            int allColors = 0;
            int roundColors = 0;
            int boardLength = 0;
            int seriesLength = 0;

            int.TryParse(AllColorsTextBox.Text, out allColors);
            int.TryParse(RoundColorsTextBox.Text, out roundColors);
            int.TryParse(BoardLengthTextBox.Text, out boardLength);
            int.TryParse(SeriesLengthTextBox.Text, out seriesLength);

            Settings = new Settings(allColors, roundColors, boardLength, seriesLength);
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
