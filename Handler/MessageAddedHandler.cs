using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static valhallappweb.PublicFunction;

namespace valhallappweb
{
    public class MessageAddedHandler
    {
        public MessageAddedHandler(DiscordSocketClient _client, CommandService _commands, IServiceProvider _services)
        {
            this._client = _client;
            this._commands = _commands;
            this._services = _services;
        }

        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;

        /*----------------------------*/
        /*  MESSAGE CONTENT HANDLER   */
        /*----------------------------*/

        // Handle each message recieved into the right command (if it exists)
        public async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);

            // if the message is from bot then ignore
            if (message.Author.IsBot) return;
            // if the message is in the art channel 
            if (message.Channel.Id == galleryId) await CheckImageArtChannelAsync(message);
            // command prompt
            int argPos = 0;
            if (message.HasStringPrefix(prefix, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
        }

        private async Task CheckImageArtChannelAsync(SocketUserMessage message)
        {
            //init channels
            ITextChannel galleryChannel = (ITextChannel)_client.GetChannel(galleryId);
            ITextChannel galleryTalkChannel = (ITextChannel)_client.GetChannel(galleryTalkId);

            // Delete if message is empty
            if ((message.Attachments.Count == 0) && (GetAllUrlFromString(message.Content).Count == 0))
            {
                Embed embedMessage = PostEmbedText(message.Author.Username, message.Author.GetAvatarUrl(), "Deleted message content:", message.Content);
                await galleryTalkChannel.SendMessageAsync(
                $"{message.Author.Username} No posting in the gallery <#{message.Channel.Id}>"
                , embed: embedMessage
                );
                await galleryChannel.DeleteMessageAsync(message);
                return;
            }

            // Post if message has image
            string[] extensionList = { ".png", ".jpeg", ".gif", ".jpg" };
            List<string> urlList = GetAllUrlFromString(message.Content);
            Console.WriteLine($"{message.Attachments.Count} attachment and {urlList.Count} URLs");
            // if the message has no attachments and no url
            if ((message.Attachments.Count == 0 && urlList.Count == 0)) return;
            // post every attachment as an embed
            foreach (var attachment in message.Attachments)
            {
                if (attachment.IsSpoiler())
                {
                    string messageContent = $"{message.Author.Username} posted: {Regex.Replace(message.Content, @"http[^\s]+", "")}\nUrl link: ||{attachment.Url} ||\nDiscord link: https://discord.com/channels/{serverId}/{galleryId}/{message.Id}";
                    await MessageChannel(_client, messageContent, galleryTalkId);
                }
                else
                {
                    bool isEmbedable = false;
                    foreach (var extensionItem in extensionList)
                        if (isEmbedable = attachment.Url.EndsWith(extensionItem)) break;
                    // if the attachment is an image
                    if (isEmbedable)
                        await galleryTalkChannel.SendMessageAsync(embed:
                        PostEmbedImage(message.Author.Username, message.Author.Id, Regex.Replace(message.Content, @"http[^\s]+", ""), message.Author.GetAvatarUrl(), attachment.Url, message.Id));
                    // if it's something else (like a video)
                    else
                    {
                        string messageContent = $"{message.Author.Username} posted: {Regex.Replace(message.Content, @"http[^\s]+", "")}\nUrl link:{attachment.Url}\nDiscord link: https://discord.com/channels/{serverId}/{galleryId}/{message.Id}";
                        await MessageChannel(_client, messageContent, galleryTalkId);
                    }
                }
            }
            // post every attachment as an embed
            foreach (var url in urlList)
            {
                if (message.Content.Contains("||"))
                {
                    string messageContent = $"{message.Author.Username} posted: {Regex.Replace(message.Content, @"http[^\s]+", "")}\nUrl link: ||{url} ||\nDiscord link: https://discord.com/channels/{serverId}/{galleryId}/{message.Id}";
                    await MessageChannel(_client, messageContent, galleryTalkId);
                }
                else
                {

                    bool isEmbedable = false;
                    foreach (var extensionItem in extensionList)
                        if (isEmbedable = url.EndsWith(extensionItem)) break;
                    // if the attachment is an image
                    if (isEmbedable)
                        await galleryTalkChannel.SendMessageAsync(embed:
                            PostEmbedImage(message.Author.Username, message.Author.Id, Regex.Replace(message.Content, @"http[^\s]+", ""), message.Author.GetAvatarUrl(), url, message.Id));
                    // if it's something else (like a video)
                    else
                    {
                        string messageContent = $"{message.Author.Username} posted: {Regex.Replace(message.Content, @"http[^\s]+", "")}\nUrl link:{url}\nDiscord link: https://discord.com/channels/{serverId}/{galleryId}/{message.Id}";
                        await MessageChannel(_client, messageContent, galleryTalkId);
                    }
                }
            };
        }
    }
}
