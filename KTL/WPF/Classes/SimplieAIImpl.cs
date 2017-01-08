using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Interfaces;

namespace WPF.Classes
{
    public class SimplieAIImpl : IPlayer1
    {
        public GameEngine GameEngine;
        private Random Rand = new Random();
        public SimplieAIImpl(GameEngine engine)
        {
            GameEngine = engine;

        }
        public int SelectBoardCell()
        {

            var tmp = GameEngine.EvaluationFunctionPlayer1();
            if (tmp == null || tmp.Count() == 0)
            {
                int randInt = Rand.Next(0, GameEngine.GameBoard.Count);
                while (GameEngine.GameBoard.ElementAt(randInt).Color.ColorId != 0)
                    randInt = Rand.Next(0, GameEngine.GameBoard.Count);
                return randInt;
            }
            else
            {
                return tmp.FirstOrDefault().GameCell.CellNumber;
            }
        }
    }
}
