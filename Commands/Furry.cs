using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using static valhallappweb.PublicFunction;

namespace Valhallapp.Modules
{
    public class Furry : ModuleBase<SocketCommandContext>
    {
        [Command("furry")]
        public async Task FurryCommand()
        {
            SimpleCommands command = new SimpleCommands();
            await command.RandomCommand("furry", 1,100);
        }

        [Command("furry")]
        public async Task FurryCommand([Remainder] string param)
        {
            SimpleCommands command = new SimpleCommands();
            await command.RandomCommand(param,"gay", 1,100);
        }
    }
}
