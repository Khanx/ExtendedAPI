using System;
using System.Collections.Generic;
using Pipliz;

namespace ExtendedAPI.Commands
{
    [ModLoader.ModManager]
    public static class CommandManager
    {
        private static List<Type> commands = new List<Type>();

        public static void Add(Type type)
        {
            commands.Add(type);
        }

        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterStartup, "Khanx.ExtendedAPI.RegisterCommands")]
        public static void RegisterCommands()
        {
            foreach(var command in commands)
            {
                BaseCommand newCommand = Activator.CreateInstance(command) as BaseCommand;

                if(newCommand.equalsTo.Count == 0 && newCommand.startWith.Count == 0)
                {
                    Log.Write("<color=red>Trying to add a BaseCommand without defining the equalsTo and startWith property.</color>");
                    continue;
                }

                ChatCommands.CommandManager.RegisterCommand(newCommand);
            }
        }
    }
}
