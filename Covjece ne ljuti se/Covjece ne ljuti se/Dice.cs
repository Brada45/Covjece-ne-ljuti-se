using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Covjece_ne_ljuti_se
{

    
    class Dice
    {

        private Random random;
        private Image image;


        public Dice(Image image)
        {
            this.image = image;
            random = new Random();
        }
        public int RollDice()
        {
            int result = random.Next(1, 7);
            string imageName = $"kocka_{result}.png"; 

            SetImage(imageName);
            return result;
        }

        private void SetImage(string imageName)
        {
            string imagePath = System.IO.Path.Combine("Resources", imageName); 
            image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }
    }
}
