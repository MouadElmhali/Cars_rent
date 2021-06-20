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
    /// Logique d'interaction pour Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        DB_LocationVoituresEntities1 dbContext = new DB_LocationVoituresEntities1();
        public Window3()
        {
            InitializeComponent();
            notif.ItemsSource = "La contarat dont le nmero " + dbContext.Contrats.Where(r => r.Date_F_Contrat == DateTime.Now.ToString()).Select(p => new { p.ID_Contrat }).ToList() + " est expirer";
            notif.ItemsSource = "La visite technique de la voiture dont le code "+dbContext.Visit_Technique.Where(r => r.Date_F == DateTime.Now).Select(p => new { p.ID_Voiture }).ToList() +" est expirer";
            notif.ItemsSource = dbContext.Assurances.Where(r => r.Date_F == DateTime.Now).ToList();
            notif.ItemsSource = dbContext.Assurances.Where(r => r.Date_F == DateTime.Now).ToList();
            if (notif.Items.Count == 0)
            {
                notif.Visibility = Visibility.Hidden;
                labe.Visibility = Visibility.Visible;
            }
        }

        private void Button_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

        }
    }
}
