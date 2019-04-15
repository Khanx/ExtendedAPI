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
            equalsTo.Add("/command");   //Command without args
            startWith.Add("/command_arg");  //Command with args
        }

        public override bool TryDoCommand(Players.Player player, string command)
        {
            if(player == null || player.ID == NetworkID.Server)
                return true;

            var args = ChatCommands.CommandManager.SplitCommand(command);
            foreach(var arg in args)
                Pipliz.Chatting.Chat.SendToAll(string.Format("Argumentos: {0}", arg));

            return true;
        }
    }
}
#endif