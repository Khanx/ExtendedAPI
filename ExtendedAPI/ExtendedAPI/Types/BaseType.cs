namespace ExtendedAPI.Types
    {

    public class BaseType
        {
        public string key;

        /// <summary>
        /// When the block is added on the world in the position {position} by the player {causedBy}
        /// </summary>
        /// <param name="position">Position of the block</param>
        /// <param name="newType"> ¿? Ask ZUN</param>
        /// <param name="causedBy"> Player who has added the block</param>
        public virtual void RegisterOnAdd(Pipliz.Vector3Int position, ushort newType, Players.Player causedBy)
            { }

        /// <summary>
        /// When the block is removed from the world in the position {position} by the player {causedBy}
        /// </summary>
        /// <param name="position">Position of the block</param>
        /// <param name="newType"> ¿? Ask ZUN</param>
        /// <param name="causedBy"> Player who has removed the block</param>
        public virtual void RegisterOnRemove(Pipliz.Vector3Int position, ushort type, Players.Player causedBy)
            { }

        /// <summary>
        /// Will be triggered if a block directly adjacent to it (no diagonals) is changed
        /// </summary>
        /// <param name="onUpdateAdjacent">Info, check the structure</param>
        public virtual void RegisterOnUpdateAdjacent(ItemTypesServer.OnUpdateData onUpdateAdjacent)
            { }
        }

    }
