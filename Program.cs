using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.RegularExpressions;

namespace valhallappweb
{
    public class Program
    {

        /*APP INIT */
        static void Main(string[] args)
        {
            Console.WriteLine("Valhalla application start");
            Task.Run(() => new Program().RunBotAsync().GetAwaiter().GetResult());
            CreateWebHostBuilder(args).Build().Run();
        }

        /*WEB APP INIT*/
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            {
            Console.WriteLine("Valhalla webpage start");
                var port = Environment.GetEnvironmentVariable("PORT");

                return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseUrls($"http://*:{port}");
            }

        /*DISCORD BOT INIT*/
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            Console.WriteLine("Valhalla bot start");
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            // Logging
            _client.Log += Client_Log;

            // Command init
            await RegisterCommandsAsync();

            // Bot Authentification init
            var token = Environment.GetEnvironmentVariable("TOKEN");
            await _client.LoginAsync(TokenType.Bot, token);

            // Start Discord bot
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        // Logging
        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        // Command init
        public async Task RegisterCommandsAsync()
        {
            _client.ReactionAdded += HandleReactionAsync;
            _client.ReactionRemoved += HandleReactionAsync;
            _client.ReactionsCleared += HandleReactionClearAsync;
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleReactionClearAsync(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel channel)
        {
            await Task.Delay(0); // remove asap, it's just to remove a warning that makes me anxious
            Console.WriteLine($"Message ID of reaction: {arg1.Id}");
            Console.WriteLine($"Channel of reaction: {channel.Name}");
        }

        // Handle each reaction recieved
        private async Task HandleReactionAsync(Cacheable<IUserMessage, ulong> arg, ISocketMessageChannel channel, SocketReaction reaction)
        {
            await Task.Delay(0); // remove asap, it's just to remove a warning that makes me anxious
            Console.WriteLine($"Message ID of reaction: {arg.Id}");
            Console.WriteLine($"Channel of reaction: {channel.Name}");
            Console.WriteLine($"Emote of reaction: {reaction.Emote.Name}");
            return;
        }

        // Handle each message recieved into the right command (if it exists)
        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);

            // if the message is from bot then ignore
            if (message.Author.IsBot) return;
            // does something with message
            CheckImageArtChannel(message);
            // command prompt
            int argPos = 0;
            if (message.HasStringPrefix("&", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
        }

        /*SIMPLE REUSABLE COMMANDS */
        public void CheckImageArtChannel(SocketUserMessage message)
        {
            const ulong artChannelId = 482894390570909706;
            // if the message isn't in the art channel, return
            if (message.Channel.Id != artChannelId) return;
            const ulong artTalkChannelId = 561322620931538944;
            string[] extensionList = { ".mp4", ".mp3", ".png", ".jpeg", ".gif" };
            List<string> urlList = GetAllUrlFromString(message.Content);
            Console.WriteLine($"{message.Attachments.Count} attachment and {urlList.Count} URLs");
            // if the message has no attachments and no url
            if ((message.Attachments.Count == 0 && urlList.Count == 0)) return;
            // post every attachment as an embed
            foreach (var attachment in message.Attachments) 
                PostEmbedImage(message.Author.Username, message.Author.Id,message.Content, message.Author.GetAvatarUrl(), artTalkChannelId, attachment.Url,message.Id);
            // post every attachment as an embed
            foreach (var url in urlList) {
                bool isEmbedable = false;
                foreach (var extensionItem in extensionList)
                    if (isEmbedable = url.EndsWith(extensionItem)) break;
                if (isEmbedable) 
                    PostEmbedImage(message.Author.Username, message.Author.Id, message.Content, message.Author.GetAvatarUrl(), artTalkChannelId, url, message.Id);
                else MessageChannel($"{message.Author.Username} posted: {url}", artTalkChannelId);
            };
        }
        public List<string> GetAllUrlFromString(string stringToAnalyse)
        {
            List<string> strList = new List<string>();
            var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match m in linkParser.Matches(stringToAnalyse)) {
                strList.Add(m.ToString());
            }
            return strList;
        }

        public void PostEmbedImage(string username, ulong userId,string description, string userURL, ulong channelID, string url, ulong messageID)
        {
            // removes all urls
            string cleanDescription = Regex.Replace(description, @"http[^\s]+", "");
            const ulong serverId = 482631363233710106;
            const ulong artChannelId = 482894390570909706;
            Console.WriteLine($"url to post {url}");
            var embed = new EmbedBuilder();
            embed.WithAuthor(username, userURL, url)
                .WithDescription($"[<@{userId}> posted:](https://discord.com/channels/{serverId}/{artChannelId}/{messageID})\n{cleanDescription}")
                .WithColor(Color.Purple)
                .WithImageUrl(url)
                .WithCurrentTimestamp()
                .Build();
            var chnl = _client.GetChannel(channelID) as IMessageChannel;
            chnl.SendMessageAsync(embed: embed.Build());
        }
        public void MessageChannel(string messageContent, ulong channelId)
        {
            Console.WriteLine($"url of image: {messageContent}");
            var chnl = _client.GetChannel(channelId) as IMessageChannel;
            chnl.SendMessageAsync(messageContent);
        }
    }
}