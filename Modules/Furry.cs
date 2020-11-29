using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Valhallapp.Modules
{
    public class Furry : ModuleBase<SocketCommandContext>
    {
        [Command("furry")]
        public async Task FurryCommand()
        {
            SimpleCommands.DisplayCommandLine("Furry", Context);
            await ReplyAsync(embed:
                SimpleCommands.PostEmbedPercent(Context.User.Username, $"<@{Context.User.Id}>", Context.User.GetAvatarUrl(), 100, "furry"));
        }

        [Command("furry")]
        public async Task FurryCommand([Remainder] string param)
        {
            SimpleCommands.DisplayCommandLine("Furry with param", Context);
            if (SimpleCommands.IsDigitsOnly(param)) await ReplyAsync(embed:
                SimpleCommands.PostEmbedPercent(Context.User.Username, $"<@{param}>", Context.User.GetAvatarUrl(), 100, "furry"));
            // TODO: username function
            else await ReplyAsync(embed:
                SimpleCommands.PostEmbedPercent(Context.User.Username, $"{param}", Context.User.GetAvatarUrl(), 100, "furry"));
        }
    }
}
