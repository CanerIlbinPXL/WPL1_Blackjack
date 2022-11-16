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
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
         //   hier maak ik een array van de kaarten +de kaartvalues 
            string[] kaarten = { "Klaveren", "Klaveren Koning", "Klaveren Vrouw", "Klaveren Boer",
                "Schoppen", "Schoppen Koning", "Schoppen Vrouw", "Schoppen Boer", "Ruiten", "Ruiten Koning",
                "Ruiten Boer", "Ruiten Vrouw", "Harten", "Harten Koning", "Harten Vrouw", "Harten Boer",
                "Klaveren Aas", "Schoppen Aas", "Ruiten Aas", "Harten Aas"};
            int[] kaartvalues = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
          //  hier generate ik een random voor de kaart, en de kaartvalue
            Random rndkaart = new Random();
            Random rndvalue = new Random();
            // stringbuilder om de kaarten later in de textboxes toe te voegen
            StringBuilder kaartenSpeler = new StringBuilder();
            StringBuilder kaartenBank = new StringBuilder();
           // de 2 kaarten worden toegewezen aan de speler
            string kaart1Speler = kaarten[rndkaart.Next(kaarten.Length)];
            int kaartvalue1Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            string kaart2Speler = kaarten[rndkaart.Next(kaarten.Length)];
            int kaartvalue2Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            // zorgt ervoor dat 2 kaarten nooit hetzelfde kunnen zijn
            while(kaartvalue1Speler == kaartvalue2Speler)
                kaartvalue2Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
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
            /// idem voor de bank --> alleen moet de tweede kaart voor de bank hidden zijn
            string kaart1Bank = kaarten[rndkaart.Next(kaarten.Length)];
            int kaartvalue1Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            string kaart2Bank = kaarten[rndkaart.Next(kaarten.Length)];
            int kaartvalue2Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            // zorgt ervoor dat 2 kaarten nooit hetzelfde kunnen zijn
            while (kaartvalue1Bank == kaartvalue2Bank)
                kaartvalue2Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
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
            int somSpeler = kaartvalue1Speler + kaartvalue2Speler;
            int somBank = kaartvalue1Bank;
            LblscoreSpeler.Content = somSpeler.ToString();
            LblscoreBank.Content = somBank.ToString();
            BtnStand.IsEnabled = true;
            BtnHit.IsEnabled = true;
           // BtnDeel.IsEnabled = false;

        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
         
            

        }
    }
}
