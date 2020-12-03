using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Valhallapp.Modules
{
    public class SimpleCommands : ModuleBase<SocketCommandContext>
    {

        [Command("github")]
        public async Task Github()
        {
            DisplayCommandLine("Github", Context);
            await ReplyAsync("https://github.com/dryadt/valhallapp");
        }
        [Command("heroku")]
        public async Task Heroku()
        {
            DisplayCommandLine("Heroku", Context);
            await ReplyAsync("https://dashboard.heroku.com/apps/valhallapp");
        }

        [Command("ping")]
        public async Task Ping()
        {
            DisplayCommandLine("Ping", Context);
            TimeSpan ping = DateTime.Now - Context.Message.Timestamp;
            await ReplyAsync($"Pong: {ping.TotalMilliseconds} ms");
        }

        [Command("pong")]
        public async Task Pong()
        {
            DisplayCommandLine("Pong", Context);
            await ReplyAsync("Ping");
        }

        [Command("website")]
        public async Task Website()
        {
            DisplayCommandLine("Website", Context);
            await ReplyAsync("https://valhallapp.herokuapp.com/");
        }
        [Command("hi")]
        public async Task Reggie()
        {
            DisplayCommandLine("Reggie", Context);
            await ReplyAsync("My name is reggie.");
        }
        public static void DisplayCommandLine(string CommandName, SocketCommandContext Context)
        {
            //Console.WriteLine($"{CommandName} command executed by user: {Context.User.Username} on channel: {Context.Channel.Name}");
        }
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
                if (c < '0' || c > '9')
                    return false;
            return true;
        }
        public static Embed PostEmbedPercent(string username, string selectedUser, string userIconURL, int percent, string commandType)
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
