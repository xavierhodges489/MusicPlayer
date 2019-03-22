using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.CommandPattern
{
    class PlayCommand : Command
    {
        Song song;

        public PlayCommand(Song song)
        {
            this.song = song;
        }

        public void execute()
        {
            song.play();     //call's the song play() method
        }
    }
}
