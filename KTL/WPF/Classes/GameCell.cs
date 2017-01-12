using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF.Classes
{
    public class GameCell
    {
        public int CellNumber { get; set; }
        public GameColor Color { get; set; }

        public GameCell()
        {
        }

        public GameCell( int _cellNum, GameColor _color)
        {
            CellNumber = _cellNum;
            Color = _color;
        }

        public GameCell(GameCell gameCell)
        {
            this.CellNumber = gameCell.CellNumber;
            this.Color = new GameColor(gameCell.Color.ColorId, gameCell.Color.Color);
        }

    }
}
