using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using static valhallappweb.PublicFunction;

namespace Valhallapp.Modules
{
    public class RythmHeaven : ModuleBase<SocketCommandContext>
    {
        [Command("Oh! You go big guy!")]
        public async Task BigGuy()
        {
            DisplayCommandLine("BigGuy", Context);
            await ReplyAsync("Hmph Umph!");
        }
        [Command("Pose for the fans!")]
        public async Task Pose()
        {
            DisplayCommandLine("Pose", Context);
            await ReplyAsync("AAAAUGH!");
        }
        [Command("Wubbadubadub is that true?")]
        public async Task Wubbadubadub()
        {
            DisplayCommandLine("Wubbadubadub", Context);
            await ReplyAsync("Yes.");
        }
    }
}
