#define EXAMPLES
using System.Collections.Generic;
using Chatting;

#if EXAMPLES
namespace ExtendedAPI.Examples
{

    [ChatCommandAutoLoader]
    class TestCommand : IChatCommand
    {
        public bool TryDoCommand(Players.Player player, string chat, List<string> splits)
        {
            if(!chat.StartsWith("/command"))
                return false;

            if(player == null || player.ID == NetworkID.Server)
                return true;

            foreach(var arg in splits)
                Chat.Send(player, string.Format("Argumentos: {0}", arg));

            return true;
        }
    }
}
#endif