using Microsoft.Win32;
using MusicPlayer.CommandPattern;
using MusicPlayer.IteratorPattern;
using MusicPlayer.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TagLib;

namespace MusicPlayer
{
    

    public sealed class Player
    {
        public static MediaPlayer mplayer;
        public ObservableCollection<Song> queue { get; set; }
        //public List<Song> queue { get; set; }
        public int currentSongPointer { get; set; }
        int slot;
        CommandInvoker cmdControl;

        SongIterator it;

        //testing observer pattern
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

        public void skipBack()
        {
            if (currentSongPointer <= 0)
            {
                //do nothing
            }
            else
            {
                subject.setState(--currentSongPointer);
                //currentSongPointer--;
                //cmdControl.playbtnPushed(currentSongPointer);
                mplayer.Open(queue[currentSongPointer].filePath);
                cmdControl.playbtnPushed(subject.getState());
                
            }
        }

        public void pause()
        {
            if(currentSongPointer > -1 && currentSongPointer <= queue.Count)
            {
                //cmdControl.pausebtnPushed(currentSongPointer);
                subject.setState(currentSongPointer);
                cmdControl.pausebtnPushed(subject.getState());
            }
            
        }

        public void play()
        {
            if(currentSongPointer > -1 && currentSongPointer <= queue.Count)
            {
                //cmdControl.playbtnPushed(currentSongPointer);
                subject.setState(currentSongPointer);
                
                cmdControl.playbtnPushed(subject.getState());
                //mplayer.MediaEnded += (sender, eventArgs) => mplayer.Position = new TimeSpan(0, 0, 00);
                //mplayer.MediaEnded += (sender, eventArgs) => skipForward();
                
            }
            
        }

        /*internal void import(Song song)
        {
            throw new NotImplementedException();
        }*/

        public void skipForward()
        {
            if (currentSongPointer >= queue.Count - 1|| currentSongPointer <= -1)
            {
                //do nothing
            }
            else
            {
                //currentSongPointer++;
                //cmdControl.playbtnPushed(currentSongPointer);
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
                //queue.Remove(queue[currentSongPointer]);
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
            currentSongPointer = 0;
            return tempQueue;
        }

        public void undoShuffle()
        {
            //queue = savedQueue;	//issue
            queue = new ObservableCollection<Song>();
            for (int i = 0; i < savedQueue.Count; i++)
            {
                queue.Add(savedQueue[i]);		//storing contents of savedQueue into queue
                Console.WriteLine(queue[i].title);
            }
            currentSongPointer = 0;
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
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.Multiselect = true;
           // openFileDialog1.ShowDialog();

            //String[] s = openFileDialog1.FileNames;

            //for (int i = 0; i < s.Length; i++)
           // {
               // Uri uri = new Uri(s[i], UriKind.Absolute);
                mplayer.Open(song.filePath);
                //File file = File.Create(uri.OriginalString);
                // queue.Add(new Song(uri, file.Tag.Title, file.Tag.Album, file.Tag.JoinedPerformers, (int)file.Tag.Year));
                //it.addItem(uri, file.Tag.Title, file.Tag.Album, file.Tag.JoinedPerformers, (int)file.Tag.Year);     //add item to iterator
                it.addItem(song);
                /*currentSongPointer++;
                cmdControl.setCommand(slot, new PlayCommand((Song)queue[currentSongPointer]), new PauseCommand((Song)queue[currentSongPointer]));
                slot++;*/
           // }
            Itermediary iter = new Itermediary(it);
            queue = iter.printInventory();
            currentSongPointer++;
            //for (int i = 0; i < queue.Count; i++)
            //{
                /*Console.WriteLine(i);
                queue.Add(queue[i]);
                Console.WriteLine(" added to queue");*/
                cmdControl.setCommand(slot, new PlayCommand(queue[i]), new PauseCommand(queue[i]));
                slot++;
            //}
        }

        private void example()
        {
            currentSongPointer = 0;
            mplayer.Open(queue[currentSongPointer].filePath);
            mplayer.Play();
        }
    }
}
