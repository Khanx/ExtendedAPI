using Pipliz;
using System;
using System.Collections.Generic;

namespace ExtendedAPI.Types
    {
    public static class TypeManager
        {
        private static List<Type> toRegister = new List<Type>();

        public static void Add(Type type)
            {
            toRegister.Add(type);
            }

        public static void RegisterCallBacks()
            {
            foreach(var type in toRegister)
                {
                var newType = Activator.CreateInstance(type) as BaseType;

                if(type.GetMethod("RegisterOnAdd").DeclaringType == type)
                    ItemTypesServer.RegisterOnAdd(newType.key, newType.RegisterOnAdd);

                if(type.GetMethod("RegisterOnRemove").DeclaringType == type)
                    ItemTypesServer.RegisterOnRemove(newType.key, newType.RegisterOnRemove);

                if(type.GetMethod("RegisterOnUpdateAdjacent").DeclaringType == type)
                    ItemTypesServer.RegisterOnUpdateAdjacent(newType.key, newType.RegisterOnUpdateAdjacent);
                }
            toRegister.Clear();
            }

        }
    }
