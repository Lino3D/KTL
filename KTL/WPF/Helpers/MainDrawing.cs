using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF.Classes;
using static WPF.Classes.Settings;

namespace WPF.Helpers
{
    public static class MainDrawing
    {
        private static double RectangleSize = 30;

        public static  void DrawListBox( this ListBox lstBox, List<GameColor> colors)
        {
            while(lstBox.Items.Count!=0)
                lstBox.Items.RemoveAt(0);
          

            int id = 0;
            foreach (var item in colors)
            {
                Label MyLabel = new Label();
                MyLabel.Content = item.ColorId.ToString();
                MyLabel.Background = new SolidColorBrush(item.Color);              
                lstBox.Items.Add(MyLabel);
                id++;
            }
        }

        public static void DrawListBox(this ListBox lstBox, List<GameCell> colors)
        {
            while (lstBox.Items.Count != 0)
                lstBox.Items.RemoveAt(0);


            int id = 0;
            foreach (var item in colors)
            {
                Label MyLabel = new Label();
                MyLabel.Focusable = false;
                MyLabel.Height = RectangleSize;
                MyLabel.Width = RectangleSize;
                MyLabel.Content = item.CellNumber.ToString();
                MyLabel.Background = new SolidColorBrush(item.Color.Color);
                lstBox.Items.Add(MyLabel);
                         
                id++;
            }
        }


    }
}
