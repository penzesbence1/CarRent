using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRent
{
   
    public partial class HomePage : Page
    {
        CarRent.Context.KolcsonzoModel cn;
        int activeuser;
        public HomePage(int user)
        {
            cn = new CarRent.Context.KolcsonzoModel();
            activeuser = user;

            InitializeComponent();
            
            Main.Content = new StartingPage();

            var felhasznalo =  cn.Felhasznaloks.FirstOrDefault(u => u.FelhasznaloID == user);

            lbUser.Content = felhasznalo.Felhasznalonev.ToString();
        }

        private void MyLabel_MouseEnter(object sender, MouseEventArgs e)
        {

            if (sender is System.Windows.Controls.Label)
            {
                System.Windows.Controls.Label label = sender as System.Windows.Controls.Label;
                label.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0CA5DB")); // Világosabb árnyalatú szín beállítása
            }                                                                                     // Sötétebb háttérszín beállítása

        }
       

        private void MyLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Label)
            {
                System.Windows.Controls.Label label = sender as System.Windows.Controls.Label;
                label.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF08568A")); // Világosabb árnyalatú szín beállítása
            }


            
        }
       


        private void Megnyit(object sender, MouseEventArgs e)
        {
            
                System.Windows.Controls.Label label = sender as System.Windows.Controls.Label;

            string Name = label.Name;
            switch (Name)
            {
                case "lbUser":
                    Main.Content = new UserPage();
                    break;


                case "lbFooldal":
                    Main.Content = new StartingPage();
                    break;

                case "lbAuto":
                    Main.Content = new CarsPage(activeuser);
                    break;

                case "lbRendeles":
                    
                    Main.Content = new RentedPage(activeuser);
                    break;

                case "lbLogout":
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.Height = 100;
                    mainWindow.MinWidth = 100;
                    mainWindow.MinHeight = 100;
                    mainWindow.mainFrame.Navigate(new LoginPage());
                    break;


            }
            
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new UserPage();
        }


       


    }
}
