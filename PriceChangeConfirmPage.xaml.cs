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
    /// Interaction logic for PriceChangeConfirmPage.xaml
    /// </summary>
    public partial class PriceChangeConfirmPage : Page
    {
        public int kocsiid = 0;
        public int ujAr = 0;
        public int regiAr = 0;
        CarRent.Context.KolcsonzoModel cn;
        public PriceChangeConfirmPage(int kocsiid, int ujAr, int regiAr)
        {
            InitializeComponent();
            this.kocsiid = kocsiid;
            this.ujAr = ujAr;
            this.regiAr = regiAr;

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

            lbAr.Content = $"Biztos szeretnéd módosítani az árat {regiAr}-ról/-ről  {ujAr}-ra/-re?";


        }

        private void btMegse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditCarPage(kocsiid));
        }



        private void btMegerosit_Click(object sender, RoutedEventArgs e)
        {
                var auto = cn.Autoks.FirstOrDefault(a => a.AutoID == kocsiid);
                if (auto.Ar != ujAr)
                {
                    auto.Ar = ujAr;
                    cn.SaveChanges();
                    NavigationService.Navigate(new AdminCarPage());
                }

        }
    }
}
