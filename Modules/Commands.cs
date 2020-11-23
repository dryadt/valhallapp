using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Valhallapp.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task Help()
        {
            var embed = new EmbedBuilder();
            string prefix = "&";
            embed.WithTitle("Commands list:")
                .WithAuthor(Context.Client.CurrentUser)
                .AddField(prefix + "femboy", "Display the seeded percentage rating of the a user. Can accept one paramater.")
                .AddField(prefix + "help", "Displays help related to the bot!")
                .AddField(prefix + "github", "Display the github repo of the [application](https://github.com/dryadt/valhallapp)!")
                .AddField(prefix + "heroku", "Display the app hosting site of the [application](https://dashboard.heroku.com/apps/valhallapp)!")
                .AddField(prefix + "ping", "")
                .AddField(prefix + "website", "Display the website of the [application](https://valhallapp.herokuapp.com/)!")
                .WithFooter(footer => footer.Text = "Page 1 out of 1.")
                .WithColor(Color.Blue)
                .WithCurrentTimestamp()
                .Build();
            await ReplyAsync(embed: embed.Build());
        }
        [Command("website")]
        public async Task Website()
        {
            Console.WriteLine("Website command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("https://valhallapp.herokuapp.com/");
        }
        [Command("github")]
        public async Task Github()
        {
            Console.WriteLine("Github command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("https://github.com/dryadt/valhallapp");
        }
        [Command("heroku")]
        public async Task Heroku()
        {
            Console.WriteLine("Heroku command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("https://dashboard.heroku.com/apps/valhallapp");
        }
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
            await ReplyAsync("My name is reggie.");
        }
        [Command("costro")]
        public async Task Costro()
        {
            Console.WriteLine("Costro command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name);
            await ReplyAsync("Costro is a hella cute stag. <3 <3 <3 <3 <3");
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
            await ReplyAsync(":dress::dress::dress::dress::dress::dress::dress::dress::dress::bikini::bikini::bikini::bikini::bikini::bikini::kimono::kimono::kimono:");
        }

        [Command("ping")]
        public async Task Ping()
        {
            Console.WriteLine("Ping command executed by user: " + Context.User.Username + " on channel: " + Context.Channel.Name + " " + DateTime.Now + " " + Context.Message.CreatedAt.Date);
            TimeSpan ping = DateTime.Now - Context.Message.CreatedAt.Date;
            await ReplyAsync("Pong : " + ping + "ms");
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
