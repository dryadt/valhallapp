using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Valhallapp.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("Wubbadubadub is that true?")]
        public async Task Wubbadubadub()
        {
            Console.WriteLine("Wubbadubadub command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("Yes.");
        }
        [Command("Oh! You go big guy!")]
        public async Task BigGuy()
        {
            Console.WriteLine("BigGuy command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("Hmph Umph!");
        }
        [Command("Pose for the fans!")]
        public async Task Pose()
        {
            Console.WriteLine("Pose command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("AAAAUGH!");
        }
        [Command("hi")]
        public async Task Reggie()
        {
            Console.WriteLine("Reggie command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("My name is reggie");
        }
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

        [Command("embed")]
        public async Task SendRichEmbedAsync()
        {
            var embed = new EmbedBuilder
            {
                // Embed property can be set within object initializer
                Title = "Hello world!",
                Description = "I am a description set by initializer."
            };
            // Or with methods
            embed.WithAuthor(Context.User)
                .WithColor(Color.Blue)
                .WithTitle("I overwrote \"Hello world!\"")
                .WithDescription("I am a description.")
                .WithUrl("https://example.com")
                .WithCurrentTimestamp()
                .WithImageUrl("https://cdn.discordapp.com/attachments/658791767168122923/780413149114400808/Untitled1263_20201123204303.png")
                .Build();
            await ReplyAsync(embed: embed.Build());
        }
    }
}
