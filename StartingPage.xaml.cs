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

namespace CarRent
{
    /// <summary>
    /// Interaction logic for StartingPage.xaml
    /// </summary>
    public partial class StartingPage : Page
    {
        CarRent.Context.KolcsonzoModel cn;
        public StartingPage()
        {
            InitializeComponent();
            cn = new CarRent.Context.KolcsonzoModel();

            var autok2 = from a in cn.Autoks
                         join m in cn.Markaks on a.MarkaID equals m.MarkaID
                         join b in cn.Kedvezmenyeks on a.KedvezmenyID equals b.KedvezmenyID

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
                             b.KedvezmenyErteke
                             
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
                    Kedvezmeny =item.KedvezmenyErteke

                };



                foreach (var extrak in item.Extraks)
                {
                    auto.ExtrakNev.Add(extrak.ExtraNev);
                }

                autok3.Add(auto);


                // Létrehozzuk a bmwAutok listát és a LINQ segítségével szűrjük az autok3 listát
                List<Auto> bmwAutok = autok3.Where(auto => auto.Marka == "BMW" && auto.Kedvezmeny >= 15).ToList();
                List<Auto> vwAutok = autok3.Where(auto => auto.Marka == "Volkswagen" && auto.Kedvezmeny >= 15).ToList();
                List<Auto> fordAutok = autok3.Where(auto => auto.Marka == "Ford" && auto.Kedvezmeny >= 15).ToList();
                List<Auto> hondaAutok = autok3.Where(auto => auto.Marka == "Honda" &&  auto.Kedvezmeny > 20).ToList();
                
                bmwlist.ItemsSource = bmwAutok;
                vwlist.ItemsSource = vwAutok;
                hondalist.ItemsSource = hondaAutok;
                fordlist.ItemsSource = fordAutok;
                
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

            public int Kedvezmeny {  get; set; }
            public List<Extrak> Extrak { get; set; }
            public List<string> ExtrakNev { get; set; }




            public Auto()
            {
                ExtrakNev = new List<string>();
            }
        }

    }
}
