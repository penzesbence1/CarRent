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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        CarRent.Context.KolcsonzoModel cn;
        public List<Felhasznalok> felhasznalok = new List<Felhasznalok>();
        public int felhasznaloID;
        private dynamic elsoFelhasznalo;
        public UserPage(int felhasznaloID)
        {
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

            lbEmail.Content = "Email: " + felhasznalok[0].Email;
            lbNev.Content = "Név: " + felhasznalok[0].Felhasznalonev;
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
