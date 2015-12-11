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
using UFO.Commander.ViewModels;
using UFO.Domain;
using UFO.Server;

namespace UFO.Commander.Views.Controls
{
    /// <summary>
    /// Interaktionslogik für ArtistTab.xaml
    /// </summary>
    public partial class ArtistTab : UserControl
    {
        IUFOServer server;

        public ArtistTab()
        {
            server = UFOServerFactory.GetUFOServer();

            InitializeComponent();
        }
        private void AddArtist(object sender, RoutedEventArgs e)
        {
            ArtistCollectionVM artistCollection = ((FrameworkElement)sender).DataContext as ArtistCollectionVM;

            string name = txtArtistnameNew.Text;
            Category category = (Category)cmbCategoryNew.SelectedItem;
            Country country = (Country)cmbCountryNew.SelectedItem;
            string email = txtEmailNew.Text;

            Artist artist = new Artist();

            bool success = false;
            try
            {
                artist.Name = name;
                artist.CategoryId = category.Id;
                artist.CountryId = country.Abbreviation;
                artist.Email = email;

                success = server.InsertArtist(artist);
            }
            catch (Exception exc)
            {
                // TODO User hinweisen
            }

            if (success)
            {
                artist = server.FindArtistByName(artist.Name);
                ArtistVM artistVM = new ArtistVM(artist, category, country, artistCollection, server);
                artistCollection.Artists.Add(artistVM);
                artistCollection.CurrentArtist = artistVM;
                dgArtists.MoveFocus(new TraversalRequest(FocusNavigationDirection.Last));
                dgArtists.ScrollIntoView(artistVM);
            }
        }

        private void RemoveArtist(object sender, RoutedEventArgs e)
        {
            ArtistVM artistVM = ((FrameworkElement)sender).DataContext as ArtistVM;

            ArtistVM currentArtist = artistVM.ArtistCollection.Artists[0];
            

            artistVM.ArtistCollection.Artists.Remove(artistVM);

            artistVM.ArtistCollection.CurrentArtist = currentArtist;
            dgArtists.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            dgArtists.ScrollIntoView(currentArtist);
            server.DeleteArtist(artistVM.Artist);
        }


        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            ArtistVM artist = ((FrameworkElement)sender).DataContext as ArtistVM;
            string newName = txtArtistname.Text;
            string oldName = artist.Name;
            Category newCategory = (Category)cmbCategory.SelectedItem;
            Category oldCategory = artist.Category;
            Country newCountry = (Country)cmbCountry.SelectedItem;
            Country oldCountry = artist.Country;
            string newEmail = txtEmail.Text;
            string oldEmail = artist.Email;

            artist.Name = newName;
            artist.Category = newCategory;
            artist.Country = newCountry;
            artist.Email = newEmail;

            bool success = false;
            try
            {
                success = server.UpdateArtist(artist.Artist);
            }
            catch (Exception exc)
            {
                // TODO User hinweisen
            }

            if (!success)
            {
                artist.Name = oldName;
                artist.Category = oldCategory;
                artist.Country = oldCountry;
                artist.Email = oldEmail;
            }
        }

        private void UrlClick(object sender, RoutedEventArgs e)
        {
            string link = ((Hyperlink)sender).NavigateUri.ToString();
            System.Diagnostics.Process.Start(link);
        }

        private void EditMedia(object sencer, RoutedEventArgs e)
        {
            Object dc = this.DataContext;
            MediaWindow mediaWindow = new MediaWindow();
            mediaWindow.DataContext = dc;
            mediaWindow.Show();
        }
    }
}
