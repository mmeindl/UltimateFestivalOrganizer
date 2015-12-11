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
            ArtistPictureVM oldPicture = artist.ProfilePicture;
            ArtistVideoVM newVideo = (ArtistVideoVM)cmbPromoVideo.SelectedItem;
            ArtistVideoVM oldVideo = artist.PromoVideo;

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

            if (!success)
            {
                artist.WebsiteURL = oldWebsite;
            }

            if (newPicture != null)
            {
                if (oldPicture != null)
                {
                    oldPicture.IsProfilePicture = false;
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

                if (!success)
                {
                    newPicture.IsProfilePicture = false;
                    if (oldPicture != null)
                    {
                        oldPicture.IsProfilePicture = true;
                    }
                }
                else
                {
                    artist.ProfilePicture = newPicture;
                }
            }

            if (newVideo != null)
            {
                if (oldVideo != null)
                {
                    oldVideo.IsPromoVideo = false;
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

                if (!success)
                {
                    newVideo.IsPromoVideo = false;
                    if (oldVideo != null)
                    {
                        oldVideo.IsPromoVideo = true;
                    }
                }
                else
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
                picture = server.FindArtistPictureByURL(picture.PictureURL);
                artist.Pictures.Add(new ArtistPictureVM(picture, artist, server));
                txtPictureURL.Clear();
            }
        }

        private void RemovePicture(object sender, RoutedEventArgs e)
        {
            ArtistPictureVM pictureVM = ((FrameworkElement)sender).DataContext as ArtistPictureVM;
            ArtistPicture picture = server.FindArtistPictureByURL(pictureVM.PictureURL);

            if (picture.IsProfilePicture)
            {
                pictureVM.Artist.ProfilePicture = null;
            }

            pictureVM.Artist.Pictures.Remove(pictureVM);

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
                video = server.FindArtistVideoByURL(video.VideoURL);
                artist.Videos.Add(new ArtistVideoVM(video, artist, server));
                txtVideoURL.Clear();
            }
        }

        private void RemoveVideo(object sender, RoutedEventArgs e)
        {
            ArtistVideoVM videoVM = ((FrameworkElement)sender).DataContext as ArtistVideoVM;
            ArtistVideo video = server.FindArtistVideoByURL(videoVM.VideoURL);

            if (video.IsPromoVideo)
            {
                videoVM.Artist.PromoVideo = null;
            }

            videoVM.Artist.Videos.Remove(videoVM);

            server.DeleteArtistVideo(video);
        }

        void UrlClick(object sender, RoutedEventArgs e)
        {
            string link = ((Hyperlink)sender).NavigateUri.ToString();
            System.Diagnostics.Process.Start(link);
        }
    }
}
