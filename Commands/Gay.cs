using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using static valhallappweb.PublicFunction;

namespace Valhallapp.Modules
{
    public class Gay : ModuleBase<SocketCommandContext>
    {
        [Command("gay")]
        public async Task GayCommand()
        {
            SimpleCommands command = new SimpleCommands();
            await command.RandomCommand("gay", 1, -1);
        }


        [Command("gay")]
        public async Task GayCommand([Remainder] string param)
        {
            SimpleCommands command = new SimpleCommands();
            await command.RandomCommand(param, "gay", 1, -1);
        }
    }
}
