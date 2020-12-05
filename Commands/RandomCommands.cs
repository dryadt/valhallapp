using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using static valhallappweb.PublicFunction;

namespace Valhallapp.Modules
{
    public class RandomCommands : ModuleBase<SocketCommandContext>
    {
        private readonly SimpleCommands command = new SimpleCommands();

        [Command("tomboy")]
        public async Task TomboyCommand() => await command.RandomCommand(Context, "tomboy", 1, 0);

        [Command("tomboy")]
        public async Task TomboyCommand([Remainder] string param) => await command.RandomCommand(Context, param, "tomboy", 1, 0);

        [Command("furry")]
        public async Task FurryCommand() => await command.RandomCommand(Context, "furry", 1, 100);

        [Command("furry")]
        public async Task FurryCommand([Remainder] string param) => await command.RandomCommand(Context, param, "furry", 1, 100);

        [Command("femboy")]
        public async Task FemboyCommand() => await command.RandomCommand(Context, "femboy", 0, -1);

        [Command("femboy")]
        public async Task FemboyCommand([Remainder] string param) => await command.RandomCommand(Context, param, "femboy", 0, -1);

        [Command("gay")]
        public async Task GayCommand() => await command.RandomCommand(Context, "gay", 1, -1);

        [Command("gay")]
        public async Task GayCommand([Remainder] string param) => await command.RandomCommand(Context, param, "gay", 1, -1);

        [Command("realfurry")]
        public async Task RealFurryCommand() => await command.RandomCommand(Context, "furry", 2, -1);

        [Command("realfurry")]
        public async Task RealFurryCommand([Remainder] string param) => await command.RandomCommand(Context, param, "furry", 2, -1);

        [Command("slut")]
        public async Task SlutCommand() => await command.RandomCommand(Context, "furry", 3, -1);

        [Command("slut")]
        public async Task SlutCommand([Remainder] string param) => await command.RandomCommand(Context, param, "furry", 3, -1);
    }
}
