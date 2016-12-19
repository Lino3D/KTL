﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Interfaces;

namespace WPF.Classes
{
    class BasicAIImpl : IPlayer1
    {
        public GameEngine GameEngine;
        private Random Rand = new Random();
        public BasicAIImpl( GameEngine engine )
        {
            GameEngine = engine;

        }
        public int SelectBoardCell()
        {
            int randInt = Rand.Next(0, GameEngine.GameBoard.Count);
            while( GameEngine.GameBoard.ElementAt(randInt).Color.ColorId !=  0)
                randInt = Rand.Next(0, GameEngine.GameBoard.Count);
            return randInt;
        }
    }
}
