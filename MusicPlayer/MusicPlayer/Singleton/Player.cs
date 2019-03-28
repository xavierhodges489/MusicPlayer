using Microsoft.Win32;
using MusicPlayer.CommandPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MusicPlayer
{
    

    class Player
    {
        public static MediaPlayer mplayer;
        ArrayList songs;
        int currentSongPointer;
        int slot;
        CommandInvoker cmdControl;

        public Player()
        {
            mplayer = new MediaPlayer();
            songs = new ArrayList();
            currentSongPointer = -1;
            cmdControl = new CommandInvoker();
            slot = 0;               //slot to be used when setting commands
        }

        public void skipBack()
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

        public void pause()
        {
            cmdControl.pausebtnPushed(currentSongPointer);
        }

        public void play()
        {
            cmdControl.playbtnPushed(currentSongPointer);
        }

        public void skipForward()
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

        public void import()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            Uri uri = new Uri(openFileDialog1.FileName);
            mplayer.Open(uri);
            songs.Add(new Song(uri));
            currentSongPointer++;
            cmdControl.setCommand(slot, new PlayCommand((Song)songs[currentSongPointer]), new PauseCommand((Song)songs[currentSongPointer]));
            slot++;
        }
    }
}
