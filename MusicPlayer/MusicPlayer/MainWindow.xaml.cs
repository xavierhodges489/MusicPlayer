using Microsoft.Win32;
using MusicPlayer.CommandPattern;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TagLib;

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Player player;
        private ObservableCollection<Song> songs = new ObservableCollection<Song>();
        int lvSongSelected;



        public MainWindow()
        {
            InitializeComponent();
            player = Player.Instance;
            lvSongs.ItemsSource = songs;
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
            refreshAlbumView();
            currentsong.Text = player.subject.getState() + 1 + "/" + player.queue.Count;
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            player.pause();
            currentsong.Text = player.subject.getState() + 1 + "/" + player.queue.Count;
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            
            if (lvSongs.SelectedIndex != -1)        //if a song is selected from the list view
            {
                player.currentSongPointer = lvSongs.SelectedIndex;  //update current song pointer
                Player.mplayer.Open(songs[lvSongs.SelectedIndex].filePath); //open the song in the player
                player.play();  //play the selected song
                currentsong.Text = player.subject.getState() + 1 + "/" + player.queue.Count;     //update song position label in window
                lvSongs.SelectedIndex = -1;     //reset selected index so that no song is selected
            } else
            {
                player.play();
                currentsong.Text = player.subject.getState() + 1 + "/" + player.queue.Count;
            }
            
        }

        private void skip_forward_Click(object sender, RoutedEventArgs e)
        {
            player.skipForward();
            refreshAlbumView();
            currentsong.Text = player.subject.getState() + 1 + "/" + player.queue.Count;
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();

            String[] s = openFileDialog1.FileNames;

            for (int i = 0; i < s.Length; i++)
            {
                Uri uri = new Uri(s[i], UriKind.Absolute);

                TagLib.File file = TagLib.File.Create(uri.OriginalString);
                Song song = new Song(uri, file.Tag.Title, file.Tag.Album, file.Tag.JoinedPerformers, (int)file.Tag.Year);
                songs.Add(song);

                player.import(song, i);
            } 
            //player.import();

            refreshAlbumView();
        }

        private void refreshAlbumView()
        {
            if (player.currentSongPointer > -1)
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
                if (!System.IO.File.Exists(filename))
                {
                    filename = "/album_art/unknown.jpg";
                }
                image_blurred.Source = new BitmapImage(new Uri(@filename, UriKind.Relative));
                image_main.Source = new BitmapImage(new Uri(@filename, UriKind.Relative));
                
            }
        }
    }
}
