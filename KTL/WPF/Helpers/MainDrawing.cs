using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static WPF.Classes.Settings;

namespace WPF.Helpers
{
    public static class MainDrawing
    {
        private static int RectangleSize = 30;

        public static  void DrawListBox( this ListBox lstBox, List<Color> colors)
        {
            int id = 0;
            foreach (var item in colors)
            {
                var myRectangle = new Rectangle();
                Label MyLabel = new Label();

                MyLabel.Content = item.ToString();
                MyLabel.Background = new SolidColorBrush(item); 
                    
                //myWrapPanel.set
                //myRectangle.Height = RectangleSize;
                //myRectangle.Width = RectangleSize;
                //myRectangle.Stroke = new SolidColorBrush(Color.FromRgb(0, 111, 0));
                myRectangle.Fill = new SolidColorBrush(item);                
                lstBox.Items.Add(MyLabel);
                id++;
            }
        }

    }
}
