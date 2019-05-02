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
        string currentDir;
        public Player player;
        private ObservableCollection<Song> songs = new ObservableCollection<Song>();
        private bool paused = false;

        public MainWindow()
        {
            InitializeComponent();
            player = Player.Instance;
            lvSongs.ItemsSource = songs;
            lvSongs.ItemsSource = songs;    //list view
            Player.mplayer.MediaEnded += (s, eventArgs) => playNextSong();  //play next song anytime a song ends

            currentDir = System.IO.Path.GetDirectoryName(Environment.CurrentDirectory);

            Import(System.IO.File.ReadAllLines(currentDir + "/files.txt"));

            image_blurred.Source = new BitmapImage(new Uri(currentDir+"/album_art/unknown.jpg", UriKind.Absolute));
            image_main.Source = new BitmapImage(new Uri(currentDir + "/album_art/unknown.jpg", UriKind.Absolute));

            refreshAlbumView();
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
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            if (paused == false)
            {
                paused = true;
                player.pause();
            }
            else
            {
                paused = false;
                player.play();
            }
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            
            if (lvSongs.SelectedIndex != -1)        //if a song is selected from the list view
            {
                player.currentSongPointer = lvSongs.SelectedIndex;  //update current song pointer
                Player.mplayer.Open(songs[lvSongs.SelectedIndex].filePath); //open the song in the player
                player.play();  //play the selected song
                lvSongs.SelectedIndex = -1;     //reset selected index so that no song is selected
                refreshAlbumView(); //update album view
            } else
            {
                player.play();
                refreshAlbumView();
            }
            
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            player.stop();
            paused = false;
        }

        private void skip_forward_Click(object sender, RoutedEventArgs e)
        {
            player.skipForward();
            refreshAlbumView();
        }


        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "mp3|*.mp3|All Files|*.*";
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();

            String[] s = openFileDialog1.FileNames;

            Import(s);
        }

        private void Import(String[] s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (System.IO.File.ReadAllLines(System.IO.Path.GetDirectoryName(Environment.CurrentDirectory) + "/files.txt").Contains(s[i]))
                {
                    if (System.IO.File.Exists(s[i]))
                    {
                        Uri uri = new Uri(s[i], UriKind.Absolute);

                        TagLib.File file = TagLib.File.Create(uri.OriginalString);
                        Song song = new Song(uri, file.Tag.Title, file.Tag.Album, file.Tag.JoinedPerformers, (int)file.Tag.Year);
                        songs.Add(song);

                        player.import(song, i);
                    }
                    
                }
                else
                {
                    using (System.IO.StreamWriter outfile =
                                        new System.IO.StreamWriter(System.IO.Path.GetDirectoryName(Environment.CurrentDirectory) + "/files.txt", true))
                    {
                        outfile.WriteLine(s[i]);
                    }

                    Uri uri = new Uri(s[i], UriKind.Absolute);

                    TagLib.File file = TagLib.File.Create(uri.OriginalString);
                    Song song = new Song(uri, file.Tag.Title, file.Tag.Album, file.Tag.JoinedPerformers, (int)file.Tag.Year);
                    songs.Add(song);

                    player.import(song, i);
                }
                
            }
            //player.import();

            refreshAlbumView();
        }

        private void loop_clicked(object sender, RoutedEventArgs e)
        {
            if(loop.IsChecked == true)
            {
                player.loop();
                refreshAlbumView();
            }
        }

        private void loop_unclicked(object sender, RoutedEventArgs e)
        {
            if(loop.IsChecked == false)
            {
                Player.mplayer.MediaEnded += (s, eventArgs) => playNextSong();
                refreshAlbumView();
            }
        }

        public void refreshAlbumView()
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

                string filename = currentDir + String.Format("/album_art/{0}.jpg", album);
                //if (!System.IO.File.Exists(filename))
                //{
                //    filename = "/album_art/unknown.jpg";
                //}
                if (System.IO.File.Exists(filename))
                {
                    image_blurred.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));
                    image_main.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));
                }
                else
                {
                    image_blurred.Source = new BitmapImage(new Uri(currentDir + "/album_art/unknown.jpg", UriKind.Absolute));
                    image_main.Source = new BitmapImage(new Uri(currentDir + "/album_art/unknown.jpg", UriKind.Absolute));
                }
                currentsong.Text = player.subject.getState() + 1 + "/" + player.queue.Count;
            }
        }

        private void playNextSong()
        {
            player.skipForward();
            refreshAlbumView();
        }
        
        private void shuffleToggle_Checked(object sender, RoutedEventArgs e)
        {
            player.queue = player.shuffle();    //shuffle function
        }

        private void shuffleToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            player.undoShuffle();       //remove shuffling and restore queue to original state
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            System.IO.File.WriteAllText(System.IO.Path.GetDirectoryName(Environment.CurrentDirectory) + "/files.txt", string.Empty);
            songs.Clear();
            player.reset();
            refreshAlbumView();
            image_blurred.Source = new BitmapImage(new Uri(currentDir + "/album_art/unknown.jpg", UriKind.Absolute));
            image_main.Source = new BitmapImage(new Uri(currentDir + "/album_art/unknown.jpg", UriKind.Absolute));
            tb_title.Text = "Title";
            tb_artist.Text = "Artist";
            tb_album.Text = "Album";
            tb_year.Text = "xxxx";

        }
    }
}
