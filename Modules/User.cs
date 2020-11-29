using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Valhallapp.Modules
{
    public class User : ModuleBase<SocketCommandContext>
    {
        [Command("astral")]
        public async Task Astral()
        {
            SimpleCommands.DisplayCommandLine("Astral",Context); 
            await ReplyAsync("A\nA\nA\nA\nA\nA\nA\n");
        }
        [Command("costro")]
        public async Task Costro()
        {
            SimpleCommands.DisplayCommandLine("Costro", Context);
            await ReplyAsync("Costro is a hella cute stag. <3 <3 <3 <3 <3");
        }
        [Command("starless")]
        public async Task Starless()
        {
            SimpleCommands.DisplayCommandLine("Starless",Context);
            await ReplyAsync(":dress::dress::dress::dress::dress::dress::dress::dress::dress::bikini::bikini::bikini::bikini::bikini::bikini::kimono::kimono::kimono:");
        }
    }
}
