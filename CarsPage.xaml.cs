﻿using System;
using System.Collections;
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
    /// Interaction logic for CarsPage.xaml
    /// </summary>
    public partial class CarsPage : Page
    {
        public CarsPage()
        {
            InitializeComponent();


            var autok = new List<Auto>
            {

                new Auto { Marka = "BMW", Modell = "X5", Uzemanyag = "Benzin", Valto = "Automata", Tipus = "SUV", UlesekSzama = 5, Ar = 5000000, Kedvezmeny = "20%" }, 
                new Auto { Marka = "Toyota", Modell = "Corolla", Uzemanyag = "Dizel", Valto = "Manuális", Tipus = "Furgon", UlesekSzama = 5, Ar = 3000000 },
                // Egyéb autók...
            };

            for (int i = 0; i < 100; i++) {
                autok.Add(new Auto { Marka = "BMW", Modell = "X5", Uzemanyag = "Benzin", Valto = "Automata", Tipus = "SUV", UlesekSzama = 5, Ar = 5000000 });
            }
            myListView.ItemsSource = autok;

            List<string> Markak = new List<string>();

            foreach (var item in autok)
            {
                Markak.Add(item.Marka);
            }



            List<string> Szurt1 = new List<string>();


            Szurt1 = Markak.Distinct().ToList();


            List<string> SzurtModell = new List<string>();
            List<string> SzurtUzemanyag = new List<string>();
            List<string> SzurtValto = new List<string>();
            List<string> SzurtTipus = new List<string>();
            List<string> SzurtUlesekSzama = new List<string>();

           

            List<List<string>> Szurtek = new List<List<string>>();

            
            Szurtek.Add(Szurt1);

            
            foreach (var item in Szurtek)
            {
                foreach (var item1 in item)
                {
                    cBMarka.Items.Add(item1);
                }
                
            }

          



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


        public class Auto
        {
            public string Marka { get; set; }
            public string Modell { get; set; }
            public string Uzemanyag { get; set; }
            public string Valto { get; set; }
            public string Tipus { get; set; }
            public int UlesekSzama { get; set; }
            public decimal Ar { get; set; }
            public string Kedvezmeny { get; set; }
        }

    }
}
