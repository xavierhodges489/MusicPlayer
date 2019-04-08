using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.CommandPattern
{
    class CommandInvoker
    {
        Command[] playcmds;
        Command[] pausecmds;

        public CommandInvoker()
        {
            playcmds = new Command[100];
            pausecmds = new Command[100];
        }

        public void setCommand(int slot, Command play, Command pause)
        {
            playcmds[slot] = play;
            pausecmds[slot] = pause;
        }

        public void playbtnPushed(int slot)
        {
            playcmds[slot].execute();
        }

        public void pausebtnPushed(int slot)
        {
            pausecmds[slot].execute();
        }
    }
}
