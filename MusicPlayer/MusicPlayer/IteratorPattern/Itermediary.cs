using MusicPlayer.CommandPattern;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// communicates between client and iterator creator implementation
namespace MusicPlayer.IteratorPattern
{
    class Itermediary
    {
        //has a song iterator
        IteratorCreator songIterator;
        public ObservableCollection<Song> example;
        //constructor
        public Itermediary(IteratorCreator songIterator)
        {
            this.songIterator = songIterator;
            example = new ObservableCollection<Song>();
        }
        //prints songs in the iterable list
        public ObservableCollection<Song> printInventory()
        {
            IEnumerator<Song> songIteratorIterator = songIterator.createIterator();
            return printShop(songIteratorIterator);
        }
        //helper method that makes use of the IteratorCreator functions
        private ObservableCollection<Song> printShop(IEnumerator<Song> iterator)
        {
            bool moving = iterator.MoveNext();
            while (moving == true)
            {
                Song item = iterator.Current;
                example.Add(item);
                moving = iterator.MoveNext();
            }

            return example;
        }
    }
}
