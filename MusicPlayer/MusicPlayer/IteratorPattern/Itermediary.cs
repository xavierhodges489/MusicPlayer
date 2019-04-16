using MusicPlayer.CommandPattern;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.IteratorPattern
{
    class Itermediary
    {
        IteratorCreator songIterator;
        public ObservableCollection<Song> example;
        public Itermediary(IteratorCreator songIterator)
        {
            this.songIterator = songIterator;
            example = new ObservableCollection<Song>();
        }

        public ObservableCollection<Song> printInventory()
        {
            IEnumerator<Song> songIteratorIterator = songIterator.createIterator();
            return printShop(songIteratorIterator);
        }

        private ObservableCollection<Song> printShop(IEnumerator<Song> iterator)
        {
            bool moving = iterator.MoveNext();
            while (moving == true)
            {
                Song item = iterator.Current;
                Console.WriteLine(item.filePath + " " + item.title + " " + item.album + " " + item.artist + " " + item.year);
                example.Add(item);
                moving = iterator.MoveNext();
            }

            return example;
        }
    }
}
