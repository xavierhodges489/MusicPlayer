using MusicPlayer.CommandPattern;
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
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : Page
    {
        Player player;
        public ListView()
        {
            InitializeComponent();
            this.player = Player.Instance;
        }

        private void ToAlbumView_Click(object sender, RoutedEventArgs e)
        {
            if(player.currentSongPointer < 0) this.NavigationService.Navigate(new AlbumView());
            else
            {
                Song currentSong = player.queue[player.currentSongPointer];
                this.NavigationService.Navigate(new AlbumView(currentSong.title, currentSong.artist, currentSong.album, currentSong.year));
            }
            
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            lvSongs.ItemsSource = player.queue;
        }
    }
}
