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
using System.Windows.Shapes;

namespace WPFUI
{
    /// <summary>
    /// Logika interakcji dla klasy MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void OnClick_Quit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnClick_NewGame(object sender, RoutedEventArgs e)
        {
            NewGame newGame = new NewGame();
            Close();
            newGame.ShowDialog();
        }
    }
}
