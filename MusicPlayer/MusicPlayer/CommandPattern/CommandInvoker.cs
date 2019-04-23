using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.CommandPattern
{
    //class that holds all commands in the command pattern
    class CommandInvoker
    {
        Command[] playcmds;
        Command[] pausecmds;

        public CommandInvoker()
        {   
            //user can store up to 100 play and pause commands
            playcmds = new Command[100];
            pausecmds = new Command[100];
        }
        //user can set the commands for each song by providing a slot
        public void setCommand(int slot, Command play, Command pause)
        {
            playcmds[slot] = play;
            pausecmds[slot] = pause;
        }
        //calls play execute method
        public void playbtnPushed(int slot)
        {
            playcmds[slot].execute();
        }
        //calls pause execute method
        public void pausebtnPushed(int slot)
        {
            pausecmds[slot].execute();
        }
    }
}
