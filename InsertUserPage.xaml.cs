using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
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
    /// Interaction logic for InsertUserPage.xaml
    /// </summary>
    public partial class InsertUserPage : Page
    {
        CarRent.Context.KolcsonzoModel cn;
        public InsertUserPage()
        {
            InitializeComponent();
            cn = new CarRent.Context.KolcsonzoModel();
        }

        private void btMegse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminUserPage());
        }

        // Felhasználónév és jelszó ellenőrzése
        private void btHozzaadas_Click(object sender, RoutedEventArgs e)
        {
            // Ellenőrizzük az e-mail cím formátumát
            string email = tbEmail.Text;
            string username = tbFelhaszlonev.Text;
            string pass = tbJelszo.Text;

            if (IsEmailValid(email))
            {
                // Ellenőrizzük, hogy a felhasználónév és a jelszó meg vannak-e adva
                if (tbFelhaszlonev.Text == "" || tbJelszo.Text == "")
                {
                    lbError.Content = "Adjon meg felhasználónevet és jelszót!";
                }
                else
                {

                    // Ellenőrizzük, hogy a felhasználónév már létezik-e az adatbázisban
                    if (cn.Felhasznaloks.Any(f => f.Felhasznalonev == username))
                    {
                        lbError.Content = "A megadott felhasználónév már létezik!";
                    }
                    else
                    {
                        // Jelszó titkosítása SHA256-algoritmus használatával
                        using (SHA256 sha256Hash = SHA256.Create())
                        {
                            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(pass));
                            StringBuilder builder = new StringBuilder();
                            for (int i = 0; i < bytes.Length; i++)
                            {
                                builder.Append(bytes[i].ToString("x2"));
                            }
                            pass = builder.ToString();
                        }

                        // Új felhasználó létrehozása és hozzáadása az adatbázishoz
                        Felhasznalok ujFelhasznalo = new Felhasznalok
                        {
                            Felhasznalonev = username,
                            Email = email,
                            Jelszo = pass
                        };

                        cn.Felhasznaloks.Add(ujFelhasznalo);
                        cn.SaveChanges();
                        NavigationService.Navigate(new AdminUserPage());
                    }

                }
            }
            else
            {
                lbError.Content = "Hibás email formátum!";
            }
        }


        private static bool IsEmailValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}
