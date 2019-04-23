using MusicPlayer.CommandPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.IteratorPattern
{
    //iterator interface -- each iterator will have a hasNext() and next() function
    interface Iterator
    {
        bool hasNext();
        Song next();

    }
}
