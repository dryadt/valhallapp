using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using static valhallappweb.PublicFunction;


namespace Valhallapp.Modules
{
    public class Femboy : ModuleBase<SocketCommandContext>
    {
        [Command("femboy")]
        public async Task FemboyCommand()
        {
            SimpleCommands command = new SimpleCommands();
            await command.RandomCommand("femboy", 0,-1);
        }
        [Command("femboy")]
        public async Task FemboyCommand([Remainder] string param)
        {
            SimpleCommands command = new SimpleCommands();
            await command.RandomCommand(param, "femboy", 0, -1);
        }
    }
}
