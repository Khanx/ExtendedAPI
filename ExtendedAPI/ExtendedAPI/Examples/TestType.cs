using ExtendedAPI.Types;
using Pipliz;

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
        }
    }
