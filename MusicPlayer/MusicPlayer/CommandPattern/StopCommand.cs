using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.CommandPattern
{
    class StopCommand : Command
    {
        Song song;

        public StopCommand(Song song)
        {
            this.song = song;
        }

        public void execute()
        {
            song.stop();     //call's the song stop() method
        }
    }
}
