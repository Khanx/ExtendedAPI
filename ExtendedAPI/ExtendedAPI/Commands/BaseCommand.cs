using System;
using System.Collections.Generic;
using ChatCommands;

namespace ExtendedAPI.Commands
    {
    class BaseCommand : IChatCommand
        {
        public List<string> startWith = new List<string>();

        public bool IsCommand(string chat)
            {
            bool isCommand = false;
            foreach(var start in startWith)
                if(chat.StartsWith(start, StringComparison.OrdinalIgnoreCase))
                    isCommand = true;

            return isCommand;
            }

        public virtual bool TryDoCommand(Players.Player player, string chat)
            {
            return false;
            }
        }
    }
