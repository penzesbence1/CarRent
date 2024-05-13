using CarRent.KolcsonzoModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        CarRent.Context.KolcsonzoModel cn;
        public LoginPage()
        {

            
            cn = new CarRent.Context.KolcsonzoModel();


            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Height = 600;
            mainWindow.Width = 400;

            mainWindow.MinHeight = 525;
            mainWindow.MinWidth = 400;
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && (textBox.Text == "Email" || textBox.Text == "Felhasználónév" || textBox.Text == "Jelszó"))
            {
                
                    textBox.Text = ""; 
                
                textBox.Opacity = 1.0; 
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "" )
            {
                switch (textBox.Name)
                {
                    case "textBox1": textBox.Text = "Felhasználónév";
                        break;
                    case "textBox2":
                        textBox.Text = "Jelszó";
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
            mainWindow.mainFrame.Navigate(new Uri("RegistrationPage.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            var mainWindow = (MainWindow)Application.Current.MainWindow;


            string user = textBox1.Text;
            string pass = textBox2.Password;

            static string HashPass(string password)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }

            pass = HashPass(pass);

            string adminpass = HashPass("admin");



           // lbError.Content = user + " " + pass+ " "+adminpass;

            if (user == "admin" && pass == adminpass)
            {
                mainWindow.Width = 1000;
                mainWindow.Height = 650;
                mainWindow.MinWidth = 800;
                mainWindow.MinHeight = 500;
                mainWindow.mainFrame.Navigate(new AdminPage(user));
            }
            else
            {

                var felhasznalok = (from f in cn.Felhasznaloks
                                   where f.Felhasznalonev == user && f.Jelszo == pass
                                   select new
                                   {
                                       f.FelhasznaloID
                                   }).FirstOrDefault();


                if (felhasznalok != null)
                {
                    
                    mainWindow.Width = 1000; // ablak szélességének beállítása 1000-re
                    mainWindow.Height = 650;
                    mainWindow.MinWidth = 800;
                    mainWindow.MinHeight = 500;
                    // Felhasználó megtalálva
                    mainWindow.mainFrame.Navigate(new HomePage(felhasznalok.FelhasznaloID));

                    
                }
                else
                {
                    textBox1.Opacity = 0.5;
                    textBox1.Text="Felhasználónév";
                    textBox2.Clear();
                    textBox2.Opacity = 0.5;
                    txJelszo.Content = "Jelszó";
                    

                    // Felhasználó nem található
                    lbError.Content = "Hibás felhasználónév vagy jelszó!";
                }

                
            }
            
        }

        
        private void MouseClick(object sender, MouseButtonEventArgs e)
        {

            MainWindow mainWindow2 = (MainWindow)Application.Current.MainWindow;
            mainWindow2.mainFrame.Navigate(new Uri("ForgetpassPage.xaml", UriKind.Relative));
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

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            button.Style = null;
            string hexColor = "#FF0000"; 

            
            Color color = (Color)ColorConverter.ConvertFromString(hexColor);
            SolidColorBrush brush = new SolidColorBrush(color);

            // Háttérszín beállítása
            button.Background = brush;
            
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;

            Style style = new Style(typeof(Button));
            
            button.MouseEnter += (sender, e) => VisualStateManager.GoToElementState(button, "Normal", true);

   
         


            string hexColor = "#FF01266C";


            Color color = (Color)ColorConverter.ConvertFromString(hexColor);
            SolidColorBrush brush = new SolidColorBrush(color);

            // Háttérszín beállítása
            button.Background = brush;

            
        }
    }
}
