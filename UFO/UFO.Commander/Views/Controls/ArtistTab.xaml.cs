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
            ArtistCollectionVM artistCollectionVM = ((FrameworkElement)sender).DataContext as ArtistCollectionVM;
            Artist artist = new Artist();

            Category category = (Category)cmbCategoryNew.SelectedItem;
            Country country = (Country)cmbCountryNew.SelectedItem;

            bool success = false;
            try
            {
                string name = txtArtistnameNew.Text;
                string email = txtEmailNew.Text;

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
                ArtistVM artistVM = new ArtistVM(artist, category, country, artistCollectionVM, server);
                artistCollectionVM.Artists.Add(artistVM);
                artistCollectionVM.CurrentArtist = artistVM;
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


        private void UpdateArtist(object sender, RoutedEventArgs e)
        {
            ArtistVM artist = ((FrameworkElement)sender).DataContext as ArtistVM;

            string oldName = artist.Name;
            Category oldCategory = artist.Category;
            Country oldCountry = artist.Country;
            string oldEmail = artist.Email;

            bool success = false;
            try
            {
                string newName = txtArtistname.Text;
                Category newCategory = (Category)cmbCategory.SelectedItem;
                Country newCountry = (Country)cmbCountry.SelectedItem;
                string newEmail = txtEmail.Text;

                artist.Name = newName;
                artist.Category = newCategory;
                artist.Country = newCountry;
                artist.Email = newEmail;

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
