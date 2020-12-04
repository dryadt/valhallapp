using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using static valhallappweb.PublicFunction;

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
    }
}
