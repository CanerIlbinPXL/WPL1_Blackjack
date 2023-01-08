using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

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
        string kaartSpelerDoubledown;
        int kaartvalueSpelerDoubledown;
        // houdt de som van speler en bank bij
        int somSpeler;
        int somBank;
        // een startkapitaal voor als er een nieuwe game wordt gestart
        int startKapitaal = 100;
        int Kapitaal;
        int inzet;
        string resultaat;
        // images laatste kaart
        Image laatstekaartSpeler = new Image();
        Image laatstekaartBank = new Image();
        
        public MainWindow()
        {
            InitializeComponent();
            // timer om de tijd onderaan te displayen
            DispatcherTimer huidigeTijd = new DispatcherTimer();
            huidigeTijd.Interval = TimeSpan.FromSeconds(1);
            huidigeTijd.Tick += HuidigeTijd_Tick;
            huidigeTijd.Start();
        }

        // private method voor de timer
        private void HuidigeTijd_Tick(object sender, EventArgs e)
        {
            lblTijdstip.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        // private method voor de inzet
        private void InzetSpeler()
        {
            Kapitaal = int.Parse(txtKapitaal.Text);
            bool GeheelGetal = int.TryParse(txtInzet.Text, out inzet);
            // deze bool controlleert of dat de inzet wel een geheel getal is, zo niet dan ronden we af  naar boven
            if (GeheelGetal)
            {
                InzetSlider.Minimum = Math.Round((Kapitaal * 0.1), 0);
                decimal tempInzet = decimal.Parse(txtInzet.Text);
                decimal tempInzet2 = Math.Round(tempInzet, 0);
                txtInzet.Text = tempInzet2.ToString();
                inzet = int.Parse(txtInzet.Text);
                InzetSlider.Maximum = Kapitaal;
            }
            else
            {
                MessageBox.Show("Je kan geen kommagetal inzetten.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                // tijdelijke decimals om de waarde in te bewaren
                decimal tempInzet = decimal.Parse(txtInzet.Text);
                decimal tempInzet2 = Math.Round(tempInzet, 0);
                txtInzet.Text = tempInzet2.ToString();
                inzet = int.Parse(txtInzet.Text);
                // de minimal value van de slider moet ook aangepast worden anders bugged deze             
                InzetSlider.Minimum = Math.Round((Kapitaal * 0.1), 0);
                InzetSlider.Maximum = Kapitaal;
            }

        }       
        private void NieuweRonde() // deze zorgt ervoor dat wanneer er opnieuw op deel word geklikt een nieuwe ronde word gestart en alles vd vorige ronde gecleared word
        {
            kaartenSpeler.Clear();
            kaartenBank.Clear();
            somSpeler = 0;
            somBank = 0;
        }
        private void ClearLabel() // deze zorgt ervoor dat het label resultaat gecleared wordt na elke ronde
        {
            LblResultaat.Content = "";
        }
     
        private void KiesKaartenSpeler()
        {
             // de kaarten worden toegewezen aan de speler
            kaart1Speler = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalue1Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            kaart2Speler = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalue2Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            kaartSpelerHit = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalueSpelerHit = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            kaartSpelerDoubledown = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalueSpelerDoubledown = kaartvalues[rndvalue.Next(kaartvalues.Length)];
           // dubbele kaarten vermijden. Deze bool zorgt ervoor dat speler niet 2x dezelfde kaart kan krijgen
           // dit doe ik door ervoor te zorgen dat de speler niet 2x dezelfde value kan krijgen
           if ((kaartvalue1Speler == kaartvalue2Speler))
            {
                kaartvalue1Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
                kaartvalue2Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            }
           else if ((kaartvalue2Speler == kaartvalueSpelerHit))
            {
                kaartvalue2Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
                kaartvalueSpelerHit = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            }
            else if ((kaartvalue1Speler == kaartvalueSpelerHit))
            {
                kaartvalue1Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
                kaartvalueSpelerHit = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            }
            else if ((kaartvalue1Speler == kaartvalueSpelerDoubledown))
            {
                kaartvalue1Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
                kaartvalueSpelerDoubledown = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            }
            else if ((kaartvalue2Speler == kaartvalueSpelerDoubledown))
            {
                kaartvalue2Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
                kaartvalueSpelerDoubledown = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            }
        }
        private void KiesKaartenBank()
        {
            // kaarten en values van de kaartenbank
            kaart1Bank = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalue1Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            kaart2Bank = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalue2Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            kaartBankStand = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalueBankStand = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            // zorgt ervoor dat de bank niet 2x dezelfde kaart kan krijgen
            if ((kaartvalue1Bank == kaartvalue2Bank))
            {
                kaartvalue1Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            }
            else if ((kaartvalue2Bank == kaartvalueBankStand))
            {
                kaartvalue2Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            }
            else if ((kaartvalue1Bank == kaartvalueBankStand))
            {
                kaartvalue1Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            }
        }

        // we gebruiken dit om het rondenummer bij te houden om deze dan in de historiek te steken
        private static int rondeCounter;
        // we gebruiken dit om het gewonnen geld bij te houden
        private static int gewonnenInzet;
        // we gebruiken dit om het verloren geld bij te houden
        private static int velorenInzet;
        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            rondeCounter++;
            InzetSpeler();
            ClearLabel();
            NieuweRonde();
            // de 2 methods kieskaarten worden opgeroepen
            KiesKaartenSpeler();
            KiesKaartenBank();           
            //grote if bool voor speler --> als kaart koning, boer, of vrouw is (value 10, anders normal value),
            //als aas = 1 of 11 (depending on score)
            if (kaart1Speler.Contains("Koning") == true || kaart1Speler.Contains("Vrouw") == true || kaart1Speler.Contains("Boer") == true)
            {
                kaartvalue1Speler = 10;
                kaartenSpeler.AppendLine($"{kaart1Speler}");
                somSpeler += kaartvalue1Speler;

            }
            // de aasvalue 11 wordt enkel toegewezen wanneer de speler een score heeft kleiner of gelijk aan
            // 10 zodat deze in het voordeel van de speler is en en niet bust
            else if (kaart1Speler.Contains("Aas") == true)
            {
             kaartvalue1Speler = 11;
             kaartenSpeler.AppendLine($"{kaart1Speler}");
             somSpeler += kaartvalue1Speler;

            }
            else
            {
                kaartenSpeler.AppendLine($"{kaart1Speler} {kaartvalue1Speler}");
                somSpeler += kaartvalue1Speler;
            }
            if (kaart2Speler.Contains("Koning") == true || kaart2Speler.Contains("Vrouw") == true || kaart2Speler.Contains("Boer") == true)
            {
                kaartvalue2Speler = 10;
                kaartenSpeler.AppendLine($"{kaart2Speler}");
                somSpeler += kaartvalue2Speler;
            }
            // de aasvalue is 1 wanneer de totale score van de speler groter is dan 10, anders zou de speler
            // al busten
            else if (kaart2Speler.Contains("Aas") == true && somSpeler > 10)
            {
                kaartvalue2Speler = 1;
                kaartenSpeler.AppendLine($"{kaart2Speler}");
                somSpeler += kaartvalue2Speler;
            }
            // indien de speler zijn score gelijk is aan 10 of minder dan wordt de aas wel 11, dit is
            // in het voordeel van de speler
            else if (kaart2Speler.Contains("Aas") == true && somSpeler <= 10)
            {
                kaartvalue2Speler = 11;
                kaartenSpeler.AppendLine($"{kaart2Speler}");
                somSpeler += kaartvalue2Speler;
            }
            else
            {
                kaartenSpeler.AppendLine($"{kaart2Speler} {kaartvalue2Speler}");
                somSpeler += kaartvalue2Speler;
            }
            // kaarten speler worden toegevoegd aan de textbox
            txtSpeler.Text = kaartenSpeler.ToString();

            /// if bool voor de bank --> alleen moet de tweede kaart die generate wordt voor de bank hidden zijn
            /// en niet bij de som opgeteld worden
            
            if (kaart1Bank.Contains("Koning") == true || kaart1Bank.Contains("Vrouw") == true || kaart1Bank.Contains("Boer") == true)
            {
                kaartvalue1Bank = 10;
                kaartenBank.AppendLine($"{kaart1Bank}");
                somBank += kaartvalue1Bank;


            }
            // indien de eerste kaart van de bank een aas is wordt dit zwz 11 want de score van de bank is
            // dan kleiner dan 10
            else if (kaart1Bank.Contains("Aas") == true)
            {
                kaartvalue1Bank = 11;
                kaartenBank.AppendLine($"{kaart1Bank}");
                somBank += kaartvalue1Bank;
            }
            else
            {
                kaartenBank.AppendLine($"{kaart1Bank} {kaartvalue1Bank}");
                somBank += kaartvalue1Bank;
            }
            // de 2de kaart van de bank moet voorloppig hidden blijven dus hier doen we in deel nog niets mee
           
            txtBank.Text = kaartenBank.ToString();
            LblscoreSpeler.Content = somSpeler.ToString();
            LblscoreBank.Content = somBank.ToString();
            BtnStand.IsEnabled = true;
            BtnHit.IsEnabled = true;
            BtnDeel.IsEnabled = false;
            // if bool. De double down optie is enkel beschikbaar wanneer de speler een kapitaal heeft groter
            // of gelijk
            // aan inzet * 2, zo niet dan krijgt hij een messagebox dat de optie double down niet beschikbaar is
            if (Kapitaal >= inzet * 2 )
            {
                BtnDoubleDown.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Je hebt niet voldoende kapitaal om de double down functie te gebruiken. Je kan uiteraard wel verder spelen zonder deze optie.", "Double down niet beschikbaar", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
            txtInzet.IsEnabled = false;
            txtKapitaal.IsEnabled = false;
            InzetSlider.IsEnabled = false;

            // hier komt een grote bool om ervoor te zorgen dat de foto van de laatst gelezen kaart van zowel
            // spelers als bank correct getoond wordt
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
            if (kaart1Bank.Contains("Klaveren") && kaartvalue1Bank == 2)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Klaveren") && kaartvalue1Bank == 3)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Klaveren") && kaartvalue1Bank == 4)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Klaveren") && kaartvalue1Bank == 5)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Klaveren") && kaartvalue1Bank == 6)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Klaveren") && kaartvalue1Bank == 7)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Klaveren") && kaartvalue1Bank == 8)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Klaveren") && kaartvalue1Bank == 9)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Schoppen") && kaartvalue1Bank == 2)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Schoppen") && kaartvalue1Bank == 3)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Schoppen") && kaartvalue1Bank == 4)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Schoppen") && kaartvalue1Bank == 5)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Schoppen") && kaartvalue1Bank == 6)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Schoppen") && kaartvalue1Bank == 7)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Schoppen") && kaartvalue1Bank == 8)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Schoppen") && kaartvalue1Bank == 9)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Ruiten") && kaartvalue1Bank == 2)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Ruiten") && kaartvalue1Bank == 3)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Ruiten") && kaartvalue1Bank == 4)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Ruiten") && kaartvalue1Bank == 5)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Ruiten") && kaartvalue1Bank == 6)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Ruiten") && kaartvalue1Bank == 7)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Ruiten") && kaartvalue1Bank == 8)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Ruiten") && kaartvalue1Bank == 9)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Harten") && kaartvalue1Bank == 2)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Harten") && kaartvalue1Bank == 3)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Harten") && kaartvalue1Bank == 4)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Harten") && kaartvalue1Bank == 5)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Harten") && kaartvalue1Bank == 6)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Harten") && kaartvalue1Bank == 7)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Harten") && kaartvalue1Bank == 8)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Harten") && kaartvalue1Bank == 9)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Klaveren Aas"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Ruiten Aas"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Schoppen Aas"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Harten Aas"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Klaveren Koning"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_clubs2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Klaveren Boer"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_clubs2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Klaveren Vrouw"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_clubs2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Ruiten Koning"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_diamonds2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Ruiten Boer"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_diamonds2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Ruiten Vrouw"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_diamonds2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Harten Koning"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_hearts2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Harten Boer"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_hearts2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Harten Vrouw"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_hearts2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Schoppen Koning"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_spades2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Schoppen Boer"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_spades2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart1Bank.Contains("Schoppen Vrouw"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_spades2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
        }
        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            KiesKaartenSpeler();
            //grote if bool voor de nieuwe kaart speler --> als kaart koning, boer, of vrouw is (value 10,
            //anders normal value), als aas = 1
            if (kaartSpelerHit.Contains("Koning") == true || kaartSpelerHit.Contains("Vrouw") == true || kaartSpelerHit.Contains("Boer") == true)
            {
                kaartvalueSpelerHit = 10;
                kaartenSpeler.AppendLine($"{kaartSpelerHit}");
                somSpeler += kaartvalueSpelerHit;

            }
            // indien de speler zijn score groter is dan 10 word de ace 1
            else if (kaartSpelerHit.Contains("Aas") == true && somSpeler > 10)
            {
                kaartvalueSpelerHit = 1;
                kaartenSpeler.AppendLine($"{kaartSpelerHit}");
                somSpeler += kaartvalueSpelerHit;
            }
            else if (kaartSpelerHit.Contains("Aas") == true && somSpeler <= 10)
            {
                kaartvalueSpelerHit = 11;
                kaartenSpeler.AppendLine($"{kaartSpelerHit}");
                somSpeler += kaartvalueSpelerHit;
            }
            else
            {
                kaartenSpeler.AppendLine($"{kaartSpelerHit} {kaartvalueSpelerHit}");
                somSpeler += kaartvalueSpelerHit;
            }

            txtSpeler.Text = kaartenSpeler.ToString();
            
            // komt een if bool. Speler verliest indien som groter dan 21.
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
                BtnDoubleDown.IsEnabled = false;
                InzetSlider.IsEnabled = true;
                txtInzet.IsEnabled = true;
                txtKapitaal.IsEnabled = true;
                if (resultaat.Contains("Verloren") == true)
                {
                    Kapitaal -= inzet;
                    // we houden de  verloreninzet bij voor historiek purposes
                    velorenInzet += inzet;

                    txtKapitaal.Text = Kapitaal.ToString();
                }
                // inzetslider wordt afgerond zodat we geen kommagetal kunnen verkrijgen
                InzetSlider.Minimum = Math.Round((Kapitaal * 0.1), 0);
                decimal tempInzet = decimal.Parse(txtInzet.Text);
                decimal tempInzet2 = Math.Round(tempInzet, 0);
                txtInzet.Text = tempInzet2.ToString();
                inzet = int.Parse(txtInzet.Text);
                InzetSlider.Maximum = Kapitaal;
                NieuweRonde();               
                }
            // indien kapitaal 0 is kan de game niet meer verder en moet er een nieuwe game gestart worden
                if (Kapitaal == 0)
                 {
                MessageBox.Show("Je hebt geen kapitaal meer, start een nieuwe game!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                BtnNieuw.IsEnabled = true;
                BtnDeel.IsEnabled = false;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                BtnDoubleDown.IsEnabled = false;
                InzetSlider.IsEnabled = true;
                txtInzet.IsEnabled = true;
                txtKapitaal.IsEnabled = true;
                NieuweRonde();
            }

            // idem laatste kaart image correct tonen 

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
                somBank += kaartvalue2Bank;
            }
            // indien de som kleiner of gelijk is aan 10 is de aas 11 anders 1
            else if (kaart2Bank.Contains("Aas") == true && somBank <= 10)
            {
                kaartvalue2Bank = 11;
                kaartenBank.AppendLine($"{kaart2Bank}");
                txtBank.Text = kaartenBank.ToString();
                somBank += kaartvalue2Bank;
            }
            else if (kaart2Bank.Contains("Aas") == true && somBank > 10)
            {
                kaartvalue2Bank = 1;
                kaartenBank.AppendLine($"{kaart2Bank}");
                txtBank.Text = kaartenBank.ToString();
                somBank += kaartvalue2Bank;
            }
            else
            {
                kaartenBank.AppendLine($"{kaart2Bank} {kaartvalue2Bank}");
                txtBank.Text = kaartenBank.ToString();
                somBank += kaartvalue2Bank;
            }
            // idem image
            if (kaart2Bank.Contains("Klaveren") && kaartvalue2Bank == 2)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartSpeler;
            }
            else if (kaart2Bank.Contains("Klaveren") && kaartvalue2Bank == 3)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Klaveren") && kaartvalue2Bank == 4)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Klaveren") && kaartvalue2Bank == 5)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Klaveren") && kaartvalue2Bank == 6)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Klaveren") && kaartvalue2Bank == 7)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Klaveren") && kaartvalue2Bank == 8)
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Klaveren") && kaartvalue2Bank == 9 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Schoppen") && kaartvalue2Bank == 2 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Schoppen") && kaartvalue2Bank == 3 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Schoppen") && kaartvalue2Bank == 4 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Schoppen") && kaartvalue2Bank == 5 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Schoppen") && kaartvalue2Bank == 6 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Schoppen") && kaartvalue2Bank == 7 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Schoppen") && kaartvalue2Bank == 8 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Schoppen") && kaartvalue2Bank == 9 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Ruiten") && kaartvalue2Bank == 2 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Ruiten") && kaartvalue2Bank == 3 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Ruiten") && kaartvalue2Bank == 4 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Ruiten") && kaartvalue2Bank == 5 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Ruiten") && kaartvalue2Bank == 6 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Ruiten") && kaartvalue2Bank == 7 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Ruiten") && kaartvalue2Bank == 8 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Ruiten") && kaartvalue2Bank == 9 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Harten") && kaartvalue2Bank == 2 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Harten") && kaartvalue2Bank == 3 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Harten") && kaartvalue2Bank == 4 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Harten") && kaartvalue2Bank == 5 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Harten") && kaartvalue2Bank == 6 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Harten") && kaartvalue2Bank == 7 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Harten") && kaartvalue2Bank == 8 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Harten") && kaartvalue2Bank == 9 )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Klaveren Aas"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_clubs.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Ruiten Aas"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_diamonds.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Schoppen Aas"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_spades.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Harten Aas"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_hearts.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Klaveren Koning"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_clubs2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Klaveren Boer"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_clubs2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Klaveren Vrouw"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_clubs2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Ruiten Koning"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_diamonds2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Ruiten Boer"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_diamonds2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Ruiten Vrouw"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_diamonds2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Harten Koning"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_hearts2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Harten Boer"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_hearts2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Harten Vrouw"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_hearts2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Schoppen Koning") )
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_spades2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Schoppen Boer"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_spades2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }
            else if (kaart2Bank.Contains("Schoppen Vrouw"))
            {
                laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_spades2.png"));
                BtnLaatstekaartBank.Content = laatstekaartBank;
            }

            // de kaartbankstand word enkel generate indien de som van de bank kleiner is dan 16
           if (somBank < 16)
            {             
                if (kaartBankStand.Contains("Koning") == true || kaartBankStand.Contains("Vrouw") == true || kaartBankStand.Contains("Boer") == true)
                {
                    kaartvalueBankStand = 10;
                    kaartenBank.AppendLine($"{kaartBankStand}");
                    somBank += kaartvalueBankStand;
                }
                else if (kaartBankStand.Contains("Aas") == true)
                {
                    kaartvalueBankStand = 1;
                    kaartenBank.AppendLine($"{kaartBankStand}");
                    somBank += kaartvalueBankStand;
                }
                else
                    {
                    kaartenBank.AppendLine($"{kaartBankStand} {kaartvalueBankStand}");
                    somBank += kaartvalueBankStand;
                }

                // HIER WORDT DE LAATSTE KAART VAN DE BANK GETOOND

                if (kaartBankStand.Contains("Klaveren") && kaartvalueBankStand == 2)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_clubs.png"));
                    BtnLaatstekaartBank.Content = laatstekaartSpeler;
                }
                else if (kaartBankStand.Contains("Klaveren") && kaartvalueBankStand == 3)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_clubs.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Klaveren") && kaartvalueBankStand == 4)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_clubs.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Klaveren") && kaartvalueBankStand == 5)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_clubs.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Klaveren") && kaartvalueBankStand == 6)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_clubs.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Klaveren") && kaartvalueBankStand == 7)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_clubs.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Klaveren") && kaartvalueBankStand == 8)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_clubs.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Klaveren") && kaartvalueBankStand == 9)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_clubs.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Schoppen") && kaartvalueBankStand == 2)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_spades.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Schoppen") && kaartvalueBankStand == 3)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_spades.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Schoppen") && kaartvalueBankStand == 4)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_spades.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Schoppen") && kaartvalueBankStand == 5)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_spades.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Schoppen") && kaartvalueBankStand == 6)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_spades.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Schoppen") && kaartvalueBankStand == 7)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_spades.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Schoppen") && kaartvalueBankStand == 8)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_spades.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Schoppen") && kaartvalueBankStand == 9)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_spades.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Ruiten") && kaartvalueBankStand == 2)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_diamonds.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Ruiten") && kaartvalueBankStand == 3)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_diamonds.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Ruiten") && kaartvalueBankStand == 4)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_diamonds.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Ruiten") && kaartvalueBankStand == 5)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_diamonds.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Ruiten") && kaartvalueBankStand == 6)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_diamonds.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Ruiten") && kaartvalueBankStand == 7)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_diamonds.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Ruiten") && kaartvalueBankStand == 8)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_diamonds.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Ruiten") && kaartvalueBankStand == 9)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_diamonds.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Harten") && kaartvalueBankStand == 2)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_hearts.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Harten") && kaartvalueBankStand == 3)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_hearts.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Harten") && kaartvalueBankStand == 4)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_hearts.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Harten") && kaartvalueBankStand == 5)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_hearts.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Harten") && kaartvalueBankStand == 6)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_hearts.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Harten") && kaartvalueBankStand == 7)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_hearts.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Harten") && kaartvalueBankStand == 8)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_hearts.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Harten") && kaartvalueBankStand == 9)
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_hearts.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Klaveren Aas"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_clubs.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Ruiten Aas"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_diamonds.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Schoppen Aas"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_spades.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Harten Aas"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_hearts.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Klaveren Koning"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_clubs2.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Klaveren Boer"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_clubs2.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Klaveren Vrouw"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_clubs2.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Ruiten Koning"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_diamonds2.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Ruiten Boer"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_diamonds2.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Ruiten Vrouw"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_diamonds2.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Harten Koning"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_hearts2.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Harten Boer"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_hearts2.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Harten Vrouw"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_hearts2.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Schoppen Koning"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_spades2.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Schoppen Boer"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_spades2.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }
                else if (kaartBankStand.Contains("Schoppen Vrouw"))
                {
                    laatstekaartBank.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_spades2.png"));
                    BtnLaatstekaartBank.Content = laatstekaartBank;
                }

            }
            else
            {

            }     
            txtBank.Text = kaartenBank.ToString();

            LblscoreBank.Content = somBank.ToString();

            // voorwaarden resultaat
           
            if (somBank == somSpeler)
            {
                resultaat = "Push";
                LblResultaat.Foreground = Brushes.Black;
                LblResultaat.Content = "Push";
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
                // we houden de gewonnen inzet bij voor historiek
                    gewonnenInzet += inzet;
                   txtKapitaal.Text = Kapitaal.ToString();
                     }
                   else if (Kapitaal > 0 && resultaat.Contains("Push") == true)
                   {
                       txtKapitaal.Text = Kapitaal.ToString();

                   }
                   else if (Kapitaal > 0 && resultaat.Contains("Verloren") == true)
                  {
                      Kapitaal -= inzet;
                // we houden de verloren inzet bij
                velorenInzet += inzet;
                      txtKapitaal.Text = Kapitaal.ToString();
                  }

                  else
                   {
                      MessageBox.Show("Je hebt geen kapitaal meer, start een nieuwe game!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                      BtnNieuw.IsEnabled = true;
                      BtnDeel.IsEnabled = false;
                      BtnHit.IsEnabled = false;
                      BtnStand.IsEnabled = false;
                      BtnDoubleDown.IsEnabled = false;
               

                  }
            BtnDeel.IsEnabled = true;
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDoubleDown.IsEnabled = false;
            InzetSlider.IsEnabled = true;
            txtInzet.IsEnabled = true;
            txtKapitaal.IsEnabled = true;
            txtKapitaal.Text = Kapitaal.ToString();
            // slider minimum wordt geround
            InzetSlider.Minimum = Math.Round((Kapitaal * 0.1), 0);
            decimal tempInzet = decimal.Parse(txtInzet.Text);
            decimal tempInzet2 = Math.Round(tempInzet, 0);
            txtInzet.Text = tempInzet2.ToString();
            inzet = int.Parse(txtInzet.Text);
            InzetSlider.Maximum = Kapitaal;
            NieuweRonde();
        }
        private void BtnNieuw_Click(object sender, RoutedEventArgs e)
        {
            kaartenSpeler.Clear();
            kaartenBank.Clear();         
            txtKapitaal.Text = startKapitaal.ToString();
            BtnDeel.IsEnabled = true;
            BtnNieuw.IsEnabled = false;
            InzetSlider.IsEnabled = true;
            txtInzet.IsEnabled = true;
            txtKapitaal.IsEnabled = true;
            InzetSpeler();           
        }
        private void BtnDoubleDown_Click(object sender, RoutedEventArgs e)
        {
            KiesKaartenSpeler();
            KiesKaartenBank();
            if (kaartSpelerDoubledown.Contains("Koning") == true || kaartSpelerDoubledown.Contains("Vrouw") == true || kaartSpelerDoubledown.Contains("Boer") == true)
            {
                kaartvalueSpelerDoubledown = 10;
                kaartenSpeler.AppendLine($"{kaartSpelerDoubledown}");
                somSpeler += kaartvalueSpelerDoubledown;

            }
            // indien de speler zijn score groter is dan 10 word de ace 1
            else if (kaartSpelerDoubledown.Contains("Aas") == true && somSpeler > 10)
            {
                kaartvalueSpelerDoubledown = 1;
                kaartenSpeler.AppendLine($"{kaartSpelerDoubledown}");
                somSpeler += kaartvalueSpelerDoubledown;
            }
            else if (kaartSpelerHit.Contains("Aas") == true && somSpeler <= 10)
            {
                kaartvalueSpelerDoubledown = 11;
                kaartenSpeler.AppendLine($"{kaartSpelerDoubledown}");
                somSpeler += kaartvalueSpelerDoubledown;
            }
            else
            {
                kaartenSpeler.AppendLine($"{kaartSpelerDoubledown} {kaartvalueSpelerDoubledown}");
                somSpeler += kaartvalueSpelerDoubledown;
            }
            txtSpeler.Text = kaartenSpeler.ToString();

            LblscoreSpeler.Content = somSpeler.ToString();
            // idem laatstekaartimage
            if (kaartSpelerDoubledown.Contains("Klaveren") && kaartvalueSpelerDoubledown == 2)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Klaveren") && kaartvalueSpelerDoubledown == 3)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Klaveren") && kaartvalueSpelerDoubledown == 4)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Klaveren") && kaartvalueSpelerDoubledown == 5)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Klaveren") && kaartvalueSpelerDoubledown == 6)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Klaveren") && kaartvalueSpelerDoubledown == 7)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Klaveren") && kaartvalueSpelerDoubledown == 8)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Klaveren") && kaartvalueSpelerDoubledown == 9)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Schoppen") && kaartvalueSpelerDoubledown == 2)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Schoppen") && kaartvalueSpelerDoubledown == 3)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Schoppen") && kaartvalueSpelerDoubledown == 4)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Schoppen") && kaartvalueSpelerDoubledown == 5)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Schoppen") && kaartvalueSpelerDoubledown == 6)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Schoppen") && kaartvalueSpelerDoubledown == 7)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Schoppen") && kaartvalueSpelerDoubledown == 8)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Schoppen") && kaartvalueSpelerDoubledown == 9)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Ruiten") && kaartvalueSpelerDoubledown == 2)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Ruiten") && kaartvalueSpelerDoubledown == 3)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Ruiten") && kaartvalueSpelerDoubledown == 4)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Ruiten") && kaartvalueSpelerDoubledown == 5)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Ruiten") && kaartvalueSpelerDoubledown == 6)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Ruiten") && kaartvalueSpelerDoubledown == 7)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Ruiten") && kaartvalueSpelerDoubledown == 8)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Ruiten") && kaartvalueSpelerDoubledown == 9)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Harten") && kaartvalueSpelerDoubledown == 2)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\2_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Harten") && kaartvalueSpelerDoubledown == 3)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\3_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Harten") && kaartvalueSpelerDoubledown == 4)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\4_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Harten") && kaartvalueSpelerDoubledown == 5)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\5_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Harten") && kaartvalueSpelerDoubledown == 6)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\6_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Harten") && kaartvalueSpelerDoubledown == 7)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\7_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Harten") && kaartvalueSpelerDoubledown == 8)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\8_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Harten") && kaartvalueSpelerDoubledown == 9)
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\9_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Klaveren Aas"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_clubs.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Ruiten Aas"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_diamonds.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Schoppen Aas"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_spades.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Harten Aas"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\ace_of_hearts.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Klaveren Koning"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_clubs2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Klaveren Boer"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_clubs2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Klaveren Vrouw"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_clubs2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Ruiten Koning"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_diamonds2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Ruiten Boer"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_diamonds2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Ruiten Vrouw"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_diamonds2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Harten Koning"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_hearts2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Harten Boer"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_hearts2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Harten Vrouw"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_hearts2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Schoppen Koning"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\King_of_spades2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Schoppen Boer"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\jack_of_spades2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            else if (kaartSpelerDoubledown.Contains("Schoppen Vrouw"))
            {
                laatstekaartSpeler.Source = new BitmapImage(new Uri(@"C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\queen_of_spades2.png"));
                BtnLaatstekaartSpeler.Content = laatstekaartSpeler;
            }
            // kaarten van de bank worden ook gedeeld
            if (kaart2Bank.Contains("Koning") == true || kaart2Bank.Contains("Vrouw") == true || kaart2Bank.Contains("Boer") == true)
            {
                kaartvalue2Bank = 10;
                kaartenBank.AppendLine($"{kaart2Bank}");
                txtBank.Text = kaartenBank.ToString();
                somBank += kaartvalue2Bank;
            }
            // indien de som kleiner of gelijk is aan 10 is de aas 11 anders 1
            else if (kaart2Bank.Contains("Aas") == true && somBank <= 10)
            {
                kaartvalue2Bank = 11;
                kaartenBank.AppendLine($"{kaart2Bank}");
                txtBank.Text = kaartenBank.ToString();
                somBank += kaartvalue2Bank;
            }
            else if (kaart2Bank.Contains("Aas") == true && somBank > 10)
            {
                kaartvalue2Bank = 1;
                kaartenBank.AppendLine($"{kaart2Bank}");
                txtBank.Text = kaartenBank.ToString();
                somBank += kaartvalue2Bank;
            }
            else
            {
                kaartenBank.AppendLine($"{kaart2Bank} {kaartvalue2Bank}");
                txtBank.Text = kaartenBank.ToString();
                somBank += kaartvalue2Bank;
            }
            // de kaartbankstand word enkel generate indien de som van de bank kleiner is dan 16
            if (somBank < 16)
            {
                if (kaartBankStand.Contains("Koning") == true || kaartBankStand.Contains("Vrouw") == true || kaartBankStand.Contains("Boer") == true)
                {
                    kaartvalueBankStand = 10;
                    kaartenBank.AppendLine($"{kaartBankStand}");
                    somBank += kaartvalueBankStand;
                }
                else if (kaartBankStand.Contains("Aas") == true)
                {
                    kaartvalueBankStand = 1;
                    kaartenBank.AppendLine($"{kaartBankStand}");
                    somBank += kaartvalueBankStand;
                }
                else
                {
                    kaartenBank.AppendLine($"{kaartBankStand} {kaartvalueBankStand}");
                    somBank += kaartvalueBankStand;
                }
            }
            else
            {

            }
                txtBank.Text = kaartenBank.ToString();
                LblscoreBank.Content = somBank.ToString();
            if (somBank == somSpeler)
            {
                resultaat = "Push";
                LblResultaat.Foreground = Brushes.Black;
                LblResultaat.Content = "Push";
            }
            else if (somBank >= 22)
            {
                resultaat = "Gewonnen";
                LblResultaat.Foreground = Brushes.Green;
                LblResultaat.Content = "Gewonnen";
            }
            else if (somSpeler > somBank && somSpeler <= 21)
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
            // als de speler wint dan word zijn kapitaal verhoogd met dubbel maar ook verliezen met dubbel
            if (Kapitaal > 0 && resultaat.Contains("Gewonnen") == true)
            {
                int dubbelinzet = inzet * 2;
                Kapitaal += dubbelinzet;
                gewonnenInzet += dubbelinzet;
                txtKapitaal.Text = Kapitaal.ToString();
            }
            else if (Kapitaal > 0 && resultaat.Contains("Push") == true)
            {
                txtKapitaal.Text = Kapitaal.ToString();

            }
            else if (Kapitaal > 0 && resultaat.Contains("Verloren") == true)
            {
                int dubbelinzet = inzet * 2;
                Kapitaal -= dubbelinzet;
                velorenInzet += dubbelinzet;
                txtKapitaal.Text = Kapitaal.ToString();
            }

            else
            {
                MessageBox.Show("Je hebt geen kapitaal meer, start een nieuwe game!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                BtnNieuw.IsEnabled = true;
                BtnDeel.IsEnabled = false;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                BtnDoubleDown.IsEnabled = false;


            }
            BtnDeel.IsEnabled = true;
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDoubleDown.IsEnabled = false;
            InzetSlider.IsEnabled = true;
            txtInzet.IsEnabled = true;
            txtKapitaal.IsEnabled = true;
            txtKapitaal.Text = Kapitaal.ToString();
            InzetSlider.Minimum = Math.Round((Kapitaal * 0.1), 0);
            decimal tempInzet = decimal.Parse(txtInzet.Text);
            decimal tempInzet2 = Math.Round(tempInzet, 0);
            txtInzet.Text = tempInzet2.ToString();
            inzet = int.Parse(txtInzet.Text);
            InzetSlider.Maximum = Kapitaal;
            NieuweRonde();

        }
        // laat de historiek zien
        private void BtnHistoriek_Click(object sender, RoutedEventArgs e)
        {

            int inzetStatus = gewonnenInzet - velorenInzet;
            
            MessageBox.Show($"Aantal gespeelde rondes: {rondeCounter}, Gewonnen/veloren inzet: €{inzetStatus} ", "Overzicht gespeelde rondes", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
