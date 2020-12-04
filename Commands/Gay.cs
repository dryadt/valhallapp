using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using static valhallappweb.PublicFunction;

namespace Valhallapp.Modules
{
    public class Gay : ModuleBase<SocketCommandContext>
    {
        [Command("gay")]
        public async Task GayCommand()
        {
            DisplayCommandLine("Gay", Context);
            Random rnd = new Random((int)(Convert.ToUInt64(Context.User.Id) + 1 % 10000000));
            await ReplyAsync(embed:
                PostEmbedPercent(Context.User.Username, $"<@{Context.User.Id}>", Context.User.GetAvatarUrl(), rnd.Next(101), "gay"));
        }


        [Command("gay")]
        public async Task GayCommand([Remainder] string param)
        {
            DisplayCommandLine("Gay with params", Context);
            Random rnd;
            string userID = param;
            // userping function
            if (param.Contains("<@"))
            {
                Console.WriteLine("userping");
                userID = userID.Remove(userID.Length - 1);
                userID = userID.Substring(2, userID.Length - 2);
                if (userID[0] == '!') userID = userID.Substring(1, userID.Length - 1);
                rnd = new Random((int)(Convert.ToUInt64(userID) + 1 % 10000000));
                await ReplyAsync(embed:
                    PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), rnd.Next(101), "gay"));
            }
            // id function
            else if (IsDigitsOnly(userID))
            {
                Console.WriteLine("id");
                rnd = new Random((int)(Convert.ToUInt64(userID) + 1 % 10000000));
                await ReplyAsync(embed:
                    PostEmbedPercent(Context.User.Username, $"<@{userID}>", Context.User.GetAvatarUrl(), rnd.Next(101), "gay"));
            }
            // TODO: username function
            // random string function
            else
            {
                Console.WriteLine("Nothing");
                rnd = new Random();
                await ReplyAsync(embed:
                    PostEmbedPercent(Context.User.Username, $"{param}", Context.User.GetAvatarUrl(), rnd.Next(101), "gay"));
            }
        }
    }
}
