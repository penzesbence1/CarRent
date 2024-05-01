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
using static CarRent.CarsPage;

namespace CarRent
{
    /// <summary>
    /// Interaction logic for CarsPage.xaml
    /// </summary>
    public partial class CarsPage : Page
    {
        CarRent.Context.KolcsonzoModel cn;
       
        public CarsPage()
        {
            InitializeComponent();
            

            cbSzurok.Items.Insert(0,"Extrák");
            cbSzurok.SelectedIndex = 0;

            cn = new CarRent.Context.KolcsonzoModel();


            var autok = new List<Auto>();

           // {

           //     new Auto { Id = 0, Marka = "BMW", Modell = "X5", Uzemanyag = "Benzin", Valto = "Automata", Tipus = "SUV", UlesekSzama = 5, Ar = 50000 }, 
           //     new Auto { Id = 1, Marka = "Toyota", Modell = "Corolla", Uzemanyag = "Dizel", Valto = "Manuális", Tipus = "Furgon", UlesekSzama = 5, Ar = 30000 },
           //     new Auto { Id = 2, Marka = "Audi", Modell = "A4", Uzemanyag = "Benzin", Valto = "Manuális", Tipus = "Sedan", UlesekSzama = 5, Ar = 40000 },
           //     new Auto { Id = 3, Marka = "Mercedes-Benz", Modell = "C-Class", Uzemanyag = "Dizel", Valto = "Automata", Tipus = "Kombi", UlesekSzama = 5, Ar = 45000 },
           //     new Auto { Id = 4, Marka = "Ford", Modell = "Mustang", Uzemanyag = "Benzin", Valto = "Automata", Tipus = "Sport", UlesekSzama = 4, Ar = 60000 }

           // };

            var dbautok = from a in cn.Autoks join m in cn.Markaks on a.MarkaID equals m.MarkaID
                        select new
                        {
                            a.AutoID, m.MarkaNev, a.Modell, a.Evjarat, a.Uzemanyag, 
                            a.Valto, a.Kivitel, a.Ulesekszama, a.Ar
                        };


            foreach (var item in dbautok)
            {
                autok.Add(new Auto
                {
                    Id = item.AutoID,
                    Marka = item.MarkaNev,
                    Modell = item.Modell,
                    Evjarat = item.Evjarat,
                    Uzemanyag = item.Uzemanyag,
                    Valto = item.Valto,
                    Tipus = item.Kivitel, 
                    UlesekSzama = item.Ulesekszama,
                    Ar = item.Ar
                });
            }


             myListView.ItemsSource = autok;

            List<string> Markak = new List<string>();
            List<string> Modellek = new List<string>();
            List<int> Evjaratok = new List<int>();
            List<string> Uzemanyagok = new List<string>();
            List<string> Valtok = new List<string>();
            List<string> Tipusok = new List<string>();
            List<int> Ulesszamok = new List<int>();
            List<int> Arak = new List<int>();

            foreach (var item in autok)
            {
                Markak.Add(item.Marka);
                Modellek.Add(item.Modell);
                Evjaratok.Add(item.Evjarat);
                Uzemanyagok.Add(item.Uzemanyag);
                Valtok.Add(item.Valto);
                Tipusok.Add(item.Tipus);
                Ulesszamok.Add(item.UlesekSzama);
                Arak.Add(item.Ar);
            }



            List<string> Szurt1 = Markak.Distinct().ToList();
            List<string> Szurt2 = Modellek.Distinct().ToList();
            List<int> Szurt3 = Evjaratok.Distinct().ToList();
            List<string> Szurt4 = Uzemanyagok.Distinct().ToList();
            List<string> Szurt5 = Valtok.Distinct().ToList();
            List<string> Szurt6 = Tipusok.Distinct().ToList();
            List<int> Szurt7 = Ulesszamok.Distinct().ToList();
            List<int> Szurt8 = Arak.Distinct().ToList();



           
            foreach (string s in Szurt1) {
                cBMarka.Items.Add(s);
            }
            foreach (string s in Szurt2)
            {
                cBModell.Items.Add(s);
            }
            foreach (int s in Szurt3)
            {
                cBEvjarat.Items.Add(s);
            }
            foreach (string s in Szurt4)
            {
                cBUzemanyag.Items.Add(s);
            }
            foreach (string s in Szurt5)
            {
                cBValto.Items.Add(s);
            }
            foreach (string s in Szurt6)
            {
                cBKivitel.Items.Add(s);
            }
            foreach (int s in Szurt7)
            {
                cBUles.Items.Add(s);
            }


            foreach (int s in Szurt8)
            {
                cBAr.Items.Add(s);
            }






        }

        
       

        

        

        

        public class Auto
        {
            public int Id { get; set; }
            public string Marka { get; set; }
            public string Modell { get; set; }
            public int Evjarat { get; set; }
            public string Uzemanyag { get; set; }
            public string Valto { get; set; }
            public string Tipus { get; set; }
            public int UlesekSzama { get; set; }
            public int Ar { get; set; }
            
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
