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
            DisplayCommandLine("Github");
            await ReplyAsync("https://github.com/dryadt/valhallapp");
        }
        [Command("heroku")]
        public async Task Heroku()
        {
            DisplayCommandLine("Heroku");
            await ReplyAsync("https://dashboard.heroku.com/apps/valhallapp");
        }

        [Command("ping")]
        public async Task Ping()
        {
            DisplayCommandLine("Ping");
            TimeSpan ping = DateTime.Now - Context.Message.Timestamp;
            await ReplyAsync($"Pong: {ping.TotalMilliseconds} ms");
        }

        [Command("pong")]
        public async Task Pong()
        {
            DisplayCommandLine("Pong");
            await ReplyAsync("Ping");
        }

        [Command("website")]
        public async Task Website()
        {
            DisplayCommandLine("Website");
            await ReplyAsync("https://valhallapp.herokuapp.com/");
        }
        [Command("hi")]
        public async Task Reggie()
        {
            DisplayCommandLine("Reggie");
            await ReplyAsync("My name is reggie.");
        }
        void DisplayCommandLine(string CommandName)
        {
            Console.WriteLine($"{CommandName} command executed by user: {Context.User.Username} on channel: {Context.Channel.Name}");
        }
    }
}
