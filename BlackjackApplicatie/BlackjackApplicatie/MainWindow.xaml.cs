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
        int[] kaartvalues = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
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
        int somSpeler;
        int somBank;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MaakKaartenLeeg() // deze zorgt ervoor dat wanneer er opnieuw op deel word geklikt, de vorige kaarten gewist worden. Made for codetesting
        {
            kaartenSpeler.Clear();
            kaartenBank.Clear();
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
            // zorgt ervoor dat de kaarten niet hetzelfde kunnen zijn
            while (kaartvalue1Speler == kaartvalue2Speler || kaartvalue1Speler == kaartvalueSpelerHit || kaartvalue2Speler == kaartvalueSpelerHit)
                kaartvalue2Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
                kaartvalueSpelerHit = kaartvalues[rndvalue.Next(kaartvalues.Length)];
        }
        private void KiesKaartenBank()
        {
            kaart1Bank = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalue1Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            kaart2Bank = kaarten[rndkaart.Next(kaarten.Length)];
            kaartvalue2Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            // zorgt ervoor dat 2 kaarten nooit hetzelfde kunnen zijn
            while (kaartvalue1Bank == kaartvalue2Bank)
                kaartvalue2Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
        }
        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            // de 2 methods kieskaarten worden opgeroepen
            KiesKaartenSpeler();
            KiesKaartenBank();
          
            //grote if bool voor speler --> als kaart koning, boer, of vrouw is (value 10, anders normal value), als aas = 1
            if (kaart1Speler.Contains("Koning") == true || kaart1Speler.Contains("Vrouw") == true || kaart1Speler.Contains("Boer") == true)
            {
                kaartvalue1Speler = 10;
                kaartenSpeler.AppendLine($"{kaart1Speler}");
                
            }
            else if (kaart1Speler.Contains("Aas") == true)
            {
                kaartvalue1Speler = 1;
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
                kaartvalue1Bank = 1;
                kaartenBank.AppendLine($"{kaart1Bank}");
            }
            else
            {
                kaartenBank.AppendLine($"{kaart1Bank} {kaartvalue1Bank}");
            }
            if (kaart2Bank.Contains("Koning") == true || kaart2Bank.Contains("Vrouw") == true || kaart2Bank.Contains("Boer") == true)
            {
                kaartvalue2Bank = 10;
                // kaartenBank.AppendLine($"{kaart2Bank}");
                if (kaart2Bank.Contains("Aas") == true)
                {
                    kaartvalue2Bank = 1;
                    // kaartenBank.AppendLine($"{kaart2Bank}");
                }
            }
            else if (kaart2Bank.Contains("Aas") == true)
            {
                kaartvalue2Bank = 1;
                //  kaartenBank.AppendLine($"{kaart2Bank}");
            }
            // 2de kaart van de bank moet verborgen worden en niet in de som meegerekend worden
            else
            {
               // kaartenBank.AppendLine($"{kaart2Bank} {kaartvalue2Bank}");
            }
            txtBank.Text = kaartenBank.ToString();
            somSpeler = kaartvalue1Speler + kaartvalue2Speler;
            somBank = kaartvalue1Bank;
            LblscoreSpeler.Content = somSpeler.ToString();
            LblscoreBank.Content = somBank.ToString();
            BtnStand.IsEnabled = true;
            BtnHit.IsEnabled = true;
            BtnDeel.IsEnabled = false;

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
        }
    }
}
