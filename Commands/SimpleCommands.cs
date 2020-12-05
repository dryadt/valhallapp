using System;
using System.Threading.Tasks;
using Discord.Commands;
using static valhallappweb.PublicFunction;

namespace Valhallapp.Modules
{
    public class SimpleCommands : ModuleBase<SocketCommandContext>
    {
        public SimpleCommands () { }

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

        // RANDOM COMMAND BASIC FUNCTIONS
        public async Task RandomCommand(string name, ulong randomModifier, int isRigged)
        {
            Console.WriteLine("No param");
            DisplayCommandLine(name, Context);
            Random rnd = new Random((int)(Convert.ToUInt64(Context.User.Id) + randomModifier % 10000000));
            if (isRigged != -1)
                await ReplyAsync(embed:
                    PostEmbedPercent(Context.User.Username, $"<@{Context.User.Id}>", Context.User.GetAvatarUrl(), rnd.Next(101), name));
            else
                await ReplyAsync(embed:
                    PostEmbedPercent(Context.User.Username, $"<@{Context.User.Id}>", Context.User.GetAvatarUrl(), isRigged, name));
        }

        public async Task RandomCommand([Remainder] string param, string name, ulong randomModifier, int isRigged)
        {
            DisplayCommandLine($"{name} with params", Context);
            Random rnd;
            string userID = param;
            // userping function
            if (param.Contains("<@"))
            {
                userID = userID.Remove(userID.Length - 1);
                userID = userID.Substring(2, userID.Length - 2);
                if (userID[0] == '!') userID = userID.Substring(1, userID.Length - 1);
                rnd = new Random((int)(Convert.ToUInt64(userID) + randomModifier % 10000000));
                if (isRigged!=-1)
                    await ReplyAsync(embed:
                        PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), rnd.Next(101), name));
                else
                    await ReplyAsync(embed:
                        PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), isRigged, name));

            }
            // id function
            else if (IsDigitsOnly(userID))
            {
                rnd = new Random((int)(Convert.ToUInt64(userID) + randomModifier % 10000000));
                if (Convert.ToUInt64(userID) == 156997866605248512) await ReplyAsync(embed:
                    PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), 101, name));
                if (isRigged != -1)
                    await ReplyAsync(embed:
                        PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), rnd.Next(101), name));
                else
                    await ReplyAsync(embed:
                        PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), isRigged, name));

            }
            // TODO: username function
            // random string function
            else
            {
                rnd = new Random();
                if (isRigged != -1)
                    await ReplyAsync(embed:
                        PostEmbedPercent(Context.User.Username, $"{param}", Context.User.GetAvatarUrl(), rnd.Next(101), name));
                else
                    await ReplyAsync(embed:
                        PostEmbedPercent(Context.User.Username, $"{param}", Context.User.GetAvatarUrl(), isRigged, name));

            }
        }
    }
}
