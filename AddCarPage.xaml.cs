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
        public int id;
        public AddCarPage(int id)
        {
            this.id = id;
            InitializeComponent();

            Label label = FindName("Kocsi") as Label;
            label.Content = "Kiválasztott autó: (id) "+id;
            
        }

        public DateTime mettol = DateTime.Parse("1888-01-01");
        public DateTime meddig = DateTime.Parse("1888-01-01");
        TimeSpan kulonbseg;
        private void dpMettol_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // lbMettol.Content =
            // 
            mettol = dpMettol.SelectedDate.Value;

           

        }

        private void dpMeddig_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            meddig = dpMeddig.SelectedDate.Value;

            if (mettol != DateTime.Parse("1888-01-01"))
            {

                if(mettol > meddig)
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
                    int ar = nap * 20000;
                    lbLeiras.Content = $"{id} (id) autó bérlése {nap} napra {ar} forintért.";
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
