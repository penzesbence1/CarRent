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
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        CarRent.Context.KolcsonzoModel cn;
        public List<Felhasznalok> felhasznalok = new List<Felhasznalok>();
        public int felhasznaloID;
        private dynamic elsoFelhasznalo;
        public EditUserPage(int felhasznaloID)
        {
            this.felhasznaloID = felhasznaloID;

            InitializeComponent();

            cn = new CarRent.Context.KolcsonzoModel();

            var dbFelhasznalok = from f in cn.Felhasznaloks
                                 where f.FelhasznaloID == felhasznaloID
                                 select new
                                 {
                                     f.FelhasznaloID,
                                     f.Felhasznalonev,
                                     f.Email,
                                     f.Jelszo
                                 };
            
            elsoFelhasznalo = dbFelhasznalok.FirstOrDefault();

            felhasznalok.Add(new Felhasznalok
            {
                FelhasznaloID = elsoFelhasznalo.FelhasznaloID,
                Felhasznalonev = elsoFelhasznalo.Felhasznalonev,
                Email = elsoFelhasznalo.Email,
                Jelszo = elsoFelhasznalo.Jelszo
            });

            lbKiiras.Content = "(" + elsoFelhasznalo.FelhasznaloID + ") " + elsoFelhasznalo.Felhasznalonev;
            tbFelhasznalonev.Text = elsoFelhasznalo.Felhasznalonev;

        }

        private void btModositas_Click(object sender, RoutedEventArgs e)
        {
                string ujFelhasznalonev = tbFelhasznalonev.Text;

                var felhasznalo = cn.Felhasznaloks.FirstOrDefault(f => f.FelhasznaloID == felhasznaloID);
                if (felhasznalo.Felhasznalonev != ujFelhasznalonev)
                {
                    NavigationService.Navigate(new UsernameChangeConfirmPage(felhasznaloID, ujFelhasznalonev, felhasznalo.Felhasznalonev));
                }
            
        }

        private void btTorles_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FelhasznaloDeleteConfirmPage(felhasznaloID));
        }

        public class Felhasznalo
        {
            public int FelhasznaloID { get; set; }
            public string Felhasznalonev { get; set; }
            public string Email { get; set; }
            public string Jelszo { get; set; }

        }

    }
}
