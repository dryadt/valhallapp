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
                .AddField($"{prefix}femboy", "Display the seeded femboy percentage rating of the a user. Can accept one paramater.")
                .AddField($"{prefix}furry", "Display the seeded furry percentage rating of the a user. Can accept one paramater.")
                .AddField($"{prefix}gay", "Display the seeded gay percentage rating of the a user. Can accept one paramater.")
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
            Console.WriteLine($"id user: {Context.User.Id}");
            Random rnd = new Random((int)(Convert.ToUInt64(Context.User.Id) % 10000000));
            await ReplyAsync(embed: 
                PostEmbedPercent(Context.User.Username,Context.User.Id, Context.User.GetAvatarUrl(), rnd.Next(101), "femboy"));
        }
        [Command("femboy")]
        public async Task Femboy([Remainder] string param)
        {
            DisplayCommandLine($"Femboy with params");
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
                    PostEmbedPercent(Context.User.Username, Context.User.Id, Context.User.GetAvatarUrl(), rnd.Next(101), "femboy"));
            }
            // id function
            else if (IsDigitsOnly(userID))
            {
                rnd = new Random((int)(Convert.ToUInt64(userID) % 10000000));
                await ReplyAsync($"<@{userID}> is {rnd.Next(101)}% femboy");
            }
            // TODO: username function
            // random string function
            else
            {
                rnd = new Random();
                await ReplyAsync($"{userID} is {rnd.Next(101)}% femboy");
            }
        }
        [Command("furry")]
        public async Task Furry()
        {
            DisplayCommandLine("Furry");
            await ReplyAsync(embed:
                PostEmbedPercent(Context.User.Username, Context.User.Id, Context.User.GetAvatarUrl(), 100, "furry"));
        }

        [Command("furry")]
        public async Task Furry([Remainder] string param)
        {
            DisplayCommandLine("Furry with param");
            await ReplyAsync(embed:
                PostEmbedPercent(Context.User.Username, Context.User.Id, Context.User.GetAvatarUrl(), 100, "furry"));
        }

        [Command("gay")]
        public async Task Gay()
        {
            DisplayCommandLine("Gay");
            Random rnd = new Random((int)(Convert.ToUInt64(Context.User.Id)+1 % 10000000));
            await ReplyAsync(embed:
                PostEmbedPercent(Context.User.Username, Context.User.Id, Context.User.GetAvatarUrl(), rnd.Next(101), "gay"));
        }


        [Command("gay")]
        public async Task Gay([Remainder] string param)
        {
            DisplayCommandLine("Gay with params");
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
                await ReplyAsync($"<@{userID}> is {rnd.Next(101)}% gay");
            }
            // id function
            else if (IsDigitsOnly(userID))
            {
                Console.WriteLine("id");
                rnd = new Random((int)(Convert.ToUInt64(userID) + 1 % 10000000));
                await ReplyAsync($"<@{userID}> is {rnd.Next(101)}% gay");
            }
            // TODO: username function
            // random string function
            else
            {
                Console.WriteLine("Nothing");
                rnd = new Random();
                await ReplyAsync($"{userID} is {rnd.Next(101)}% gay");
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
                .AddField($"{prefix}costro", "Gay stag.")
                .AddField($"{prefix}hi", "He likes guys")
                .AddField($"{prefix}Oh! You go big guy!\n"+ $"{prefix}Pose for the fans!\n"+ $"{prefix}Wubbadubadub is that true?", "Do i need to say anything about the GOTY?")
                .AddField($"{prefix}starless", "Mega Bi stag.")
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
        Embed PostEmbedPercent(string username, ulong userIdToPing, string userIconURL,int percent,string commandType)
        {
            // removes all urls
            var embed = new EmbedBuilder();
            embed.WithAuthor(username, userIconURL)
                .WithDescription($"<@{userIdToPing}> is {percent}% {commandType}")
                .WithColor(Color.DarkBlue)
                .Build();
            return embed.Build();
        }
    }
}
