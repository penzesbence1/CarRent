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
    /// Interaction logic for ForgetpassPage.xaml
    /// </summary>
    public partial class ForgetpassPage : Page
    {
        public ForgetpassPage()
        {
            InitializeComponent();
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

    }
}
