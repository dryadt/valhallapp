using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Valhallapp.Modules
{
    public class Femboy : ModuleBase<SocketCommandContext>
    {
        [Command("femboy")]
        public async Task FemboyCommand()
        {
            DisplayCommandLine("Femboy");
            Console.WriteLine($"id user: {Context.User.Id}");
            Random rnd = new Random((int)(Convert.ToUInt64(Context.User.Id) % 10000000));
            await ReplyAsync(embed:
                PostEmbedPercent(Context.User.Username, $"<@{Context.User.Id}>", Context.User.GetAvatarUrl(), rnd.Next(101), "femboy"));
        }
        [Command("femboy")]
        public async Task FemboyCommand([Remainder] string param)
        {
            DisplayCommandLine($"Femboy with params");
            Random rnd;
            string userID = param;
            // ping function
            if (param.Contains("<@"))
            {
                // gets only the id
                userID = userID.Remove(userID.Length - 1);
                userID = userID.Substring(2, userID.Length - 2);
                if (userID[0] == '!') userID = userID.Substring(1, userID.Length - 1);
                // rng creation
                rnd = new Random((int)(Convert.ToUInt64(userID) % 10000000));
                await ReplyAsync(embed:
                    PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), rnd.Next(101), "femboy"));
            }
            // id function
            else if (IsDigitsOnly(userID))
            {
                rnd = new Random((int)(Convert.ToUInt64(userID) % 10000000));
                await ReplyAsync(embed:
                    PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), rnd.Next(101), "femboy"));
            }
            // TODO: username function
            // random string function
            else
            {
                rnd = new Random();
                await ReplyAsync(embed:
                    PostEmbedPercent(Context.User.Username, param, Context.User.GetAvatarUrl(), rnd.Next(101), "femboy"));
            }
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
