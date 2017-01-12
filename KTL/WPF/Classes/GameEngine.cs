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

        private GameStateEvaluation RoundEvaluation;

        public void Player1MakeMove(int SelectedGameBoardItemID)
        {
            SelectedGameCell = GameBoard.FirstOrDefault(x => x.CellNumber == SelectedGameBoardItemID);
                        
            InitializeCurrentRoundColors();
            if (CheckForGameEnd())
                GameStatus = GameStatusEnum.GameOver;
            else
                GameStatus = GameStatusEnum.Player2Turn;
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
            var allIndexesOfGivenColor = GameBoard.FindAll(x => x.Color == LastRoundChosenColor).OrderBy(x => x.CellNumber)
                .Select(x => x.CellNumber).ToList();
            //  var lastIndex = allIndexesOfGivenColor.LastOrDefault();

            if (Settings.SeriesLength == 1)
                return true;

            RoundEvaluation.UpdateEvaluation(LastRoundChosenColor, GameBoard);

            return CheckForSequence(allIndexesOfGivenColor);

        }

        public List<Move> EvaluationFunctionPlayer1()
        {
            var ColorsOnGameBoard = GameBoard.Where(x => x.Color.ColorId != 0).Select(x => x.Color).Distinct();
            if (ColorsOnGameBoard.Count() == 0)
                return null;
            var UncoloredCells = GameBoard.Where(x => x.Color.ColorId == 0);


            List<Move> lst = new List<Move>();
            foreach (var item in UncoloredCells)
            {
                // Sprawdz dla kazdego koloru obecnego na mapie, czy utworzy nowy ciag
                foreach (var color in ColorsOnGameBoard)
                {
                    Move movement = EvaluateMove(item, color);
                    if (movement.IsBetter)
                        lst.Add(movement);
                }
            }

            return lst;
        }

        public void EvaluationFunction()
        {

        }


        private Move EvaluateMove(GameCell Cell, GameColor color)
        {
            var MockGameBoard = new List<GameCell>(GameBoard);
            GameColor PreviousColor = MockGameBoard.First(x => x == Cell).Color;
            MockGameBoard.First(x => x == Cell).Color = color;

            Move movement = new Move();
            movement.Color = color;
            movement.GameCell = Cell;
            movement.SeriesLength = RoundEvaluation.Evaluate(color, MockGameBoard);
            movement.IsBetter = movement.SeriesLength > RoundEvaluation.GameStates.FirstOrDefault(x => x.color == color).LongestSeriesLength;
            GameBoard.First(x => x == Cell).Color = PreviousColor;
            return movement;
        }



        public bool CheckForSequence(List<int> colorIndexes)
        {
            if ((colorIndexes.Count >= 2) && (colorIndexes.Count >= Settings.SeriesLength))
            {
                int secondIndexNumber = 1;
                var firstIndex = colorIndexes.FirstOrDefault();
                while ((colorIndexes.Count() - secondIndexNumber + 1) - (Settings.SeriesLength) >= 0)
                {
                    var secondIndex = colorIndexes[secondIndexNumber];

                    var difference = Math.Abs(secondIndex - firstIndex);

                    if (difference == 0) return false;
                    var counter = 0;
                    int j = firstIndex;
                    for (var i = 0; i < colorIndexes.Count; i++)
                    {
                        if (colorIndexes.FindAll(x => x == j).Count == 0)
                            break;
                        j = j + difference;
                        counter++;
                        if (counter == Settings.SeriesLength)
                            return true;
                    }
                    secondIndexNumber++;
                }
                colorIndexes.RemoveRange(0, 1);
                return CheckForSequence(colorIndexes);
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
            //GameBoard.ElementAt(0).Color = AllColors.ElementAt(0);
            //GameBoard.ElementAt(1).Color = AllColors.ElementAt(1);
            //GameBoard.ElementAt(2).Color = AllColors.ElementAt(1);
            //GameBoard.ElementAt(3).Color = AllColors.ElementAt(0);
            //GameBoard.ElementAt(4).Color = AllColors.ElementAt(1);
            //GameBoard.ElementAt(5).Color = AllColors.ElementAt(1);

            //GameBoard.ElementAt(2).Color = AllColors.ElementAt(0);
            //GameBoard.ElementAt(3).Color = AllColors.ElementAt(1);
            //GameBoard.ElementAt(4).Color = AllColors.ElementAt(1);
            //GameBoard.ElementAt(5).Color = AllColors.ElementAt(0);
            //GameBoard.ElementAt(6).Color = AllColors.ElementAt(1);
            //GameBoard.ElementAt(7).Color = AllColors.ElementAt(1);

        }
        public void InitializeEvaluationFunction()
        {
            RoundEvaluation = new GameStateEvaluation(AllColors, Settings.SeriesLength);
        }
    }
}
