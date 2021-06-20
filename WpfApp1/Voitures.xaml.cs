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
using Microsoft.Win32;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Voitures.xaml
    /// </summary>
    public partial class Voitures : Page
    {
        DB_LocationVoituresEntities1 dbContext = new DB_LocationVoituresEntities1();

        public void clear()
        {
            Matricule.Text = "";
            cmb_Marque.SelectedItem = null;
            cmb_Model.SelectedItem = null;
            prixParjour.Text = "";
            Etat.Text = "";
            couleur.Text = null;
            path.Text = null;
            nbr_place.Value = 0;
        }


        public void charger_grid()
        {
            voiturGrid.ItemsSource = dbContext.Voitures.Select(v => new
            {
                v.ID_Voiture,
                v.Matricule,
                v.Model.Marque.Libelle_Marque,
                v.Model.Libelle_Model,
                v.PrixPar_Jour,
                v.Etat,
                v.color,
                v.Nombre_Place,
                v.images
            }

            ).ToList();
        }
        public void PopupMSG(string sourceIMG, string msg)
        {
            IMGPopup.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/" + sourceIMG));
            txtPopup.Text = msg;
            MsgPopup.IsOpen = true;
        }
        public Voitures()
        {
            InitializeComponent();
            cmb_Marque.ItemsSource = dbContext.Marques.Select(v => new { v.ID_Marque,v.Libelle_Marque}).ToList();
            cmb_Marque.DisplayMemberPath = "Libelle_Marque";
            cmb_Marque.SelectedValuePath = "ID_Marque";

            cmb_Model.ItemsSource = dbContext.Models.Select(v => new { v.ID_Model,v.Libelle_Model }).ToList();
            cmb_Model.DisplayMemberPath = "Libelle_Model";
            cmb_Model.SelectedValuePath = "ID_Model";

            charger_grid();

            ID_voitures.Text = Convert.ToString(dbContext.Voitures.Max(co => co.ID_Voiture) + 1);

        }

      
        private void btnLoad_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                path.Text = op.FileName;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

           
                if(Matricule.Text =="" ||cmb_Marque.SelectedItem == null || cmb_Model.SelectedItem == null || prixParjour.Text == "" || Etat.Text == "" || couleur.Text == null || path.Text == null || nbr_place.Value == 0)
                {
                    PopupMSG("refuse.png", "un ou plusieures champs sont vides");
                }

                else
                {
                try
                {
                    Voiture voi = new Voiture();
                    voi.Matricule = Matricule.Text;
                    voi.ID_Marque = Int32.Parse(cmb_Marque.SelectedValue.ToString());
                    voi.ID_Model = Int32.Parse(cmb_Model.SelectedValue.ToString());
                    voi.PrixPar_Jour = Int32.Parse(prixParjour.Text);
                    voi.Etat = Etat.Text;
                    voi.color = couleur.Text;
                    voi.images = path.Text;
                    voi.Nombre_Place = Int32.Parse(nbr_place.Value.ToString());
                    dbContext.Voitures.Add(voi);
                    dbContext.SaveChanges();
                    App.Current.Properties["id_voitures"] = ID_voitures.Text;
                    Window2 w = new Window2();
                    w.Show();
                    charger_grid();
                    clear();
                    PopupMSG("Successful.png", "Ajouté avec succès ");
                }
            catch (Exception ex)
            {
                PopupMSG("refuse.png", ex.Message);
            }

        }




    }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (cmb_Marque.SelectedItem == null || cmb_Model.SelectedValue == null || prixParjour.Text == "" || Etat.Text == "" || couleur.Text == null || path.Text == null || nbr_place.Value == 0)
            {
                PopupMSG("refuse.png", "un ou plusieures champs sont vides");
            }
            else
            {
                int id = Int32.Parse(ID_voitures.Text);
                Voiture voi = dbContext.Voitures.Where(c => c.ID_Voiture == id).First();
                voi.Matricule = Matricule.Text;
                voi.ID_Marque = Int32.Parse(cmb_Marque.SelectedValue.ToString());
                voi.ID_Model = Int32.Parse(cmb_Model.SelectedValue.ToString());
                voi.PrixPar_Jour = Int32.Parse(prixParjour.Text);
                voi.Etat = Etat.Text;
                voi.color = couleur.Text;
                voi.images = path.Text;
                voi.Nombre_Place = Int32.Parse(nbr_place.Value.ToString());
                dbContext.SaveChanges();
                charger_grid();
                clear();
                PopupMSG("Successful.png", "Modifier avec succès ");
            }


        }

        private void txt_rechercher_TextChanged(object sender, TextChangedEventArgs e)
        {
             

                voiturGrid.ItemsSource = dbContext.Voitures.Where(v => v.Matricule == rechercher.Text).Select(v => new
                {
                    v.ID_Voiture,
                    v.Matricule,
                    v.Model.Marque.Libelle_Marque,
                    v.Model.Libelle_Model,
                    v.PrixPar_Jour,
                    v.Etat,
                    v.color,
                    v.Nombre_Place,
                    v.images
                }
                 ).ToList();
           
           
        }

        private void dgv_voitures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var data = voiturGrid.SelectedItem;

                ID_voitures.Text = (voiturGrid.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                Matricule.Text = (voiturGrid.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
                cmb_Marque.Text = (voiturGrid.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
                cmb_Model.Text = (voiturGrid.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
                prixParjour.Text = (voiturGrid.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text;
                Etat.Text = (voiturGrid.SelectedCells[5].Column.GetCellContent(data) as TextBlock).Text;
                couleur.Text = (voiturGrid.SelectedCells[6].Column.GetCellContent(data) as TextBlock).Text;
                nbr_place.Value = Int32.Parse((voiturGrid.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text);
                path.Text = (voiturGrid.SelectedCells[8].Column.GetCellContent(data) as TextBlock).Text;
                imgPhoto.Source = new BitmapImage(new Uri(path.Text));
            }
            catch(Exception ex)
            {
                PopupMSG("refuse.png", ex.Message);
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_nouveau_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }
    }
    }
