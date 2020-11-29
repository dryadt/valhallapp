using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Valhallapp.Modules
{
    public class RythmHeaven : ModuleBase<SocketCommandContext>
    {
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
    }
}
