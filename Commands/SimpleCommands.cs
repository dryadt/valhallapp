using System;
using System.Threading.Tasks;
using Discord.Commands;
using static valhallappweb.PublicFunction;

namespace Valhallapp.Modules
{
    public class SimpleCommands : ModuleBase<SocketCommandContext>
    {

        [Command("github")]
        public async Task Github()
        {
            DisplayCommandLine("Github",Context.Message.Author.Username,Context.Channel.Name);
            await ReplyAsync("https://github.com/dryadt/valhallapp");
        }
        [Command("heroku")]
        public async Task Heroku()
        {
            DisplayCommandLine("Heroku", Context.Message.Author.Username, Context.Channel.Name);
            await ReplyAsync("https://dashboard.heroku.com/apps/valhallapp");
        }

        [Command("ping")]
        public async Task Ping()
        {
            DisplayCommandLine("Ping", Context.Message.Author.Username, Context.Channel.Name);
            TimeSpan ping = DateTime.Now - Context.Message.Timestamp;
            await ReplyAsync($"Pong: {ping.TotalMilliseconds} ms");
        }

        [Command("pong")]
        public async Task Pong()
        {
            DisplayCommandLine("Pong", Context.Message.Author.Username, Context.Channel.Name);
            await ReplyAsync("Ping");
        }

        [Command("website")]
        public async Task Website()
        {
            DisplayCommandLine("Website", Context.Message.Author.Username, Context.Channel.Name);
            await ReplyAsync("https://valhallapp.herokuapp.com/");
        }

        // RANDOM COMMAND BASIC FUNCTIONS
        public async Task RandomCommand(SocketCommandContext context, string name, ulong randomModifier, int isRigged)
        {
            DisplayCommandLine(name, context.Message.Author.Username, context.Channel.Name);
            Random rnd = new Random((int)(Convert.ToUInt64(context.User.Id) + randomModifier % 10000000));
            if (isRigged == -1)
                await context.Channel.SendMessageAsync(embed:
                    PostEmbedPercent(context.User.Username, $"<@{context.User.Id}>", context.User.GetAvatarUrl(), rnd.Next(101), name));
            else
                await context.Channel.SendMessageAsync(embed:
                    PostEmbedPercent(context.User.Username, $"<@{context.User.Id}>", context.User.GetAvatarUrl(), isRigged, name));
        }

        public async Task RandomCommand(SocketCommandContext context, [Remainder] string param, string name, ulong randomModifier, int isRigged)
        {
            DisplayCommandLine($"{name} with params", context.Message.Author.Username, context.Channel.Name);
            Random rnd;
            string userID = param;
            // userping function
            if (param.Contains("<@"))
            {
                userID = userID.Remove(userID.Length - 1);
                userID = userID.Substring(2, userID.Length - 2);
                if (userID[0] == '!') userID = userID.Substring(1, userID.Length - 1);
                rnd = new Random((int)(Convert.ToUInt64(userID) + randomModifier % 10000000));
                if (isRigged == -1)
                    await context.Channel.SendMessageAsync(embed:
                        PostEmbedPercent(context.User.Username, $"<@{userID}>", context.User.GetAvatarUrl(), rnd.Next(101), name));
                else
                    await context.Channel.SendMessageAsync(embed:
                        PostEmbedPercent(context.User.Username, $"<@{userID}>", context.User.GetAvatarUrl(), isRigged, name));

            }
            // id function
            else if (IsDigitsOnly(userID))
            {
                rnd = new Random((int)(Convert.ToUInt64(userID) + randomModifier % 10000000));
                if (Convert.ToUInt64(userID) == 156997866605248512) await ReplyAsync(embed:
                    PostEmbedPercent(context.User.Username, $"<@{userID}>", context.User.GetAvatarUrl(), 101, name));
                if (isRigged == -1)
                    await context.Channel.SendMessageAsync(embed:
                        PostEmbedPercent(context.User.Username, $"<@{userID}>", context.User.GetAvatarUrl(), rnd.Next(101), name));
                else
                    await context.Channel.SendMessageAsync(embed:
                        PostEmbedPercent(context.User.Username, $"<@{userID}>", context.User.GetAvatarUrl(), isRigged, name));

            }
            // TODO: username function
            // random string function
            else
            {
                rnd = new Random();
                if (isRigged == -1)
                    await context.Channel.SendMessageAsync(embed:
                        PostEmbedPercent(context.User.Username, $"{param}", context.User.GetAvatarUrl(), rnd.Next(101), name));
                else
                    await context.Channel.SendMessageAsync(embed:
                        PostEmbedPercent(context.User.Username, $"{param}", context.User.GetAvatarUrl(), isRigged, name));

            }
        }
    }
}
