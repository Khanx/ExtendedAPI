namespace ExtendedAPI.Types
{
    public class BaseType
    {
        public string key { get; protected set; }

        /// <summary>
        /// When this type is added on the world in the position {position} by the player {causedBy}
        /// </summary>
        /// <param name="position">Position of the block</param>
        /// <param name="newType">Ask ZUN</param>
        /// <param name="causedBy">Player who has added the block</param>
        public virtual void RegisterOnAdd(Pipliz.Vector3Int position, ushort newType, Players.Player causedBy) { }

        /// <summary>
        /// When this type is removed from the world in the position {position} by the player {causedBy}
        /// </summary>
        /// <param name="position">Position of the block</param>
        /// <param name="newType">Ask ZUN</param>
        /// <param name="causedBy">Player who has removed the block</param>
        public virtual void RegisterOnRemove(Pipliz.Vector3Int position, ushort type, Players.Player causedBy) { }

        /// <summary>
        /// Will be triggered if a type directly adjacent to this one (no diagonals) is changed
        /// </summary>
        /// <param name="onUpdateAdjacent">Info, check the structure</param>
        public virtual void RegisterOnUpdateAdjacent(ItemTypesServer.OnUpdateData onUpdateAdjacent) { }

        /// <summary>
        /// Will be triggered when a player {player} makes left click on this type (the type must be in the world)
        /// </summary>
        /// <param name="player">Player who makes the action</param>
        /// <param name="boxedData">Ask ZUN</param>
        public virtual void OnLeftClickOn(Players.Player player, Pipliz.Box<Shared.PlayerClickedData> boxedData) { }

        /// <summary>
        /// Will be triggered when a player {player} makes right click on this type (the type must be in the world)
        /// </summary>
        /// <param name="player">Player who makes the action</param>
        /// <param name="boxedData">Ask ZUN</param>
        public virtual void OnRightClickOn(Players.Player player, Pipliz.Box<Shared.PlayerClickedData> boxedData) { }

        /// <summary>
        /// Will be triggered when a player {player} makes left click while holding this type on hand
        /// </summary>
        /// <param name="player">Player who makes the action</param>
        /// <param name="boxedData">Ask ZUN</param>
        public virtual void OnLeftClickWith(Players.Player player, Pipliz.Box<Shared.PlayerClickedData> boxedData) { }

        /// <summary>
        /// Will be triggered when a player {player} makes right click while holding this type on hand
        /// </summary>
        /// <param name="player">Player who makes the action</param>
        /// <param name="boxedData">Ask ZUN</param>
        public virtual void OnRightClickWith(Players.Player player, Pipliz.Box<Shared.PlayerClickedData> boxedData) { }
    }
}
