using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Valhallapp.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {

        [Command("help")]
        public async Task HelpCommand()
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
        void DisplayCommandLine(string CommandName)
        {
            Console.WriteLine($"{CommandName} command executed by user: {Context.User.Username} on channel: {Context.Channel.Name}");
        }
    }
}
