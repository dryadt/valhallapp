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
            SimpleCommands.DisplayCommandLine("BigGuy", Context);
            await ReplyAsync("Hmph Umph!");
        }
        [Command("Pose for the fans!")]
        public async Task Pose()
        {
            SimpleCommands.DisplayCommandLine("Pose", Context);
            await ReplyAsync("AAAAUGH!");
        }
        [Command("Wubbadubadub is that true?")]
        public async Task Wubbadubadub()
        {
            SimpleCommands.DisplayCommandLine("Wubbadubadub", Context);
            await ReplyAsync("Yes.");
        }
    }
}
