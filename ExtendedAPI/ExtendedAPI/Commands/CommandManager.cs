using System;
using System.Collections.Generic;
using Pipliz;

namespace ExtendedAPI.Commands
{
    [ModLoader.ModManager]
    public static class CommandManager
    {
        private static List<BaseCommand> commands = new List<BaseCommand>();

        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterStartup, "Khanx.ExtendedAPI.LoadCommands")]
        [ModLoader.ModCallbackProvidesFor("Khanx.ExtendedAPI.RegisterCommands")]
        public static void LoadCommands()
        {
            foreach(var modAssembly in ModLoader.LoadedMods)
            {
                if(modAssembly.HasAssembly)
                {
                    foreach(Type type in modAssembly.LoadedAssemblyTypes)
                    {
                        if(type.IsDefined(typeof(AutoLoadCommandAttribute), true))
                        {
                            BaseCommand newCommand = Activator.CreateInstance(type) as BaseCommand;

                            commands.Add(newCommand);
                        }
                    }
                }
            }
        }

        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterStartup, "Khanx.ExtendedAPI.RegisterCommands")]
        public static void RegisterCommands()
        {
            foreach(BaseCommand command in commands)
            {
                if(command.equalsTo.Count == 0 && command.startWith.Count == 0)
                {
                    Log.Write("<color=red>Trying to add a BaseCommand without defining the equalsTo and startWith property.</color>");
                    continue;
                }

                ChatCommands.CommandManager.RegisterCommand(command);
            }
        }
    }
}
