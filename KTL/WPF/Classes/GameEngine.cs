using System;
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

        public Settings Settings { get; set; }

        public List<GameColor> AllColors;
        public List<GameCell> GameBoard;
        public List<GameColor> CurrentRoundColors;

        public void Player1MakeMove(int SelectedGameBoardItemID)
        {

        }

        public void Player2MakeMove(int SeletedColorID)
        {

        }


        public void InitializeGameBoard()
        {
            GameBoard = new List<GameCell>();
            for (int i = 0; i < Settings.BoardLength; i++)
            {
                GameBoard.Add(new GameCell() { CellNumber = i, Color = new GameColor() });
            }

            foreach (var item in GameBoard)
            {

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
