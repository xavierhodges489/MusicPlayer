using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Observer
{
    abstract class Observer
    {
        protected Subject subject;
        public abstract void update();
    }
}
