﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using static valhallappweb.PublicFunction;

namespace Valhallapp.Modules
{
    public class RhythmHeaven : ModuleBase<SocketCommandContext>
    {
        [Command("fine")]
        public async Task Fine()
        {
            DisplayCommandLine("fine", Context.Message.Author.Username, Context.Channel.Name);
            await ReplyAsync("I guess you're my little PogChamp.\nCome here~");
        }
        [Command("Oh! You go big guy!")]
        public async Task BigGuy()
        {
            DisplayCommandLine("BigGuy", Context.Message.Author.Username, Context.Channel.Name);
            await ReplyAsync("Hmph Umph!");
        }
        [Command("Pose for the fans!")]
        public async Task Pose()
        {
            DisplayCommandLine("Pose", Context.Message.Author.Username, Context.Channel.Name);
            await ReplyAsync("AAAAUGH!");
        }
        [Command("Wubbadubadub is that true?")]
        public async Task Wubbadubadub()
        {
            DisplayCommandLine("Wubbadubadub", Context.Message.Author.Username, Context.Channel.Name);
            await ReplyAsync("Yes.");
        }
        [Command("dikidikidikidesu ka?")]
        public async Task Dikidikidesu()
        {
            DisplayCommandLine("dikidikidikidesu ka?", Context.Message.Author.Username, Context.Channel.Name);
            await ReplyAsync("え。");
        }
    }
}
