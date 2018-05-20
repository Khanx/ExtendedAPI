//#define EXAMPLES
using ExtendedAPI.Commands;

#if EXAMPLES
namespace ExtendedAPI.Examples
{

    [AutoLoadCommand]
    class TestCommand : BaseCommand
    {
        public TestCommand()
        {
            startWith.Add("/my_command");
        }

        public override bool TryDoCommand(Players.Player player, string chat)
        {
            if(player == null || player.ID == NetworkID.Server)
                return true;

            var args = ChatCommands.CommandManager.SplitCommand(chat);
            foreach(var arg in args)
                Pipliz.Chatting.Chat.SendToAll(string.Format("Argumentos: {0}", arg));

            return true;
        }
    }
}
#endif