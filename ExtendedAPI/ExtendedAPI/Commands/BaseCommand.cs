using System;
using System.Collections.Generic;
using ChatCommands;

namespace ExtendedAPI.Commands
{
    public class BaseCommand : IChatCommand
    {
        protected List<string> equalsTo = new List<string>(); //Use when a command has no arguments
        protected List<string> startWith = new List<string>(); //Use when a command has arguments

        public bool IsCommand(string chat)
        {
            foreach(string start in startWith)
                if(chat.StartsWith(start, StringComparison.OrdinalIgnoreCase))
                    return true;

            foreach(string start in equalsTo)
                if(chat.Equals(start, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }

        public virtual bool TryDoCommand(Players.Player player, string command)
        {
            throw new NotImplementedException();
        }
    }
}
