using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Windows.Media.Animation;
using System.ComponentModel.Design;
using System.Windows.Automation.Provider;

namespace Covjece_ne_ljuti_se
{
    internal class Service
    {
        List<Image> fields;

        public Service() { }

        public Service(List<Image> fields)
        {
            this.fields= fields;
        }

        public int getCode(Image im)
        {
            if (im.Source is BitmapImage bitmapImage)
            {
                string imagePath = getPath(im);

                if (AreImagesIdentical(imagePath, "Resources/crveniPijun.png"))
                {
                    return 1;
                }
                else if (AreImagesIdentical(imagePath, "Resources/zeleniPijun.png"))
                {
                    return 2;
                }
                else if (AreImagesIdentical(imagePath, "Resources/zutiPijun.png"))
                {
                    return 3;
                }
                else if (AreImagesIdentical(imagePath, "Resources/plaviPijun.png"))
                {
                    return 4;
                }
            }
            return 0;
        }

        public string getPath(Image image)
        {
            if (image.Source is BitmapImage bitmapImage)
            {
                string imagePath;
                if (bitmapImage.UriSource.IsAbsoluteUri)
                {
                    imagePath = bitmapImage.UriSource.LocalPath;
                }
                else
                {
                    imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, bitmapImage.UriSource.ToString().TrimStart('/'));

                }
                return imagePath;

            }
            return "";
        }
        public bool checkIdenticalImages(Image image, Image tmp)
        {
            if (tmp.Source is BitmapImage bitmapImage && image.Source is BitmapImage imageImage)
            {
                string imagePath = getPath(image);
                string tmpPath = getPath(tmp);
                if (AreImagesIdentical(imagePath, tmpPath))
                {
                    return true;
                }

            }
            return false;
        }

        public bool checkIdenticalImages(Image image, string path)
        {
            if (image.Source is BitmapImage bitmapImage)
            {
                string imagePath = getPath(image);
                if (AreImagesIdentical(imagePath, path))
                {
                    return true;
                }


            }
            return false;
        }

        public void executeEat(bool tmp, Image endField, Image startField, string figure)
        {
            if (!tmp)
            {
                MessageBox.Show("You can't eat your own figure");
            }
            else
            {
                setImage(startField, figure);
                endField.Source = null;
            }
        }

        public void setImage(Image image, string name)
        {
            string imagePath = System.IO.Path.Combine("Resources", name);
            image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }
        public bool AreImagesIdentical(string imagePath1, string imagePath2)
        {
            byte[] imageBytes1 = File.ReadAllBytes(imagePath1);
            byte[] imageBytes2 = File.ReadAllBytes(imagePath2);

            return imageBytes1.Length == imageBytes2.Length && !imageBytes1.Where((t, i) => t != imageBytes2[i]).Any();
        }

        public bool moveFinish(Image current, int number, List<Image> finish)
        {
            if (number > 3)
            {
                return false;
            }
            else
            {
                int brojac = 1;
                bool flag = false, empty = true;
                foreach (Image image in finish)
                {
                    if (image.Equals(current))
                    {
                        flag = true;
                    }
                    else if (flag)
                    {
                        if (brojac == number)
                        {
                            if (image.Source == null && empty == true)
                            {
                                image.Source = current.Source;
                                current.Source = null;
                                return true;
                            }
                        }
                        else
                        {
                            brojac++;
                            if (image.Source != null)
                            {
                                empty = false;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public Image findField(string name)
        {
            foreach (Image im in fields)
            {
                if (im.Name.Equals(name))
                {
                    return im;
                }
            }
            return null;
        }

        public bool goFinish(Image firstEnd, Image secondEnd, Image thirdEnd, Image fourthEnd, int number, string image, bool check = false)
        {
            if (number == 1)
            {
                if (firstEnd.Source == null)
                {
                    if (check == false)
                        setImage(firstEnd, image);
                    return true;
                }
            }
            else if (number == 2 && firstEnd.Source == null)
            {
                if (secondEnd.Source == null)
                {
                    if (check == false)
                        setImage(secondEnd, image);
                    return true;
                }
            }
            else if (number == 3 && firstEnd.Source == null && secondEnd.Source == null)
            {
                if (thirdEnd.Source == null)
                {
                    if (check == false)
                        setImage(thirdEnd, image);
                    return true;
                }
            }
            else if (number == 4 && firstEnd.Source == null && secondEnd.Source == null && thirdEnd.Source == null)
            {
                if (fourthEnd.Source == null)
                {
                    if (check == false)
                        setImage(fourthEnd, image);
                    return true;
                }
            }
            else if (number == 5 || number == 6)
            {
                return false;
            }
            return false;
        }
    }
}
