using System;
using System.Collections;
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

namespace BlackjackApplicatie
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //  hier maak ik een array van de kaarten + de kaartvalues 
        string[] kaarten = { "Klaveren", "Klaveren Koning", "Klaveren Vrouw", "Klaveren Boer",
                "Schoppen", "Schoppen Koning", "Schoppen Vrouw", "Schoppen Boer", "Ruiten", "Ruiten Koning",
                "Ruiten Boer", "Ruiten Vrouw", "Harten", "Harten Koning", "Harten Vrouw", "Harten Boer",
                "Klaveren Aas", "Schoppen Aas", "Ruiten Aas", "Harten Aas"};
        int[] kaartvalues = { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        // stringbuilder om later de kaarten van speler en bank in te voegen in de textboxes
        StringBuilder kaartenSpeler = new StringBuilder();
        StringBuilder kaartenBank = new StringBuilder();
        // random kaart en value generaten
        Random rndkaart = new Random();
        Random rndvalue = new Random();
        // alle nodige kaarten worden hier al declared
        string kaart1Speler;
        int kaartvalue1Speler;
        string kaart2Speler;
        int kaartvalue2Speler;
        string kaart1Bank;
        int kaartvalue1Bank;
        string kaart2Bank;
        int kaartvalue2Bank;
        string kaartSpelerHit;
        int kaartvalueSpelerHit;
        string kaartBankStand;
        int kaartvalueBankStand;
        int somSpeler;
        int somBank;
        int Kapitaal;
        int inzet;
        string resultaat;
        Image laatstekaartSpeler = new Image();


        public MainWindow()
        {
            InitializeComponent();
        }
        //private void StartKapitaal()
        //{
        //    kapitaal = 100;
        //    kapitaalSpeler = kapitaal.ToString();
        //    txtKapitaal.Text = kapitaalSpeler;
        //}
        private void InzetSpeler()
        {
            Kapitaal = int.Parse(txtKapitaal.Text);
            inzet = int.Parse(txtInzet.Text);
            // als de speler wint dan word zijn kapitaal verhoogd met wat hij ingezet heeft
            //if (Kapitaal > 0 && resultaat.Contains("Gewonnen") == true)
            //{

            //    Kapitaal += inzet;
            //    txtKapitaal.Text = Kapitaal.ToString();
            //}
            //else if (Kapitaal > 0 && resultaat.Contains("Stand") == true)
            //{
            //    txtKapitaal.Text = Kapitaal.ToString();

            //}
            //else if (Kapitaal > 0 && resultaat.Contains("Verloren") == true)
            //{
            //    Kapitaal -= inzet;
            //    txtKapitaal.Text = Kapitaal.ToString();
            //}

            //else
            //{
            //    MessageBox.Show("Je hebt geen kapitaal meer, start een nieuwe game!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            //    BtnNieuw.IsEnabled = true;
            //    BtnDeel.IsEnabled = false;
            //    BtnHit.IsEnabled = false;
            //    BtnStand.IsEnabled = false;

            //}

        }
       
        //private void ButtonImage()
        //{
        //    //ImageBrush ib = new ImageBrush();
        //    //ib.ImageSource = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_clubs.png", UriKind.Relative));
        //    //BtnLaatstekaartSpeler.Content = ib;
           
        //    laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_clubs.png"));
        //    BtnLaatstekaartSpeler.Content = laatstekaartSpeler;


        //}
        private void MaakKaartenLeeg() // deze zorgt ervoor dat wanneer er opnieuw op deel word geklikt, de vorige kaarten gewist worden. Made for codetesting
        {
            kaartenSpeler.Clear();
            kaartenBank.Clear();
        }
        private void MaakResultLeeg() // zorgt ervoor dat het resultaat gecleared wordt wanneer er een nieuwe game wordt gestart
        {
            LblResultaat.Content = "";
        }
        private void KiesKaartenSpeler()
        {
             // de 2 kaarten worden toegewezen aan de speler
            kaart1Speler = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalue1Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            kaart2Speler = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalue2Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            kaartSpelerHit = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalueSpelerHit = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            
            // dit stuk zorgt ervoor dat een afbeelding van latest card die de speler getrokken heeft getoond word
            // zorgt ervoor dat de kaarten niet hetzelfde kunnen zijn
            //while (kaartvalue1Speler == kaartvalue2Speler || kaartvalue1Speler == kaartvalueSpelerHit || kaartvalue2Speler == kaartvalueSpelerHit)
            //    kaartvalue2Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            //    kaartvalueSpelerHit = kaartvalues[rndvalue.Next(kaartvalues.Length)];
        }
        private void KiesKaartenBank()
        {
            kaart1Bank = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalue1Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            kaart2Bank = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalue2Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            kaartBankStand = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalueBankStand = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            //  kaartBankStand = kaarten[rndkaart.Next(kaarten.Length)];
            //   kaartvalueBankStand = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            // zorgt ervoor dat 2 kaarten nooit hetzelfde kunnen zijn
            //while (kaartvalue1Bank == kaartvalue2Bank || kaartvalue1Bank == kaartvalueBankStand || kaartvalue2Bank == kaartvalueBankStand)
            //    kaartvalue2Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            //    kaartvalueBankStand = kaartvalues[rndvalue.Next(kaartvalues.Length)];
        }
        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            InzetSpeler();
            MaakResultLeeg();
            // de 2 methods kieskaarten worden opgeroepen
            KiesKaartenSpeler();
            KiesKaartenBank();           
            
            //grote if bool voor speler --> als kaart koning, boer, of vrouw is (value 10, anders normal value), als aas = 1 of 11 (depending on score)
            if (kaart1Speler.Contains("Koning") == true || kaart1Speler.Contains("Vrouw") == true || kaart1Speler.Contains("Boer") == true)
            {
                kaartvalue1Speler = 10;
                kaartenSpeler.AppendLine($"{kaart1Speler}");

            }
            // de aasvalue 11 wordt enkel toegewezen wanneer de speler een score heeft kleiner of gelijk aan 10 zodat deze in het voordeel van de speler is en en niet bust
            else if (kaart1Speler.Contains("Aas") == true)
            {
             kaartvalue1Speler = 11;
             kaartenSpeler.AppendLine($"{kaart1Speler}");
                
            }
            else
            {
                kaartenSpeler.AppendLine($"{kaart1Speler} {kaartvalue1Speler}");
            }
            if (kaart2Speler.Contains("Koning") == true || kaart2Speler.Contains("Vrouw") == true || kaart2Speler.Contains("Boer") == true)
            {
                kaartvalue2Speler = 10;
                kaartenSpeler.AppendLine($"{kaart2Speler}");
            }
            // de aasvalue is 1 wanneer de totale score van de speler groter is dan 10, anders zou de speler al busten
            else if (kaart2Speler.Contains("Aas") == true)
            {
                kaartvalue2Speler = 1;
                kaartenSpeler.AppendLine($"{kaart2Speler}");
            }
            else
            {
                kaartenSpeler.AppendLine($"{kaart2Speler} {kaartvalue2Speler}");
            }
            // kaarten speler worden toegevoegd aan de textbox
            txtSpeler.Text = kaartenSpeler.ToString();

            /// if bool voor de bank --> alleen moet de tweede kaart die generate wordt voor de bank hidden zijn en niet bij de som opgeteld worden
            
            if (kaart1Bank.Contains("Koning") == true || kaart1Bank.Contains("Vrouw") == true || kaart1Bank.Contains("Boer") == true)
            {
                kaartvalue1Bank = 10;
                kaartenBank.AppendLine($"{kaart1Bank}");
               
            }
            else if (kaart1Bank.Contains("Aas") == true)
            {
                kaartvalue1Bank = 11;
                kaartenBank.AppendLine($"{kaart1Bank}");
            }
            else
            {
                kaartenBank.AppendLine($"{kaart1Bank} {kaartvalue1Bank}");
            }
            //if (kaart2Bank.Contains("Koning") == true || kaart2Bank.Contains("Vrouw") == true || kaart2Bank.Contains("Boer") == true)
            //{
            //    kaartvalue2Bank = 10;
            //    // kaartenBank.AppendLine($"{kaart2Bank}");
            //}
            //else if (kaart2Bank.Contains("Aas") == true)
            //{
            //    kaartvalue2Bank = 1;
            //    //  kaartenBank.AppendLine($"{kaart2Bank}");
            //}
            // 2de kaart van de bank moet verborgen worden en niet in de som meegerekend worden
            //else
            //{
            //    // kaartenBank.AppendLine($"{kaart2Bank} {kaartvalue2Bank}");
            //}
           
            txtBank.Text = kaartenBank.ToString();
            somSpeler = kaartvalue1Speler + kaartvalue2Speler;
            somBank = kaartvalue1Bank; //de 2de kaart is hidden dus die tellen we er voorlopig niet bij, deze word met de button stand meegegeven
            LblscoreSpeler.Content = somSpeler.ToString();
            LblscoreBank.Content = somBank.ToString();
            BtnStand.IsEnabled = true;
            BtnHit.IsEnabled = true;
            BtnDeel.IsEnabled = false;

            // hier komt een grote bool om ervoor te zorgen dat de foto van de laatst gelezen kaart van zowel spelers als bank correct getoond wordt
            if(kaart2Speler.Contains("Klaveren") && kaartvalue2Speler == 2)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler; 
            }
            else if (kaart2Speler.Contains("Klaveren") && kaartvalue2Speler == 3)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Klaveren") && kaartvalue2Speler == 4)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Klaveren") && kaartvalue2Speler == 5)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Klaveren") && kaartvalue2Speler == 6)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Klaveren") && kaartvalue2Speler == 7)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Klaveren") && kaartvalue2Speler == 8)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Klaveren") && kaartvalue2Speler == 9)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Schoppen") && kaartvalue2Speler == 2)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Schoppen") && kaartvalue2Speler == 3)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Schoppen") && kaartvalue2Speler == 4)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Schoppen") && kaartvalue2Speler == 5)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Schoppen") && kaartvalue2Speler == 6)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Schoppen") && kaartvalue2Speler == 7)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Schoppen") && kaartvalue2Speler == 8)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Schoppen") && kaartvalue2Speler == 9)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Ruiten") && kaartvalue2Speler == 2)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Ruiten") && kaartvalue2Speler == 3)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Ruiten") && kaartvalue2Speler == 4)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Ruiten") && kaartvalue2Speler == 5)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Ruiten") && kaartvalue2Speler == 6)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Ruiten") && kaartvalue2Speler == 7)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Ruiten") && kaartvalue2Speler == 8)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Ruiten") && kaartvalue2Speler == 9)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Harten") && kaartvalue2Speler == 2)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Harten") && kaartvalue2Speler == 3)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Harten") && kaartvalue2Speler == 4)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Harten") && kaartvalue2Speler == 5)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Harten") && kaartvalue2Speler == 6)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Harten") && kaartvalue2Speler == 7)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Harten") && kaartvalue2Speler == 8)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Harten") && kaartvalue2Speler == 9)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Klaveren Aas"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Ruiten Aas"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Schoppen Aas"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Harten Aas"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Klaveren Koning"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_clubs2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Klaveren Boer"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_clubs2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Klaveren Vrouw"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_clubs2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Ruiten Koning"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_diamonds2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Ruiten Boer"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_diamonds2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Ruiten Vrouw"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_diamonds2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Harten Koning"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_hearts2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Harten Boer"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_hearts2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Harten Vrouw"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_hearts2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Schoppen Koning"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_spades2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Schoppen Boer"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_spades2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaart2Speler.Contains("Schoppen Vrouw"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_spades2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            // zorgt ervoor dat moest er opnieuw op deel geklikt worden dat er nieuwe kaarten generate worden
            //  MaakKaartenLeeg();

        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            KiesKaartenSpeler();
            //grote if bool voor de nieuwe kaart speler --> als kaart koning, boer, of vrouw is (value 10, anders normal value), als aas = 1
            if (kaartSpelerHit.Contains("Koning") == true || kaartSpelerHit.Contains("Vrouw") == true || kaartSpelerHit.Contains("Boer") == true)
            {
                kaartvalueSpelerHit = 10;
                kaartenSpeler.AppendLine($"{kaartSpelerHit}");

            }
            else if (kaartSpelerHit.Contains("Aas") == true)
            {
                kaartvalueSpelerHit = 1;
                kaartenSpeler.AppendLine($"{kaartSpelerHit}");
            }
            else
            {
                kaartenSpeler.AppendLine($"{kaartSpelerHit} {kaartvalueSpelerHit}");
            }

            txtSpeler.Text = kaartenSpeler.ToString();
            somSpeler += kaartvalueSpelerHit;
            // komt een if bool
            LblscoreSpeler.Content = somSpeler.ToString();
            InzetSpeler();
            if (somSpeler > 21) 
            { 
                resultaat = "Verloren";
                LblResultaat.Foreground = Brushes.Red;
                LblResultaat.Content = "Verloren";
                BtnDeel.IsEnabled = true;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                if (resultaat.Contains("Verloren") == true)
                {
                    Kapitaal -= inzet;
                    txtKapitaal.Text = Kapitaal.ToString();
                }
                MaakKaartenLeeg();
                
            }
            if (Kapitaal == 0)
            {
                MessageBox.Show("Je hebt geen kapitaal meer, start een nieuwe game!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                BtnNieuw.IsEnabled = true;
                BtnDeel.IsEnabled = false;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                MaakKaartenLeeg();
            }

            if (kaartSpelerHit.Contains("Klaveren") && kaartvalueSpelerHit == 2)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Klaveren") && kaartvalueSpelerHit == 3)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Klaveren") && kaartvalueSpelerHit == 4)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Klaveren") && kaartvalueSpelerHit == 5)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Klaveren") && kaartvalueSpelerHit == 6)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Klaveren") && kaartvalueSpelerHit == 7)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Klaveren") && kaartvalueSpelerHit == 8)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Klaveren") && kaartvalueSpelerHit == 9)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Schoppen") && kaartvalueSpelerHit == 2)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Schoppen") && kaartvalueSpelerHit == 3)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Schoppen") && kaartvalueSpelerHit == 4)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Schoppen") && kaartvalueSpelerHit == 5)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Schoppen") && kaartvalueSpelerHit == 6)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Schoppen") && kaartvalueSpelerHit == 7)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Schoppen") && kaartvalueSpelerHit == 8)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Schoppen") && kaartvalueSpelerHit == 9)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Ruiten") && kaartvalueSpelerHit == 2)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Ruiten") && kaartvalueSpelerHit == 3)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Ruiten") && kaartvalueSpelerHit == 4)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Ruiten") && kaartvalueSpelerHit == 5)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Ruiten") && kaartvalueSpelerHit == 6)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Ruiten") && kaartvalueSpelerHit == 7)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Ruiten") && kaartvalueSpelerHit == 8)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Ruiten") && kaartvalueSpelerHit == 9)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Harten") && kaartvalueSpelerHit == 2)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Harten") && kaartvalueSpelerHit == 3)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Harten") && kaartvalueSpelerHit == 4)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Harten") && kaartvalueSpelerHit == 5)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Harten") && kaartvalueSpelerHit == 6)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Harten") && kaartvalueSpelerHit == 7)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Harten") && kaartvalueSpelerHit == 8)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Harten") && kaartvalueSpelerHit == 9)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Klaveren Aas"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Ruiten Aas"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Schoppen Aas"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Harten Aas"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Klaveren Koning"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_clubs2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Klaveren Boer"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_clubs2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Klaveren Vrouw"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_clubs2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Ruiten Koning"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_diamonds2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Ruiten Boer"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_diamonds2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Ruiten Vrouw"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_diamonds2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Harten Koning"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_hearts2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Harten Boer"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_hearts2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Harten Vrouw"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_hearts2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Schoppen Koning"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_spades2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Schoppen Boer"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_spades2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerHit.Contains("Schoppen Vrouw"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_spades2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }


            //if (resultaat.Contains("Verloren") == true)
            //     {
            //        Kapitaal -= inzet;
            //        txtKapitaal.Text = Kapitaal.ToString();
            //     }
            //else
            //{
            //    MessageBox.Show("Je hebt geen kapitaal meer, start een nieuwe game!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            //    BtnNieuw.IsEnabled = true;
            //    BtnDeel.IsEnabled = false;
            //    BtnHit.IsEnabled = false;
            //    BtnStand.IsEnabled = false;

            //}


        }
        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            KiesKaartenBank();
            // dit zorgt ervoor dat de 2de hidden card van de bank correct getoond wordt
            if (kaart2Bank.Contains("Koning") == true || kaart2Bank.Contains("Vrouw") == true || kaart2Bank.Contains("Boer") == true)
            {
                kaartvalue2Bank = 10;
                kaartenBank.AppendLine($"{kaart2Bank}");
                txtBank.Text = kaartenBank.ToString();
            }
            else
            {
                kaartenBank.AppendLine($"{kaart2Bank} {kaartvalue2Bank}");
                txtBank.Text = kaartenBank.ToString();
            }
           // txtBank.Text = kaartenBank.ToString();
            somBank += kaartvalue2Bank;

            if (somBank < 16 && kaartBankStand.Contains("Koning") == true || kaartBankStand.Contains("Vrouw") == true || kaartBankStand.Contains("Boer") == true)
            {
                kaartvalueBankStand = 10;
                kaartenBank.AppendLine($"{kaartBankStand}");
            }   
            else if (somBank < 16 && kaartBankStand.Contains("Aas") == true)
            {
                kaartvalueBankStand = 1;
                kaartenBank.AppendLine($"{kaartBankStand}");
            }
            else if (somBank < 16)
            {
                kaartenBank.AppendLine($"{kaartBankStand} {kaartvalueBankStand}");
            }
            else
            {

            }
            somBank += kaartvalueBankStand;
            txtBank.Text = kaartenBank.ToString();

            LblscoreBank.Content = somBank.ToString();

            if(somBank == somSpeler)
            {
                resultaat = "Stand";
                LblResultaat.Foreground = Brushes.Black;
                LblResultaat.Content = "Stand";
            }
            else if (somBank >= 22)
            {
                resultaat = "Gewonnen";
                LblResultaat.Foreground = Brushes.Green;
                LblResultaat.Content = "Gewonnen";
            }
            else if (somSpeler > somBank)
            {
                resultaat = "Gewonnen";
                LblResultaat.Foreground = Brushes.Green;
                LblResultaat.Content = "Gewonnen";
            }
            else
            {
                resultaat = "Verloren";
                LblResultaat.Foreground = Brushes.Red;
                LblResultaat.Content = "Verloren";
            }
            InzetSpeler();
                 // als de speler wint dan word zijn kapitaal verhoogd met wat hij ingezet heeft
                  if (Kapitaal > 0 && resultaat.Contains("Gewonnen") == true)
                   { 

                   Kapitaal += inzet;
                   txtKapitaal.Text = Kapitaal.ToString();
                     }
                   else if (Kapitaal > 0 && resultaat.Contains("Stand") == true)
                   {
                       txtKapitaal.Text = Kapitaal.ToString();

                   }
                   else if (Kapitaal > 0 && resultaat.Contains("Verloren") == true)
                  {
                      Kapitaal -= inzet;
                       txtKapitaal.Text = Kapitaal.ToString();
                  }

                  else
                   {
                      MessageBox.Show("Je hebt geen kapitaal meer, start een nieuwe game!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                      BtnNieuw.IsEnabled = true;
                      BtnDeel.IsEnabled = false;
                      BtnHit.IsEnabled = false;
                      BtnStand.IsEnabled = false;

                  }

            BtnDeel.IsEnabled = true;
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            MaakKaartenLeeg();

        }
        private void BtnNieuw_Click(object sender, RoutedEventArgs e)
        {
            InzetSpeler();
            kaartenSpeler.Clear();
            kaartenBank.Clear();
            Kapitaal = 100;
            txtKapitaal.Text = Kapitaal.ToString();
            BtnDeel.IsEnabled = true;
            BtnNieuw.IsEnabled = false;
        }

       
    }
}
