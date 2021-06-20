using LiveCharts;
using LiveCharts.Wpf;
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
using Separator = LiveCharts.Wpf.Separator;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
 
        DB_LocationVoituresEntities1 dbContext = new DB_LocationVoituresEntities1();
        public Dashboard()
        {
            InitializeComponent();
            louer.Content = dbContext.Voitures.Where(r => r.Etat == "non").Count();
            dispo.Content = dbContext.Voitures.Where(r => r.Etat == "oui").Count();
            reserv.Content = dbContext.Resevations.Count();
            var data = dbContext.Contrats.Select(p => new { p.Date_D_Contrat, p.Client });
            visualisation.ItemsSource = dbContext.Resevations.Select(s => new { nomC = s.Client.Nom + " " +s.Client.Prenom, s.Client.Telephone, s.Voiture.Matricule, s.Voiture.Model.Libelle_Model }).ToList();

            Values = new ChartValues<double> { 3,0,10,5,6,1,11 };

            DataContext = this;

            DateTime dt = DateTime.Now;
            today.Text = dt.ToString();
        }
        public ChartValues<double> Values { get; set; }

        private void UpdateOnclick(object sender, RoutedEventArgs e)
        {
            Chart.Update(true);
        }
    }
}
