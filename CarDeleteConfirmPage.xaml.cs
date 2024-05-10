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
using Microsoft.EntityFrameworkCore;

namespace CarRent
{
    /// <summary>
    /// Interaction logic for CarDeleteConfirmPage.xaml
    /// </summary>
    public partial class CarDeleteConfirmPage : Page
    {
        public int kocsiid;
        CarRent.Context.KolcsonzoModel cn;
        public CarDeleteConfirmPage(int kocsiid)
        {
            InitializeComponent();
            this.kocsiid = kocsiid;

            cn = new CarRent.Context.KolcsonzoModel();


            var dbautok = from a in cn.Autoks
                          join m in cn.Markaks on a.MarkaID equals m.MarkaID
                          where a.AutoID == kocsiid
                          select new
                          {
                              a.AutoID,
                              m.MarkaNev,
                              a.Modell,
                              a.Evjarat,
                          };

            var elsoAuto = dbautok.FirstOrDefault();

            lbKocsi.Content = $"Biztos kiszeretnéd törölni a(z) {elsoAuto?.MarkaNev} {elsoAuto?.Modell} ( {elsoAuto?.Evjarat} ) autót?";


        }

        private void btMegse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditCarPage(kocsiid));
        }



        private void btMegerosit_Click(object sender, RoutedEventArgs e)
        {
            var auto = cn.Autoks.FirstOrDefault(a => a.AutoID == kocsiid);
            var kolcsonzes = cn.Kolcsonzeseks.FirstOrDefault(k => k.AutoID == kocsiid);

            if (auto != null)
            {
                // Töröld az autót az adatbázisból
                //cn.Kolcsonzeseks.Remove(kolcsonzes);
                cn.Autoks.Remove(auto);

                // Mentsd el a változtatásokat az adatbázisban
                cn.SaveChanges();

                // Visszatérhetsz az AdminCarPage-re vagy bármely más oldalra, amit meg szeretnél jeleníteni
                NavigationService.Navigate(new AdminCarPage());
            }

           

        }
    }
}
