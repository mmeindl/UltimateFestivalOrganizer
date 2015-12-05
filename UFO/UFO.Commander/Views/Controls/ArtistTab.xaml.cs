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
        private IUFOServer server;
        private bool selectItem;

        public ArtistTab()
        {
            server = UFOServerFactory.GetUFOServer();
            selectItem = false;

            InitializeComponent();
        }

        private void OnSelectItem(object sender, MouseEventArgs e)
        {
            selectItem = true;
        }

        private void OnProfilePictureSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectItem) {
                var selectedItem = e.AddedItems;

                if (selectedItem.Count > 0)
                {
                    ArtistPicture picture = (ArtistPicture)selectedItem[0];
                    picture.IsProfilePicture = true;
                    server.UpdateArtistPicture(picture);
                }

                selectItem = false;
            }
        }

        private void OnPromoVideoSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
