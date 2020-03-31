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
    /// Logika interakcji dla klasy NewGame.xaml
    /// </summary>
    public partial class NewGame : Window
    {
        public NewGame()
        {
            InitializeComponent();
        }

        private void OnClick_BackToMainMenu(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            Close();
            mainMenu.ShowDialog();
        }

        private void OnClick_StartGame(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(txtPlayerName.Text, txtPlayerCharacterClass.Text);
            Close();
            mainWindow.ShowDialog();
        }

        public void txtPlayerName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Enter Name")
            {
                tb.Text = string.Empty;
                tb.GotFocus -= txtPlayerName_GotFocus;
            }
        }

        public void txtPlayerCharacterClass_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Enter Class Name")
            {
                tb.Text = string.Empty;
                tb.GotFocus -= txtPlayerCharacterClass_GotFocus;
            }
        }

    }
}
