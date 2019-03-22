using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.CommandPattern
{
    class PauseCommand : Command
    {
        Song song;

        public PauseCommand(Song song)
        {
            this.song = song;
        }

        public void execute()
        {
            song.pause();     //call's the song pause() method
        }
    }
}
