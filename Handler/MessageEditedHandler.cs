using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static valhallappweb.PublicFunction;

namespace valhallappweb
{
    public class MessageEditedHandler
    {
        public MessageEditedHandler(DiscordSocketClient _client)
        {
            this._client = _client;
        }

        private readonly DiscordSocketClient _client;

        public async Task HandleEditAsync(Cacheable<IMessage, ulong> message, SocketMessage socketMessage, ISocketMessageChannel channel)
        {
            Console.WriteLine($"edited message in channel {channel.Id} with message id {message.Id}");
            if (channel.Id == galleryId) await HandleGalleryEdit(socketMessage,channel);
        }

        private async Task HandleGalleryEdit(SocketMessage socketMessage, ISocketMessageChannel galleryChannel)
        {
            ITextChannel galleryTalkChannel = (ITextChannel)_client.GetChannel(galleryTalkId);
            Console.WriteLine($"Content: {socketMessage.Content}");
            if (!(galleryTalkChannel is ITextChannel)|| !(galleryChannel is ITextChannel)) return;
            var messageList = await galleryTalkChannel.GetMessagesAsync(socketMessage.Id, Direction.After, 10).LastOrDefaultAsync();
            IMessage messageToEdit = null;
            foreach (var item in messageList.Reverse())
            {
                // only tests message with the bot
                if (item.Author.IsBot == false) continue;
                // if no embed return
                if (item.Embeds.Count == 0) continue;
                //test if the embed contains 
                if (item.Embeds.First().Description.Contains(socketMessage.Id.ToString()))
                {
                    messageToEdit = item;
                    break;
                }
            }
            //  if no message fits returns
            if (messageToEdit == null) return;
            //  edit the message
            IUserMessage userMessageToEdit = messageToEdit as IUserMessage;
            await userMessageToEdit.ModifyAsync(editMessage=>editMessage.Embed=EditEmbed(socketMessage, userMessageToEdit));
        }

        private Embed EditEmbed(SocketMessage socketMessage, IUserMessage userMessageToEdit)
        {
            return PostEmbedImage(socketMessage.Author.Username, socketMessage.Author.Id, socketMessage.Content,socketMessage.Author.GetAvatarUrl(), userMessageToEdit.Embeds.First().Image.Value.Url,socketMessage.Id);
        }
    }
}