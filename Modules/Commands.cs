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
            DisplayCommandLine("Help");
            var embed = new EmbedBuilder();
            string prefix = "&";
            embed.WithTitle("Commands list:")
                .WithAuthor(Context.Client.CurrentUser)
                .AddField($"{prefix}femboy", "Display the seeded percentage rating of the a user. Can accept one paramater.")
                .AddField($"{prefix}furry", "Display the seeded percentage rating of the a user. Can accept one paramater.")
                .AddField($"{prefix}gay", "Display the seeded percentage rating of the a user. Can accept one paramater.")
                .AddField($"{prefix}heroku", "Display the app hosting site of the [application](https://dashboard.heroku.com/apps/valhallapp)!")
                .AddField($"{prefix}github", "Display the github repo of the [application](https://github.com/dryadt/valhallapp)!")
                .AddField($"{prefix}help", "Displays help related to the bot!")
                .AddField($"{prefix}ping", "Replies with the ping of the bot")
                .AddField($"{prefix}website", "Display the website of the [application](https://valhallapp.herokuapp.com/)!")
                .WithFooter(footer => footer.Text = "Page 1 out of 1.")
                .WithColor(Color.Blue)
                .WithCurrentTimestamp()
                .Build();
            await ReplyAsync(embed: embed.Build());
        }
        [Command("femboy")]
        public async Task Femboy()
        {
            DisplayCommandLine("Femboy");
            Random rnd = new Random((int)(Convert.ToUInt64(Context.User.Id) % 20000000));
            await ReplyAsync($"<@{Context.User.Id}> is {rnd.Next(101)}% femboy");
        }
        [Command("femboy")]
        public async Task Femboy([Remainder] string param)
        {
            DisplayCommandLine("Femboy with params");
            Random rnd;
            string text = param;
            if (param.Contains("<@!"))
            {
                Console.WriteLine("userping");
                text = text.Remove(text.Length - 1);
                text = text.Substring(3, text.Length - 3);
                rnd = new Random((int)(Convert.ToUInt64(text) % 10000000));
                await ReplyAsync($"<@{text}> is {rnd.Next(101)}% femboy");
            }
            else if (IsDigitsOnly(text))
            {
                Console.WriteLine("id");
                rnd = new Random((int)(Convert.ToUInt64(text) % 10000000));
                await ReplyAsync($"<@{text}> is {rnd.Next(101)}% femboy");
            }
            else
            {
                Console.WriteLine("Nothing");
                rnd = new Random();
                await ReplyAsync($"{text} is {rnd.Next(101)}% femboy");
            }
        }
        [Command("furry")]
        public async Task Furry()
        {
            DisplayCommandLine("Furry");
            await ReplyAsync($"<@{Context.User.Id}> is 100% furry");
        }

        [Command("furry")]
        public async Task Furry([Remainder] string param)
        {
            DisplayCommandLine("Furry with param");
            await ReplyAsync($"{param} is 100% furry");
        }

        [Command("gay")]
        public async Task Gay()
        {
            DisplayCommandLine("Gay");
            Random rnd = new Random((int)(Convert.ToUInt64(Context.User.Id) % 20000000));
            await ReplyAsync($"<@{Context.User.Id}> is {rnd.Next(101)}% gay");
        }


        [Command("gay")]
        public async Task Gay([Remainder] string param)
        {
            DisplayCommandLine("Gay with params");
            Random rnd;
            string text = param;
            if (param.Contains("<@!"))
            {
                Console.WriteLine("userping");
                text = text.Remove(text.Length - 1);
                text = text.Substring(3, text.Length - 3);
                rnd = new Random((int)(Convert.ToUInt64(text) % 20000000));
                await ReplyAsync($"<@{text}> is {rnd.Next(101)}% gay");
            }
            else if (IsDigitsOnly(text))
            {
                Console.WriteLine("id");
                rnd = new Random((int)(Convert.ToUInt64(text) % 20000000));
                await ReplyAsync($"<@{text}> is {rnd.Next(101)}% gay");
            }
            else
            {
                Console.WriteLine("Nothing");
                rnd = new Random();
                await ReplyAsync($"{text} is {rnd.Next(101)}% gay");
            }
        }
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

        [Command("help meme")]
        public async Task HelpMeme()
        {
            DisplayCommandLine("Help meme");
            var embed = new EmbedBuilder();
            string prefix = "&";
            embed.WithTitle("Meme commands list:")
                .WithAuthor(Context.Client.CurrentUser)
                .AddField($"{prefix}astral", "A.")
                .AddField($"{prefix}costro", "Aeeeeeee")
                .AddField($"{prefix}hi", "Aeeeeeee")
                .AddField($"{prefix}Oh! You go big guy!", "Aeeeeeee")
                .AddField($"{prefix}Pose for the fans!", "Aeeeeeee")
                .AddField($"{prefix}starless", "Aeeeeeee")
                .AddField($"{prefix}Wubbadubadub is that true?", "Aeeeeeee")
                .WithFooter(footer => footer.Text = "Page 1 out of 1.")
                .WithColor(Color.Blue)
                .WithCurrentTimestamp()
                .Build();
            await ReplyAsync(embed: embed.Build());
        }
        [Command("astral")]
        public async Task Astral()
        {
            DisplayCommandLine("Astral");
            await ReplyAsync("A\nA\nA\nA\nA\nA\nA\n");
        }
        [Command("costro")]
        public async Task Costro()
        {
            DisplayCommandLine("Costro");
            await ReplyAsync("Costro is a hella cute stag. <3 <3 <3 <3 <3");
        }
        [Command("hi")]
        public async Task Reggie()
        {
            DisplayCommandLine("Reggie");
            await ReplyAsync("My name is reggie.");
        }
        [Command("Oh! You go big guy!")]
        public async Task BigGuy()
        {
            DisplayCommandLine("BigGuy");
            await ReplyAsync("Hmph Umph!");
        }
        [Command("Pose for the fans!")]
        public async Task Pose()
        {
            DisplayCommandLine("Pose");
            await ReplyAsync("AAAAUGH!");
        }
        [Command("starless")]
        public async Task Starless()
        {
            DisplayCommandLine("Starless");
            await ReplyAsync(":dress::dress::dress::dress::dress::dress::dress::dress::dress::bikini::bikini::bikini::bikini::bikini::bikini::kimono::kimono::kimono:");
        }
        [Command("Wubbadubadub is that true?")]
        public async Task Wubbadubadub()
        {
            DisplayCommandLine("Wubbadubadub");
            await ReplyAsync("Yes.");
        }
        void DisplayCommandLine(string CommandName)
        {
            Console.WriteLine($"{CommandName} command executed by user: {Context.User.Username} on channel: {Context.Channel.Name}");
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
