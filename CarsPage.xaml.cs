using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRent
{
    /// <summary>
    /// Interaction logic for CarsPage.xaml
    /// </summary>
    public partial class CarsPage : Page
    {
        public CarsPage()
        {
            InitializeComponent();


            var autok = new List<Auto>
            {

                new Auto { Marka = "BMW", Modell = "X5", Uzemanyag = "Benzin", Valto = "Automata", Tipus = "SUV", UlesekSzama = 5, Ar = 5000000 },
                new Auto { Marka = "Toyota", Modell = "Corolla", Uzemanyag = "Dizel", Valto = "Manuális", Tipus = "Szabadidőjármű", UlesekSzama = 5, Ar = 3000000 },
                // Egyéb autók...
            };

            for (int i = 0; i < 100; i++) {
                autok.Add(new Auto { Marka = "BMW", Modell = "X5", Uzemanyag = "Benzin", Valto = "Automata", Tipus = "SUV", UlesekSzama = 5, Ar = 5000000 });
            }
            myListView.ItemsSource = autok;


        }

        private void MyLabel_MouseEnter(object sender, MouseEventArgs e)
        {

            if (sender is System.Windows.Controls.Label)
            {
                System.Windows.Controls.Label label = sender as System.Windows.Controls.Label;
                label.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0CA5DB")); // Világosabb árnyalatú szín beállítása
            }                                                                                     // Sötétebb háttérszín beállítása

        }
       

        private void MyLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Label)
            {
                System.Windows.Controls.Label label = sender as System.Windows.Controls.Label;
                label.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF08568A")); // Világosabb árnyalatú szín beállítása
            }


            
        }

        private void myLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }



        public class Auto
        {
            public string Marka { get; set; }
            public string Modell { get; set; }
            public string Uzemanyag { get; set; }
            public string Valto { get; set; }
            public string Tipus { get; set; }
            public int UlesekSzama { get; set; }
            public decimal Ar { get; set; }
        }

    }
}
