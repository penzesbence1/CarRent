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
        public int extraID;
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
                              a.Extraks
                          };

            var elsoAuto = dbautok.FirstOrDefault();

            foreach (var item in elsoAuto.Extraks)
            {
                extraID = item.ExtraID;
            }

            lbKocsi.Content = $"Biztos kiszeretnéd törölni a(z) {elsoAuto?.MarkaNev} {elsoAuto?.Modell} ( {elsoAuto?.Evjarat} ) autót?";


        }

        private void btMegse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditCarPage(kocsiid));
        }

        public class Kolcsonzes
        {
            
        }

        public class Extrak_Kapcsolo
        {
            public int AutoID { get; set; }
            public int ExtraID { get; set; }

            public Autok Auto { get; set; }
            public Extrak Extra { get; set; }
        }

        public class KolcsonzoModel : DbContext
        {
            // other DbSet properties

            public DbSet<Extrak_Kapcsolo> Extrak_Kapcsolos { get; set; }

            // other code
        }

        private void btMegerosit_Click(object sender, RoutedEventArgs e)
        {
            var auto = cn.Autoks.FirstOrDefault(a => a.AutoID == kocsiid);
            var extra = cn.Extraks.FirstOrDefault(e => e.ExtraID == extraID);
            var kolcsonzes = cn.Kolcsonzeseks.FirstOrDefault(k => k.AutoID == kocsiid);

            if (kolcsonzes != null)
            {

                cn.Kolcsonzeseks.Remove(kolcsonzes);
                cn.SaveChanges();
            }
            

            if (auto != null)
            {
                
                cn.Autoks.Remove(auto);
                cn.SaveChanges();
                NavigationService.Navigate(new AdminCarPage());
            }

           

        }
    }
}
