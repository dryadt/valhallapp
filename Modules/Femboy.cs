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
            SimpleCommands.DisplayCommandLine("Femboy", Context);
            Console.WriteLine($"id user: {Context.User.Id}");
            Random rnd = new Random((int)(Convert.ToUInt64(Context.User.Id) % 10000000));
            await ReplyAsync(embed:
                SimpleCommands.PostEmbedPercent(Context.User.Username, $"<@{Context.User.Id}>", Context.User.GetAvatarUrl(), rnd.Next(101), "femboy"));
        }
        [Command("femboy")]
        public async Task FemboyCommand([Remainder] string param)
        {
            SimpleCommands.DisplayCommandLine($"Femboy with params", Context);
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
                    SimpleCommands.PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), rnd.Next(101), "femboy"));
            }
            // id function
            else if (SimpleCommands.IsDigitsOnly(userID))
            {
                rnd = new Random((int)(Convert.ToUInt64(userID) % 10000000));
                await ReplyAsync(embed:
                    SimpleCommands.PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), rnd.Next(101), "femboy"));
            }
            // TODO: username function
            // random string function
            else
            {
                rnd = new Random();
                await ReplyAsync(embed:
                    SimpleCommands.PostEmbedPercent(Context.User.Username, param, Context.User.GetAvatarUrl(), rnd.Next(101), "femboy"));
            }
        }
    }
}
