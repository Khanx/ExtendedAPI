using System;
using System.Collections.Generic;
using ChatCommands;

namespace ExtendedAPI.Commands
    {
    public static class CommandManager
        {
        private static List<Type> toRegister = new List<Type>();

        public static void Add(Type type)
            {
            toRegister.Add(type);
            }

        public static void Register()
            {
            foreach(var command in toRegister)
                {
                BaseCommand newCommand = Activator.CreateInstance(command) as BaseCommand;
                ChatCommands.CommandManager.RegisterCommand(newCommand);
                }
            }
        }
    }
