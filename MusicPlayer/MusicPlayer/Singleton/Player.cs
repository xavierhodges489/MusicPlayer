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
        public ObservableCollection<Song> savedQueue { get; set; }
        //public List<Song> queue { get; set; }
        public int currentSongPointer { get; set; }
        int slot;
        CommandInvoker cmdControl;

        SongIterator it;

        Boolean loopOn;

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

        //method to shuffle queue
        public ObservableCollection<Song> shuffle(ObservableCollection<Song> queue)
        {
            /*copy the queue variable to the savedQueue variable*/
            savedQueue = queue;
           
            ObservableCollection<Song> tempQueue = new ObservableCollection<Song>();
            if (currentSongPointer > -1)
            {

                Random ran = new Random();
                
                  /*assign the currently pointed song as the currSong variable*/
                Song currSong = queue[currentSongPointer];

                //loop to iterate through queue
                while (queue.Count > 0)
                {
                    /*create a random index number between 0 and the queue variable count,
                     add the song tied to the random index to the temporary queue variable
                     and remove song tried to the random index from the queue variable*/
                    int ranIdx = ran.Next(0, queue.Count);
                    tempQueue.Add(queue[ranIdx]);
                    queue.RemoveAt(ranIdx);
                }
                /*remove the song tied with the current song pointer from the queue variable,
                  insert the song tied to the current song pointer to the zeroth index of the temporary queue variable
                  and return temporary queue varaiable to the global queue variable*/
                tempQueue.Remove(currSong);
                tempQueue.Insert(0, currSong);
                
            }
            return tempQueue;
        }

        public ObservableCollection<Song> undoShuffle(ObservableCollection<Song> queue)
        {
            queue = savedQueue;
            if (currentSongPointer > -1)
            {                
                for (int i = currentSongPointer; i < queue.Count; i++)
                {
                    queue.Add(queue[i]);
                }
            }
            return queue;
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
            Console.WriteLine("queue count = " + queue.Count);
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
    }
}
