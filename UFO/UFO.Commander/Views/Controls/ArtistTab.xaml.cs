using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private IUFOServer server;
        private RegexUtilities regexUtilities = new RegexUtilities();
        private const string msgSaveException = "Unable to save changes. Please check your input!";
        private const string msgEmptyNameException = "Unable to save changes. Please enter an artist!";
        private const string msgEmptyCategoryException = "Unable to save changes. Please enter a category!";
        private const string msgEmptyCountryException = "Unable to save changes. Please enter a country!";
        private const string msgEmptyEmailException = "Unable to save changes. Please enter an e-mail address!";
        private const string msgInvalidEmailException = "Unable to save changes. Please enter a valid e-mail address!";
        private const string msgDuplicateException = "Unable to save chnges. Artist already exists.";

        const string msgWindowTitle = "Error";

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
            string name = txtArtistnameNew.Text;
            string email = txtEmailNew.Text;
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
                MessageBoxResult result;
                if (name == "")
                    result = MessageBox.Show(msgEmptyNameException, msgWindowTitle);
                else if (category == null)
                    result = MessageBox.Show(msgEmptyCategoryException, msgWindowTitle);
                else if (country == null)
                    result = MessageBox.Show(msgEmptyCountryException, msgWindowTitle);
                else if (email == "")
                    result = MessageBox.Show(msgEmptyEmailException, msgWindowTitle);
                else if (!regexUtilities.IsValidEmail(email))
                    result = MessageBox.Show(msgInvalidEmailException, msgWindowTitle);
                else if (server.FindAreaByName(name) != null)
                    result = MessageBox.Show(msgDuplicateException, msgWindowTitle);
                else
                    result = MessageBox.Show(msgSaveException, msgWindowTitle);
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

            string newName = txtArtistname.Text;
            Category newCategory = (Category)cmbCategory.SelectedItem;
            Country newCountry = (Country)cmbCountry.SelectedItem;
            string newEmail = txtEmail.Text;

            try
            {
                if (newName == "" ||
                    newCategory == null ||
                    newCountry == null ||
                    newEmail == "" ||
                    !regexUtilities.IsValidEmail(newEmail))
                { throw new Exception(); }

                artist.Name = newName;
                artist.Category = newCategory;
                artist.Country = newCountry;
                artist.Email = newEmail;

                success = server.UpdateArtist(artist.Artist);
            }
            catch (Exception exc)
            {
                MessageBoxResult result;
                if (newName == "")
                    result = MessageBox.Show(msgEmptyNameException, msgWindowTitle);
                else if (newCategory == null)
                    result = MessageBox.Show(msgEmptyCategoryException, msgWindowTitle);
                else if (newCountry == null)
                    result = MessageBox.Show(msgEmptyCountryException, msgWindowTitle);
                else if (newEmail == "")
                    result = MessageBox.Show(msgEmptyEmailException, msgWindowTitle);
                else if (!regexUtilities.IsValidEmail(newEmail))
                    result = MessageBox.Show(msgInvalidEmailException, msgWindowTitle);
                else if (oldName != txtArtistname.Text && server.FindAreaByName(newName) != null)
                    result = MessageBox.Show(msgDuplicateException, msgWindowTitle);
                else
                    result = MessageBox.Show(msgSaveException, msgWindowTitle);
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
            ArtistMediaWindow artistMediaWindow = new ArtistMediaWindow();
            artistMediaWindow.DataContext = this.DataContext;
            artistMediaWindow.ShowDialog();
        }
    }
}
