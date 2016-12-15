using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public GameCell CurentCell;
        private static readonly Random Rand = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RefreshMainWindowOnInitialization()
        {
            PlayerColorPalette.DrawListBox(CurrentRoundColors);
            AllColorsPalette.DrawListBox(AllColors);
            GameBoardPalette.DrawListBox(GameBoard);
        }

        private void RefreshMainWindow()
        {
            AllColorsPalette.DrawListBox(AllColors);
            GameBoardPalette.DrawListBox(GameBoard);
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
            InitializeGameBoard();
            InitializeAllColors();
            InitializeCurrentRoundColors();
            RefreshMainWindowOnInitialization();

        }

        private void InitializeGameBoard()
        {
            GameBoard = new List<GameCell>();
            for (int i = 0; i < Settings.BoardLength; i++)
            {
                GameBoard.Add(new GameCell() {CellNumber = i, Color = new GameColor()});
            }

            foreach (var item in GameBoard)
            {
                
            }


        }

        private void InitializeCurrentRoundColors()
        {

            CurrentRoundColors = new List<GameColor>();
            CurrentRoundColors = AllColors.OrderBy(x => Rand.Next()).Take(Settings.RoundColors).ToList();
            PlayerColorPalette.DrawListBox(CurrentRoundColors);
        }

        private void InitializeAllColors()
        {
            AllColors = new List<GameColor>();

            for (int i = 0; i < Settings.AllColors; i++)
            {
                AllColors.Add(new GameColor(i + 1, Color.FromArgb((byte)Rand.Next(255), (byte)Rand.Next(255), (byte)Rand.Next(255), 255)));
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerColorPalette.SelectedItems != null)
            {
                var chosenColor = CurrentRoundColors[PlayerColorPalette.SelectedIndex];
            }
            else
            {
                MessageBox.Show("Proszę wybrać Kolor");
            }
        }
    }
}
