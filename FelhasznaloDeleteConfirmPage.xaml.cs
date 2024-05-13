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
    /// Interaction logic for FelhasznaloDeleteConfirmPage.xaml
    /// </summary>
    public partial class FelhasznaloDeleteConfirmPage : Page
    {
        public int kolcsonzesID;
        public int felhasznaloID;
        CarRent.Context.KolcsonzoModel cn;
        public FelhasznaloDeleteConfirmPage(int felhasznaloID)
        {
            InitializeComponent();
            this.felhasznaloID = felhasznaloID;

            cn = new CarRent.Context.KolcsonzoModel();

            var dbfelhasznalok = from f in cn.Felhasznaloks
                                 where f.FelhasznaloID == felhasznaloID
                                 select new
                                  {
                                      f.FelhasznaloID,
                                      f.Felhasznalonev
                                  };

            var elsoFelhasznalo = dbfelhasznalok.FirstOrDefault();
            lbKocsi.Content = $"Biztos kiszeretnéd törölni a(z) ({elsoFelhasznalo?.FelhasznaloID}) {elsoFelhasznalo?.Felhasznalonev} felhasználót?";


        }

        private void btMegse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditUserPage(felhasznaloID));
        }

        private void btMegerosit_Click(object sender, RoutedEventArgs e)
        {
            var felhasznalo = cn.Felhasznaloks.FirstOrDefault(f => f.FelhasznaloID == felhasznaloID);
            var kolcsonzes = cn.Kolcsonzeseks.FirstOrDefault(k => k.FelhasznaloID == felhasznaloID);

            if (kolcsonzes != null)
            {

                cn.Kolcsonzeseks.Remove(kolcsonzes);
                cn.SaveChanges();
            }


            if (felhasznalo != null)
            {

                cn.Felhasznaloks.Remove(felhasznalo);
                cn.SaveChanges();
                NavigationService.Navigate(new AdminUserPage());
            }


        }
        }
    }
