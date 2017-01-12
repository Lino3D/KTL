using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Interfaces;
using WPF.MinMax;

namespace WPF.Classes
{
    public class MinMaxPlayerImpl : IPlayer1
    {
        public GameEngine GameEngine;
        private Random Rand = new Random();
        private int selectedCell;
        public MinMaxPlayerImpl(GameEngine engine)
        {
            GameEngine = engine;

        }

        public int LastSelectedCell
        {
            get
            {
                return selectedCell;
            }
        }

        public int SelectBoardCell()
        {
            MinMaxAlgorithm minmax = new MinMaxAlgorithm(GameEngine.Settings.SeriesLength, GameEngine.AllColors);

            //      int result = minmax.RunMinMaxAlfaBetaPruning(GameEngine.GameBoard, 11);
            double bestMove = double.MinValue;
            int bestIndex = -1;
            var PossibleMoves = GameEngine.GameBoard.Where(x => x.Color.ColorId == 0).Select(x => x.CellNumber);
            foreach (var item in PossibleMoves)
            {
                double result1 = minmax.RunMinMaxAlfaBetaPruning(GameEngine.GameBoard,4, item);
                if (result1 > bestMove)
                {
                    bestIndex = item;
                    bestMove = result1;
                }

            }
            //    int result = minmax.RunMinMaxClassic(GameEngine.GameBoard, 7);
            if (bestIndex != -1)
            {
                selectedCell = bestIndex;
                return LastSelectedCell;
            }



            var tmp = GameEngine.EvaluationFunctionPlayer1();
            if (tmp == null || tmp.Count() == 0)
            {
                int randInt = Rand.Next(0, GameEngine.GameBoard.Count);
                while (GameEngine.GameBoard.ElementAt(randInt).Color.ColorId != 0)
                    randInt = Rand.Next(0, GameEngine.GameBoard.Count);
                selectedCell = randInt;
            }
            else
            {
                selectedCell = tmp.FirstOrDefault().GameCell.CellNumber;
            }
            return LastSelectedCell;
        }
    }
}
