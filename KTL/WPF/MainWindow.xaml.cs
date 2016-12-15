using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Helpers;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var lst = new List<Color>() { Color.FromRgb(0, 111, 111), Color.FromRgb(10, 111, 1), Color.FromRgb(90, 150, 200), Color.FromRgb(0, 111, 111), Color.FromRgb(10, 111, 1), Color.FromRgb(90, 150, 200) };
            PlayerColorPalette.DrawListBox(lst);

        }
    }
}
