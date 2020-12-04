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
    public class MessageHandler
    {
        public MessageHandler(DiscordSocketClient _client, CommandService _commands, IServiceProvider _services)
        {
            this._client = _client;
        }

        DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

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
        private void CheckImageArtChannel(SocketUserMessage message)
        {
            // if the message isn't in the art channel, return
            if (message.Channel.Id != artChannelId) return;
            string[] extensionList = { ".mp4", ".mp3", ".png", ".jpeg", ".gif", ".jpg" };
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
        private void MessageChannel(string messageContent, ulong channelId)
        {
            Console.WriteLine($"url of image: {messageContent}");
            var chnl = _client.GetChannel(channelId) as IMessageChannel;
            chnl.SendMessageAsync(messageContent);
        }
    }
}
