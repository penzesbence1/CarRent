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
using CarRent.KolcsonzoModel;
using Microsoft.EntityFrameworkCore;

namespace CarRent
{
    /// <summary>
    /// Interaction logic for InsertCarPage.xaml
    /// </summary>
    public partial class InsertCarPage : Page
    {
        CarRent.Context.KolcsonzoModel cn;
        public string marka, modell, valto, uzemanyag, kivitel;
        public int evjarat, ulesekszama, ar, kedvezmeny, markaid, kedvezmenyid;
        public InsertCarPage()
        {
            cn = new CarRent.Context.KolcsonzoModel();
            InitializeComponent();

        }



        private void btMegse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminCarPage());
        }

        private void btHozzaadas_Click(object sender, RoutedEventArgs e)
        {
            int i,j,k,l;
            


            if (tbMarka.Text != "")
            {
                if (tbModell.Text != "")
                {
                    if (tbEvjarat.Text != "" && int.TryParse(tbEvjarat.Text, out i) && i <= DateTime.Now.Year && i >= 1800)
                    {
                        if (tbValto.Text != "")
                        {
                            if (tbUzemanyag.Text != "")
                            {
                                if (tbUlesekszama.Text != "" && int.TryParse(tbUlesekszama.Text, out j) && j <= 10 && j >= 1)
                                {
                                    if (tbKivitel.Text != "")
                                    {
                                        if (tbAr.Text != "" && int.TryParse(tbAr.Text, out k) && k >= 1 && k <= 1000000)
                                        {
                                            if (tbKedvezmeny.Text != "" && int.TryParse(tbKedvezmeny.Text, out l) && l <= 100 && l >= 0)
                                            {
                                                var dbmarkak = from au in cn.Autoks
                                                               join m in cn.Markaks on au.MarkaID equals m.MarkaID
                                                               where m.MarkaNev == tbMarka.Text
                                                               select new
                                                               {
                                                                   m.MarkaID
                                                               };
                                                var elsoMarka = dbmarkak.FirstOrDefault();
                                                if (elsoMarka != null) markaid = elsoMarka.MarkaID;
                                                else
                                                {

                                                    using (var transaction = cn.Database.BeginTransaction())
                                                    {
                                                        try
                                                        {
                                                            markaid = cn.Markaks.Max(m => m.MarkaID) + 1;
                                                            cn.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT dbo.Markak ON; INSERT INTO dbo.Markak (Marka_ID, Marka_nev) VALUES ({markaid}, '{tbMarka.Text}'); SET IDENTITY_INSERT dbo.Markak OFF;");
                                                            transaction.Commit();
                                                            
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            transaction.Rollback();
                                                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                                        }
                                                    }
                                                }
                                               
                                                l = int.Parse(tbKedvezmeny.Text);
                                                var dbkedvezmenyek = from ke in cn.Kedvezmenyeks
                                                                     join aut in cn.Autoks on ke.KedvezmenyID equals aut.KedvezmenyID
                                                                     where ke.KedvezmenyErteke == l
                                                                     select new
                                                                     {
                                                                         ke.KedvezmenyID,
                                                                     };
                                                var elsoKedvezmeny = dbkedvezmenyek.FirstOrDefault();
                                                if (elsoKedvezmeny != null) kedvezmenyid = elsoKedvezmeny.KedvezmenyID;
                                                else
                                                {
                                                    using (var transaction = cn.Database.BeginTransaction())
                                                    {
                                                        try
                                                        {
                                                            kedvezmenyid = cn.Kedvezmenyeks.Max(k => k.KedvezmenyID) + 1;
                                                            cn.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT dbo.Kedvezmenyek ON; INSERT INTO dbo.Kedvezmenyek (Kedvezmeny_ID, Kedvezmeny_erteke) VALUES ({kedvezmenyid}, '{tbKedvezmeny.Text}'); SET IDENTITY_INSERT dbo.Kedvezmenyek OFF;");
                                                            transaction.Commit();
                                                            
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            transaction.Rollback();
                                                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                                        }
                                                    }
                                                }
                                                

                                                evjarat = i;
                                                ulesekszama = j;
                                                ar = k;
                                                //kedvezmeny = l;
                                                //marka = tbMarka.Text;
                                                modell = tbModell.Text;
                                                valto = tbValto.Text;
                                                uzemanyag = tbUzemanyag.Text;
                                                kivitel = tbKivitel.Text;
                                                

                                                lbError.Content = "";

                                                using (var transaction = cn.Database.BeginTransaction())
                                                {
                                                    try
                                                    {
                                                        int autoId = cn.Autoks.Max(a => a.AutoID) + 1;
                                                        cn.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT dbo.Autok ON; INSERT INTO dbo.Autok (Auto_ID, Marka_ID, Modell, Evjarat, Valto, Uzemanyag, Ulesekszama, Kivitel, Ar, Kedvezmeny_ID) VALUES ({autoId}, {markaid}, '{modell}', {evjarat}, '{valto}', '{uzemanyag}', {ulesekszama}, '{kivitel}', {ar}, {kedvezmenyid}); SET IDENTITY_INSERT dbo.Autok OFF;");
                                                        transaction.Commit();
                                                        NavigationService.Navigate(new AdminCarPage());
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        transaction.Rollback();
                                                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                                    }
                                                }


                                            }
                                            else if (tbKedvezmeny.Text == "") lbError.Content = "A kedvezmeny mező nem lehet üres!";
                                            else if (!int.TryParse(tbKedvezmeny.Text, out l)) lbError.Content = "A kedvezmeny csak számot tartalmazhat!";
                                            else if (l > 100 || l < 0) lbError.Content = "A kedvezmeny nem megfelelő (0-100)!";
                                        }
                                        else if (tbAr.Text == "") lbError.Content = "Az ár  mező nem lehet üres!";
                                        else if (!int.TryParse(tbAr.Text, out k)) lbError.Content = "Az ár csak számot tartalmazhat!";
                                        else if (k < 1 || k > 1000000) lbError.Content = "Az ár nem megfelelő (1-1000000)!";
                                    }
                                    else lbError.Content = "A kivitel mező nem lehet üres!";
                                }
                                else if (tbUlesekszama.Text == "") lbError.Content = "Az ülések száma mező nem lehet üres!";
                                else if (!int.TryParse(tbUlesekszama.Text, out j)) lbError.Content = "Az ülések száma csak számot tartalmazhat!";
                                else if (j > 10 || j < 1) lbError.Content = "Az ülések száma nem megfelelő (1-10)!";
                            }
                            else lbError.Content = "Az üzemanyag mező nem lehet üres!";
                        }
                        else lbError.Content = "A váltó mező nem lehet üres!";
                    }
                    else if (tbEvjarat.Text == "") lbError.Content = "Az évjárat mező nem lehet üres!";
                    else if (!int.TryParse(tbEvjarat.Text, out i)) lbError.Content = "Az évjárat csak számot tartalmazhat!";
                    else if (i > DateTime.Now.Year || i < 1800) lbError.Content = "Az évjárat nem megfelelő (1800-Jelenlegi évig)!";
                }
                else lbError.Content = "A modell mező nem lehet üres!";


            }
            else lbError.Content = "A márka mező nem lehet üres!";

         

        }
    }
}
