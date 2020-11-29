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
            DisplayCommandLine("Furry");
            await ReplyAsync(embed:
                PostEmbedPercent(Context.User.Username, $"<@{Context.User.Id}>", Context.User.GetAvatarUrl(), 100, "furry"));
        }

        [Command("furry")]
        public async Task FurryCommand([Remainder] string param)
        {
            DisplayCommandLine("Furry with param");
            if (IsDigitsOnly(param)) await ReplyAsync(embed:
                PostEmbedPercent(Context.User.Username, $"<@{param}>", Context.User.GetAvatarUrl(), 100, "furry"));
            // TODO: username function
            else await ReplyAsync(embed:
                PostEmbedPercent(Context.User.Username, $"{param}", Context.User.GetAvatarUrl(), 100, "furry"));
        }

        void DisplayCommandLine(string CommandName)
        {
            Console.WriteLine($"{CommandName} command executed by user: {Context.User.Username} on channel: {Context.Channel.Name}");
        }
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        Embed PostEmbedPercent(string username, string selectedUser, string userIconURL, int percent, string commandType)
        {
            // removes all urls
            var embed = new EmbedBuilder();
            embed.WithAuthor(username, userIconURL)
                .WithDescription($"{selectedUser} is {percent}% {commandType}")
                .WithColor(Color.DarkBlue)
                .Build();
            return embed.Build();
        }
    }
}
