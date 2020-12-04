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
            DisplayCommandLine("Furry", Context);
            await ReplyAsync(embed:
                PostEmbedPercent(Context.User.Username, $"<@{Context.User.Id}>", Context.User.GetAvatarUrl(), 100, "furry"));
        }

        [Command("furry")]
        public async Task FurryCommand([Remainder] string param)
        {
            DisplayCommandLine("Furry with param", Context);
            if (IsDigitsOnly(param)) await ReplyAsync(embed:
                PostEmbedPercent(Context.User.Username, $"<@{param}>", Context.User.GetAvatarUrl(), 100, "furry"));
            // TODO: username function
            else await ReplyAsync(embed:
                PostEmbedPercent(Context.User.Username, $"{param}", Context.User.GetAvatarUrl(), 100, "furry"));
        }
    }
}
