using Pipliz;
using System;
using System.Collections.Generic;
using static Shared.PlayerClickedData;

namespace ExtendedAPI.Types
    {
    public static class TypeManager
        {
        private static List<Type> toRegister = new List<Type>();
        private static Dictionary<string, BaseType> toCall = new Dictionary<string, BaseType>();

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

                toCall.Add(newType.key, newType);
                }
            toRegister.Clear();
            }

        public static void OnPlayerClicked(Players.Player player, Box<Shared.PlayerClickedData> boxedData)
            {

            BaseType myItem;
            bool clickOnType;   // Has clicked ON type (block in world)
            bool clickWithType;   // Has clicked WITH type (on hand)
            string itemClicked;
            string itemOnHand;

            clickOnType = ItemTypes.IndexLookup.IndexLookupTable.TryGetValue(boxedData.item1.typeHit, out itemClicked);
            clickWithType = ItemTypes.IndexLookup.IndexLookupTable.TryGetValue(boxedData.item1.typeSelected, out itemOnHand);

            if(boxedData.item1.clickType == ClickType.Left)
                {
                if(clickWithType)
                    if(toCall.TryGetValue(itemOnHand, out myItem))
                        myItem.OnLeftClickWith(player, boxedData);

                if(clickOnType)
                    if(toCall.TryGetValue(itemClicked, out myItem))
                        myItem.OnLeftClickOn(player, boxedData);
                }
            else if(boxedData.item1.clickType == ClickType.Right)
                {
                if(clickWithType)
                    if(toCall.TryGetValue(itemOnHand, out myItem))
                        myItem.OnRightClickWith(player, boxedData);

                    if(clickOnType)
                        if(toCall.TryGetValue(itemClicked, out myItem))
                            myItem.OnRightClickOn(player, boxedData);
                }
            }

        }
    }
