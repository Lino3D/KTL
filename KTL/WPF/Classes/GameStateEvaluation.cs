using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Classes
{
    public class GameStateEvaluation
    {
        public List<StateParticle> GameStates = new List<StateParticle>();
        public int Iteration;
        public List<GameColor> Colors = new List<GameColor>();
        public int SeriesLength;

        public GameStateEvaluation(List<GameColor> _colors, int _SeriesLength)
        {
            Colors = _colors;
            SeriesLength = _SeriesLength;
        }

        public void UpdateEvaluation(GameColor selectedColor, List<GameCell> GameBoard)
        {
            // Nie nie umiem na to napisać Linq xD. i niestety to nie jest jedyna opcja, 
            //ciągów może być kilka. nie wiem jak to ugryźć, by nie zabić CPU. Trza się zastanowić.
            var allIndexesOfGivenColor = GameBoard.FindAll(x => x.Color == selectedColor).OrderBy(x => x.CellNumber)
                .Select(x => x.CellNumber).ToList();
            //  var lastIndex = allIndexesOfGivenColor.LastOrDefault();

            var retLength = CheckForSequence(allIndexesOfGivenColor, 1);
            var gameState = GameStates.FirstOrDefault(x => x.color == selectedColor);
            if (gameState == null)
            {
                GameStates.Add(new StateParticle() { color = selectedColor, LongestSeriesLength = retLength });
            }
            else
            {
                gameState.LongestSeriesLength = retLength;
            }
        }

        public int Evaluate(GameColor selectedColor, List<GameCell> GameBoard)
        {
            var allIndexesOfGivenColor = GameBoard.FindAll(x => x.Color == selectedColor).OrderBy(x => x.CellNumber)
                .Select(x => x.CellNumber).ToList();

            return CheckForSequence(allIndexesOfGivenColor, 1);
        }


        public int CheckForSequence(List<int> colorIndexes, int LongestSeries)
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
