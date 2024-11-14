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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dice dice;

        int numOfPlayers = 2;
        int code=1;

        int move=0;

        bool played ;
        int round = 1;

        List<Image> fields=new List<Image>();
        List<Button> buttonFields=new List<Button>();

        List<Image> finishes= new List<Image>();
        List<Image> houses = new List<Image>();
        List<Button> buttonHouses = new List<Button>();


        List<Image> finishRed=new List<Image> ();
        List<Image> finishGreen=new List<Image> ();
        List<Image> finishYellow=new List<Image> ();
        List<Image> finishBlue=new List<Image> ();

  
        public MainWindow()
        {

            
            InitializeComponent();
            initializeRedPlayer();
            initializeGreenPlayer();
            initializeYellowPlayer();
            initializeBluePlayer();

            dice = new Dice(Kocka);

            fields.Add(Polje1);fields.Add(Polje2);fields.Add(Polje3);fields.Add(Polje4);fields.Add(Polje5);fields.Add(Polje6);fields.Add(Polje7);fields.Add(Polje8);
            fields.Add(Polje9);fields.Add(Polje10);fields.Add(Polje11);fields.Add(Polje12);fields.Add(Polje12);fields.Add(Polje13);fields.Add(Polje14);fields.Add(Polje15);
            fields.Add(Polje16);fields.Add(Polje17);fields.Add(Polje18);fields.Add(Polje19);fields.Add(Polje20);fields.Add(Polje21);fields.Add(Polje22);fields.Add(Polje23);
            fields.Add(Polje24);fields.Add(Polje25);fields.Add(Polje26);fields.Add(Polje27);fields.Add(Polje28);fields.Add(Polje29);fields.Add(Polje30);fields.Add(Polje31);
            fields.Add(Polje32);fields.Add(Polje33);fields.Add (Polje34);fields.Add(Polje35);fields.Add(Polje36);fields.Add(Polje37);fields.Add(Polje38);fields.Add(Polje39);fields.Add(Polje40);

            buttonFields.Add(ButtonPolje1); buttonFields.Add(ButtonPolje2); buttonFields.Add(ButtonPolje3); buttonFields.Add(ButtonPolje4); buttonFields.Add(ButtonPolje5); buttonFields.Add(ButtonPolje6); buttonFields.Add(ButtonPolje7); buttonFields.Add(ButtonPolje8);
            buttonFields.Add(ButtonPolje9); buttonFields.Add(ButtonPolje10); buttonFields.Add(ButtonPolje11); buttonFields.Add(ButtonPolje12); buttonFields.Add(ButtonPolje12); buttonFields.Add(ButtonPolje13); buttonFields.Add(ButtonPolje14); buttonFields.Add(ButtonPolje15);
            buttonFields.Add(ButtonPolje16); buttonFields.Add(ButtonPolje17); buttonFields.Add(ButtonPolje18); buttonFields.Add(ButtonPolje19); buttonFields.Add(ButtonPolje20); buttonFields.Add(ButtonPolje21); buttonFields.Add(ButtonPolje22); buttonFields.Add(ButtonPolje23);
            buttonFields.Add(ButtonPolje24); buttonFields.Add(ButtonPolje25); buttonFields.Add(ButtonPolje26); buttonFields.Add(ButtonPolje27); buttonFields.Add(ButtonPolje28); buttonFields.Add(ButtonPolje29); buttonFields.Add(ButtonPolje30); buttonFields.Add(ButtonPolje31);
            buttonFields.Add(ButtonPolje32); buttonFields.Add(ButtonPolje33); buttonFields.Add(ButtonPolje34); buttonFields.Add(ButtonPolje35); buttonFields.Add(ButtonPolje36); buttonFields.Add(ButtonPolje37); buttonFields.Add(ButtonPolje38); buttonFields.Add(ButtonPolje39); buttonFields.Add(ButtonPolje40);

            finishRed.Add(KrajCrveni1);finishRed.Add(KrajCrveni2);finishRed.Add(KrajCrveni3);finishRed.Add(KrajCrveni4);
            finishes.Add(KrajCrveni1); finishes.Add(KrajCrveni2); finishes.Add(KrajCrveni3); finishes.Add(KrajCrveni4);
            buttonHouses.Add(ButtonKucicaCrvena1);buttonHouses.Add(ButtonKucicaCrvena2);buttonHouses.Add(ButtonKucicaCrvena4);buttonHouses.Add(ButtonKucicaCrvena4);
            houses.Add(KucicaCrvena1);houses.Add(KucicaCrvena2);houses.Add(KucicaCrvena3);houses.Add(KucicaCrvena4);


            finishGreen.Add(KrajZeleni1); finishGreen.Add(KrajZeleni2); finishGreen.Add(KrajZeleni3); finishGreen.Add(KrajZeleni4);
            finishes.Add(KrajZeleni1);finishes.Add(KrajZeleni2);finishes.Add(KrajZeleni3);finishes.Add(KrajZeleni4);
            buttonHouses.Add(ButtonKucicaZelena1); buttonHouses.Add(ButtonKucicaZelena2); buttonHouses.Add(ButtonKucicaZelena4); buttonHouses.Add(ButtonKucicaZelena4);
            houses.Add(KucicaZelena1);houses.Add(KucicaZelena2);houses.Add(KucicaZelena3);houses.Add(KucicaZelena4);


            finishYellow.Add(KrajZuti1); finishYellow.Add(KrajZuti2); finishYellow.Add(KrajZuti3); finishYellow.Add(KrajZuti4);
            finishes.Add(KrajZuti1);finishes.Add(KrajZuti2);finishes.Add(KrajZuti3);finishes.Add(KrajZuti4);
            buttonHouses.Add(ButtonKucicaZuta1); buttonHouses.Add(ButtonKucicaZuta2); buttonHouses.Add(ButtonKucicaZuta4); buttonHouses.Add(ButtonKucicaZuta4);
            houses.Add(KucicaZuta1);houses.Add(KucicaZuta2);houses.Add(KucicaZuta3);houses.Add(KucicaZuta4);

            finishBlue.Add(KrajPlavi1);finishBlue.Add(KrajPlavi2);finishBlue.Add(KrajPlavi3);finishBlue.Add(KrajPlavi4);
            finishes.Add(KrajPlavi1); finishes.Add(KrajPlavi2); finishes.Add(KrajPlavi3); finishes.Add(KrajPlavi4);
            buttonHouses.Add(ButtonKucicaPlava1); buttonHouses.Add(ButtonKucicaPlava2); buttonHouses.Add(ButtonKucicaPlava4); buttonHouses.Add(ButtonKucicaPlava4);
            houses.Add(KucicaPlava1);houses.Add(KucicaPlava2);houses.Add(KucicaPlava3);houses.Add(KucicaPlava4);

            initializePlayerTurn();
            lockField(code);
        }

        public void initializeRedPlayer()
        {
            setImage(KucicaCrvena1, "crveniPijun.png");
            setImage(KucicaCrvena2, "crveniPijun.png");
            setImage(KucicaCrvena3, "crveniPijun.png");
            setImage(KucicaCrvena4, "crveniPijun.png");

        }

        public void initializeGreenPlayer()
        {
            setImage(KucicaZelena1, "zeleniPijun.png");
            setImage(KucicaZelena2, "zeleniPijun.png");
            setImage(KucicaZelena3, "zeleniPijun.png");
            setImage(KucicaZelena4, "zeleniPijun.png");

        }

        public void initializeBluePlayer()
        {
            setImage(KucicaPlava1, "plaviPijun.png");
            setImage(KucicaPlava2, "plaviPijun.png");
            setImage(KucicaPlava3, "plaviPijun.png");
            setImage(KucicaPlava4, "plaviPijun.png");
        }

        public void initializeYellowPlayer()
        {
            setImage(KucicaZuta1, "zutiPijun.png");
            setImage(KucicaZuta2, "zutiPijun.png");
            setImage(KucicaZuta3, "zutiPijun.png");
            setImage(KucicaZuta4, "zutiPijun.png");

        }

        public void initializePlayerTurn()
        {
            if (code == 1)
            {
                Player.Content = "Red";
            }else if (code == 2)
            {
                Player.Content = "Green";
            }else if(code == 3)
            {
                Player.Content = "Yellow";
            }else if (code == 4)
            {
                Player.Content = "Blue";
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
        private void dice_clicked(object sender, MouseButtonEventArgs e)
        {
            if (played == true || round==1)
                move = dice.RollDice();
            played=false;
        }

        public int getCode(Image im)
        {
            if (im.Source is BitmapImage bitmapImage)
            {
                string imagePath=getPath(im);

                if (AreImagesIdentical(imagePath, "Resources/crveniPijun.png"))
                {
                    return 1;
                } else if (AreImagesIdentical(imagePath, "Resources/zeleniPijun.png"))
                {
                    return 2;
                } else if (AreImagesIdentical(imagePath, "Resources/zutiPijun.png"))
                {
                    return 3;
                } else if (AreImagesIdentical(imagePath, "Resources/plaviPijun.png"))
                {
                    return 4;
                }
            }
            return 0;
        }

        public bool eat(Image im)
        {
            int tmp = getCode(im);
            if (tmp == 1 && code!=1)
            {
                if (KucicaCrvena1.Source == null)
                {
                    setImage(KucicaCrvena1, "crveniPijun.png");
                }
                else if (KucicaCrvena2.Source == null)
                {
                    setImage(KucicaCrvena2, "crveniPijun.png");
                }
                else if (KucicaCrvena3.Source == null)
                {
                    setImage(KucicaCrvena3, "crveniPijun.png");
                }
                else if (KucicaCrvena4.Source == null)
                {
                    setImage(KucicaCrvena4, "crveniPijun.png");
                }
                return true;
            }
            else if (tmp == 2 && code!=2)
            {
                if (KucicaZelena1.Source == null)
                {
                    setImage(KucicaZelena1, "zeleniPijun.png");
                }
                else if (KucicaZelena2.Source == null)
                {
                    setImage(KucicaZelena2, "zeleniPijun.png");
                }
                else if (KucicaZelena3.Source == null)
                {
                    setImage(KucicaZelena3, "zeleniPijun.png");
                }
                else if (KucicaZelena4.Source == null)
                {
                    setImage(KucicaZelena4, "zeleniPijun.png");
                }
                return true;
            }else if(tmp==3 && code!=3)
            {
                if (KucicaZuta1.Source == null)
                {
                    setImage(KucicaZuta1, "zutiPijun.png");
                }
                else if (KucicaZuta2.Source == null)
                {
                    setImage(KucicaZuta2, "zutiPijun.png");
                }
                else if (KucicaZuta3.Source == null)
                {
                    setImage(KucicaZuta3, "zutaPijun.png");
                }
                else if (KucicaZelena4.Source == null)
                {
                    setImage(KucicaZuta4, "zutaPijun.png");
                }
                return true;
            } else if(tmp==4 && code!=4)
            {
                if (KucicaPlava1.Source == null)
                {
                    setImage(KucicaPlava1, "plaviPijun.png");
                }
                else if (KucicaPlava2.Source == null)
                {
                    setImage(KucicaPlava2, "plaviPijun.png");
                }
                else if (KucicaPlava3.Source == null)
                {
                    setImage(KucicaPlava3, "plaviPijun.png");
                }
                else if (KucicaPlava4.Source == null)
                {
                    setImage(KucicaPlava4, "plaviPijun.png");
                }
                return true;
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

        public void lockField(int tmpCode)
        {
            if (tmpCode == 1)
            {
                foreach (Image tmp in houses)
                {
                    Button parent=tmp.Parent as Button;
                    if (tmp.Source != null && tmp.Name.Contains("Crvena")) 
                        parent.IsEnabled = true;
                    else
                        parent.IsEnabled = false;
                }
                foreach (Image tmp in finishes)
                {
                    Button parent = tmp.Parent as Button;
                    if (tmp.Source != null && tmp.Name.Contains("Crveni"))
                        parent.IsEnabled = true;
                    else
                        parent.IsEnabled = false;
                }
                foreach (Image tmp in fields)
                {
                    Button parent = tmp.Parent as Button;
                    if (checkIdenticalImages(tmp, "Resources/crveniPijun.png"))
                        parent.IsEnabled = true;
                    else
                        parent.IsEnabled=false;
                }
            }else if (tmpCode == 2)
            {
                foreach (Image tmp in houses)
                {
                    Button parent = tmp.Parent as Button;
                    if (tmp.Source != null && tmp.Name.Contains("Zelena"))
                        parent.IsEnabled = true;
                    else
                        parent.IsEnabled = false;
                }
                foreach (Image tmp in finishes)
                {
                    Button parent = tmp.Parent as Button;
                    if (tmp.Source != null && tmp.Name.Contains("Zeleni"))
                        parent.IsEnabled = true;
                    else
                        parent.IsEnabled = false;
                }
                foreach (Image tmp in fields)
                {
                    Button parent = tmp.Parent as Button;
                    if (checkIdenticalImages(tmp, "Resources/zeleniPijun.png"))
                        parent.IsEnabled = true;
                    else
                        parent.IsEnabled = false;
                }
            }else if(tmpCode == 3)
            {
                foreach (Image tmp in houses)
                {
                    Button parent = tmp.Parent as Button;
                    if (tmp.Source != null && tmp.Name.Contains("Zuta"))
                        parent.IsEnabled = true;
                    else
                        parent.IsEnabled = false;
                }
                foreach (Image tmp in finishes)
                {
                    Button parent = tmp.Parent as Button;
                    if (tmp.Source != null && tmp.Name.Contains("Zuti"))
                        parent.IsEnabled = true;
                    else
                        parent.IsEnabled = false;
                }
                foreach (Image tmp in fields)
                {
                    Button parent = tmp.Parent as Button;
                    if (checkIdenticalImages(tmp, "Resources/zutiPijun.png"))
                        parent.IsEnabled = true;
                    else
                        parent.IsEnabled = false;
                }
            }else if(tmpCode == 4)
            {
                foreach (Image tmp in houses)
                {
                    Button parent = tmp.Parent as Button;
                    if (tmp.Source != null && tmp.Name.Contains("Plava"))
                        parent.IsEnabled = true;
                    else
                        parent.IsEnabled = false;
                }
                foreach (Image tmp in finishes)
                {
                    Button parent = tmp.Parent as Button;
                    if (tmp.Source != null && tmp.Name.Contains("Plavi"))
                        parent.IsEnabled = true;
                    else
                        parent.IsEnabled = false;
                }
                foreach (Image tmp in fields)
                {
                    Button parent = tmp.Parent as Button;
                    if (checkIdenticalImages(tmp, "Resources/plavaPijun.png"))
                        parent.IsEnabled = true;
                    else
                        parent.IsEnabled = false;
                }
            }
        }

        private void clicked_redHouseOne(object sender, RoutedEventArgs e)
        {
            if (KucicaCrvena1.Source != null && move == 6)
            {
                if (Polje1.Source == null)
                {
                    setImage(Polje1, "crveniPijun.png");
                    KucicaCrvena1.Source = null;
                    lockField(code);
                }
                else
                {
                    
                   bool tmp=eat(Polje1);
                   executeEat(tmp, KucicaCrvena1, Polje1, "crveniPijun.png");
                    lockField(code);

                }
            }
            else if(checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_redHouseTwo(object sender, RoutedEventArgs e)
        {
            if (KucicaCrvena2.Source != null && move == 6)
            {
                if (Polje1.Source == null)
                {
                    setImage(Polje1, "crveniPijun.png");
                    KucicaCrvena2.Source = null;
                    lockField(code);

                }
                else
                {

                    bool tmp = eat(Polje1);
                    executeEat(tmp, KucicaCrvena2, Polje1, "crveniPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_redHouseThree(object sender, RoutedEventArgs e)
        {
            if (KucicaCrvena3.Source != null && move == 6)
            {
                if (Polje1.Source == null)
                {
                    setImage(Polje1, "crveniPijun.png");
                    KucicaCrvena3.Source = null;
                    lockField(code);

                }
                else
                {

                    bool tmp = eat(Polje1);
                    executeEat(tmp, KucicaCrvena3, Polje1, "crveniPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_redHouseFour(object sender, RoutedEventArgs e)
        {
            if (KucicaCrvena4.Source != null && move == 6)
            {
                if (Polje1.Source == null)
                {
                    setImage(Polje1, "crveniPijun.png");
                    KucicaCrvena4.Source = null;
                    lockField(code);

                }
                else
                {

                    bool tmp = eat(Polje1);
                    executeEat(tmp, KucicaCrvena4, Polje1, "crveniPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_greenHouseOne(object sender, RoutedEventArgs e)
        {
            if (KucicaZelena1.Source != null && move == 6)
            {
                if (Polje11.Source == null)
                {
                    setImage(Polje11, "zeleniPijun.png");
                    KucicaZelena1.Source = null;
                    lockField(code);

                }
                else
                {

                    bool tmp = eat(Polje11);
                    executeEat(tmp, KucicaZelena1, Polje11, "zeleniPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_greenHouseTwo(object sender, RoutedEventArgs e)
        {
            if (KucicaZelena2.Source != null && move == 6)
            {
                if (Polje11.Source == null)
                {
                    setImage(Polje11, "zeleniPijun.png");
                    KucicaZelena2.Source = null;
                    lockField(code);

                }
                else
                {
                    bool tmp = eat(Polje11);
                    executeEat(tmp, KucicaZelena2, Polje11, "zeleniPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

       
        

        private void clicked_greenHouseThree(object sender, RoutedEventArgs e)
        {
            if (KucicaZelena3.Source != null && move == 6)
            {
                if (Polje11.Source == null)
                {
                    setImage(Polje11, "zeleniPijun.png");
                    KucicaZelena3.Source = null;
                    lockField(code);

                }
                else
                {

                    bool tmp = eat(Polje11);
                    executeEat(tmp, KucicaZelena3, Polje11, "zeleniPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_greenHouseFour(object sender, RoutedEventArgs e)
        {
            if (KucicaZelena4.Source != null && move == 6)
            {
                if (Polje11.Source == null)
                {
                    setImage(Polje11, "zeleniPijun.png");
                    KucicaZelena4.Source = null;
                    lockField(code);

                }
                else
                {

                    bool tmp = eat(Polje11);
                    executeEat(tmp, KucicaZelena4, Polje11, "zeleniPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_blueHouseOne(object sender, RoutedEventArgs e)
        {
            if (KucicaPlava1.Source != null && move == 6)
            {
                if (Polje31.Source == null)
                {
                    setImage(Polje31, "plaviPijun.png");
                    KucicaPlava1.Source = null;
                    lockField(code);

                }
                else
                {
                    bool tmp = eat(Polje31);
                    executeEat(tmp, KucicaPlava1, Polje31, "plaviPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_blueHouseTwo(object sender, RoutedEventArgs e)
        {
            if (KucicaPlava2.Source != null && move == 6)
            {
                if (Polje31.Source == null)
                {
                    setImage(Polje31, "plaviPijun.png");
                    KucicaPlava2.Source = null;
                    lockField(code);

                }
                else
                {
                    bool tmp = eat(Polje31);
                    executeEat(tmp, KucicaPlava2, Polje31, "plaviPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_blueHouseThree(object sender, RoutedEventArgs e)
        {
            if (KucicaPlava3.Source != null && move == 6)
            {
                if (Polje31.Source == null)
                {
                    setImage(Polje31, "plaviPijun.png");
                    KucicaPlava3.Source = null;
                    lockField(code);

                }
                else
                {
                    bool tmp = eat(Polje31);
                    executeEat(tmp, KucicaPlava3, Polje31, "plaviPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_blueHouseFour(object sender, RoutedEventArgs e)
        {
            if (KucicaPlava4.Source != null && move == 6)
            {
                if (Polje31.Source == null)
                {
                    setImage(Polje31, "plaviPijun.png");
                    KucicaPlava4.Source = null;
                    lockField(code);

                }
                else
                {
                    bool tmp = eat(Polje31);
                    executeEat(tmp, KucicaPlava4, Polje31, "plaviPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_yellowHouseOne(object sender, RoutedEventArgs e)
        {
            if (KucicaZuta1.Source != null && move == 6)
            {
                if (Polje21.Source == null)
                {
                    setImage(Polje21, "zutiPijun.png");
                    KucicaZuta1.Source = null;
                    lockField(code);

                }
                else
                {
                    bool tmp = eat(Polje21);
                    executeEat(tmp, KucicaZuta1, Polje21, "zutiPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_yellowHouseTwo(object sender, RoutedEventArgs e)
        {
            if (KucicaZuta2.Source != null && move == 6)
            {
                if (Polje21.Source == null)
                {
                    setImage(Polje21, "zutiPijun.png");
                    KucicaZuta2.Source = null;
                    lockField(code);

                }
                else
                {
                    bool tmp = eat(Polje21);
                    executeEat(tmp, KucicaZuta2, Polje21, "zutiPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_yellowHouseThree(object sender, RoutedEventArgs e)
        {
            if (KucicaZuta3.Source != null && move == 6)
            {
                if (Polje21.Source == null)
                {
                    setImage(Polje21, "zutiPijun.png");
                    KucicaZuta3.Source = null;
                    lockField(code);

                }
                else
                {
                    bool tmp = eat(Polje21);
                    executeEat(tmp, KucicaZuta3, Polje21, "zutiPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }

        private void clicked_yellowHouseFour(object sender, RoutedEventArgs e)
        {
            if (KucicaZuta4.Source != null && move == 6)
            {
                if (Polje21.Source == null)
                {
                    setImage(Polje21, "zutiPijun.png");
                    KucicaZuta4.Source = null;
                    lockField(code);

                }
                else
                {
                    bool tmp = eat(Polje21);
                    executeEat(tmp, KucicaZuta4, Polje21, "zutiPijun.png");
                    lockField(code);

                }
            }
            else if (checkPossibleMove(move) || checkHouseOptions(move))
            {
                MessageBox.Show("Invalid move. You have other options");
                return;
            }
            else
            {
                findNextPlayer();
            }

            played = true;
        }
       

        public Image findField(string name)
        {
            foreach(Image im in fields)
            {
                if (im.Name.Equals(name))
                {
                    return im;
                }
            }
            return null;
        }

        public bool goRedFinish(int number,bool check=false)
        {
            if (number == 1)
            {
                if (KrajCrveni1.Source == null)
                {   
                    if(check==false)
                        setImage(KrajCrveni1, "crveniPijun.png");
                    return true;
                }
            }
            else if (number == 2 && KrajCrveni1.Source == null)
            {
                if(KrajCrveni2.Source == null)
                {
                    if(check==false)
                        setImage(KrajCrveni2, "crveniPijun.png");
                    return true;
                }
            }else if(number == 3 && KrajCrveni1.Source == null && KrajCrveni2.Source == null)
            {
                if(KrajCrveni3.Source == null)
                {
                    if (check == false)
                        setImage(KrajCrveni3, "crveniPijun.png");
                    return true;

                }
            }
            else if(number == 4 && KrajCrveni1.Source==null && KrajCrveni2.Source==null && KrajCrveni3.Source == null)
            {
                if(KrajCrveni4.Source == null)
                {
                    if (check == false)
                        setImage(KrajCrveni4, "crveniPijun.png");
                    return true;

                }
            }
            else if(number == 5 || number==6)
            {
                return false;
            }
            return false;
        }

        public bool goGreenFinish(int number,bool check = false)
        {
            if (number == 1)
            {
                if (KrajZeleni1.Source == null)
                {
                    if (check == false)
                        setImage(KrajZeleni1, "zeleniPijun.png");
                    return true;
                }
            }
            else if (number == 2 && KrajZeleni1.Source == null)
            {
                if (KrajZeleni2.Source == null)
                {
                    if (check == false)
                        setImage(KrajZeleni2, "zeleniPijun.png");
                    return true;
                }
            }
            else if (number == 3 && KrajZeleni1.Source == null && KrajZeleni2.Source == null)
            {
                if (KrajZeleni3.Source == null)
                {
                    if(check == false)
                        setImage(KrajZeleni3, "zeleniPijun.png");
                    return true;

                }
            }
            else if (number == 4 && KrajZeleni1.Source == null && KrajZeleni2.Source == null && KrajZeleni3.Source == null)
            {
                if (KrajZeleni4.Source == null)
                {
                    if(check==false)
                        setImage(KrajZeleni4, "zeleniPijun.png");
                    return true;

                }
            }
            else if (number == 5 || number == 6)
            {
                return false;
            }
            return false;
        }
        public bool goYellowFinish(int number,bool check=false)
        {
            if (number == 1)
            {
                if (KrajZuti1.Source == null)
                {
                    if(check==false)
                        setImage(KrajZuti1, "zutiPijun.png");
                    return true;
                }
            }
            else if (number == 2 && KrajZuti1.Source == null)
            {
                if (KrajZuti2.Source == null)
                {
                    if (check == false)
                        setImage(KrajZuti2, "zutiPijun.png");
                    return true;
                }
            }
            else if (number == 3 && KrajZuti1.Source == null && KrajZuti2.Source == null)
            {
                if (KrajZuti3.Source == null)
                {
                    if (check == false)
                        setImage(KrajZuti3, "zutiPijun.png");
                    return true;

                }
            }
            else if (number == 4 && KrajZuti1.Source == null && KrajZuti2.Source == null && KrajZuti3.Source == null)
            {
                if (KrajZuti4.Source == null)
                {
                    if (check == false)
                        setImage(KrajZuti4, "zutiPijun.png");
                    return true;

                }
            }
            else if (number == 5 || number == 6)
            {
                return false;
            }
            return false;
        }

        public bool goBlueFinish(int number, bool check = false)
        {
            if (number == 1)
            {
                if (KrajPlavi1.Source == null)
                {
                    if (check == false)
                        setImage(KrajPlavi1, "plaviPijun.png");
                    return true;
                }
            }
            else if (number == 2 && KrajPlavi1.Source == null)
            {
                if (KrajPlavi2.Source == null)
                {
                    if (check == false)
                        setImage(KrajPlavi2, "plaviPijun.png");
                    return true;
                }
            }
            else if (number == 3 && KrajPlavi1.Source == null && KrajPlavi2.Source == null)
            {
                if (KrajPlavi3.Source == null)
                {
                    if (check == false)
                        setImage(KrajPlavi3, "plaviPijun.png");
                    return true;
                }
            }
            else if (number == 4 && KrajPlavi1.Source == null && KrajPlavi2.Source == null && KrajPlavi3.Source == null)
            {
                if (KrajPlavi4.Source == null)
                {
                    if (check == false)
                        setImage(KrajPlavi4, "plaviPijun.png");
                    return true;
                }
            }
            else if (number == 5 || number == 6)
            {
                return false;
            }
            return false;
        }



        public bool go(int currentField,int movement,Image im)
        {
            int tmp = currentField + movement;
            int currentCode=getCode(im);
            bool check=false;
            if(tmp>40)
            {
                tmp = tmp - 40;
            }
            if (currentCode == 1 && tmp >= 1 && tmp <= 6 && (im.Name.Contains("35") || im.Name.Contains("36") || im.Name.Contains("37") || im.Name.Contains("38") || im.Name.Contains("39") || im.Name.Contains("40")))
            {
                check = goRedFinish(tmp);
                if (check == true)
                    im.Source = null;
                if (check == false)
                {
                    check = true;
                }
                return true;
            }
            else if (currentCode == 2 && tmp >= 11 && tmp <= 16 && (im.Name.Contains("10") || im.Name.Contains("9") || im.Name.Contains("8") || im.Name.Contains("7") || im.Name.Contains("6") || im.Name.Contains("5")))
            {
                int moveTmp = tmp - 10;
                check = goGreenFinish(moveTmp);
                if (check == true)
                    im.Source = null;
                if (check == false)
                {
                    check = true;
                }
                return true;
            }else if(currentCode == 3 && tmp >= 21 && tmp<=26 && (im.Name.Contains("20") || im.Name.Contains("19") || im.Name.Contains("18") || im.Name.Contains("17") || im.Name.Contains("16") || im.Name.Contains("15")))
            {
                int moveTmp = tmp - 20;
                check = goYellowFinish(moveTmp);
                if(check == true)
                {
                    im.Source = null;
                }
                if (check == false)
                {
                    check=true; 
                }
                return true;
            }else if(currentCode==4 && tmp >= 31 && tmp<=36 && (im.Name.Contains("30") || im.Name.Contains("29") || im.Name.Contains("28") || im.Name.Contains("27") || im.Name.Contains("26") || im.Name.Contains("25")))
            {
                int moveTmp = tmp - 30;
                check=goBlueFinish(moveTmp);
                if (check == true)
                {
                    im.Source = null;
                }
                if(check == false)
                {
                    check = true;
                }
                return true;
            }
            if (check == false)
            {
                Image field = findField($"Polje{tmp}");
                if (field != null)
                {
                    if (field.Source == null)
                    {
                        field.Source = im.Source;
                        im.Source = null;
                    }
                    else
                    {
                        bool flag=eat(field);
                        if (flag)
                        {
                            field.Source = im.Source;
                            im.Source = null;
                        }else
                        {
                            MessageBox.Show("You can't eat your own figure");
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public void findNextPlayer()
        {
            if (numOfPlayers == 2)
            {
                if (code == 1)
                {
                    code = 3;
                    Player.Content = "Yellow";
                }
                else if (code == 3)
                {
                    code = 1;
                    Player.Content = "Red";

                    round++;
                }
                else if (code == 2)
                {
                    code = 4;
                    Player.Content = "Blue";
                }
                else if (code == 4)
                {
                    code = 2;
                    Player.Content = "Green";

                    round++;
                }
            }
            else if (numOfPlayers == 3)
            {
                if (code == 1)
                {
                    code = 2;
                    Player.Content = "Green";
                }
                else if (code == 2)
                {
                    code = 3;
                    Player.Content = "Yellow";
                }
                else if (code == 3)
                {
                    code = 1;
                    Player.Content = "Red";

                    round++;
                }
            }
            else if (numOfPlayers == 4)
            {
                if (code == 1)
                {
                    code = 2;
                    Player.Content = "Green";
                }
                else if (code == 2)
                {
                    code = 3;
                    Player.Content = "Yelow";
                }
                else if (code == 3)
                {
                    code = 4;
                    Player.Content = "Blue";
                }
                else if (code == 4)
                {
                    code = 1;
                    Player.Content = "Red";

                    round++;
                }
            }
            lockField(code);
            RundaDesni.Content = round;
            RundaLijevi.Content = round;
        }

        public void finishMove(bool flag,int movement)
        {
            if (move == 6)
            {
                played = true;
                lockField(code);
                move = 0;
                return;
            }
            else if (flag == false)
            {
                if (checkHouseOptions(movement) && checkPossibleMove(movement))
                {
                    MessageBox.Show("Invalid move. There are other options");
                    played = true;

                    return;
                }
                MessageBox.Show("You don't have any possible move.");
                played = true;
                move = 0;
                findNextPlayer();
            }
            else if (flag == true && move!=6)
            {
                findNextPlayer();
                played = true;
                move = 0;
            }
        }
        private void clicked_FieldOne(object sender, RoutedEventArgs e)
        {
           bool flag=go(1, move, Polje1);
           finishMove(flag, move);
        }

        private void clicked_FieldTwo(object sender, RoutedEventArgs e)
        {
            bool flag = go(2, move, Polje2);
            finishMove(flag, move);
        }

        private void clicked_FieldThree(object sender, RoutedEventArgs e)
        {
            bool flag = go(3, move, Polje3);
            finishMove(flag, move);
        }

        private void clicked_FieldFour(object sender, RoutedEventArgs e)
        {
            bool flag = go(4, move, Polje4);
            finishMove(flag, move);
        }

        private void clicked_FieldFive(object sender, RoutedEventArgs e)
        {
            bool flag = go(5, move, Polje5);
            finishMove(flag, move);
        }

        private void clicked_FieldSix(object sender, RoutedEventArgs e)
        {
            bool flag = go(6, move, Polje6);
            finishMove(flag, move);
        }

        private void clicked_FieldSeven(object sender, RoutedEventArgs e)
        {
            bool flag = go(7, move, Polje7);
            finishMove(flag, move);
        }

        private void clicked_FieldEight(object sender, RoutedEventArgs e)
        {
            bool flag = go(8, move, Polje8);
            finishMove(flag, move);
        }

        private void clicked_FieldNine(object sender, RoutedEventArgs e)
        {
            bool flag = go(9, move, Polje9);
            finishMove(flag, move);
        }

        private void clicked_FieldTen(object sender, RoutedEventArgs e)
        {
            bool flag = go(10, move, Polje10);
            finishMove(flag, move);
        }

        private void clicked_FieldEleven(object sender, RoutedEventArgs e)
        {
            bool flag = go(11, move, Polje11);
            finishMove(flag, move);
        }

        private void clicked_FieldTwelve(object sender, RoutedEventArgs e)
        {
            bool flag = go(12, move, Polje12);
            finishMove(flag, move);
        }

        private void clicked_FieldThirteen(object sender, RoutedEventArgs e)
        {
            bool flag = go(13, move, Polje13);
            finishMove(flag, move);
        }

        private void clicked_FieldFourteen(object sender, RoutedEventArgs e)
        {
            bool flag = go(14, move, Polje14);
            finishMove(flag, move);
        }

        private void clicked_FieldFifteen(object sender, RoutedEventArgs e)
        {
            bool flag = go(15, move, Polje15);
            finishMove(flag, move);
        }

        private void clicked_FieldSixteen(object sender, RoutedEventArgs e)
        {
            bool flag = go(16, move, Polje16);
            finishMove(flag, move);
        }

        private void clicked_FieldSeventeen(object sender, RoutedEventArgs e)
        {
            bool flag = go(17, move, Polje17);
            finishMove(flag, move);
        }

        private void clicked_FieldEighteen(object sender, RoutedEventArgs e)
        {
            bool flag = go(18, move, Polje18);
            finishMove(flag, move);
        }

        private void clicked_FieldNineteen(object sender, RoutedEventArgs e)
        {
            bool flag = go(19, move, Polje19);
            finishMove(flag, move);
        }

        private void clicked_FieldTwenty(object sender, RoutedEventArgs e)
        {
            bool flag = go(20, move, Polje20);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentyone(object sender, RoutedEventArgs e)
        {
            bool flag = go(21, move, Polje21);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentytwo(object sender, RoutedEventArgs e)
        {
            bool flag = go(22, move, Polje22);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentythree(object sender, RoutedEventArgs e)
        {
            bool flag = go(23, move, Polje23);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentyfour(object sender, RoutedEventArgs e)
        {
            bool flag = go(24, move, Polje24);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentyfive(object sender, RoutedEventArgs e)
        {
            bool flag = go(25, move, Polje25);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentysix(object sender, RoutedEventArgs e)
        {
            bool flag = go(26, move, Polje26);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentyseven(object sender, RoutedEventArgs e)
        {
            bool flag = go(27, move, Polje27);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentyeight(object sender, RoutedEventArgs e)
        {
            bool flag = go(28, move, Polje28);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentynine(object sender, RoutedEventArgs e)
        {
            bool flag = go(29, move, Polje29);
            finishMove(flag, move);
        }

        private void clicked_FieldThirty(object sender, RoutedEventArgs e)
        {
            bool flag = go(30, move, Polje30);
            finishMove(flag, move);
        }

        private void clicked_FieldThiryone(object sender, RoutedEventArgs e)
        {
            bool flag = go(31, move, Polje31);
            finishMove(flag, move);
        }

        private void clicked_FieldThirtytwo(object sender, RoutedEventArgs e)
        {
            bool flag = go(32, move, Polje32);
            finishMove(flag, move);
        }

        private void clicked_FieldThirtythree(object sender, RoutedEventArgs e)
        {
            bool flag = go(33, move, Polje33);
            finishMove(flag, move);
        }

        private void clicked_FieldThirtyfour(object sender, RoutedEventArgs e)
        {
            bool flag = go(34, move, Polje34);
            finishMove(flag, move);
        }

        private void clicked_FieldThirtyfive(object sender, RoutedEventArgs e)
        {
            bool flag = go(35, move, Polje35);
            finishMove(flag, move);
        }

        private void clicked_FieldThritysix(object sender, RoutedEventArgs e)
        {
            bool flag = go(36, move, Polje36);
            finishMove(flag, move);
        }

        private void clicked_FIeldThirtyseven(object sender, RoutedEventArgs e)
        {
            bool flag = go(37, move, Polje37);
            finishMove(flag, move);
        }

        private void clicked_FieldThirtyeight(object sender, RoutedEventArgs e)
        {
            bool flag = go(38, move, Polje38);
            finishMove(flag, move);
        }

        private void clicked_FieldThirtynine(object sender, RoutedEventArgs e)
        {
            bool flag = go(39, move, Polje39);
            finishMove(flag, move);
        }

        private void clicked_FieldFourty(object sender, RoutedEventArgs e)
        {
            bool flag = go(40, move, Polje40);
            finishMove(flag, move);
        }

        public bool moveFinish(Image current,int number,List<Image> finish)
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
                            if (image.Source == null && empty==true)
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
                                empty= false;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool checkIdenticalImages(Image image,string path)
        {
                if (image.Source is BitmapImage bitmapImage)
                {
                    // Uzimamo lokalnu putanju od URI
                    string imagePath = getPath(image);
                    if (AreImagesIdentical(imagePath, path))
                    {
                        return true;
                    }

                
            }
            return false;
        }
        public bool checkIdenticalImages(Image image,Image tmp)
        {
                if (tmp.Source is BitmapImage bitmapImage && image.Source is BitmapImage imageImage)
                {
                    // Uzimamo lokalnu putanju od URI
                    string imagePath = getPath(image);
                    string tmpPath = getPath(tmp);
                    if (AreImagesIdentical(imagePath, tmpPath))
                    {
                        return true;
                    }

                }
            return false;
        }

        public string getPath(Image image)
        {
            if (image.Source is BitmapImage bitmapImage )
            {
                // Uzimamo lokalnu putanju od URI
                string imagePath;

                // Proverava da li je URI apsolutan
                if (bitmapImage.UriSource.IsAbsoluteUri)
                {
                    imagePath = bitmapImage.UriSource.LocalPath;
                }
                else
                {
                    // Kombinuje relativni URI sa osnovnom putanjom aplikacije
                    imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, bitmapImage.UriSource.ToString().TrimStart('/'));

                }
                return imagePath;

            }
            return "";
        }
       
       

        public bool checkPossibleMove(int movement)
        {

            bool found = false;
            foreach (Image im in fields)
            {
                int tmpCode = getCode(im);

                int field=0;
                Image tmpImage=null;
                if (code == tmpCode)
                {
                    field= int.Parse(im.Name.Split("e")[1]) + movement;

                    if (field > 40)
                    {
                        field = field - 40;
                    }
                    tmpImage = findField($"Polje{field}");
                }
                else
                {
                    continue;
                }
                if(tmpImage!=null && !checkIdenticalImages(im, tmpImage))
                {
                    found = true;
                }
                if (tmpCode == 1 && field >= 1 && field <= 6 && (tmpImage.Name.Contains("35") || tmpImage.Name.Contains("36") || tmpImage.Name.Contains("37") || tmpImage.Name.Contains("38") || tmpImage.Name.Contains("39") || tmpImage.Name.Contains("40")))
                {
                    if (goRedFinish(field,true))
                        found = true;
                }
                else if (tmpCode == 2 && field >= 11 && field <= 16 && (tmpImage.Name.Contains("10") || tmpImage.Name.Contains("9") || tmpImage.Name.Contains("8") || tmpImage.Name.Contains("7") || tmpImage.Name.Contains("6") || tmpImage.Name.Contains("5")))
                {
                    int moveTmp = field - 10;
                    if (goGreenFinish(moveTmp,true))
                        found = true;
                }
                else if (tmpCode == 3 && field >= 21 && field <= 26 && (tmpImage.Name.Contains("20") || tmpImage.Name.Contains("19") || tmpImage.Name.Contains("18") || tmpImage.Name.Contains("17") || tmpImage.Name.Contains("16") || tmpImage.Name.Contains("15")))
                {
                    int moveTmp = field - 20;
                    if (goYellowFinish(moveTmp,true))
                        found = true;
                }
                else if (tmpCode == 4 && field >= 31 && field <= 36 && (tmpImage.Name.Contains("30") || tmpImage.Name.Contains("29") || tmpImage.Name.Contains("28") || tmpImage.Name.Contains("27") || tmpImage.Name.Contains("26") || tmpImage.Name.Contains("25")))
                {
                    int moveTmp = field - 30;
                    if (goBlueFinish(moveTmp,true))
                        found = true;
                }

            }
            if (found == true)
            {
                return true;
            }else
            {
                return false;
            }

            
        }

        public bool checkHouseOptions( int movement)
        {
            if (code == 1)
            {
                if (movement == 6 && (KucicaCrvena1.Source != null || KucicaCrvena2.Source != null || KucicaCrvena3.Source != null || KucicaCrvena4.Source != null))
                {
                    return true;
                }
            }
            else if (code == 2)
            {
                if (movement == 6 && (KucicaZelena1.Source != null || KucicaZelena2.Source != null || KucicaZelena3.Source != null || KucicaZelena4.Source != null))
                {
                    return true;
                }
            }
            else if (code == 4)
            {
                if (movement == 6 && (KucicaPlava1.Source != null || KucicaPlava2.Source != null || KucicaPlava3.Source != null || KucicaPlava4.Source != null))
                {
                    return true;
                }
            }
            else if (code == 3)
            {
                if (movement == 6 && (KucicaZuta1.Source != null || KucicaZuta2.Source != null || KucicaZuta3.Source != null || KucicaZuta4.Source != null))
                {
                    return true;
                }
            }
            return false;
        }
        private void clicked_redFisnishOne(object sender, RoutedEventArgs e)
        {
            bool flag=moveFinish(KrajCrveni1,move,finishRed);
            finishMove(flag, move);
        }

        private void clicked_redFinishTwo(object sender, RoutedEventArgs e)
        {
            bool flag = moveFinish(KrajCrveni2, move, finishRed);
            finishMove(flag, move);
        }

        private void clicked_redFinishThree(object sender, RoutedEventArgs e)
        {
            bool flag = moveFinish(KrajCrveni3, move, finishRed);
            finishMove(flag, move);
        }

        private void clicked_blueFinishOne(object sender, RoutedEventArgs e)
        {
            bool flag = moveFinish(KrajPlavi1, move, finishBlue);
            finishMove(flag, move);
        }

        private void clicked_blueFinishTwo(object sender, RoutedEventArgs e)
        {
            bool flag = moveFinish(KrajPlavi2, move, finishBlue);
            finishMove(flag, move);
        }

        private void clicked_blueFinishThree(object sender, RoutedEventArgs e)
        {
            bool flag = moveFinish(KrajPlavi3, move, finishBlue);
            finishMove (flag, move);
        }

        private void clicked_yellowFinishOne(object sender, RoutedEventArgs e)
        {
            bool flag = moveFinish(KrajZuti1, move, finishYellow);
            finishMove(flag, move);
        }

        private void clicked_yellowFinishTwo(object sender, RoutedEventArgs e)
        {
            bool flag = moveFinish(KrajZuti2, move, finishYellow);
            finishMove(flag, move);
        }

        private void clicked_yellowFinishThree(object sender, RoutedEventArgs e)
        {
            bool flag = moveFinish(KrajZuti3, move, finishYellow);
            finishMove(flag, move);
        }

        private void clicked_greenFinishOne(object sender, RoutedEventArgs e)
        {
            bool flag = moveFinish(KrajZeleni1, move, finishGreen);
            finishMove(flag, move);
        }

        private void clicked_greenFinishTwo(object sender, RoutedEventArgs e)
        {
            bool flag = moveFinish(KrajZeleni2, move, finishGreen);
            finishMove(flag, move);
        }

        private void clicked_greenFinishThree(object sender, RoutedEventArgs e)
        {
            bool flag = moveFinish(KrajZeleni3, move, finishGreen);
            finishMove(flag, move);
        }
    }
}