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

        const ulong artChannelId = 482894390570909706;
        const ulong artTalkChannelId = 561322620931538944;
        const ulong serverId = 482631363233710106;

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

        /*----------------------------*/
        /*  MESSAGE REACTION HANDLER   */
        /*----------------------------*/

        private async Task HandleReactionClearAsync(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel)
        {
            if (artChannelId == channel.Id) UpdateBotMessage(message.Id);
            await Task.Delay(0); // remove asap, it's just to remove a warning that makes me anxious
        }

        // Handle each reaction recieved
        private async Task HandleReactionAsync(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (artChannelId == channel.Id) UpdateBotMessage(message.Id);
            await Task.Delay(0); // remove asap, it's just to remove a warning that makes me anxious
        }

        public async void UpdateBotMessage(ulong messageId)
        {
            // get message by id and channel id
            ITextChannel artChannel = (ITextChannel)_client.GetChannel(artChannelId);
            ITextChannel artTalkChannel = (ITextChannel)_client.GetChannel(artTalkChannelId);
            if (artTalkChannel is null) return;
            if (!(artTalkChannel is ITextChannel)) return;
            IMessage message;
            message = await artChannel.GetMessageAsync(messageId);
            // get emote list
            IReadOnlyDictionary<IEmote, ReactionMetadata> emoteList;
            if (message == null) return;
            emoteList = message.Reactions;
            var messageLinkUrl = $"https://discord.com/channels/{serverId}/{artChannelId}/{messageId}";
            // get 100 message around the timeperiod of the original message from the other channel
            var messageList = await artTalkChannel.GetMessagesAsync(messageId, Direction.After, 10).LastOrDefaultAsync();
            IMessage messageToEdit = null;
            Console.WriteLine(messageList.Count);
            foreach (var item in messageList.Reverse())
            {
                Console.WriteLine("Message content for the loop : "+item.Content);
                // only tests message with the bot
                if (item.Author.IsBot == false) continue;
                // if no embed return
                if (item.Embeds.Count == 0) continue;
                Console.WriteLine("Embed content for the loop : " + item.Embeds.First().Description);
                //test if the embed contains 
                if (item.Embeds.First().Description.Contains(messageLinkUrl))
                {
                    messageToEdit = item;
                    break;
                }
            }
            //  if no message fits returns
            if (messageToEdit == null) return;
            Console.WriteLine("edited message: "+messageId);
            //  edit the message
            IUserMessage userMessageToEdit = messageToEdit as IUserMessage;
            await userMessageToEdit.ModifyAsync(messageItem => {
                messageItem.Content = "";
                messageItem.Embed = ModifyFooter(userMessageToEdit.Embeds, emoteList, message.Content,message.Id,message.Author.Id);
            });
        }
        private Embed ModifyFooter(IReadOnlyCollection<IEmbed> embeds, IReadOnlyDictionary<IEmote, ReactionMetadata> emoteList, string originalMessage, ulong originalMessageID, ulong userID)
        {
            IEmbed embedMessage = embeds.First();
            ulong userId;
            string username,userUrl,url;
            //get some values in the embed
            username = embedMessage.Author.Value.Name;
            userUrl = embedMessage.Author.Value.IconUrl;
            url = embedMessage.Image.Value.Url;
            Embed embedReturn;
            string cleanDescription = Regex.Replace(originalMessage, @"http[^\s]+", "");
            if (emoteList.Count > 0)
            {
                foreach (var emoteItem in emoteList)
                    // For basic Emojis
                    if (emoteItem.Key is Emoji)
                        cleanDescription += $"\n{emoteItem.Key}x{emoteItem.Value.ReactionCount} ";
                    //for Custom Emojis.
                    else
                    {
                        Emote customeEmoji = (Emote)emoteItem.Key;
                        if (customeEmoji.Animated)
                            cleanDescription += $"\n<a:{emoteItem.Key.Name}:{customeEmoji.Id}> x {emoteItem.Value.ReactionCount}";
                        else
                            cleanDescription += $"\n<:{emoteItem.Key.Name}:{customeEmoji.Id}> x {emoteItem.Value.ReactionCount}";
                    }
                embedReturn = PostEmbedImage(username, userID, cleanDescription, userUrl, url, originalMessageID);
            }
            else
                embedReturn = (Embed)embedMessage;
            return embedReturn;
        }

        /*----------------------------*/
        /*  MESSAGE CONTENT HANDLER   */
        /*----------------------------*/

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
        public void CheckImageArtChannel(SocketUserMessage message)
        {
            // if the message isn't in the art channel, return
            if (message.Channel.Id != artChannelId) return;
            string[] extensionList = { ".mp4", ".mp3", ".png", ".jpeg", ".gif" };
            List<string> urlList = GetAllUrlFromString(message.Content);
            Console.WriteLine($"{message.Attachments.Count} attachment and {urlList.Count} URLs");
            // if the message has no attachments and no url
            if ((message.Attachments.Count == 0 && urlList.Count == 0)) return;
            // post every attachment as an embed
            var chnl = _client.GetChannel(artTalkChannelId) as IMessageChannel;
            foreach (var attachment in message.Attachments)
                chnl.SendMessageAsync(embed:
                    PostEmbedImage(message.Author.Username, message.Author.Id, Regex.Replace(message.Content, @"http[^\s]+", ""), message.Author.GetAvatarUrl(), attachment.Url, message.Id));
            // post every attachment as an embed
            foreach (var url in urlList)
            {
                bool isEmbedable = false;
                foreach (var extensionItem in extensionList)
                    if (isEmbedable = url.EndsWith(extensionItem)) break;
                if (isEmbedable)
                    chnl.SendMessageAsync(embed:
                        PostEmbedImage(message.Author.Username, message.Author.Id, Regex.Replace(message.Content, @"http[^\s]+", ""), message.Author.GetAvatarUrl(), url, message.Id));
                else MessageChannel($"{message.Author.Username} posted: {url}", artTalkChannelId);
            };
        }

        /*SIMPLE REUSABLE COMMANDS */

        public string GetUntilOrEmpty(string text,char charToStopAt)
        {
            string stringToReturn="";
            foreach (char character in text)
            {
                if (character == charToStopAt) break;
                stringToReturn += character;
            }
            return stringToReturn;
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

        public Embed PostEmbedImage(string username, ulong userId,string description, string userURL, string url, ulong messageId)
        {
            // removes all urls
            Console.WriteLine($"url to post {url}");
            var embed = new EmbedBuilder();
            embed.WithAuthor(username, userURL, $"{url}")
                .WithDescription($"[<@{userId}> posted:](https://discord.com/channels/{serverId}/{artChannelId}/{messageId})\n{description}")
                .WithColor(Color.Purple)
                .WithImageUrl(url)
                .Build();
            return embed.Build(); 
        }
        public void MessageChannel(string messageContent, ulong channelId)
        {
            Console.WriteLine($"url of image: {messageContent}");
            var chnl = _client.GetChannel(channelId) as IMessageChannel;
            chnl.SendMessageAsync(messageContent);
        }
    }
}