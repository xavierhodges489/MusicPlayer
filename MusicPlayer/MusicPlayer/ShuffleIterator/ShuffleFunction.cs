using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.ShuffleIterator
{
    class ShuffleFunction
    {
        ArrayList songList = new ArrayList(10);
        int currentSongPointer = 3;
       ArrayList shuffle()
        {
            for(int i = 0; i<songList.Count; i++)
            {
                songList.Add(i);
            }
            songList.Remove(currentSongPointer);

            //SHUFFLE FUNCTION [WIP]

            songList.Insert(0, currentSongPointer);
            Console.WriteLine(songList);
            return songList;
        } 

        ArrayList undo()
        {
            for(int i = currentSongPointer; i<songList.Count; i++)
            {
                songList.Add(i);
            }
            Console.WriteLine(songList);
            return songList;
        }
    }
}
