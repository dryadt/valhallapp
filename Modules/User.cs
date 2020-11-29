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
            DisplayCommandLine("Astral");
            await ReplyAsync("A\nA\nA\nA\nA\nA\nA\n");
        }
        [Command("costro")]
        public async Task Costro()
        {
            DisplayCommandLine("Costro");
            await ReplyAsync("Costro is a hella cute stag. <3 <3 <3 <3 <3");
        }
        [Command("starless")]
        public async Task Starless()
        {
            DisplayCommandLine("Starless");
            await ReplyAsync(":dress::dress::dress::dress::dress::dress::dress::dress::dress::bikini::bikini::bikini::bikini::bikini::bikini::kimono::kimono::kimono:");
        }

        void DisplayCommandLine(string CommandName)
        {
            Console.WriteLine($"{CommandName} command executed by user: {Context.User.Username} on channel: {Context.Channel.Name}");
        }
    }
}
