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
            MainWindow.player.Open(this.filePath);
            MainWindow.player.Play();
        }

        public void pause()
        {
            MainWindow.player.Pause();
        }
    }
}
