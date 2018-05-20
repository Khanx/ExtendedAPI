using System;
using System.Collections.Generic;
using ChatCommands;

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
                ChatCommands.CommandManager.RegisterCommand(newCommand);
            }
        }
    }
}
