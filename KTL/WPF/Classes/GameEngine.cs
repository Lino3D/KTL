﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF.Classes
{
    public class GameEngine
    {
        // Player 1  - to ten, co wybieranumerek na planszy
        // Player 2 - to ten, co wybiera kolor, z losowanej listy;
        public GameStatusEnum GameStatus = GameStatusEnum.NotInitialized;
        private static readonly Random Rand = new Random();
        private Action RefreshMainWindow;

        public GameEngine(Action callback)
        {
            RefreshMainWindow = callback;
        }

        public Settings Settings { get; set; }

        public List<GameColor> AllColors;
        public List<GameCell> GameBoard;
        public List<GameColor> CurrentRoundColors;

        private GameCell SelectedGameCell;
        private GameColor LastRoundChosenColor;

        public void Player1MakeMove(int SelectedGameBoardItemID)
        {
            SelectedGameCell = GameBoard.FirstOrDefault(x => x.CellNumber == SelectedGameBoardItemID);


            GameStatus = GameStatusEnum.Player2Turn;
            InitializeCurrentRoundColors();
            RefreshMainWindow();
        }

        public void Player2MakeMove(int SeletedColorID)
        {
            if (SelectedGameCell == null)
                throw new Exception("Bład logiki gry - nie został wybrany kolor");
            SelectedGameCell.Color = AllColors.FirstOrDefault(x => x.ColorId == SeletedColorID);
            LastRoundChosenColor = SelectedGameCell?.Color;
            SelectedGameCell = null;
            if (CheckForGameEnd())
                GameStatus = GameStatusEnum.GameOver;
            else
                GameStatus = GameStatusEnum.Player1Turn;
            RefreshMainWindow();
        }

        private bool CheckForGameEnd()
        {

            // Nie nie umiem na to napisać Linq xD. i niestety to nie jest jedyna opcja, 
            //ciągów może być kilka. nie wiem jak to ugryźć, by nie zabić CPU. Trza się zastanowić.
            var allIndexesOfGivenColor = GameBoard.FindAll(x => x.Color == LastRoundChosenColor).OrderBy(x=>x.CellNumber)
                .Select(x=>x.CellNumber).ToList();
            //  var lastIndex = allIndexesOfGivenColor.LastOrDefault();

            if (Settings.SeriesLength == 1)
                return true;




              CheckForSequence(allIndexesOfGivenColor);

            return false;
        }

        public bool CheckForSequence(List<int> colorIndexes)
        {

            if ((colorIndexes.Count >= 2) && (colorIndexes.Count >= Settings.SeriesLength))
            {
                var firstIndex = colorIndexes.FirstOrDefault();
                var secondIndex = colorIndexes[1];

                var difference = Math.Abs(secondIndex - firstIndex);

                if (difference == 0) return false;
                var counter = 1;
                int j = firstIndex;
                for (var i = 0; i < colorIndexes.Count; i++)
                {
                    if(colorIndexes[i]!=j)
                        break;
                    j = j + difference;
                    counter++;
                    if (counter == Settings.SeriesLength)
                        return true;
                }
                colorIndexes.RemoveRange(0,1);
                CheckForSequence(colorIndexes);
            }
            return false;
        }





        public void InitializeGameBoard()
        {
            GameBoard = new List<GameCell>();
            for (int i = 0; i < Settings.BoardLength; i++)
            {
                GameBoard.Add(new GameCell() { CellNumber = i, Color = new GameColor() });
            }
        }

        public void InitializeCurrentRoundColors()
        {
            if (CurrentRoundColors == null)
                CurrentRoundColors = new List<GameColor>();
            CurrentRoundColors.Clear();
            CurrentRoundColors = AllColors.OrderBy(x => Rand.Next()).Take(Settings.RoundColors).ToList();
        }

        public void InitializeAllColors()
        {
            AllColors = new List<GameColor>();

            for (int i = 0; i < Settings.AllColors; i++)
            {
                AllColors.Add(new GameColor(i + 1, Color.FromArgb(255, (byte)Rand.Next(255), (byte)Rand.Next(255), (byte)Rand.Next(255))));
            }

        }
    }
}
