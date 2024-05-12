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
    /// Interaction logic for UsernameChangeConfirmPage.xaml
    /// </summary>
    public partial class UsernameChangeConfirmPage : Page
    {
        public int felhasznaloID = 0;
        public string ujFelhasznalonev = "";
        public string regiFelhasznalonev = "";
        CarRent.Context.KolcsonzoModel cn;
        public UsernameChangeConfirmPage(int felhasznaloID, string ujFelhasznalonev, string regiFelhasznalonev)
        {
            InitializeComponent();
            this.felhasznaloID = felhasznaloID;
            this.ujFelhasznalonev = ujFelhasznalonev;
            this.regiFelhasznalonev = regiFelhasznalonev;

            cn = new CarRent.Context.KolcsonzoModel();


            var dbfelhasznalok = from f in cn.Felhasznaloks
                                 where f.FelhasznaloID == felhasznaloID
                                 select new
                                  {
                                      f.FelhasznaloID,
                                      f.Felhasznalonev
                                      
                                  };

            var elsoFelhasznalo = dbfelhasznalok.FirstOrDefault();

            lbFelhasznalo.Content = $"Biztos szeretnéd módosítani a felhasználónevét {regiFelhasznalonev}-ról/-ről  {ujFelhasznalonev}-ra/-re?";


        }

        private void btMegse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditUserPage(felhasznaloID));
        }



        private void btMegerosit_Click(object sender, RoutedEventArgs e)
        {
            var felhasznalo = cn.Felhasznaloks.FirstOrDefault(f => f.FelhasznaloID == felhasznaloID);
            if (felhasznalo.Felhasznalonev != ujFelhasznalonev)
            {
                felhasznalo.Felhasznalonev = ujFelhasznalonev;
                cn.SaveChanges();
                NavigationService.Navigate(new AdminUserPage());
            }

        }
    }
}
