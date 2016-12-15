using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF.Classes
{
    public class Settings
    {
        public int AllColors { get; set; }
        public int RoundColors { get; set; }
        public int BoardLength { get; set; }
        public int SeriesLength { get; set; }

        public Settings(int a, int b, int c, int d)
        {
            AllColors = a;
            RoundColors = b;
            BoardLength = c;
            SeriesLength = d;
        }
    }
}
