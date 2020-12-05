using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Valhallapp.Modules
{
    public class RandomCommands : ModuleBase<SocketCommandContext>
    {
        private readonly SimpleCommands command = new SimpleCommands();

        [Command("furry")]
        public async Task FurryCommand()=>await command.RandomCommand("furry", 1, 100);

        [Command("furry")]
        public async Task FurryCommand([Remainder] string param) => await command.RandomCommand(param, "furry", 1, 100);

        [Command("femboy")]
        public async Task FemboyCommand() => await command.RandomCommand("femboy", 0, -1);

        [Command("femboy")]
        public async Task FemboyCommand([Remainder] string param) => await command.RandomCommand(param, "femboy", 0, -1);

        [Command("gay")]
        public async Task GayCommand() => await command.RandomCommand("gay", 1, -1);

        [Command("gay")]
        public async Task GayCommand([Remainder] string param) => await command.RandomCommand(param, "gay", 1, -1);

        [Command("realfurry")]
        public async Task RealFurryCommand() => await command.RandomCommand("furry", 2, -1);

        [Command("realfurry")]
        public async Task RealFurryCommand([Remainder] string param) => await command.RandomCommand(param, "furry", 2, -1);

        [Command("slut")]
        public async Task SlutCommand() => await command.RandomCommand("furry", 3, -1);

        [Command("slut")]
        public async Task SlutCommand([Remainder] string param) => await command.RandomCommand(param, "furry", 3, -1);
    }
}
