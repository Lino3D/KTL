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
using WPF.Classes;
using WPF.Helpers;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Windows.SettingsWindow SettingsWindow;
        public Settings Settings1 { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            var lst = new List<Color>() { Color.FromRgb(100, 100, 200), Color.FromRgb(100, 0, 200), Color.FromRgb(100, 100, 0), Color.FromRgb(100, 100, 150), Color.FromRgb(100, 200, 150), Color.FromRgb(100, 100, 150) };
            PlayerColorPalette.DrawListBox(lst);

        }



        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow = new Windows.SettingsWindow();
            SettingsWindow.ShowDialog();
            if (SettingsWindow.Settings != null)
                Settings1 = SettingsWindow.Settings;

        }
    }
}
