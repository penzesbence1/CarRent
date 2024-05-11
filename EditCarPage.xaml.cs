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
using CarRent.KolcsonzoModel;

namespace CarRent
{
    /// <summary>
    /// Interaction logic for EditCarPage.xaml
    /// </summary>
    public partial class EditCarPage : Page
    {
        CarRent.Context.KolcsonzoModel cn;
        public List<Auto> Kocsik = new List<Auto>();
        public int kocsiid;
        private dynamic elsoAuto;
        public EditCarPage(int kocsiid)
        {
            this.kocsiid = kocsiid;

            InitializeComponent();

            cn = new CarRent.Context.KolcsonzoModel();

            var dbautok = from a in cn.Autoks
                          join m in cn.Markaks on a.MarkaID equals m.MarkaID
                          join k in cn.Kedvezmenyeks on a.KedvezmenyID equals k.KedvezmenyID
                          where a.AutoID == kocsiid
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
                              k.KedvezmenyErteke,
                              a.Extraks
                          };
            /*var dbextrak = from e in cn.Extraks
                           join cn.
                           select new
                           {
                               e.ExtraNev
                           };*/
            elsoAuto = dbautok.FirstOrDefault();
            string extrak = "";

            foreach (var item in elsoAuto.Extraks)
            {
                 extrak += item.ExtraNev + "\n";
            }
            


            Kocsik.Add(new Auto
            {
                Id = elsoAuto.AutoID,
                Marka = elsoAuto.MarkaNev,
                Modell = elsoAuto.Modell,
                Evjarat = elsoAuto.Evjarat,
                Uzemanyag = elsoAuto.Uzemanyag,
                Valto = elsoAuto.Valto,
                Tipus = elsoAuto.Kivitel,
                UlesekSzama = elsoAuto.Ulesekszama,
                Ar = elsoAuto.Ar,
                Kedvezmeny = elsoAuto.KedvezmenyErteke,
                ExtrakNev = extrak
            });

            lbKiiras.Content = elsoAuto.MarkaNev + " " + elsoAuto.Modell + " ("+ elsoAuto.Evjarat + ")";
            tbAr.Text = elsoAuto.Ar+"";

        }

        private void btModositas_Click(object sender, RoutedEventArgs e)
        {
            int ujAr;

            // Próbáld meg konvertálni az Ar property értékét integer-é
            if (int.TryParse(tbAr.Text, out ujAr))
            {
                var auto = cn.Autoks.FirstOrDefault(a => a.AutoID == kocsiid);
                if (auto.Ar != ujAr)
                {
                    NavigationService.Navigate(new PriceChangeConfirmPage(kocsiid,ujAr,auto.Ar));
                }
            }
            else
            {
                // Ha nem sikerült a konvertálás, kezelheted az esetleges hibát, például hibaüzenet megjelenítésével
                MessageBox.Show("Hibás ár formátum.");
            }
        }

        private void btTorles_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CarDeleteConfirmPage(kocsiid));
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
            public int Kedvezmeny { get; set; }
            public List<Extrak> Extrak { get; set; }
            public string ExtrakNev { get; set; }
        }

        }
    }
