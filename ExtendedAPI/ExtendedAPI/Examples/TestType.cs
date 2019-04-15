#define EXAMPLES
using System.Collections.Generic;
using BlockEntities;
using Pipliz;
using Chatting;

#if EXAMPLES
namespace ExtendedAPI.Examples
{
    [BlockEntityAutoLoader]
    public class TestType : IChangedWithType, ISingleBlockEntityMapping, IUpdatedAdjacentType
    {
        public ItemTypes.ItemType TypeToRegister { get { return BlockTypes.BuiltinBlocks.Types.planks; } }

        public virtual void OnChangedWithType(Chunk chunk, BlockChangeRequestOrigin origin, Vector3Int blockPosition, ItemTypes.ItemType typeOld, ItemTypes.ItemType typeNew)
        {
            //OnRemove
            if(typeNew == BlockTypes.BuiltinBlocks.Types.air)
                Chat.SendToConnected("Planks removed");

            //OnAdd
            if(typeOld == BlockTypes.BuiltinBlocks.Types.air)
                Chat.SendToConnected("Planks add");
        }

        public void OnUpdateAdjacent(AdjacentUpdateData data)
        {
            Chat.SendToConnected("Adjacent updated");
        }

        //Not implemented: click interaction
    }
}
#endif