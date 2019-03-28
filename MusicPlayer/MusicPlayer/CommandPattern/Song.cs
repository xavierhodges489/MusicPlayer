using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.CommandPattern
{
    class Song
    {
        private Uri filePath;

        public Song(Uri file)
        {
            this.filePath = file;
        }

        public void play()
        {
            Player.mplayer.Open(this.filePath);
            Player.mplayer.Play();
        }

        public void pause()
        {
            Player.mplayer.Pause();
        }
    }
}
