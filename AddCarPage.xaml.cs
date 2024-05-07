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
    /// Interaction logic for AddCarPage.xaml
    /// </summary>
    public partial class AddCarPage : Page
    {

        CarRent.Context.KolcsonzoModel cn;

        public int activeuser;
        public int kocsiid;
        public int ara;
        public DateTime mettol;
        public DateTime meddig;
        public AddCarPage(int userid, int kocsiid, DateTime mettolx, DateTime meddigx)
        {
            activeuser = userid;
            mettol = mettolx;

            meddig = meddigx;


            this.kocsiid = kocsiid;


            InitializeComponent();

            Label label = FindName("Kocsi") as Label;

            
            cn = new CarRent.Context.KolcsonzoModel();


            var dbautok = from a in cn.Autoks
                          join m in cn.Markaks on a.MarkaID equals m.MarkaID where a.AutoID == kocsiid
                          select new
                          {
                              a.AutoID,
                              m.MarkaNev,
                              a.Modell,
                              a.Evjarat,
                              a.Ar
                          };

           var elsoAuto = dbautok.FirstOrDefault();

           ara = elsoAuto.Ar;

           label.Content = $"Kiválasztott autó: {elsoAuto?.MarkaNev} { elsoAuto?.Modell} ( {elsoAuto?.Evjarat} )";

            TimeSpan kulonbseg;

            kulonbseg = meddig - mettol;
            int nap = kulonbseg.Days + 1;


            int ar = nap * ara;

            lbMettol.Content = $"Ettől: {mettol.Year}. {mettol.Month.ToString("00")}. {mettol.Day.ToString("00")}. ";

            lbMeddig.Content = $"Eddig: {meddig.Year}. {meddig.Month.ToString("00")}. {meddig.Day.ToString("00")}. ";
            lbLeiras.Content = $"A kiválasztott autó bérlése {nap} napra {ar} forintért.";
           

        }

 

        private void btMegse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CarsPage(activeuser));
        }

       

        private void btMegerosit_Click(object sender, RoutedEventArgs e)
        {

          
            Kolcsonzesek kolcs = new Kolcsonzesek
            {
               
                FelhasznaloID = activeuser,
                AutoID = kocsiid,   
                Mettol = mettol,
                Meddig = meddig


            };

            cn.Kolcsonzeseks.Add(kolcs);
            cn.SaveChanges();

            NavigationService.Navigate(new RentedPage(activeuser));
        }
    }
}
