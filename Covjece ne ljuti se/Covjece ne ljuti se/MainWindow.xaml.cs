﻿using System.Text;
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
using System.Media;

namespace Covjece_ne_ljuti_se
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dice dice;

        public int numOfPlayers = 0;
        public int code = 0;

        int move = 0;

        bool played;
        int round = 1;

        List<Image> fields = new List<Image>();
        List<Button> buttonFields = new List<Button>();

        List<Image> finishes = new List<Image>();
        List<Image> houses = new List<Image>();
        List<Button> buttonHouses = new List<Button>();


        List<Image> finishRed = new List<Image>();
        List<Image> finishGreen = new List<Image>();
        List<Image> finishYellow = new List<Image>();
        List<Image> finishBlue = new List<Image>();

        string redImage = "";
        string greenImage = "";
        string yellowImage = "";
        string blueImage = "";
        string sound = "";

        Service service;

        static Timer timer;
        static int seconds = 0;

        Dictionary<string, string> properties = null;

        public MainWindow()
        {

            InitializeComponent();

            SetFields();
            SetFinishesFields("KrajCrveni",finishRed);
            SetFinishesFields("KrajZeleni",finishGreen);
            SetFinishesFields("KrajPlavi",finishBlue);
            SetFinishesFields("KrajZuti",finishYellow);

            SetHouses("Crvena");
            SetHouses("Zelena");
            SetHouses("Plava");
            SetHouses("Zuta");

            string filePath = "Resources/config.properties";
            properties= LoadProperties(filePath);

            sound = properties["dice_throw"];
        }

       
        private void SetFields()
        {
            for (int i = 1; i <= 40; i++)
            {
                string fieldName = $"Polje{i}";
                Image img = (Image)this.FindName(fieldName);
                if (img != null)
                {
                    fields.Add(img);
                }
                fieldName = $"buttonFields{i}";
                Button button = (Button)this.FindName(fieldName);
                if (button != null)
                {
                    buttonFields.Add(button);
                }
            }
        }

        private void SetFinishesFields(String color,List<Image> finish)
        {

            for (int i = 1; i <= 4; i++)
            {
                string fieldName =color+$"{i}";
                Image img = (Image)this.FindName(fieldName);
                if (img != null)
                {
                    finishes.Add(img);
                    finish.Add(img);
                }
            }
        }

        private void SetHouses(String color)
        {

            for (int i = 1; i <= 4; i++)
            {
                string houseName ="Kucica"+color+$"{i}";
                string buttonHouse = "ButtonKucica"+color+$"{i}";
                Image houseImg = (Image)this.FindName(houseName);
                Button button = (Button)this.FindName(buttonHouse);
                if (houseImg != null)
                {
                    houses.Add(houseImg);
                }
                if (button != null)
                {

                    buttonHouses.Add(button);
                }
            }
        }
        static Dictionary<string, string> LoadProperties(string filePath)
        {
            var properties = new Dictionary<string, string>();

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Properties file not found: {filePath}");
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (line.Trim().StartsWith("#") || string.IsNullOrWhiteSpace(line)) continue; 

                var parts = line.Split('=', 2);
                if (parts.Length == 2)
                {
                    properties[parts[0].Trim()] = parts[1].Trim();
                }
            }

            return properties;
        }

        public void setParameters()
        {

            service = new Service(fields);
            dice = new Dice(Kocka);
            initializePlayerTurn();
            seconds = 0;
            timer = new Timer(OnTimedEvent, null, 0, 1000);
            SetImages();
            if (code==1 && numOfPlayers == 2)
            {
                initializeRedPlayer();
                initializeYellowPlayer();
            }else if(code==2 && numOfPlayers == 2)
            {

                initializeGreenPlayer();
                initializeBluePlayer();
            }else if (numOfPlayers == 3)
            {
                initializeRedPlayer();
                initializeYellowPlayer();
                initializeGreenPlayer();
            }else if(numOfPlayers == 4)
            {
                initializeRedPlayer();
                initializeYellowPlayer();
                initializeGreenPlayer();
                initializeBluePlayer();
            }
            service.setPawns(redImage, greenImage, yellowImage, blueImage);
            lockField(code);
        }

        public void SetImages()
        {
            redImage = properties["redPawn"];
            greenImage = properties["greenPawn"];
            blueImage = properties["bluePawn"];
            yellowImage = properties["yellowPawn"];

        }

        public void initializeRedPlayer()
        {
            service.setImage(KucicaCrvena1, redImage);
            service.setImage(KucicaCrvena2, redImage);
            service.setImage(KucicaCrvena3, redImage);
            service.setImage(KucicaCrvena4, redImage);

        }

        public void initializeGreenPlayer()
        {
            
            service.setImage(KucicaZelena1, greenImage);
            service.setImage(KucicaZelena2, greenImage);
            service.setImage(KucicaZelena3, greenImage);
            service.setImage(KucicaZelena4, greenImage);

        }

        public void initializeBluePlayer()
        {
            service.setImage(KucicaPlava1, blueImage);
            service.setImage(KucicaPlava2, blueImage);
            service.setImage(KucicaPlava3, blueImage);
            service.setImage(KucicaPlava4, blueImage);
        }

        public void initializeYellowPlayer()
        {
            service.setImage(KucicaZuta1, yellowImage);
            service.setImage(KucicaZuta2, yellowImage);
            service.setImage(KucicaZuta3, yellowImage);
            service.setImage(KucicaZuta4, yellowImage);

        }

        public void initializePlayerTurn()
        {
            if (code == 1)
            {
                Player.Content = "Red";
            } else if (code == 2)
            {
                Player.Content = "Green";
            } else if (code == 3)
            {
                Player.Content = "Yellow";
            } else if (code == 4)
            {
                Player.Content = "Blue";
            }
        }

        private  void OnTimedEvent(object state)
        {
            seconds++;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            string vrijeme= time.ToString(@"hh\:mm\:ss");
            Dispatcher.Invoke(() =>
            {
                VrijemeDesni.Content = vrijeme;
                VrijemeLijevi.Content = vrijeme;
            });
        }

        public bool checkAllThree()
        {

            bool houseBool = checkHouseOptions(move);
            bool moveBool = checkPossibleMove(move);
            bool endBool = checkEndOptions(move);
            if (!houseBool && !moveBool && !endBool)
                return true;
            else
                return false;
        }
        private async void dice_clicked(object sender, MouseButtonEventArgs e)
        {
            if (played == true || round == 1)
            {
                SoundPlayer player = new SoundPlayer(sound);

                player.Play();
                await Task.Delay(500);
                for (int i = 0; i < 5; i++)
                {
                    move = dice.RollDice();
                    await Task.Delay(130);
                }
                if (Player.Content.ToString().Contains("roll"))
                {
                    Player.Content = Player.Content.ToString().Split(" ")[0];
                }
                Player.Content = Player.Content + " rolled";

            }
            if (checkAllThree() )
            {
                if(move!=6)
                    findNextPlayer();
                else
                    Player.Content = Player.Content.ToString().Split(" ")[0]+" roll";
                played = true;
                move = 0;
            } else
                played = false;
        }



        public bool eat(Image im)
        {
            int tmp = service.getCode(im);
            if (tmp == 1 && code != 1)
            {
                if (KucicaCrvena1.Source == null)
                {
                    service.setImage(KucicaCrvena1, redImage);
                }
                else if (KucicaCrvena2.Source == null)
                {
                    service.setImage(KucicaCrvena2, redImage);
                }
                else if (KucicaCrvena3.Source == null)
                {
                    service.setImage(KucicaCrvena3, redImage);
                }
                else if (KucicaCrvena4.Source == null)
                {
                    service.setImage(KucicaCrvena4, redImage);
                }
                return true;
            }
            else if (tmp == 2 && code != 2)
            {
                if (KucicaZelena1.Source == null)
                {
                    service.setImage(KucicaZelena1, greenImage);
                }
                else if (KucicaZelena2.Source == null)
                {
                    service.setImage(KucicaZelena2, greenImage);
                }
                else if (KucicaZelena3.Source == null)
                {
                    service.setImage(KucicaZelena3, greenImage);
                }
                else if (KucicaZelena4.Source == null)
                {
                    service.setImage(KucicaZelena4, greenImage);
                }
                return true;
            } else if (tmp == 3 && code != 3)
            {
                if (KucicaZuta1.Source == null)
                {
                    service.setImage(KucicaZuta1, yellowImage);
                }
                else if (KucicaZuta2.Source == null)
                {
                    service.setImage(KucicaZuta2, yellowImage);
                }
                else if (KucicaZuta3.Source == null)
                {
                    service.setImage(KucicaZuta3, yellowImage);
                }
                else if (KucicaZuta4.Source == null)
                {
                    service.setImage(KucicaZuta4, yellowImage);
                }
                return true;
            } else if (tmp == 4 && code != 4)
            {
                if (KucicaPlava1.Source == null)
                {
                    service.setImage(KucicaPlava1, blueImage);
                }
                else if (KucicaPlava2.Source == null)
                {
                    service.setImage(KucicaPlava2, blueImage);
                }
                else if (KucicaPlava3.Source == null)
                {
                    service.setImage(KucicaPlava3, blueImage);
                }
                else if (KucicaPlava4.Source == null)
                {
                    service.setImage(KucicaPlava4, blueImage);
                }
                return true;
            }
            return false;
        }

        public void lockExact(string house, string end, string image){
            foreach (Image tmp in houses)
            {
                Button parent = tmp.Parent as Button;
                if (tmp.Source != null && tmp.Name.ToString().Contains(house))
                    parent.IsEnabled = true;
                else
                    parent.IsEnabled = false;
            }
            foreach (Image tmp in finishes)
            {
                Button parent = tmp.Parent as Button;
                if (tmp.Source != null && tmp.Name.ToString().Contains(end))
                    parent.IsEnabled = true;
                else
                    parent.IsEnabled = false;
            }
            foreach (Image tmp in fields)
            {
                Button parent = tmp.Parent as Button;
                if (service.checkIdenticalImages(tmp, image))
                    parent.IsEnabled = true;
                else
                    parent.IsEnabled = false;
            }
        }

        public void lockField(int tmpCode)
        {
            if (tmpCode == 1)
            {
                lockExact("Crvena", "Crveni", redImage);   
            }else if (tmpCode == 2)
            {
                lockExact("Zelena", "Zeleni", greenImage);
            }else if(tmpCode == 3)
            {
                lockExact("Zuta", "Zuti", yellowImage);
            }else if(tmpCode == 4)
            {
                lockExact("Plava", "Plavi", blueImage);
            }
        }


        public void enter(Image houseField,Image startField,string image)
        {
            if (startField.Source == null)
            {
                service.setImage(startField, image);
                houseField.Source = null;
                lockField(code);
                Player.Content = Player.Content.ToString().Split(" ")[0] + " roll";
            }
            else
            {
                bool tmp = eat(startField);
                service.executeEat(tmp, houseField, startField, image);
                lockField(code);
            }
        }
        
       

        public bool go(int currentField,int movement,Image im)
        {
           
            int tmp = currentField + movement;
            int currentCode=service.getCode(im);
            bool check=false;
            if(tmp>40)
            {
                tmp = tmp - 40;
            }
            if (currentCode == 1 && tmp >= 1 && tmp <= 6 && (im.Name.Contains("35") || im.Name.Contains("36") || im.Name.Contains("37") || im.Name.Contains("38") || im.Name.Contains("39") || im.Name.Contains("40")))
            {
                check = service.goFinish(KrajCrveni1,KrajCrveni2,KrajCrveni3,KrajCrveni4,tmp,redImage);
                if (check == true)
                {
                    im.Source = null;
                    return true;
                }
                if (check == false)
                {
                    check = true;
                }
            }
            else if (currentCode == 2 && tmp >= 11 && tmp <= 16 && (im.Name.Contains("10") || im.Name.ToString().Split("e")[1].Equals("9") || im.Name.ToString().Split("e")[1].Equals("8") || im.Name.ToString().Split("e")[1].Equals("7") || im.Name.ToString().Split("e")[1].Equals("6") || im.Name.ToString().Split("e")[1].Equals("5")))
            {
                int moveTmp = tmp - 10;
                check = service.goFinish(KrajZeleni1,KrajZeleni2,KrajZeleni3,KrajZeleni4,moveTmp,greenImage);
                if (check == true)
                {
                    im.Source = null;
                    return true;
                }
                if (check == false)
                {
                    check = true;
                }
            }else if(currentCode == 3 && tmp >= 21 && tmp<=26 && (im.Name.Contains("20") || im.Name.Contains("19") || im.Name.Contains("18") || im.Name.Contains("17") || im.Name.Contains("16") || im.Name.Contains("15")))
            {
                int moveTmp = tmp - 20;
                check = service.goFinish(KrajZuti1,KrajZuti2,KrajZuti3,KrajZuti4,moveTmp,yellowImage);
                if(check == true)
                {
                    im.Source = null;
                    return true;
                }
                if (check == false)
                {
                    check=true; 
                }
            }else if(currentCode==4 && tmp >= 31 && tmp<=36 && (im.Name.Contains("30") || im.Name.Contains("29") || im.Name.Contains("28") || im.Name.Contains("27") || im.Name.Contains("26") || im.Name.Contains("25")))
            {
                int moveTmp = tmp - 30;
                check=service.goFinish(KrajPlavi1,KrajPlavi2,KrajPlavi3,KrajPlavi4,moveTmp,blueImage);
                if (check == true)
                {
                    im.Source = null;
                    return true;
                }
                if(check == false)
                {
                    check = true;
                }
            }
            if (check == false)
            {
                Image field = service.findField($"Polje{tmp}");
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
            Player.Content = Player.Content + " roll";
        }

        public void finishMove(bool flag,int movement)
        {

            checkEndGame();
            if (movement == 6)
            {
                played = true;
                Player.Content = Player.Content.ToString().Split(" ")[0] + " roll";
                lockField(code);
                if (flag == false)
                {
                    if(checkHouseOptions(movement) || checkPossibleMove(movement))
                    {
                        MessageBox.Show("Invalid move. There are other options");
                        Player.Content = Player.Content.ToString().Split(" ")[0] + " rolled";
                        return;
                    }
                }
                else {
                    move = 0;
                    return;
                }
                
            }
            else if (flag == false)
            {
                if (!checkAllThree())
                {
                    MessageBox.Show("Invalid move. There are other options");
                    return;
                }
                MessageBox.Show("You don't have any possible move.");
                played = true;
                move = 0;
                findNextPlayer();
            }
            else if (flag == true && movement!=6)
            {
                findNextPlayer();
                played = true;
                move = 0;
            }
        }

        public void checkEndGame()
        {
            if (code == 1 && KrajCrveni1.Source != null && KrajCrveni2.Source != null && KrajCrveni3.Source != null && KrajCrveni4.Source != null)
            {
                Winner winner = new Winner();
                winner.setWinner("red");
                winner.Show();
                this.Close();
            }
            else if (code == 2 && KrajZeleni1.Source != null && KrajZeleni2.Source != null && KrajZeleni3.Source != null && KrajZeleni4.Source != null)
            {
                Winner winner = new Winner();
                winner.setWinner("green");
                winner.Show();
                this.Close();
            }
            else if (code == 3 && KrajZuti1.Source != null && KrajZuti2.Source != null && KrajZuti3.Source != null && KrajZuti4.Source != null)
            {
                Winner winner = new Winner();
                winner.setWinner("yellow");
                winner.Show();
                this.Close();
            }
            else if (code == 4 && KrajPlavi1.Source != null && KrajPlavi2.Source != null && KrajPlavi3.Source != null && KrajPlavi4.Source != null)
            {
                Winner winner = new Winner();
                winner.setWinner("blue");
                winner.Show();
                this.Close();
            }
        }

        public bool overturn(Image im,Image next,int tmpCode)
        {
            string stringCurrent = im.Name.ToString().Split("e")[1];
            string stringNext = next.Name.ToString().Split("e")[1];
            if(tmpCode==1 && (im.Name.Contains("35") || im.Name.Contains("36") || im.Name.Contains("37") || im.Name.Contains("38") || im.Name.Contains("39") || im.Name.Contains("40")))
            {
                if(stringNext.Equals("1") || stringNext.Equals("2") || stringNext.Equals("3") || stringNext.Equals("4") || stringNext.Equals("5") || stringNext.Equals("6"))
                {
                    return true;
                }
            }else if (tmpCode == 2 && (stringCurrent.Equals("5") || stringCurrent.Equals("6") || stringCurrent.Equals("7") || stringCurrent.Equals("8") || stringCurrent.Equals("9") || stringCurrent.Equals("10")))
            {
                if (stringNext.Equals("11") || stringNext.Equals("12") || stringNext.Equals("13") || stringNext.Equals("14") || stringNext.Equals("15") || stringNext.Equals("16"))
                {
                    return true;
                }
            }else if (tmpCode == 3 && (im.Name.Contains("15") || im.Name.Contains("16") || im.Name.Contains("17") || im.Name.Contains("18") || im.Name.Contains("19") || im.Name.Contains("20")))
            {
                if (stringNext.Equals("21") || stringNext.Equals("22") || stringNext.Equals("23") || stringNext.Equals("24") || stringNext.Equals("25") || stringNext.Equals("26"))
                {
                    return true;
                }
            }else if (tmpCode == 4 && (im.Name.Contains("25") || im.Name.Contains("26") || im.Name.Contains("27") || im.Name.Contains("28") || im.Name.Contains("29") || im.Name.Contains("30")))
            {
                if (stringNext.Equals("31") || stringNext.Equals("32") || stringNext.Equals("33") || stringNext.Equals("34") || stringNext.Equals("35") || stringNext.Equals("36"))
                {
                    return true;
                }
            }
            return false;
        }

        public bool checkPossibleMove(int movement)
        {

            bool found = false;
            foreach (Image im in fields)
            {
                int tmpCode = service.getCode(im);

                int field = 0;
                Image tmpImage = null;
                if (code == tmpCode)
                {
                    field = int.Parse(im.Name.Split("e")[1]) + movement;

                    if (field > 40)
                    {
                        field = field - 40;
                    }
                    tmpImage = service.findField($"Polje{field}");
                }
                else
                {
                    continue;
                }
                if (tmpImage != null && !service.checkIdenticalImages(im, tmpImage) && !overturn(im,tmpImage,tmpCode))
                {
                    found = true;
                }
                if (tmpCode == 1 && field >= 1 && field <= 6 && (im.Name.Contains("35") || im.Name.Contains("36") || im.Name.Contains("37") || im.Name.Contains("38") || im.Name.Contains("39") || im.Name.Contains("40")))
                {
                    if (service.goFinish(KrajCrveni1, KrajCrveni2, KrajCrveni3, KrajCrveni4, field, redImage, true))
                        return true;
                }
                else if (tmpCode == 2 && field >= 11 && field <= 16 && (im.Name.Contains("10") || im.Name.ToString().Split("e")[1].Equals("9") || im.Name.ToString().Split("e")[1].Equals("8") || im.Name.ToString().Split("e")[1].Equals("7") || im.Name.ToString().Split("e")[1].Equals("6") || im.Name.ToString().Split("e")[1].Equals("5")))
                {
                    int moveTmp = field - 10;
                    if (service.goFinish(KrajZeleni1, KrajZeleni2, KrajZeleni3, KrajZeleni4, moveTmp, greenImage, true))
                        return true;
                }
                else if (tmpCode == 3 && field >= 21 && field <= 26 && (im.Name.Contains("20") || im.Name.Contains("19") || im.Name.Contains("18") || im.Name.Contains("17") || im.Name.Contains("16") || im.Name.Contains("15")))
                {
                    int moveTmp = field - 20;
                    if (service.goFinish(KrajZuti1, KrajZuti2, KrajZuti3, KrajZuti4, moveTmp, yellowImage, true))
                        return true;
                }
                else if (tmpCode == 4 && field >= 31 && field <= 36 && (im.Name.Contains("30") || im.Name.Contains("29") || im.Name.Contains("28") || im.Name.Contains("27") || im.Name.Contains("26") || im.Name.Contains("25")))
                {
                    int moveTmp = field - 30;
                    if (service.goFinish(KrajPlavi1, KrajPlavi2, KrajPlavi3, KrajPlavi4, moveTmp, blueImage, true))
                        return true;
                }

            }
            if (found == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool checkHouseOptions(int movement)
        {
            if (code == 1)
            {
                if (movement == 6 && (KucicaCrvena1.Source != null || KucicaCrvena2.Source != null || KucicaCrvena3.Source != null || KucicaCrvena4.Source != null))
                    return true;
            }
            else if (code == 2)
            {
                if (movement == 6 && (KucicaZelena1.Source != null || KucicaZelena2.Source != null || KucicaZelena3.Source != null || KucicaZelena4.Source != null))
                    return true;
            }
            else if (code == 4)
            {
                if (movement == 6 && (KucicaPlava1.Source != null || KucicaPlava2.Source != null || KucicaPlava3.Source != null || KucicaPlava4.Source != null))
                    return true;
            }
            else if (code == 3)
            {
                if (movement == 6 && (KucicaZuta1.Source != null || KucicaZuta2.Source != null || KucicaZuta3.Source != null || KucicaZuta4.Source != null))
                    return true;
            }
            return false;
        }

        public bool checkEndOptions(int movement)
        {
            if (code == 1)
            {
                    if (service.checkEnd(KrajCrveni1, KrajCrveni2, KrajCrveni3, KrajCrveni4, movement))
                    {
                        return true;
                    }
            }
            else if (code == 2)
            {
                    if (service.checkEnd(KrajZeleni1, KrajZeleni2, KrajZeleni3, KrajZeleni4, movement))
                    {
                        return true;
                    }
            }
            else if (code == 3)
            {
                    if (service.checkEnd(KrajZuti1, KrajZuti2, KrajZuti3, KrajZuti4, movement))
                    {
                        return true;
                    }
            }
            else if (code == 4)
            {
                    if (service.checkEnd(KrajPlavi1, KrajPlavi2, KrajPlavi3, KrajPlavi4, movement))
                    {
                        return true;
                    }
            }
            return false;
        }

        public void houseClicked(Image image,Image start, int movement,string picture)
        {
            if (image.Source != null && movement == 6)
            {
                enter(image, start, picture);
            }
            else if (!checkAllThree())
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

        public bool checkMoveZero(int movement)
        {
            if (movement == 0)
            {
                MessageBox.Show("You have to roll first.");
                return true;
            }
            return false;
        }
        private void clicked_redHouseOne(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaCrvena1, Polje1, move, redImage);
        }

        private void clicked_redHouseTwo(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaCrvena2, Polje1, move, redImage);
        }

        private void clicked_redHouseThree(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaCrvena3, Polje1, move, redImage);
        }

        private void clicked_redHouseFour(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaCrvena4, Polje1, move, redImage);
        }

        private void clicked_greenHouseOne(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaZelena1, Polje11, move, greenImage);
        }

        private void clicked_greenHouseTwo(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaZelena2, Polje11, move, greenImage);
        }

        private void clicked_greenHouseThree(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaZelena3, Polje11, move, greenImage);
        }

        private void clicked_greenHouseFour(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaZelena4, Polje11, move, greenImage);

        }

        private void clicked_blueHouseOne(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaPlava1, Polje31, move, blueImage);

        }

        private void clicked_blueHouseTwo(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaPlava2, Polje31, move, blueImage);

        }

        private void clicked_blueHouseThree(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaPlava3, Polje31, move, blueImage);

        }

        private void clicked_blueHouseFour(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaPlava4, Polje31, move, blueImage);

        }

        private void clicked_yellowHouseOne(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaZuta1, Polje21, move, yellowImage);

        }

        private void clicked_yellowHouseTwo(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaZuta2, Polje21, move, yellowImage);

        }

        private void clicked_yellowHouseThree(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaZuta3, Polje21, move, yellowImage);

        }

        private void clicked_yellowHouseFour(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            houseClicked(KucicaZuta4, Polje21, move, yellowImage);

        }
        private void clicked_FieldOne(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
           bool flag=go(1, move, Polje1);
           finishMove(flag, move);
        }

        private void clicked_FieldTwo(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(2, move, Polje2);
            finishMove(flag, move);
        }

        private void clicked_FieldThree(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(3, move, Polje3);
            finishMove(flag, move);
        }

        private void clicked_FieldFour(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(4, move, Polje4);
            finishMove(flag, move);
        }

        private void clicked_FieldFive(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(5, move, Polje5);
            finishMove(flag, move);
        }

        private void clicked_FieldSix(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(6, move, Polje6);
            finishMove(flag, move);
        }

        private void clicked_FieldSeven(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(7, move, Polje7);
            finishMove(flag, move);
        }

        private void clicked_FieldEight(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(8, move, Polje8);
            finishMove(flag, move);
        }

        private void clicked_FieldNine(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(9, move, Polje9);
            finishMove(flag, move);
        }

        private void clicked_FieldTen(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(10, move, Polje10);
            finishMove(flag, move);
        }

        private void clicked_FieldEleven(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(11, move, Polje11);
            finishMove(flag, move);
        }

        private void clicked_FieldTwelve(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(12, move, Polje12);
            finishMove(flag, move);
        }

        private void clicked_FieldThirteen(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(13, move, Polje13);
            finishMove(flag, move);
        }

        private void clicked_FieldFourteen(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(14, move, Polje14);
            finishMove(flag, move);
        }

        private void clicked_FieldFifteen(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(15, move, Polje15);
            finishMove(flag, move);
        }

        private void clicked_FieldSixteen(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(16, move, Polje16);
            finishMove(flag, move);
        }

        private void clicked_FieldSeventeen(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(17, move, Polje17);
            finishMove(flag, move);
        }

        private void clicked_FieldEighteen(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(18, move, Polje18);
            finishMove(flag, move);
        }

        private void clicked_FieldNineteen(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(19, move, Polje19);
            finishMove(flag, move);
        }

        private void clicked_FieldTwenty(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(20, move, Polje20);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentyone(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(21, move, Polje21);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentytwo(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(22, move, Polje22);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentythree(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(23, move, Polje23);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentyfour(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(24, move, Polje24);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentyfive(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(25, move, Polje25);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentysix(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(26, move, Polje26);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentyseven(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(27, move, Polje27);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentyeight(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(28, move, Polje28);
            finishMove(flag, move);
        }

        private void clicked_FieldTwentynine(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(29, move, Polje29);
            finishMove(flag, move);
        }

        private void clicked_FieldThirty(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(30, move, Polje30);
            finishMove(flag, move);
        }

        private void clicked_FieldThiryone(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(31, move, Polje31);
            finishMove(flag, move);
        }

        private void clicked_FieldThirtytwo(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(32, move, Polje32);
            finishMove(flag, move);
        }

        private void clicked_FieldThirtythree(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(33, move, Polje33);
            finishMove(flag, move);
        }

        private void clicked_FieldThirtyfour(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(34, move, Polje34);
            finishMove(flag, move);
        }

        private void clicked_FieldThirtyfive(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(35, move, Polje35);
            finishMove(flag, move);
        }

        private void clicked_FieldThritysix(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(36, move, Polje36);
            finishMove(flag, move);
        }

        private void clicked_FIeldThirtyseven(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(37, move, Polje37);
            finishMove(flag, move);
        }

        private void clicked_FieldThirtyeight(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(38, move, Polje38);
            finishMove(flag, move);
        }

        private void clicked_FieldThirtynine(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(39, move, Polje39);
            finishMove(flag, move);
        }

        private void clicked_FieldFourty(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = go(40, move, Polje40);
            finishMove(flag, move);
        }


        
        private void clicked_redFisnishOne(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag=service.moveFinish(KrajCrveni1,move,finishRed);
            finishMove(flag, move);
        }

        private void clicked_redFinishTwo(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = service.moveFinish(KrajCrveni2, move, finishRed);
            finishMove(flag, move);
        }

        private void clicked_redFinishThree(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = service.moveFinish(KrajCrveni3, move, finishRed);
            finishMove(flag, move);
        }

        private void clicked_blueFinishOne(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = service.moveFinish(KrajPlavi1, move, finishBlue);
            finishMove(flag, move);
        }

        private void clicked_blueFinishTwo(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = service.moveFinish(KrajPlavi2, move, finishBlue);
            finishMove(flag, move);
        }

        private void clicked_blueFinishThree(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = service.moveFinish(KrajPlavi3, move, finishBlue);
            finishMove (flag, move);
        }

        private void clicked_yellowFinishOne(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = service.moveFinish(KrajZuti1, move, finishYellow);
            finishMove(flag, move);
        }

        private void clicked_yellowFinishTwo(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = service.moveFinish(KrajZuti2, move, finishYellow);
            finishMove(flag, move);
        }

        private void clicked_yellowFinishThree(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = service.moveFinish(KrajZuti3, move, finishYellow);
            finishMove(flag, move);
        }

        private void clicked_greenFinishOne(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = service.moveFinish(KrajZeleni1, move, finishGreen);
            finishMove(flag, move);
        }

        private void clicked_greenFinishTwo(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = service.moveFinish(KrajZeleni2, move, finishGreen);
            finishMove(flag, move);
        }

        private void clicked_greenFinishThree(object sender, RoutedEventArgs e)
        {
            if (checkMoveZero(move))
                return;
            bool flag = service.moveFinish(KrajZeleni3, move, finishGreen);
            finishMove(flag, move);
        }
    }
}