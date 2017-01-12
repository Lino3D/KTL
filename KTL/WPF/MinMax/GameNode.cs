using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Classes;

namespace WPF.MinMax
{
    public class GameNode
    {
        public List<GameCell> GameBoard = new List<GameCell>();
        int allcolors = 3;
        public bool wasExtended = false;
        public int CellToColor;
        int LongestSeries;
        int SeriesLength;

        public List<int> GetColors()
        {
            return GameBoard.Where(x => x.Color.ColorId != 0).Select(x => x.Color.ColorId).Distinct().ToList();
        }

        public GameNode(List<GameCell> _gameBoard, int _seriesLength)
        {
            SeriesLength = _seriesLength;
            foreach (var item in _gameBoard)
            {
                GameBoard.Add(new GameCell(item));
            }
        }

        public GameNode(GameNode oldNode)
        {
            SeriesLength = oldNode.SeriesLength;
            foreach (var item in oldNode.GameBoard)
            {
                GameBoard.Add(new GameCell(item));
            }
            wasExtended = oldNode.wasExtended;
        }


        public double EvaulationFunction()
        {
            var lst = CalculateLongestSeries();
            double bestCoef, almostBestCoef, worstCoef;
            double factorValue = 1000;
            double retValue = 0;
            bestCoef = (double) lst.Where(x => x >= SeriesLength).Count() / (double)allcolors;
            almostBestCoef = (double)lst.Where(x => x == SeriesLength-1).Count() / (double)allcolors;
            worstCoef = (double)lst.Where(x => x == SeriesLength - 2).Count() / (double)allcolors;
            retValue += factorValue * bestCoef;
            retValue += (factorValue * almostBestCoef) / 2;
            retValue += (factorValue * worstCoef) / 3;

            return retValue;

            //var GameStatus = CheckForGameEnd();
            //if (GameStatus == GameEndEnum.Player1Won)
            //    return 10000;
            //else if (GameStatus == GameEndEnum.Player2Won)
            //    return -10000;

            //if (LongestSeries > 2 || GameBoard.Select(x => x.Color.ColorId != 0).Count() > 2)
            //{
            //    string domek = "";
            //}


            return lst.OrderByDescending(x=>x).FirstOrDefault();
        }

        private List<int> CalculateLongestSeries()
        {
            List<int> longestArr = new List<int>();
            int longestTemporary = 1;
            LongestSeries = 1;
            foreach (var item in GetColors())
            {
                var lst = GameBoard.FindAll(x => x.Color.ColorId == item).OrderBy(x => x.CellNumber)
                .Select(x => x.CellNumber).ToList();

                longestArr.Add( CheckForSequence(lst, 1));
                
            }
            return longestArr;
        }

        private GameEndEnum CheckForGameEnd()
        {
            if (LongestSeries >= SeriesLength)
                return GameEndEnum.Player1Won;
            else if (GameBoard.FirstOrDefault(x => x.Color.ColorId == 0) == null)
                return GameEndEnum.Player2Won;

            return GameEndEnum.NotGameOverYet;
        }

        private int CheckForSequence(List<int> colorIndexes, int LongestSeries)
        {

            if ((colorIndexes.Count >= 2) && (colorIndexes.Count >= SeriesLength))
            {
                int secondIndexNumber = 1;
                var firstIndex = colorIndexes.FirstOrDefault();
                while ((colorIndexes.Count() - secondIndexNumber + 1) - (SeriesLength) >= 0)
                {
                    var secondIndex = colorIndexes[secondIndexNumber];

                    var difference = Math.Abs(secondIndex - firstIndex);

                    if (difference == 0) return -1;
                    var counter = 0;
                    int j = firstIndex;
                    for (var i = 0; i < colorIndexes.Count; i++)
                    {
                        if (colorIndexes.FindAll(x => x == j).Count == 0)
                            break;
                        j = j + difference;
                        counter++;
                        //   if (counter == SeriesLength)
                        //      return true;
                    }
                    if (counter > LongestSeries)
                        LongestSeries = counter;
                    secondIndexNumber++;
                }
                colorIndexes.RemoveRange(0, 1);
                return CheckForSequence(colorIndexes, LongestSeries);
            }
            else if (colorIndexes.Count == 2 && colorIndexes.Count < SeriesLength && LongestSeries == 1)
                return 2;
            else
            {
                return LongestSeries;
            }
        }



    }
}
