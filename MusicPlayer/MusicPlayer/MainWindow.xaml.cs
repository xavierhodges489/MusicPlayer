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
        public static MediaPlayer player;
        ArrayList songs;
        int currentSongPointer;
        int slot;
        CommandInvoker cmdControl;

        public MainWindow()
        {
            InitializeComponent();
            player = new MediaPlayer();
            songs = new ArrayList();
            currentSongPointer = -1;
            cmdControl = new CommandInvoker();
            slot = 0;               //slot to be used when setting commands
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new ListView());
            this.DataContext = this;
        }

        /*private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }*/

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
            if (currentSongPointer <= 0)
            {
                //do nothing
            }
            else
            {
                currentSongPointer--;
                cmdControl.playbtnPushed(currentSongPointer);
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            Uri uri = new Uri(openFileDialog1.FileName);
            player.Open(uri);
            songs.Add(new Song(uri));
            currentSongPointer++;
            cmdControl.setCommand(slot, new PlayCommand((Song)songs[currentSongPointer]), new PauseCommand((Song)songs[currentSongPointer]));
            slot++;
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            cmdControl.pausebtnPushed(currentSongPointer);
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            cmdControl.playbtnPushed(currentSongPointer);
        }

        private void skip_foward_Click(object sender, RoutedEventArgs e)
        {
            if (currentSongPointer >= songs.Count)
            {
                //do nothing
            }
            else
            {
                currentSongPointer++;
                cmdControl.playbtnPushed(currentSongPointer);
            }
        }
    }
}
