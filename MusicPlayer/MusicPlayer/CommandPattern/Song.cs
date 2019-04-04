using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.CommandPattern
{
    public class Song
    {
        public Uri filePath { get; set; }

        public string title { get; set; }

        public string album { get; set; }

        public string  artist { get; set; }

        public int year { get; set; }

        public Song(Uri file, string title, string album, string artist, int year)
        {
            this.filePath = file;
            this.title = title;
            this.album = album;
            this.artist = artist;
            this.year = year;
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
