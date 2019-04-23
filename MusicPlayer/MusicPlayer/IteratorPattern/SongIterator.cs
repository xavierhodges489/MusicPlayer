using MusicPlayer.CommandPattern;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.IteratorPattern
{
    class SongIterator : IteratorCreator
    {
        public ObservableCollection<Song> songItems;
        //constructor
        public SongIterator()
        {
            songItems = new ObservableCollection<Song>();
        }
        //adds item to songItems
        public void addItem(Uri file, string title, string album, string artist, int year)
        {
            Song item = new Song(file, title, album, artist, year);
            songItems.Add(item);
        }

        public void addItem(Song song)
        {
            //Song item = song;
            songItems.Add(song);
        }
        //creates iterator for this song iterator
        IEnumerator<Song> IteratorCreator.createIterator()
        {
            return songItems.GetEnumerator();
        }
    }
}
