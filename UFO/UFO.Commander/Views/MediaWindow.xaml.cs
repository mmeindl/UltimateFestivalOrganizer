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

namespace UFO.Commander.Views
{
    /// <summary>
    /// Interaktionslogik für MediaWindow.xaml
    /// </summary>
    public partial class MediaWindow : Window
    {
        IUFOServer server;

        public MediaWindow()
        {
            server = UFOServerFactory.GetUFOServer();

            InitializeComponent();
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            ArtistVM artist = ((FrameworkElement)sender).DataContext as ArtistVM;
            string newWebsite = txtWebsite.Text;
            string oldWebsite = artist.WebsiteURL;
            ArtistPictureVM newPicture = (ArtistPictureVM)cmbProfilePicture.SelectedItem;
            ArtistVideoVM newVideo = (ArtistVideoVM)cmbPromoVideo.SelectedItem;

            artist.WebsiteURL = newWebsite;

            bool success = false;
            try
            {
                success = server.UpdateArtist(artist.Artist);
            }
            catch (Exception exc)
            {
                // TODO User hinweisen
            }

            if (success)
            {
                artist.WebsiteURL = newWebsite;
            }

            if (newPicture != null)
            {
                foreach (ArtistPictureVM item in cmbProfilePicture.Items)
                {
                    item.IsProfilePicture = false;
                }

                newPicture.IsProfilePicture = true;
                success = false;
                try
                {
                    success = server.UpdateArtistPicture(newPicture.ArtistPicture);
                }
                catch (Exception exc)
                {
                    // TODO User hinweisen
                }

                if (success)
                {
                    artist.ProfilePicture = newPicture;
                }
            }

            if (newVideo != null)
            {
                foreach (ArtistVideoVM item in cmbPromoVideo.Items)
                {
                    item.IsPromoVideo = false;
                }

                newVideo.IsPromoVideo = true;
                success = false;
                try
                {
                    success = server.UpdateArtistVideo(newVideo.ArtistVideo);
                }
                catch (Exception exc)
                {
                    // TODO User hinweisen
                }

                if (success)
                {
                    artist.PromoVideo = newVideo;
                }
            }
        }

        private void AddPicture(object sender, RoutedEventArgs e)
        {
            ArtistVM artist = ((FrameworkElement)sender).DataContext as ArtistVM;
            ArtistPicture picture = new ArtistPicture(txtPictureURL.Text, artist.Id);

            bool success = false;

            try
            {
                success = server.InsertArtistPicture(picture);
            }
            catch (Exception exc)
            {
                // TODO User hinweisen
            }

            if (success)
            {
                artist.Pictures.Add(new ArtistPictureVM(picture, artist, server));
                txtPictureURL.Clear();
            }
        }

        private void RemovePicture(object sender, RoutedEventArgs e)
        {
            ArtistPictureVM pictureVM = ((FrameworkElement)sender).DataContext as ArtistPictureVM;
            ArtistPicture picture = server.FindArtistPictureByURL(pictureVM.PictureURL);

            pictureVM.Artist.Pictures.Remove(pictureVM);

            if (picture.IsProfilePicture)
            {
                pictureVM.Artist.ProfilePicture = null;
            }

            server.DeleteArtistPicture(picture);
        }

        private void AddVideo(object sender, RoutedEventArgs e)
        {
            ArtistVM artist = ((FrameworkElement)sender).DataContext as ArtistVM;
            ArtistVideo video = new ArtistVideo(txtVideoURL.Text, artist.Id);

            bool success = false;

            try
            {
               success = server.InsertArtistVideo(video);
            }
            catch (Exception exc)
            {
                // TODO User hinweisen
            }
            
            if (success)
            {
                artist.Videos.Add(new ArtistVideoVM(video, artist, server));
                txtVideoURL.Clear();
            }
        }

        private void RemoveVideo(object sender, RoutedEventArgs e)
        {
            ArtistVideoVM videoVM = ((FrameworkElement)sender).DataContext as ArtistVideoVM;
            ArtistVideo video = server.FindArtistVideoByURL(videoVM.VideoURL);

            videoVM.Artist.Videos.Remove(videoVM);

            if (video.IsPromoVideo)
            {
                videoVM.Artist.PromoVideo = null;
            }

            server.DeleteArtistVideo(video);
        }

        void UrlClick(object sender, RoutedEventArgs e)
        {
            string link = ((Hyperlink)sender).NavigateUri.ToString();
            System.Diagnostics.Process.Start(link);
        }
    }
}
