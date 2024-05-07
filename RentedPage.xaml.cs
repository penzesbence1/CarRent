using CarRent.KolcsonzoModel;
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
using static CarRent.CarsPage;

namespace CarRent
{
    /// <summary>
    /// Interaction logic for RentedPage.xaml
    /// </summary>
    public partial class RentedPage : Page
    {
        CarRent.Context.KolcsonzoModel cn;
        int activeuser = 0;
        public RentedPage(int userid)
        {
            cn = new CarRent.Context.KolcsonzoModel();
            InitializeComponent();


            activeuser = userid;



          
            
            
            Lekerdezes();
        }





        public List<Auto> Lekerdezes()
        {
            var kolcsonzes = from k in cn.Kolcsonzeseks where k.FelhasznaloID == activeuser
                             select new
                             {
                                 k.FelhasznaloID,
                                 k.AutoID,
                                 k.Mettol,
                                 k.Meddig
                             };






            var autok2 = from a in cn.Autoks
                         join m in cn.Markaks on a.MarkaID equals m.MarkaID
                         join k in kolcsonzes on a.AutoID equals k.AutoID into kocsik
                         from kocsi in kocsik
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
                             Kikolcsonzve = true, // Minden autóhoz tartozik kölcsönzés, ha benne van a kolcsonzesek listában
                            
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

            myListView.ItemsSource = autok3;

            return autok3;

         
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


    }
}
