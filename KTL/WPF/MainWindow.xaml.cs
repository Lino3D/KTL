using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Classes;
using WPF.Helpers;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Windows.SettingsWindow SettingsWindow;
        public Settings Settings { get; private set; }

        public List<GameColor> AllColors;
        public List<GameCell> GameBoard;
        public List<GameColor> CurrentRoundColors;
        private static readonly Random Rand = new Random();
        public MainWindow()
        {
            InitializeComponent();
            var lst = new List<Color>() { Color.FromRgb(100, 100, 200), Color.FromRgb(100, 0, 200), Color.FromRgb(100, 100, 0), Color.FromRgb(100, 100, 150), Color.FromRgb(100, 200, 150), Color.FromRgb(100, 100, 150) };
            PlayerColorPalette.DrawListBox(lst);
            AllColorsPalette.DrawListBox(lst);

        }


        private void RefreshMainWindow()
        {           
           // PlayerColorPalette.DrawListBox(lst);
          //  if( Settings != null)
         //   AllColorsPalette.DrawListBox(Settings.ColorList);
        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            HandleSettings();
        }

        private void HandleSettings()
        {
            SettingsWindow = new Windows.SettingsWindow();
            SettingsWindow.ShowDialog();
            if (SettingsWindow.Settings != null)
            {
                InitializeGame();
            }
            else
            {
                MessageBox.Show("Please set game settings first");
            }
        }

        private void InitializeGame()
        {
            Settings = SettingsWindow.Settings;
            InitializeAllColors();
            InitializeCurrentRoundColors();

        }

        private void InitializeCurrentRoundColors()
        {

            CurrentRoundColors = new List<GameColor>();
            CurrentRoundColors = AllColors.OrderBy(x => Rand.Next()).Take(Settings.RoundColors).ToList();
            PlayerColorPalette.DrawListBox(CurrentRoundColors.Select(x => x.Color).ToList());
        }

        private void InitializeAllColors()
        {
            AllColors = new List<GameColor>();

            for (int i = 0; i < Settings.AllColors; i++)
            {
                AllColors.Add(new GameColor(i, Color.FromArgb((byte)Rand.Next(255), (byte)Rand.Next(255), (byte)Rand.Next(255), 255)));
            }
        }
    }
}
