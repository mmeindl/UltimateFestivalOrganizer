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

        private void RemovePicture(object sender, RoutedEventArgs e)
        {
            ArtistPictureVM pictureVM = ((FrameworkElement)sender).DataContext as ArtistPictureVM;
            ArtistPicture picture = server.FindArtistPictureByURL(pictureVM.PictureURL);

            //TODO remove pictureVM from observablecollection pictures of currentartist

            server.DeleteArtistPicture(picture);
        }

        private void RemoveVideo(object sender, RoutedEventArgs e)
        {

        }

        void UrlClick(object sender, RoutedEventArgs e)
        {
            string link = ((Hyperlink)sender).NavigateUri.ToString();
            System.Diagnostics.Process.Start(link);
        }
    }
}
