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
        public int kocsiid;
        public int ara;
        public AddCarPage(int id)
        {
            this.kocsiid = id;
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
           


        }

        public DateTime mettol = DateTime.Parse("1888-01-01");
        public DateTime meddig = DateTime.Parse("1888-01-01");
        TimeSpan kulonbseg;

        
        private void dpMettol_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // lbMettol.Content =
            // 
            mettol = dpMettol.SelectedDate.Value;

            lbLeiras.Content = $"{meddig}";
           if(mettol < DateTime.Today)
            {
                mettol = DateTime.Today;
                dpMettol.SelectedDate = mettol;
            }

            if (meddig != DateTime.Parse("1888-01-01"))
            {
                if (mettol > meddig)
                {
                    lbLeiras.Foreground = Brushes.Red;
                    lbLeiras.Content = "A kezdődátum későbbi mint a záródátum.";
                    btMegerosit.Opacity = 0;
                    btMegse.Opacity = 0;
                }
                else
                {
                    string colorCode = "#FF08568A";
                    BrushConverter converter = new BrushConverter();
                    Brush brush = (Brush)converter.ConvertFromString(colorCode);

                    lbLeiras.Foreground = brush;

                    kulonbseg = meddig - mettol;
                    int nap = kulonbseg.Days + 1;


                    int ar = nap * ara;


                    lbLeiras.Content = $"A kiválasztott autó bérlése {nap} napra {ar} forintért.";
                    btMegerosit.Opacity = 1;
                    btMegse.Opacity = 1;

                }

            }


        }

        private void dpMeddig_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            meddig = dpMeddig.SelectedDate.Value;

            
                if (meddig < DateTime.Today)
                {
                    meddig = DateTime.Today;
                    dpMeddig.SelectedDate = meddig;
                }

            if (mettol != DateTime.Parse("1888-01-01"))
            {
                if (mettol > meddig)
                {
                    lbLeiras.Foreground = Brushes.Red;
                    lbLeiras.Content = "A kezdődátum későbbi mint a záródátum.";
                    btMegerosit.Opacity = 0;
                    btMegse.Opacity = 0;
                }
                else
                {
                    string colorCode = "#FF08568A";
                    BrushConverter converter = new BrushConverter();
                    Brush brush = (Brush)converter.ConvertFromString(colorCode);

                    lbLeiras.Foreground = brush;

                    kulonbseg = meddig - mettol;
                    int nap = kulonbseg.Days + 1;
                    int ar = nap * ara;
                    lbLeiras.Content = $"A kiválasztott autó bérlése {nap} napra {ar} forintért.";
                    btMegerosit.Opacity = 1;
                    btMegse.Opacity = 1;

                }
            }
           

            

        }

        private void btMegse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CarsPage());
        }

       

        private void btMegerosit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RentedPage());
        }
    }
}
