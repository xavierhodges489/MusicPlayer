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
    

    public sealed class Player
    {
        public static MediaPlayer mplayer;
        ArrayList queue;
        int currentSongPointer;
        int slot;
        CommandInvoker cmdControl;

        private Player()
        {
            mplayer = new MediaPlayer();
            queue = new ArrayList();
            currentSongPointer = -1;
            cmdControl = new CommandInvoker();
            slot = 0;               //slot to be used when setting commands
        }

        private static Player instance = null;

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
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
            if (currentSongPointer >= queue.Count)
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
            queue.Add(new Song(uri));
            currentSongPointer++;
            cmdControl.setCommand(slot, new PlayCommand((Song)queue[currentSongPointer]), new PauseCommand((Song)queue[currentSongPointer]));
            slot++;
        }
    }
}
