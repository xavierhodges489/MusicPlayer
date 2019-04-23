using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.CommandPattern
{
    public class Song
    {
        //variables each Song will have
        public Uri filePath { get; set; }

        public string title { get; set; }

        public string album { get; set; }

        public string  artist { get; set; }

        public int year { get; set; }
        
        //song constructor
        public Song(Uri file, string title, string album, string artist, int year)
        {
            this.filePath = file;
            this.title = title;
            this.album = album;
            this.artist = artist;
            this.year = year;
        }
        //plays the song with windows media player Play() method
        public void play()
        {
            Player.mplayer.Play();
        }
        //pauses the song with windows media player Pause() method
        public void pause()
        {
            Player.mplayer.Pause();
        }
    }
}
