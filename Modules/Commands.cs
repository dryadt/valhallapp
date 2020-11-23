using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace Valhallapp.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("costro")]
        public async Task Costro()
        {
            Console.WriteLine("Costro command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("Costro is a hella gay stag. <3 <3 <3 <3 <3");
        }
        [Command("astral")]
        public async Task Astral()
        {
            Console.WriteLine("Astral command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("A\nA\nA\nA\nA\nA\nA\n");
        }
        [Command("starless")]
        public async Task Starless()
        {
            Console.WriteLine("Starless command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("👗👗👗👗👗👗👗👗👗👙👙👙👙👙👙👘👘👘");
        }

        [Command("ping")]
        public async Task Ping()
        {
            Console.WriteLine("Ping command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("Pong");
        }

        [Command("pong")]
        public async Task Pong()
        {
            Console.WriteLine("Pong command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("Ping");
        }
        
        [Command("femboy")]
        public async Task Femboy()
        {
            Console.WriteLine("Femboy command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            Random rnd = new Random((int)(Context.User.Id % 10000000));
            await ReplyAsync("<@" + Context.User.Id + "> is " + rnd.Next(101) + "% femboy");
        }
        
        [Command("femboy")]
        public async Task Femboy([Remainder]string param)
        {
            Console.WriteLine("Femboy command with params executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            Random rnd;
            string text = param;
            if(param.Contains("<@!")) {
                Console.WriteLine("userping");
                text = text.Remove(text.Length - 1);
                text = text.Substring(3, text.Length-3);
                rnd = new Random((int)(Convert.ToUInt64(text) % 10000000));
                await ReplyAsync("<@" + text + "> is " + rnd.Next(101) + "% femboy ");
            }
            else if (IsDigitsOnly(text)) {
                Console.WriteLine("id");
                rnd = new Random((int)(Convert.ToUInt64(text) % 10000000));
                await ReplyAsync("<@" + text + "> is " + rnd.Next(101) + "% femboy");
            }
            else
            {
                Console.WriteLine("Nothing");
                rnd = new Random();
                await ReplyAsync(text + " is " + rnd.Next(101) + "% femboy");
            }
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
    }
}
