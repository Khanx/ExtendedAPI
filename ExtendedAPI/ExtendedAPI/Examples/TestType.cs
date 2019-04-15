//#define EXAMPLES
using ExtendedAPI.Types;
using Pipliz;
using Shared;

#if EXAMPLES
namespace ExtendedAPI.Examples
{
    [AutoLoadType]
    class TestType : BaseType
    {
        public TestType()
        {
            key = "planks";
        }

        public override void RegisterOnAdd(Vector3Int position, ushort newType, Players.Player causedBy)
        {
            Pipliz.Chatting.Chat.SendToAll("Testing RegisterOnAdd");
        }

        public override void RegisterOnRemove(Vector3Int position, ushort type, Players.Player causedBy)
        {
            Pipliz.Chatting.Chat.SendToAll("Testing RegisterOnRemove");
        }

        public override void RegisterOnUpdateAdjacent(ItemTypesServer.OnUpdateData onUpdateAdjacent)
        {
            Pipliz.Chatting.Chat.SendToAll("Testing RegisterOnUpdateAdjacent");
        }

        public override void OnLeftClickOn(Players.Player player, Box<PlayerClickedData> boxedData)
        {
            Pipliz.Chatting.Chat.SendToAll("Testing OnLeftClickOn");
        }

        public override void OnLeftClickWith(Players.Player player, Box<PlayerClickedData> boxedData)
        {
            Pipliz.Chatting.Chat.SendToAll("Testing OnLeftClickWith");
        }

        public override void OnRightClickOn(Players.Player player, Box<PlayerClickedData> boxedData)
        {
            Pipliz.Chatting.Chat.SendToAll("Testing OnRightClickOn");
        }

        public override void OnRightClickWith(Players.Player player, Box<PlayerClickedData> boxedData)
        {
            Pipliz.Chatting.Chat.SendToAll("Testing OnRightClickWith");
        }
    }
}
#endif