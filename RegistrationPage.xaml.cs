using CarRent.KolcsonzoModel;
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

namespace CarRent
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        CarRent.Context.KolcsonzoModel cn;
        public RegistrationPage()
        {
            InitializeComponent();
            cn = new CarRent.Context.KolcsonzoModel();

            txJelszo.Content = "Jelszó";
        }




        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && (textBox.Text == "Email" || textBox.Text == "Felhasználónév" || textBox.Text == "Jelszó"))
            {
                textBox.Text = ""; // Állítsd át a TextBox szövegét üresre
                textBox.Opacity = 1.0; // Visszaállítsd az átlátszóságot, ha szükséges
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                switch (textBox.Name)
                {
                    case "textBox1":
                        textBox.Text = "Felhasználónév";
                        break;
                    case "textBox2":
                        textBox.Text = "Jelszó";
                        break;
                    case "textBox3":
                        textBox.Text = "Email";
                        break;
                }

                textBox.Opacity = 0.5;

            }
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            if (label != null)
            {
                label.FontWeight = FontWeights.Bold; // Állítsd át a vastagságot vastagra
            }
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            if (label != null)
            {
                label.FontWeight = FontWeights.Normal; // Állítsd vissza a normál vastagságot
            }
        }
        private void Label_Click(object sender, MouseButtonEventArgs e)
        {
            // Hozzáférés a mainFrame elemhez az Application.Current.MainWindow segítségével
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.mainFrame.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
        }




        // Felhasználónév és jelszó ellenőrzése
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Ellenőrizzük az e-mail cím formátumát
            string email = textBox3.Text;
            string username = textBox1.Text;
            string pass = textBox2.Password;

            if (IsEmailValid(email))
            {
                // Ellenőrizzük, hogy a felhasználónév és a jelszó meg vannak-e adva
                if (textBox1.Text == "Felhasználónév" || txJelszo.Content == "Jelszó")
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

                        
                        int userid = ujFelhasznalo.FelhasznaloID;
                       


                        var mainWindow = (MainWindow)Application.Current.MainWindow;
                        mainWindow.Width = 1000; // ablak szélességének beállítása 1000-re
                        mainWindow.Height = 650;
                        mainWindow.MinWidth = 800;
                        mainWindow.MinHeight = 500;
                        NavigationService.Navigate(new HomePage(userid));
                    }

                }
            }
            else lbError.Content = "Hibás email formátum!";

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
    



    private void textBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            
            txJelszo.Content = "";
            textBox2.Opacity = 1;

        }

        private void textBox2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBox2.Password == "")
            {
                textBox2.Opacity = 0.5;
                txJelszo.Content = "Jelszó";
            }
           
        }
    }
}
