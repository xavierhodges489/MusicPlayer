using Microsoft.Win32;
using MusicPlayer.CommandPattern;
using MusicPlayer.IteratorPattern;
using MusicPlayer.Observer;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace MusicPlayer
{
    

    public sealed class Player
    {
        public static MediaPlayer mplayer;
        public ObservableCollection<Song> queue { get; set; }
        public ObservableCollection<Song> savedQueue { get; set; }
        public int currentSongPointer { get; set; }
        int slot;
        CommandInvoker cmdControl;
        SongIterator it;        
        public Subject subject;

        private Player()
        {
            mplayer = new MediaPlayer();
            queue = new ObservableCollection<Song>();
            //queue = new List<Song>();
            currentSongPointer = -1;
            cmdControl = new CommandInvoker();
            slot = 0;               //slot to be used when setting commands
            subject = new Subject();
            subject.setState(currentSongPointer);
            new Observer1(subject);

            it = new SongIterator();
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

        public void reset()
        {
            queue.Clear();
            currentSongPointer = -1;
            cmdControl = new CommandInvoker();
            slot = 0;               //slot to be used when setting commands
            subject = new Subject();
            subject.setState(currentSongPointer);
            new Observer1(subject);

            it = new SongIterator();
            Player.mplayer.Stop();
        }

        public void skipBack()
        {
            if (currentSongPointer <= 0)
            {
                //do nothing
            }
            else
            {
                subject.setState(--currentSongPointer);
                mplayer.Open(queue[currentSongPointer].filePath);
                cmdControl.playbtnPushed(subject.getState());
                
            }
        }

        public void pause()
        {
            if(currentSongPointer > -1 && currentSongPointer <= queue.Count)
            {
                subject.setState(currentSongPointer);
                cmdControl.pausebtnPushed(subject.getState());
            }
            
        }

        public void play()
        {
            if(currentSongPointer > -1 && currentSongPointer <= queue.Count)
            {
                subject.setState(currentSongPointer);
                cmdControl.playbtnPushed(subject.getState());
            }
            
        }

        public void stop()
        {
            if(currentSongPointer > -1)
                cmdControl.stopbtnPushed(subject.getState());
        }

        public void skipForward()
        {
            if (currentSongPointer >= queue.Count - 1|| currentSongPointer <= -1)
            {
                //do nothing
            }
            else
            {
                subject.setState(++currentSongPointer);
                mplayer.Open(queue[currentSongPointer].filePath);
                cmdControl.playbtnPushed(subject.getState());
            }
        }
        public ObservableCollection<Song> shuffle()
        {
            savedQueue = new ObservableCollection<Song>();
            for (int i = 0; i < queue.Count; i++)
            {
                savedQueue.Add(queue[i]);		//storing contents of queue into savedQueue
            }
            Random ran = new Random();
            ObservableCollection<Song> tempQueue = new ObservableCollection<Song>();
            Song currSong = queue[currentSongPointer];
            while (queue.Count > 0)
            {
                int ranIdx = ran.Next(0, queue.Count);
                tempQueue.Add(queue[ranIdx]);
                queue.RemoveAt(ranIdx);
            }
            tempQueue.Remove(currSong);
            tempQueue.Insert(0, currSong);
            for(int i = 0; i < tempQueue.Count; i++)
            {
                Console.WriteLine(tempQueue[i].title);
            }
            return tempQueue;
        }

        public void undoShuffle()
        {
            queue = new ObservableCollection<Song>();
            for (int i = 0; i < savedQueue.Count; i++)
            {
                queue.Add(savedQueue[i]);		//storing contents of savedQueue into queue
                Console.WriteLine(queue[i].title);
            }
        }

        public void loop()
        {
            if(currentSongPointer == queue.Count - 1)
            {
                Console.WriteLine(currentSongPointer);
                mplayer.MediaEnded += (sender, EventArgs) => example(); 
            }
        }

        public void import(Song song, int i)
        {
            mplayer.Open(song.filePath);
                
            it.addItem(song);
            Itermediary iter = new Itermediary(it);
            queue = iter.printInventory();
            currentSongPointer++;
            cmdControl.setCommand(slot, new PlayCommand(queue[i]), new PauseCommand(queue[i]), new StopCommand(queue[i]));
            slot++;
        }

        private void example()
        {
            currentSongPointer = 0;
            mplayer.Open(queue[currentSongPointer].filePath);
            mplayer.Play();
        }
    }
}
