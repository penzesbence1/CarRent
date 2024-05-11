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
    /// Interaction logic for InsertCarPage.xaml
    /// </summary>
    public partial class InsertCarPage : Page
    {
        public InsertCarPage()
        {
            InitializeComponent();
        }



        private void btMegse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminCarPage());
        }

        private void btHozzaadas_Click(object sender, RoutedEventArgs e)
        {
            int evjarat, ulesekszama, ar, kedvezmeny;
            int i,j,k,l;
            string marka, modell, valto, uzemanyag, kivitel;

            if(!int.TryParse(tbEvjarat.Text,out i))
            {
                lbError.Content = "Az évjárat csak számot tartalmazhat!";
            }
            else
            {
                if(i > DateTime.Now.Year || i < 1800)
                {
                    lbError.Content = "Az évjárat nem megfelelő!";
                }
                else
                {
                    evjarat = i;
                }

            }

            if (!int.TryParse(tbUlesekszama.Text, out j))
            {
                lbError.Content = "Az ülések száma csak számot tartalmazhat!";
            }
            else
            {
                if (j > 10 || i < 1)
                {
                    lbError.Content = "Az ülések száma nem megfelelő!";
                }
                else
                {
                    ulesekszama = j;
                }

            }

            if (!int.TryParse(tbAr.Text, out k))
            {
                lbError.Content = "Az ár csak számot tartalmazhat!";
            }
            else
            {
                if (k < 1)
                {
                    lbError.Content = "Az ár nem megfelelő!";
                }
                else
                {
                    ar = k;
                }

            }

            if (!int.TryParse(tbKedvezmeny.Text, out l))
            {
                lbError.Content = "A kedvezmeny csak számot tartalmazhat!";
            }
            else
            {
                if (l > 100 || l < 1)
                {
                    lbError.Content = "A kedvezmeny nem megfelelő!";
                }
                else
                {
                    kedvezmeny = k;
                }

            }



        }
    }
}
