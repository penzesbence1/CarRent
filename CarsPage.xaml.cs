using CarRent.KolcsonzoModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
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
        public List<Kolcsonzes> kolcsonzesek = new List<Kolcsonzes>();
        public List<CheckBoxItem> extrak = new List<CheckBoxItem>();
        public DateTime mettol;
        public DateTime meddig;

        public CarsPage()
        {
            InitializeComponent();


            cn = new CarRent.Context.KolcsonzoModel();


            var autok = Lekerdezes();

            ComboBoxolas(autok);

            kocsik = autok;


            myListView.ItemsSource = autok;

            extrak = (from a in cn.Extraks
                          select new CheckBoxItem
                          {
                              ExtraNev = a.ExtraNev,
                              IsChecked = false
                          }).ToList();


            extrak.Insert(0, new CheckBoxItem { ExtraNev = "Extrák", IsChecked = false });

            
            cbSzurok.ItemsSource = extrak.Distinct();

            cbSzurok.SelectedIndex = 0;



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
          

            // Eredeti kiválasztott elemek mentése
            var selectedMarka = cBMarka.SelectedItem;
            var selectedModell = cBModell.SelectedItem;
            var selectedEvjarat = cBEvjarat.SelectedItem;
            var selectedUzemanyag = cBUzemanyag.SelectedItem;
            var selectedValto = cBValto.SelectedItem;
            var selectedKivitel = cBKivitel.SelectedItem;
            var selectedUlesSzam = cBUles.SelectedItem;
           

            ComboBoxFeltoltes(cBMarka, Markak);
            ComboBoxFeltoltes(cBModell, Modellek);
            ComboBoxFeltoltes(cBEvjarat, Evjaratok);
            ComboBoxFeltoltes(cBUzemanyag, Uzemanyagok);
            ComboBoxFeltoltes(cBValto, Valtok);
            ComboBoxFeltoltes(cBKivitel, Tipusok);
            ComboBoxFeltoltes(cBUles, Ulesszamok);

            if (true)
            {
                if (selectedMarka != null && Markak.Contains(selectedMarka.ToString()))
                {
                    cBMarka.SelectedItem = selectedMarka;
                }
                if (selectedModell != null && Modellek.Contains(selectedModell.ToString()))
                {
                    cBModell.SelectedItem = selectedModell;
                }
                if (selectedEvjarat != null && Evjaratok.Contains((int)selectedEvjarat))
                {
                    cBEvjarat.SelectedItem = selectedEvjarat;
                }
                if (selectedUzemanyag != null && Uzemanyagok.Contains(selectedUzemanyag.ToString()))
                {
                    cBUzemanyag.SelectedItem = selectedUzemanyag;
                }
                if (selectedValto != null && Valtok.Contains(selectedValto.ToString()))
                {
                    cBValto.SelectedItem = selectedValto;
                }
                if (selectedKivitel != null && Tipusok.Contains(selectedKivitel.ToString()))
                {
                    cBKivitel.SelectedItem = selectedKivitel;
                }
                if (selectedUlesSzam != null && Ulesszamok.Contains((int)selectedUlesSzam))
                {
                    cBUles.SelectedItem = selectedUlesSzam;
                }
            }
        }

        public void ComboBoxFeltoltes<T>(ComboBox comboBox, List<T> adatok)
        {

            if (comboBox == null || adatok == null) return; 

            
            comboBox.ItemsSource = adatok.Distinct(); 


        }


        public List<Auto> Lekerdezes()
        {
            var kolcsonzes = from k in cn.Kolcsonzeseks
                               select new
                               {
                                   k.FelhasznaloID,
                                   k.AutoID,
                                   k.Mettol,
                                   k.Meddig
                               };

           

            foreach (var item in kolcsonzes)
            {
                Kolcsonzes kolcs = new Kolcsonzes
                {
                    FelhasznaloId = item.FelhasznaloID,
                    AutoId = item.AutoID,
                    Mettol = item.Mettol,
                    Meddig = item.Meddig
                };

                kolcsonzesek.Add(kolcs);
            }

       

            var autok2 = from a in cn.Autoks
                         join m in cn.Markaks on a.MarkaID equals m.MarkaID
                        
                         select new
                         {
                             a.AutoID,
                             m.MarkaNev,
                             a.Modell,
                             a.Evjarat,
                             a.Uzemanyag,
                             a.Valto,
                              a.Kivitel,
                             a.Ulesekszama,
                             a.Ar,
                             a.Extraks,
                            
                         };


            List<Auto> autok3 = new List<Auto>();

            foreach (var item in autok2)
            {
                Auto auto = new Auto
                {
                    Id = item.AutoID,
                    Marka = item.MarkaNev,
                    Modell = item.Modell,
                    Evjarat = item.Evjarat,
                    Uzemanyag = item.Uzemanyag,
                    Valto = item.Valto,
                    Tipus = item.Kivitel,
                    UlesekSzama = item.Ulesekszama,
                    Ar = item.Ar,
                    Extrak = (List<Extrak>)item.Extraks,
                    

                };

                

            foreach (var extrak in item.Extraks)
            {
                auto.ExtrakNev.Add(extrak.ExtraNev); 
            }

            autok3.Add(auto);
            }

          
            
            return autok3;


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
            
            public List<Extrak> Extrak { get; set; }
            public List<string> ExtrakNev { get; set; }

            


            public Auto()
            {
                ExtrakNev = new List<string>();
            }
        }

        public class Kolcsonzes
        {
            public DateTime Mettol { get; set; }
            public DateTime Meddig { get; set; }
            public int AutoId { get; set; }
            public int FelhasznaloId { get; set; }

        }


        private void cbSzurok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            cbSzurok.SelectedIndex = 0;

        }


        private void cbSzurok_DropDownOpened(object sender, EventArgs e)
        {
            extrak.Remove(extrak.FirstOrDefault(item => item.ExtraNev == "Extrák"));

            cbSzurok.ItemsSource = extrak.Distinct();
        }

        private void cbSzurok_DropDownClosed(object sender, EventArgs e)
        {
            extrak.Insert(0, new CheckBoxItem { ExtraNev = "Extrák", IsChecked = false });

            cbSzurok.ItemsSource = extrak.Distinct();

            cbSzurok.SelectedIndex = 0;
        }

       

        private void myListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Auto selectedCar)
            {
                if(dPMettol.SelectedDate != null || dPMeddig.SelectedDate != null)
                {

                
                int carId = selectedCar.Id;

                NavigationService.Navigate(new AddCarPage(carId, mettol, meddig));
                }
                else
                {

                    lBHiba.Content = "Válasszon dátumot!";
                    lBHiba.Opacity = 1;
                }



            }
        }


        public void Szuresek()
        {

            string keresMarka = cBMarka.SelectedItem?.ToString();
            string keresModell = cBModell.SelectedItem?.ToString();
            string keresEvjarat = cBEvjarat.SelectedItem?.ToString();
            string keresUzemanyag = cBUzemanyag.SelectedItem?.ToString();
            string keresValto = cBValto.SelectedItem?.ToString();
            string keresKivitel = cBKivitel.SelectedItem?.ToString();
            string keresUlesSzam = cBUles.SelectedItem?.ToString();
            DateTime keresMettol;

            if (dPMettol.SelectedDate == null)
            {
                keresMettol = DateTime.Parse("1888-01-01");
            }
            else
            {
                keresMettol = (DateTime)dPMettol.SelectedDate;
            }

            DateTime keresMeddig;
            if (dPMeddig.SelectedDate == null)
            {
                keresMeddig = DateTime.Parse("1888-01-01");
            }
            else
            {
                keresMeddig = (DateTime)dPMeddig.SelectedDate;
            }

           
            var datum = kolcsonzesek.Where(item => item.Meddig < keresMettol || item.Mettol > keresMeddig).ToList();

            teszt.Content = datum.Count;

            var szurt = kocsik.Where(item =>
             (keresMarka == null || item.Marka == keresMarka) &&
             (keresModell == null || item.Modell == keresModell) &&
              (keresEvjarat == null || item.Evjarat.ToString() == keresEvjarat) &&
             (keresUzemanyag == null || item.Uzemanyag == keresUzemanyag) &&
             (keresValto == null || item.Valto == keresValto) &&
              (keresKivitel == null || item.Tipus == keresKivitel) &&
              (keresUlesSzam == null || item.UlesekSzama.ToString() == keresUlesSzam) &&
             (kolcsonzesek.Any(k => k.AutoId == item.Id && k.Meddig < keresMettol) || kolcsonzesek.Any(k => k.AutoId != item.Id) || kolcsonzesek.Any(k => k.AutoId == item.Id && k.Mettol > keresMeddig))
            ).ToList();



            List<string> selectedItems = new List<string>();


            foreach (var comboBoxItem in cbSzurok.Items)
            {

                if (comboBoxItem is CheckBoxItem item && item.IsChecked)
                {

                    selectedItems.Add(item.ExtraNev);
                }
            }


            List<Auto> szurt2 = new List<Auto>();


            if (selectedItems.Count > 0)
            {
                szurt2 = szurt.Where(auto =>
                {

                    return selectedItems.All(kivEx => auto.ExtrakNev != null && auto.ExtrakNev.Any(ex => ex == kivEx));
                }).ToList();

            }
            else
            {
                szurt2 = szurt;
            }


            List<Auto> szurt3 = new List<Auto>();

            int a = 9999999;

            if (tBAr.Text == "" || tBAr.Text == null)
            {

                a = 9999999;
            }
            else
            {
                int.TryParse(tBAr.Text, out a);
            }


            foreach (var item in szurt2)
            {


                if (item.Ar <= a)
                {
                    szurt3.Add(item);
                }
            }




            myListView.ItemsSource = szurt3;
            ComboBoxolas(szurt3);
        }
        private void Szures(object sender, RoutedEventArgs e)
        {
            Szuresek();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CarsPage());
        }






        private void dpMettol_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           
            mettol = dPMettol.SelectedDate.Value;

            
            if (mettol < DateTime.Today)
            {
                mettol = DateTime.Today;
                dPMettol.SelectedDate = mettol;
            }

            if (dPMeddig.SelectedDate != null )
            {
               
                if (mettol > meddig)
                {
                    lBHiba.Content = "A kezdődátum nem lehet korábbi mint a végdátum!";
                    lBHiba.Opacity= 1;

                }
                else
                {
                    mettol = dPMettol.SelectedDate.Value;
                    meddig = dPMeddig.SelectedDate.Value;
                    lBHiba.Opacity= 0;
                    Szuresek();
                }

               
            }

           
        }


        private void dpMeddig_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            

            meddig = dPMeddig.SelectedDate.Value;


            if (meddig < DateTime.Today)
            {
                meddig = DateTime.Today;
                dPMeddig.SelectedDate = meddig;
            }

            if (dPMeddig.SelectedDate != null)
            {

                if (mettol > meddig)
                {
                    lBHiba.Content = "A kezdődátum nem lehet korábbi mint a végdátum!";
                    lBHiba.Opacity = 1;

                }
                else
                {
                    mettol = dPMettol.SelectedDate.Value;
                    meddig = dPMeddig.SelectedDate.Value;
                    lBHiba.Opacity = 0;
                    Szuresek();
                }

            }



        }

        
    }
}
