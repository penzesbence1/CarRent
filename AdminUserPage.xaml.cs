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
    /// Interaction logic for AdminUserPage.xaml
    /// </summary>
    public partial class AdminUserPage : Page
    {
        CarRent.Context.KolcsonzoModel cn;
        public string MarkaS, ModellS, EvjaratS;
        public List<Felhasznalok> felhasznalok = new List<Felhasznalok>();
        public AdminUserPage()
        {
            InitializeComponent();
            cn = new CarRent.Context.KolcsonzoModel();

            var felhasznaloLekerdekes = Lekerdezes();

            myListView.ItemsSource = felhasznaloLekerdekes;

        }

        public List<Felhasznalok> Lekerdezes()
        {
           
            var dbFelhasznalok = from f in cn.Felhasznaloks
                                select new
                                {
                                    f.FelhasznaloID,
                                    f.Felhasznalonev,
                                    f.Email,
                                    f.Jelszo
                                };


            List<Felhasznalok> felhasznalok = new List<Felhasznalok>();

            foreach (var item in dbFelhasznalok)
            {
                Felhasznalok felhasznalo = new Felhasznalok
                {
                    FelhasznaloID = item.FelhasznaloID,
                    Felhasznalonev = item.Felhasznalonev,
                    Email = item.Email,
                    Jelszo  = item.Jelszo

                };

                  felhasznalok.Add(felhasznalo);
            }



            return felhasznalok;


        }

        private void myListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Felhasznalok selectedFelhasznalo)
            {
                int felhasznaloID = selectedFelhasznalo.FelhasznaloID;
                NavigationService.Navigate(new EditUserPage(felhasznaloID));

            }
        }

        private void btHozzaadas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InsertUserPage());
        }
    }
}
