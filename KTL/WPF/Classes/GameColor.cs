using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF.Classes
{
    public class GameColor
    {
        public int ColorId { get; set; }
        public Color Color { get; set; }

        public GameColor(int id, Color c)
        {
            ColorId = id;
            Color = c;
        }

    }
}
