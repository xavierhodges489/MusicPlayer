using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Observer
{
    public abstract class Observer
    {
        public Subject subject;
        public abstract void update();
    }
}
