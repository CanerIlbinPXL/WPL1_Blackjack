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
            // hier maak ik een array van de kaarten + de kaartvalues + de speciale kaarten
            string[] kaarten = { "Klaveren", "Klaveren Koning", "Klaveren Vrouw", "Klaveren Boer", 
                "Schoppen", "Schoppen Koning", "Schoppen Vrouw", "Schoppen Boer", "Ruiten", "Ruiten Koning",
                "Ruiten Boer", "Ruiten Vrouw", "Harten", "Harten Koning", "Harten Vrouw", "Harten Boer" };
            //  ArrayList kaartvalues = new ArrayList();
            // kaartvalues.Add(1);
            // kaartvalues.Add(2);
            //  kaartvalues.Add("Koning");
            int[] kaartvalues = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            // hier generate ik een random voor de kaart, en de kaartvalue
            Random rndkaart = new Random();
            Random rndvalue = new Random();
            StringBuilder kaartenSpeler = new StringBuilder();
            StringBuilder kaartenBank = new StringBuilder();
            // de kaarten worden toegewezen aan de speler
            string kaart1Speler = kaarten[rndkaart.Next(kaarten.Length)];
            int kaartvalue1Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            //  int kaartvalue1Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            string kaart2Speler = kaarten[rndkaart.Next(kaarten.Length)];
            int kaartvalue2Speler = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            //grote if bool voor speler --> als kaart koning, boer, of vrouw is (value 10, anders normal value)
            if (kaart1Speler.Contains("Koning") == true || kaart1Speler.Contains("Vrouw") == true || kaart1Speler.Contains("Boer") == true)
            {
                kaartvalue1Speler = 10;
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
            else
            {
                kaartenSpeler.AppendLine($"{kaart2Speler} {kaartvalue2Speler}");
            }
            txtSpeler.Text = kaartenSpeler.ToString();
            /// bank
            string kaart1Bank = kaarten[rndkaart.Next(kaarten.Length)];
            int kaartvalue1Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            string kaart2Bank = kaarten[rndkaart.Next(kaarten.Length)];
            int kaartvalue2Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            if (kaart1Bank.Contains("Koning") == true || kaart1Bank.Contains("Vrouw") == true || kaart1Bank.Contains("Boer") == true)
            {
                kaartvalue1Bank = 10;
                kaartenBank.AppendLine($"{kaart1Bank}");
            }
            else
            {
                kaartenBank.AppendLine($"{kaart1Bank} {kaartvalue1Bank}");
            }
            if (kaart2Bank.Contains("Koning") == true || kaart2Bank.Contains("Vrouw") == true || kaart2Bank.Contains("Boer") == true)
            {
                kaartvalue2Bank = 10;
                kaartenBank.AppendLine($"{kaart2Bank}");
            }
            else
            {
                kaartenBank.AppendLine($"{kaart2Bank} {kaartvalue2Bank}");
            }
            txtBank.Text = kaartenBank.ToString();
            int somSpeler = kaartvalue1Speler + kaartvalue2Speler;
            int somBank = kaartvalue1Bank + kaartvalue2Bank;
            LblscoreSpeler.Content = somSpeler.ToString();
            LblscoreBank.Content = somBank.ToString();

            //// de kaarten worden toegewezen aan de bank
            //string kaart1Bank = kaarten[rndkaart.Next(kaarten.Length)];
            //int kaartvalue1Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            //string kaart2Bank = kaarten[rndkaart.Next(kaarten.Length)];
            //int kaartvalue2Bank = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            //// kaarten speler toevoegen aan de textbox
            //StringBuilder kaartenSpeler = new StringBuilder();
            //kaartenSpeler.AppendLine($"{kaart1Speler} {kaartvalue1Speler}");
            //kaartenSpeler.AppendLine($"{kaart2Speler} {kaartvalue2Speler}");
            //txtSpeler.Text = kaartenSpeler.ToString();
            //// kaarten bank toevoegen aan de textbox
            //StringBuilder kaartenBank = new StringBuilder();
            //kaartenBank.AppendLine($"{kaart1Bank} {kaartvalue1Bank}");
            //kaartenBank.AppendLine($"{kaart2Bank} {kaartvalue2Bank}");
            //txtBank.Text = kaartenBank.ToString();
            //// som van de kaarten van speler en bank word gemaakt
            //int somSpeler = kaartvalue1Speler + kaartvalue2Speler;
            //int somBank = kaartvalue1Bank + kaartvalue2Bank;
            //LblscoreSpeler.Content = somSpeler.ToString();
            //LblscoreBank.Content = somBank.ToString();

        }
    }
}
