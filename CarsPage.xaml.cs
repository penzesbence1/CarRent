using System;
using System.Collections;
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
using System.Windows.Markup;
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
            

            cbSzurok.Items.Insert(0,"Extrák");
            cbSzurok.SelectedIndex = 0;

            var autok = new List<Auto>
            {

                new Auto { Id = 0, Marka = "BMW", Modell = "X5", Uzemanyag = "Benzin", Valto = "Automata", Tipus = "SUV", UlesekSzama = 5, Ar = 50000 }, 
                new Auto { Id = 1, Marka = "Toyota", Modell = "Corolla", Uzemanyag = "Dizel", Valto = "Manuális", Tipus = "Furgon", UlesekSzama = 5, Ar = 30000 },
                new Auto { Id = 2, Marka = "Audi", Modell = "A4", Uzemanyag = "Benzin", Valto = "Manuális", Tipus = "Sedan", UlesekSzama = 5, Ar = 40000 },
                new Auto { Id = 3, Marka = "Mercedes-Benz", Modell = "C-Class", Uzemanyag = "Dizel", Valto = "Automata", Tipus = "Kombi", UlesekSzama = 5, Ar = 45000 },
                new Auto { Id = 4, Marka = "Ford", Modell = "Mustang", Uzemanyag = "Benzin", Valto = "Automata", Tipus = "Sport", UlesekSzama = 4, Ar = 60000 }

            };

            
            myListView.ItemsSource = autok;

            List<string> Markak = new List<string>();

            foreach (var item in autok)
            {
                Markak.Add(item.Marka);
            }



            List<string> Szurt1 = new List<string>();


            Szurt1 = Markak.Distinct().ToList();


            List<string> SzurtModell = new List<string>();
            List<string> SzurtUzemanyag = new List<string>();
            List<string> SzurtValto = new List<string>();
            List<string> SzurtTipus = new List<string>();
            List<string> SzurtUlesekSzama = new List<string>();

           

            List<List<string>> Szurtek = new List<List<string>>();

            
            Szurtek.Add(Szurt1);

            
            foreach (var item in Szurtek)
            {
                foreach (var item1 in item)
                {
                    cBMarka.Items.Add(item1);
                }
                
            }

          



        }

        
       

        

        

        

        public class Auto
        {
            public int Id { get; set; }
            public string Marka { get; set; }
            public string Modell { get; set; }
            public string Uzemanyag { get; set; }
            public string Valto { get; set; }
            public string Tipus { get; set; }
            public int UlesekSzama { get; set; }
            public decimal Ar { get; set; }
            
        }

       
        private void cbSzurok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            cbSzurok.SelectedIndex = 0;
           

        }

        private void myListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Auto selectedCar)
            {
                int carId = selectedCar.Id;

                NavigationService.Navigate(new AddCarPage(carId));



                
            }
        }
    }
}
