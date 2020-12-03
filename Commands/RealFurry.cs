using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Valhallapp.Modules
{
    public class RealFurry : ModuleBase<SocketCommandContext>
    {
        [Command("realfurry")]
        public async Task RealFurryCommand()
        {
            SimpleCommands.DisplayCommandLine("Furry", Context);
            Random rnd = new Random((int)(Convert.ToUInt64(Context.User.Id) + 2 % 10000000));
            await ReplyAsync(embed:
                SimpleCommands.PostEmbedPercent(Context.User.Username, $"<@{Context.User.Id}>", Context.User.GetAvatarUrl(), rnd.Next(101), "furry"));
        }

        [Command("realfurry")]
        public async Task RealFurryCommand([Remainder] string param)
        {
            SimpleCommands.DisplayCommandLine("Furry with params", Context);
            Random rnd;
            string userID = param;
            // userping function
            if (param.Contains("<@"))
            {
                Console.WriteLine("userping");
                userID = userID.Remove(userID.Length - 1);
                userID = userID.Substring(2, userID.Length - 2);
                if (userID[0] == '!') userID = userID.Substring(1, userID.Length - 1);
                rnd = new Random((int)(Convert.ToUInt64(userID) + 2 % 10000000));
                await ReplyAsync(embed:
                    SimpleCommands.PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), rnd.Next(101), "furry"));
            }
            // id function
            else if (SimpleCommands.IsDigitsOnly(userID))
            {
                Console.WriteLine("id");
                rnd = new Random((int)(Convert.ToUInt64(userID) + 2 % 10000000));
                await ReplyAsync(embed:
                    SimpleCommands.PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), rnd.Next(101), "furry"));
            }
            // TODO: username function
            // random string function
            else
            {
                Console.WriteLine("Nothing");
                rnd = new Random();
                await ReplyAsync(embed:
                    SimpleCommands.PostEmbedPercent(Context.User.Username, $"{param}", Context.User.GetAvatarUrl(), rnd.Next(101), "furry"));
            }
        }
    }
}