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
    /// Interaction logic for Winner.xaml
    /// </summary>
    /// 
    public partial class Winner : Window
    {

        public Winner()
        {
            InitializeComponent();

        }
        public void setWinner(string winner)
        {

            Player.Content = "Winner is " + winner + " player";
        }
        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Position = new TimeSpan(0, 0, 1);
            mediaPlayer.Play();
        }
    }
}
