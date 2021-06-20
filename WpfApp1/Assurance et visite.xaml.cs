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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        DB_LocationVoituresEntities1 dbContext = new DB_LocationVoituresEntities1();
        public Window2()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {



            Assurance assu = new Assurance();
            assu.ID_Voiture = Int32.Parse(App.Current.Properties["id_voitures"].ToString());
            assu.Date_D = assuDebut.SelectedDate;
            assu.Date_F = assuFin.SelectedDate;
            assu.Agence_VT = assuAgence.Text;
            assu.Montent = Int32.Parse(assuMontant.Text);
            dbContext.Assurances.Add(assu);
            dbContext.SaveChanges();

            Visit_Technique visi = new Visit_Technique();
            visi.ID_Voiture = Int32.Parse(App.Current.Properties["id_voitures"].ToString());
            visi.Date_D = visitDebut.SelectedDate;
            visi.Date_F = visitFin.SelectedDate;
            visi.Agence_VT = visitAgence.Text;
            visi.Montent = Int32.Parse(visitMontant.Text);
            dbContext.Assurances.Add(assu);
            dbContext.SaveChanges();
            this.Close();
        }

        
    }
}
