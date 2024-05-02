using CarRent.KolcsonzoModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
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
        public string MarkaS, ModellS, EvjaratS;
        public List<Auto> kocsik = new List<Auto>();

        public CarsPage()
        {
            InitializeComponent();

           

            cn = new CarRent.Context.KolcsonzoModel();





            var autok = Lekerdezes();

            ComboBoxolas(autok);

            kocsik = autok;


            myListView.ItemsSource = autok;


            




            var extrak = (from a in cn.Extraks
                          select new CheckBoxItem
                          {
                              ExtraNev = a.ExtraNev,
                              IsChecked = false
                          }).ToList();


            foreach (var item in extrak)
            {
                cbSzurok.Items.Add(item);
            }
            // cbSzurok.ItemsSource = extrak;







        }
        public void ComboBoxolas(List<Auto> ujautok)
        {
            var autok = ujautok;

            List<string> Markak = autok.Select(a => a.Marka).ToList();
            List<string> Modellek = autok.Select(a => a.Modell).ToList();
            List<int> Evjaratok = autok.Select(a => a.Evjarat).ToList();
            List<string> Uzemanyagok = autok.Select(a => a.Uzemanyag).ToList();
            List<string> Valtok = autok.Select(a => a.Valto).ToList();
            List<string> Tipusok = autok.Select(a => a.Tipus).ToList();
            List<int> Ulesszamok = autok.Select(a => a.UlesekSzama).ToList();
            List<int> Arak = autok.Select(a => a.Ar).ToList();


            ComboBoxFeltoltes(cBMarka, Markak);
            ComboBoxFeltoltes(cBModell, Modellek);
            ComboBoxFeltoltes(cBEvjarat, Evjaratok);
            ComboBoxFeltoltes(cBUzemanyag, Uzemanyagok);
            ComboBoxFeltoltes(cBValto, Valtok);
            ComboBoxFeltoltes(cBKivitel, Tipusok);
            ComboBoxFeltoltes(cBUles, Ulesszamok);
            //ComboBoxFeltoltes(cBAr, Arak);
        }
        public void ComboBoxFeltoltes<T>(ComboBox comboBox, List<T> adatok)
        {

            if (comboBox == null || adatok == null) return;

            comboBox.Items.Clear();

            foreach (var item in adatok.Distinct())
            {
                comboBox.Items.Add(item);
            }
        }
        public List<Auto> Lekerdezes()
        {
            var autok = cn.Autoks.Join(cn.Markaks, a => a.MarkaID, m => m.MarkaID, (a, m) => new Auto
            {
                Id = a.AutoID,
                Marka = m.MarkaNev,
                Modell = a.Modell,
                Evjarat = a.Evjarat,
                Uzemanyag = a.Uzemanyag,
                Valto = a.Valto,
                Tipus = a.Kivitel,
                UlesekSzama = a.Ulesekszama,
                Ar = a.Ar
            }).ToList();

            return autok;

        }
        public class CheckBoxItem
        {
            public string ExtraNev { get; set; }
            public bool IsChecked { get; set; }
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
           
            List<CheckBoxItem> selectedItems = new List<CheckBoxItem>();

            // Végigmegyünk az összes ComboBox-ban található elemen
            foreach (var comboBoxItem in cbSzurok.Items)
            {
                // Ellenőrizzük, hogy az aktuális elem CheckBoxItem típusú és kiválasztva van-e
                if (comboBoxItem is CheckBoxItem item && item.IsChecked)
                {
                    // Ha igen, hozzáadjuk az elemet a kiválasztott elemek listájához
                    selectedItems.Add(item);
                }
            }

        }

        private void myListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Auto selectedCar)
            {
                int carId = selectedCar.Id;

                NavigationService.Navigate(new AddCarPage(carId));




            }
        }

        private void cBMarka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;
          
           

            if (comboBox.SelectedItem != null)
            {
                ComboBoxItem kiv = comboBox.SelectedItem as ComboBoxItem;
               
                string keres = comboBox.SelectedItem.ToString();
                teszt.Content = keres;
                MarkaS = keres;


                var autok = Lekerdezes();
                List<Auto> szurt = new List<Auto>();
                
                foreach (var item in autok)
                {
                    if (item.Marka == keres){
                        szurt.Add(item);
                    }
                }
              


                myListView.ItemsSource = szurt;
                ComboBoxolas(szurt);


                var uj = Lekerdezes();

                List<string> Markak = uj.Select(a => a.Marka).ToList();
                ComboBoxFeltoltes(cBMarka, Markak);
                kocsik = szurt;
               
            }
        }

        private void cBModell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> regiek = new List<string>();

            foreach (var elem in cBModell.Items)
            {
                regiek.Add(elem.ToString());
            }

            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                ComboBoxItem kiv = comboBox.SelectedItem as ComboBoxItem;
                string keres = comboBox.SelectedItem.ToString();

               

                List<Auto> dbautok = kocsik;
                List<Auto> szurt = new List<Auto>();

                foreach (var item in dbautok)
                {
                    if (item.Modell == keres)
                    {
                        szurt.Add(item);
                    }
                }

                myListView.ItemsSource = szurt;
                ComboBoxolas(szurt);

                kocsik = szurt;

                var uj = Lekerdezes();

                List<string> Markak = uj.Select(a => a.Marka).ToList();
                ComboBoxFeltoltes(cBMarka, Markak);

                List<string> Modellek = dbautok.Select(a => a.Modell).ToList();
                ComboBoxFeltoltes(cBModell, regiek);
                cBMarka.SelectedItem = kiv;


                
            }
        }

        private void cBEvjarat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                string keres = comboBox.SelectedItem.ToString();

                List<Auto> dbautok = kocsik;
                List<Auto> szurt = new List<Auto>();

                foreach (var item in dbautok)
                {
                    if (item.Evjarat.ToString() == keres)
                    {
                        szurt.Add(item);
                    }
                }

                myListView.ItemsSource = szurt;
                ComboBoxolas(szurt);

                kocsik = szurt;



                var uj = Lekerdezes();

                List<string> Markak = uj.Select(a => a.Marka).ToList();
                ComboBoxFeltoltes(cBMarka, Markak);

            }

        


            
        }

        private void cBUzemanyag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                ComboBoxItem kiv = comboBox.SelectedItem as ComboBoxItem;
                string keres = comboBox.SelectedItem.ToString();


                List<Auto> dbautok = kocsik;
                List<Auto> szurt = new List<Auto>();

                foreach (var item in dbautok)
                {
                    if (item.Uzemanyag.ToString() == keres)
                    {
                        szurt.Add(item);
                    }
                }

                myListView.ItemsSource = szurt;
                ComboBoxolas(szurt);

                kocsik = szurt;
                var uj = Lekerdezes();

                List<string> Markak = uj.Select(a => a.Marka).ToList();
                ComboBoxFeltoltes(cBMarka, Markak);
                cBMarka.SelectedItem = kiv;

            }
        }

        private void cBValto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                ComboBoxItem kiv = comboBox.SelectedItem as ComboBoxItem;
                string keres = comboBox.SelectedItem.ToString();


                List<Auto> dbautok = kocsik;
                List<Auto> szurt = new List<Auto>();

                foreach (var item in dbautok)
                {
                    if (item.Valto.ToString() == keres)
                    {
                        szurt.Add(item);
                    }
                }

                myListView.ItemsSource = szurt;
                ComboBoxolas(szurt);

                kocsik = szurt;
                var uj = Lekerdezes();

                List<string> Markak = uj.Select(a => a.Marka).ToList();
                //  ComboBoxFeltoltes(cBModell, Modellek);
                cBMarka.SelectedItem = kiv;

            }
        }

        private void cBTipus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                ComboBoxItem kiv = comboBox.SelectedItem as ComboBoxItem;
                string keres = comboBox.SelectedItem.ToString();


                List<Auto> dbautok = kocsik;
                List<Auto> szurt = new List<Auto>();

                foreach (var item in dbautok)
                {
                    if (item.Tipus.ToString() == keres)
                    {
                        szurt.Add(item);
                    }
                }

                myListView.ItemsSource = szurt;
                ComboBoxolas(szurt);

                kocsik = szurt;
                var uj = Lekerdezes();

                List<string> Markak = uj.Select(a => a.Marka).ToList();
                ComboBoxFeltoltes(cBMarka, Markak);
                cBMarka.SelectedItem = kiv;

            }
        }

        private void cBUlesek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                ComboBoxItem kiv = comboBox.SelectedItem as ComboBoxItem;
                string keres = comboBox.SelectedItem.ToString();


                List<Auto> dbautok = kocsik;
                List<Auto> szurt = new List<Auto>();

                foreach (var item in dbautok)
                {
                    if (item.UlesekSzama.ToString() == keres)
                    {
                        szurt.Add(item);
                    }
                }

                myListView.ItemsSource = szurt;
                ComboBoxolas(szurt);

                kocsik = szurt;
                var uj = Lekerdezes();

                List<string> Markak = uj.Select(a => a.Marka).ToList();
                ComboBoxFeltoltes(cBMarka, Markak);
                cBMarka.SelectedItem = kiv;

            }
        }

        private void tBAr_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text != "")
            {


                List<Auto> dbautok = kocsik;
                List<Auto> szurt = new List<Auto>();

                foreach (var item in dbautok)
                {
                    if (item.Ar <= Int32.Parse(textbox.Text))
                    {
                        szurt.Add(item);
                    }
                }

                myListView.ItemsSource = szurt;
                ComboBoxolas(szurt);

               // kocsik = szurt;

                var uj = Lekerdezes();

                List<string> Markak = uj.Select(a => a.Marka).ToList();
                ComboBoxFeltoltes(cBMarka, Markak);

                List<string> Modellek = dbautok.Select(a => a.Modell).ToList();
                ComboBoxFeltoltes(cBModell, Modellek);

            }
        }

        private void cBAr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                ComboBoxItem kiv = comboBox.SelectedItem as ComboBoxItem;
                string keres = comboBox.SelectedItem.ToString();


                List<Auto> dbautok = kocsik;
                List<Auto> szurt = new List<Auto>();

                foreach (var item in dbautok)
                {
                    if (item.Ar.ToString() == keres)
                    {
                        szurt.Add(item);
                    }
                }

                myListView.ItemsSource = szurt;
                ComboBoxolas(szurt);

                kocsik = szurt;
                var uj = Lekerdezes();

                List<string> Markak = uj.Select(a => a.Marka).ToList();
                ComboBoxFeltoltes(cBMarka, Markak);
                cBMarka.SelectedItem = kiv;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CarsPage());
        }
    }
}
