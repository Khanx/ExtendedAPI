using Pipliz;
using System;
using System.Collections.Generic;
using static Shared.PlayerClickedData;

namespace ExtendedAPI.Types
{
    [ModLoader.ModManager]
    public static class TypeManager
    {
        private static Dictionary<string, BaseType> types = new Dictionary<string, BaseType>();

        public static void Add(Type type)
        {
            BaseType newType = Activator.CreateInstance(type) as BaseType;

            types.Add(newType.key, newType);
        }

        public static bool TryGet(string key, out BaseType type)
        {
            return types.TryGetValue(key, out type);
        }

        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "Khanx.ExtendedAPI.Register_OnAddType_OnRemoveType_OnUpdateAdjacentType")]
        public static void Register_OnAddType_OnRemoveType_OnUpdateAdjacentType()
        {
            foreach(BaseType type in types.Values)
            {
                Type typeOftype = type.GetType();

                if(typeOftype.GetMethod("RegisterOnAdd").DeclaringType == typeOftype)
                    ItemTypesServer.RegisterOnAdd(type.key, type.RegisterOnAdd);

                if(typeOftype.GetMethod("RegisterOnRemove").DeclaringType == typeOftype)
                    ItemTypesServer.RegisterOnRemove(type.key, type.RegisterOnRemove);

                if(typeOftype.GetMethod("RegisterOnUpdateAdjacent").DeclaringType == typeOftype)
                    ItemTypesServer.RegisterOnUpdateAdjacent(type.key, type.RegisterOnUpdateAdjacent);
            }
        }

        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnPlayerClicked, "Khanx.ExtendedAPI.OnPlayerClickedType")]
        public static void OnPlayerClicked(Players.Player player, Box<Shared.PlayerClickedData> playerClickedData)
        {
            BaseType myItem;
            bool clickOnType;   // Has clicked ON type (block in world)
            bool clickWithType;   // Has clicked WITH type (on hand)
            string typeClicked;
            string typeOnHand;

            clickOnType = ItemTypes.IndexLookup.IndexLookupTable.TryGetValue(playerClickedData.item1.typeHit, out typeClicked);
            clickWithType = ItemTypes.IndexLookup.IndexLookupTable.TryGetValue(playerClickedData.item1.typeSelected, out typeOnHand);

            if(playerClickedData.item1.clickType == ClickType.Left)
            {
                if(clickWithType)
                    if(types.TryGetValue(typeOnHand, out myItem))
                        myItem.OnLeftClickWith(player, playerClickedData);

                if(clickOnType)
                    if(types.TryGetValue(typeClicked, out myItem))
                        myItem.OnLeftClickOn(player, playerClickedData);
            }
            else if(playerClickedData.item1.clickType == ClickType.Right)
            {
                if(clickWithType)
                    if(types.TryGetValue(typeOnHand, out myItem))
                        myItem.OnRightClickWith(player, playerClickedData);

                if(clickOnType)
                    if(types.TryGetValue(typeClicked, out myItem))
                        myItem.OnRightClickOn(player, playerClickedData);
            }
        }
    }
}
