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
        public List<ColorStruct> ColorList { get; set; }
        public int RoundColors { get; set; }
        public int BoardLength { get; set; }
        public int SeriesLength { get; set; }

        public Settings(int a, int b, int c, int d)
        {
            AllColors = a;
            RoundColors = b;
            BoardLength = c;
            SeriesLength = d;
            ColorList = new List<ColorStruct>();
            DrawColors();
        }
        private void DrawColors()
        {
            Random rand = new Random();
            for (int i = 0; i < AllColors; i++)
                ColorList.Add(new ColorStruct() { color = GenerateRandomColor(rand), Id = i });
        }

        private Color GenerateRandomColor(Random rand)
        {
            return Color.FromRgb((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255));

        }

        public struct ColorStruct
        {
            public Color color { get; set; }
            public int Id { get; set; }
        }
    }
}
