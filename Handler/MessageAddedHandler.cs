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
            //handle messages that are in the gallery channel
            if (message.Channel.Id == galleryId) HandleGalleryMessage(message);
            // command prompt
            int argPos = 0;
            if (message.HasStringPrefix(prefix, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
        }

        private void HandleGalleryMessage(SocketUserMessage message)
        {
            ITextChannel galleryChannel = (ITextChannel)_client.GetChannel(galleryId);
            ITextChannel galleryTalkChannel = (ITextChannel)_client.GetChannel(galleryTalkId);
            // if the messsage doesn't contain any media
            if ((message.Attachments.Count > 0) || (GetAllUrlFromString(message.Content).Count > 0)) return;
            Embed embedMessage = PostEmbedText(message.Author.Username, message.Author.GetAvatarUrl(), "Deleted message content:", message.Content);
            galleryTalkChannel.SendMessageAsync(
            $"{message.Author.Username} No posting in the gallery <#{message.Channel.Id}>"
            ,embed: embedMessage
            );
            galleryChannel.DeleteMessageAsync(message);
        }

        private async Task CheckImageArtChannelAsync(SocketUserMessage message)
        {
            ITextChannel galleryTalkChannel = (ITextChannel)_client.GetChannel(galleryTalkId);
            string[] extensionList = { ".png", ".jpeg", ".gif", ".jpg" };
            List<string> urlList = GetAllUrlFromString(message.Content);
            Console.WriteLine($"{message.Attachments.Count} attachment and {urlList.Count} URLs");
            // if the message has no attachments and no url
            if ((message.Attachments.Count == 0 && urlList.Count == 0)) return;
            // post every attachment as an embed
            foreach (var attachment in message.Attachments)
                await galleryTalkChannel.SendMessageAsync(embed:
                    PostEmbedImage(message.Author.Username, message.Author.Id, Regex.Replace(message.Content, @"http[^\s]+", ""), message.Author.GetAvatarUrl(), attachment.Url, message.Id));
            // post every attachment as an embed
            foreach (var url in urlList)
            {
                bool isEmbedable = false;
                foreach (var extensionItem in extensionList)
                    if (isEmbedable = url.EndsWith(extensionItem)) break;
                if (isEmbedable)
                    await galleryTalkChannel.SendMessageAsync(embed:
                        PostEmbedImage(message.Author.Username, message.Author.Id, Regex.Replace(message.Content, @"http[^\s]+", ""), message.Author.GetAvatarUrl(), url, message.Id));
                else await MessageChannel(_client, $"{message.Author.Username} posted: {url}", galleryTalkId);
            };
        }
    }
}
