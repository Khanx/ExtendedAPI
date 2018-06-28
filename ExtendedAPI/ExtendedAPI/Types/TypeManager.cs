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

            if(!types.ContainsKey(newType.key))
                types.Add(newType.key, newType);
            else
                Log.Write(string.Format("<color=red>{0} already has a callback registered in ExtendedAPI.</color>", newType.key));
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
            BaseType myTypeOn = null;
            BaseType myTypeWith = null;
            ItemTypes.ItemType typeOn = null;
            ItemTypes.ItemType typeWith = null;

            bool clickOnType = playerClickedData.item1.typeHit != 0;   // Has clicked ON type (block in world)
            bool clickWithType = playerClickedData.item1.typeSelected != 0;   // Has clicked WITH type (on hand)

            if(!clickOnType && !clickWithType)
                return;

            if(clickOnType)
                typeOn = ItemTypes.GetType(playerClickedData.item1.typeHit);

            if(clickWithType)
                typeWith = ItemTypes.GetType(playerClickedData.item1.typeSelected);

            while(typeOn != null && !types.TryGetValue(typeOn.Name, out myTypeOn))
                typeOn = typeOn.ParentItemType;

            while(typeWith != null && !types.TryGetValue(typeWith.Name, out myTypeWith))
                typeWith = typeWith.ParentItemType;

            if(null != myTypeOn)
            {
                if(playerClickedData.item1.clickType == ClickType.Left)
                    myTypeOn.OnLeftClickOn(player, playerClickedData);
                else if(playerClickedData.item1.clickType == ClickType.Right)
                    myTypeOn.OnRightClickOn(player, playerClickedData);
            }

            if(null != myTypeWith)
            {
                if(playerClickedData.item1.clickType == ClickType.Left)
                    myTypeWith.OnLeftClickWith(player, playerClickedData);
                else if(playerClickedData.item1.clickType == ClickType.Right)
                    myTypeWith.OnRightClickWith(player, playerClickedData);
            }
        }

    }
}
