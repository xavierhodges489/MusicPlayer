using Microsoft.Win32;
using MusicPlayer.CommandPattern;
using System;
using System.Collections;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Player player;

        public MainWindow()
        {
            player = Player.Instance;
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //frame.Navigate(new ListView());
            this.DataContext = this;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void DragBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Skip_back_Click(object sender, RoutedEventArgs e)
        {
            player.skipBack();
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            player.pause();
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            player.play();
        }

        private void skip_forward_Click(object sender, RoutedEventArgs e)
        {
            player.skipForward();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            player.import();
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            Song currentSong = player.queue[player.currentSongPointer];
            String title = currentSong.title;
            String artist = currentSong.artist;
            String album = currentSong.album;
            int year = currentSong.year;

            tb_title.Text = title;
            tb_album.Text = album;
            tb_artist.Text = artist;
            tb_year.Text = year.ToString();

            string filename = String.Format("/album_art/{0}.jpg", album);

            image_blurred.Source = new BitmapImage(new Uri(@filename, UriKind.Relative));
            image_main.Source = new BitmapImage(new Uri(@filename, UriKind.Relative));

            lvSongs.ItemsSource = player.queue;
        }
    }
}
