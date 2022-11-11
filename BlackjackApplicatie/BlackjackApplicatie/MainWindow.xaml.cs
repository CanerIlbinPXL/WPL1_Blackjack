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
            // hier maak ik een array van de kaarten + de kaartvalues
            string[] kaarten = { "Klaver", "Schoppen", "Ruiten", "Harten", "Klaveren koning", "Klaveren vrouw",
            "Klaveren boer", "Schoppen koning", "Schoppen vrouw", "Schoppen boer", "Ruiten koning",
            "Ruiten vrouw", "Ruiten boer", "Harten koning", "Harten vrouw", "Harten boer" };
            int[] kaartvalues = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            // hier generate ik een random voor de kaart, en de kaartvalue
            Random rndkaart = new Random();
            Random rndvalue = new Random();
            string kaart = kaarten[rndkaart.Next(kaarten.Length)];
            int kaartvalue = kaartvalues[rndvalue.Next(kaartvalues.Length)];
            // kaart toevoegen aan de textbox
            StringBuilder kaartspeler = new StringBuilder();
            kaartspeler.AppendLine($"{kaart} {kaartvalue}");
            txtSpeler.Text = kaartspeler.ToString();
           
        }




    }
}
