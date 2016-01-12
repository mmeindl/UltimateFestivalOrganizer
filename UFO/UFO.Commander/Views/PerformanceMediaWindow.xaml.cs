using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// 
    public partial class PerformanceMediaWindow : Window
    {

        IUFOServer server;
        RegexUtilities regexUtilities = new RegexUtilities();
        const string msgInvalidURLException = "URL has to start with 'http(s)://'";
        const string msgInvalidURLPathException = "Unable to save changes. Please enter a valid URL";
        const string msgDuplicateURLException = "Unable to save changes. URL already exists.";

        const string msgWindowTitle = "Error";

        public PerformanceMediaWindow()
        {
            BLType type = (BLType)Enum.Parse(typeof(BLType), ConfigurationManager.AppSettings["BLType"]);

            server = UFOServerFactory.GetUFOServer(type);

            InitializeComponent();
        }

        private void AddPicture(object sender, RoutedEventArgs e)
        {
            PerformanceMediaCollectionVM performanceMediaCollectionVM = ((FrameworkElement)sender).DataContext as PerformanceMediaCollectionVM;
            Performance performance = performanceMediaCollectionVM.Performance;

            PerformancePicture picture = new PerformancePicture(txtPictureURL.Text, performance.Id);

            bool success = false;

            try
            {
                success = server.InsertPerformancePicture(picture);
            }
            catch (Exception exc)
            {
                MessageBoxResult result;
                if (!regexUtilities.IsValidURL(txtPictureURL.Text))
                    result = MessageBox.Show(msgInvalidURLException, msgWindowTitle);
                else if(server.FindPerformancePictureByURL(txtPictureURL.Text) != null)
                    result = MessageBox.Show(msgDuplicateURLException, msgWindowTitle);
                else
                    result = MessageBox.Show(msgInvalidURLPathException, msgWindowTitle);
            }

            if (success)
            {
                picture = server.FindPerformancePictureByURL(picture.PictureURL);
                performanceMediaCollectionVM.Pictures.Add(new PerformancePictureVM(picture, performanceMediaCollectionVM, server));
                txtPictureURL.Clear();
            }
        }

        private void RemovePicture(object sender, RoutedEventArgs e)
        {
            PerformancePictureVM pictureVM = ((FrameworkElement)sender).DataContext as PerformancePictureVM;
            PerformancePicture picture = server.FindPerformancePictureByURL(pictureVM.PictureURL);

            bool success = false;

            success = server.DeletePerformancePicture(picture);

            if (success)
            {
                pictureVM.PerformanceMediaCollectionVM.Pictures.Remove(pictureVM);
            }
        }

        private void AddVideo(object sender, RoutedEventArgs e)
        {
            PerformanceMediaCollectionVM performanceMediaCollectionVM = ((FrameworkElement)sender).DataContext as PerformanceMediaCollectionVM;
            Performance performance = performanceMediaCollectionVM.Performance;

            PerformanceVideo video = new PerformanceVideo(txtVideoURL.Text, performance.Id);

            bool success = false;

            try
            {
               success = server.InsertPerformanceVideo(video);
            }
            catch (Exception exc)
            {
                MessageBoxResult result;
                if (!regexUtilities.IsValidURL(txtPictureURL.Text))
                    result = MessageBox.Show(msgInvalidURLException, msgWindowTitle);
                else if (server.FindPerformanceVideoByURL(txtVideoURL.Text) != null)
                    result = MessageBox.Show(msgDuplicateURLException, msgWindowTitle);
                else
                    result = MessageBox.Show(msgInvalidURLPathException, msgWindowTitle);
            }
            
            if (success)
            {
                video = server.FindPerformanceVideoByURL(video.VideoURL);
                performanceMediaCollectionVM.Videos.Add(new PerformanceVideoVM(video, performanceMediaCollectionVM, server));
                txtVideoURL.Clear();
            }
        }

        private void RemoveVideo(object sender, RoutedEventArgs e)
        {
            PerformanceVideoVM videoVM = ((FrameworkElement)sender).DataContext as PerformanceVideoVM;
            PerformanceVideo video = server.FindPerformanceVideoByURL(videoVM.VideoURL);

            bool success = false;

            success = server.DeletePerformanceVideo(video);

            if (success)
            {
                videoVM.PerformanceMediaCollectionVM.Videos.Remove(videoVM);
            }
        }

        void UrlClick(object sender, RoutedEventArgs e)
        {
            string link = ((Hyperlink)sender).NavigateUri.ToString();
            System.Diagnostics.Process.Start(link);
        }
    }
}
