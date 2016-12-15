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
         

        }


        private void RefreshMainWindow()
        {           
           // PlayerColorPalette.DrawListBox(lst);
            if( Settings1 != null)
            AllColorsPalette.DrawListBox(Settings1.ColorList);
        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow = new Windows.SettingsWindow();
            SettingsWindow.ShowDialog();
            if (SettingsWindow.Settings != null)
                Settings1 = SettingsWindow.Settings;
            RefreshMainWindow();

        }
    }
}
