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
    /// Interaction logic for AddCarPage.xaml
    /// </summary>
    public partial class AddCarPage : Page
    {
        public AddCarPage(int id)
        {
            InitializeComponent();

            Label label = FindName("Kocsi") as Label;
            label.Content = "Márka: "+id;
        }

       

        
               
    }
}
