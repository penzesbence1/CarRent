using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
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

        private void myLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.mainFrame.Navigate(new Uri("CarsPage.xaml", UriKind.Relative));

        }

        private void Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.mainFrame.Navigate(new Uri("UserPage.xaml", UriKind.Relative));

        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.mainFrame.Navigate(new Uri("UserPage.xaml", UriKind.Relative));

        }
        private void Autok(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.mainFrame.Navigate(new Uri("CarsPage.xaml", UriKind.Relative));

        }


    }
}
