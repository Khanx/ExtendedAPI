using Pipliz;
using System;
using System.Collections.Generic;

namespace ExtendedAPI.Types
    {
    public static class TypeManager
        {
        private static Dictionary<string, BaseType> toCall = new Dictionary<string, BaseType>();

        public static void Add(BaseType type)
            {
            toCall.Add(type.key, type);
            }

        public static void RegisterCallBacks()
            {
                foreach(var type in toCall)
                {
                    //ItemTypesServer.OnChange
                }
            }

        }
    }
