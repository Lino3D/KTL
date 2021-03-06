﻿using MahApps.Metro.Controls;
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
using WPF.Interfaces;
using WPF.Windows;

namespace WPF
{
    // Nasza gra:
    // (b) Gra komputer kontra człowiek
    // Komp optymalizuje pod kątem wyboru numerku na planszy


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Windows.SettingsWindow SettingsWindow;
        public GameEngine Engine { get; set; }
        public IPlayer1 AIPlayer;



        public GameCell CurentCell;

        public MainWindow()
        {
            InitializeComponent();
            AIMoveInProgres.Visibility = Visibility.Hidden;
            //    GameBoardPalette.IsEnabled = false;
            Engine = new GameEngine(RefreshMainWindow);
            //AIPlayer = new BasicAIImpl(Engine);
            AIPlayer = new MinMaxPlayerImpl(Engine);
            PlayerGrid.IsEnabled = false;
        }

        private void RefreshMainWindowOnInitialization()
        {
            PlayerColorPalette.DrawListBox(Engine.CurrentRoundColors);
            AllColorsPalette.DrawListBox(Engine.AllColors);
            GameBoardPalette.DrawListBox(Engine.GameBoard);

        }
        private void RefreshMainWindow()
        {
            PlayerColorPalette.DrawListBox(Engine.CurrentRoundColors);
            GameBoardPalette.DrawListBox(Engine.GameBoard);
            GameBoardPalette.SelectedIndex = AIPlayer.LastSelectedCell;
            if (Engine.GameStatus == GameStatusEnum.Player1Turn)
            {
                PlayerGrid.IsEnabled = false;
                AIMoveInProgres.Visibility = Visibility.Visible;
            }
            else if (Engine.GameStatus == GameStatusEnum.Player2Turn)
            {
                PlayerGrid.IsEnabled = true;
                AIMoveInProgres.Visibility = Visibility.Hidden;
            }
            if (Engine.GameStatus == GameStatusEnum.GameOver)
            {
                GameBoardPalette.SelectedIndex = -1;
                MessageBox.Show("GameOver!" +"\n Wygrał gracz: "+ Engine.GameResutl.ToString()  );
            }
        }

        private void PrintTurnInfo()
        {

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
                Engine.Player1MakeMove(AIPlayer.SelectBoardCell());
                RefreshMainWindow();
            }
            else
            {
                MessageBox.Show("Please set game settings first");
            }
        }

        private void InitializeGame()
        {
            Engine.Settings = SettingsWindow.Settings;
            Engine.InitializeGameBoard();
            Engine.InitializeAllColors();
            Engine.InitializeCurrentRoundColors();
            Engine.InitializeEvaluationFunction();
            RefreshMainWindowOnInitialization();
            AIDiffLabel.Content = Engine.Settings.Difficulty.ToString();
        }



        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerColorPalette.SelectedIndex != -1)
            {
                var chosenColor = Engine.CurrentRoundColors[PlayerColorPalette.SelectedIndex];
                Engine.Player2MakeMove(chosenColor.ColorId);
                if (Engine.GameStatus != GameStatusEnum.GameOver)
                    Engine.Player1MakeMove(AIPlayer.SelectBoardCell());
            }
            else
            {
                MessageBox.Show("Proszę wybrać Kolor");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }
    }
}
