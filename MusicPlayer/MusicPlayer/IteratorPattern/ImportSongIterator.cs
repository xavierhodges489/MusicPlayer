using MusicPlayer.CommandPattern;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.IteratorPattern
{
    class ImportSongIterator : Iterator
    {
        public ObservableCollection<Song> items;
        int position = 0;

        public ImportSongIterator(ObservableCollection<Song> items)
        {
            this.items = items;
        }

        public Song next()
        {
            Song item = items[position];
            position++;
            return item;
        }

        public bool hasNext()
        {
            if (position >= items.Count || items[position] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void remove()
        {
            if (position <= 0)
            {
                throw new IndexOutOfRangeException("You can't remove an item until you've done next()");
            }
            if (items[position - 1] != null)
            {
                for (int i = position - 1; i < (items.Count - 1); i++)
                {
                    items[i] = items[i + 1];
                }
                items[items.Count - 1] = null;
            }
        }
    }
}
