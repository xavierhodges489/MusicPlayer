using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Command interface that holds the execute function that all commands will use
namespace MusicPlayer.CommandPattern
{
    interface Command
    {
        void execute();
    }
}
