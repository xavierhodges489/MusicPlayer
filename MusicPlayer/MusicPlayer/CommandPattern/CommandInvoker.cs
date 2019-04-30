namespace MusicPlayer.CommandPattern
{
    //class that holds all commands in the command pattern
    class CommandInvoker
    {
        Command[] playcmds;
        Command[] pausecmds;
        Command[] stopcmds;

        public CommandInvoker()
        {   
            //user can store up to 100 play and pause commands
            playcmds = new Command[100];
            pausecmds = new Command[100];
            stopcmds = new Command[100];
        }
        //user can set the commands for each song by providing a slot
        public void setCommand(int slot, Command play, Command pause, Command stop)
        {
            playcmds[slot] = play;
            pausecmds[slot] = pause;
            stopcmds[slot] = stop;
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

        public void stopbtnPushed(int slot)
        {
            stopcmds[slot].execute();
        }
    }
}
