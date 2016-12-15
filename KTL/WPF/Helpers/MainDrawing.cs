using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF.Helpers
{
    public static class MainDrawing
    {
        private static int RectangleSize = 30;

        public static  void DrawListBox( this ListBox lstBox, List<Color> colors)
        {
            //int id = 0;
            foreach (var item in colors)
            {
                var myRectangle = new Rectangle
                {
                    Height = RectangleSize,
                    Width = RectangleSize,
                    Fill = new SolidColorBrush(item)
                };
                //myRectangle.Stroke = new SolidColorBrush(Color.FromRgb(0, 111, 0));

                lstBox.Items.Add(myRectangle);
                //id++;
            }
        }

  
    }
}
