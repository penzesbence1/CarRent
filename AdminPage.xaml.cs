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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage(string user)
        {
            InitializeComponent();
            lbUser.Content = user;
            Main.Content = new StartingPage();
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
                    Main.Content = new AdminCarPage();
                    break;

                case "lbRendeles":
                    Main.Content = new RentedPage(4);
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
