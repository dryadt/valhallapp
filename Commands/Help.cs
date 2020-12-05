using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using static valhallappweb.PublicFunction;

namespace Valhallapp.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {

        [Command("help")]
        public async Task HelpCommand()
        {
            DisplayCommandLine("help", Context.Message.Author.Username, Context.Channel.Name);
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

        [Command("help meme")]
        public async Task HelpMeme()
        {
            DisplayCommandLine("Help meme", Context.Message.Author.Username, Context.Channel.Name);
            var embed = new EmbedBuilder();
            string prefix = "&";
            embed.WithTitle("Meme commands list:")
                .WithAuthor(Context.Client.CurrentUser)
                .AddField($"{prefix}astral", "A.")
                .AddField($"{prefix}costro", "Gay stag.")
                .AddField($"{prefix}hi", "He likes guys")
                .AddField($"{prefix}Oh! You go big guy!\n" + $"{prefix}Pose for the fans!\n" + $"{prefix}Wubbadubadub is that true?", "Do i need to say anything about the GOTY?")
                .AddField($"{prefix}starless", "Mega Bi stag.")
                .WithFooter(footer => footer.Text = "Page 1 out of 1.")
                .WithColor(Color.Blue)
                .WithCurrentTimestamp()
                .Build();
            await ReplyAsync(embed: embed.Build());
        }
    }
}
