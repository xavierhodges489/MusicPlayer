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

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for AlbumView.xaml
    /// </summary>
    public partial class AlbumView : Page
    {
        public AlbumView(String title, String artists, String album, int year)
        {
            InitializeComponent();
            tb_title.Text = title;
            tb_album.Text = album;
            tb_artist.Text = artists;
            tb_year.Text = year.ToString();

            string filename = String.Format("/album_art/{0}.jpg", album);
            
            image_blurred.Source = new BitmapImage(new Uri(@filename, UriKind.Relative));
            image_main.Source = new BitmapImage(new Uri(@filename, UriKind.Relative));
        }

        public AlbumView()
        {
            InitializeComponent();
        }

        private void ToListView_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListView());
        }
    }
}
