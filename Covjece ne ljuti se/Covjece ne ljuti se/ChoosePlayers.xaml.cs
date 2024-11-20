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

namespace Covjece_ne_ljuti_se
{
    /// <summary>
    /// Interaction logic for ChoosePlayers.xaml
    /// </summary>
    public partial class ChoosePlayers : Window
    {
        public ChoosePlayers()
        {
            InitializeComponent();
        }

        private void clicked_TwoRedYellow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.code = 1;
            mainWindow.numOfPlayers = 2;
            mainWindow.setParameters();
            mainWindow.Show();
            this.Close();

        }

        private void clicked_TwoGreenBlue(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.code = 2;
            mainWindow.numOfPlayers = 2;
            mainWindow.setParameters();
            mainWindow.Show();
            this.Close();
        }

        private void clicked_Three(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.code = 1;
            mainWindow.numOfPlayers = 3;
            mainWindow.setParameters();
            mainWindow.Show();
            this.Close();
        }

        private void clicked_Four(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.code = 1;
            mainWindow.numOfPlayers = 4;
            mainWindow.setParameters();
            mainWindow.Show();
            this.Close();
        }
    }
}
