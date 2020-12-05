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
    public class MessageDeleteHandler
    {

        private readonly DiscordSocketClient _client;
        public MessageDeleteHandler(DiscordSocketClient _client)
        {
            this._client = _client;
        }

        internal async Task HandleDeleteAsync(Cacheable<IMessage, ulong> message, ISocketMessageChannel channel)
        {
            if (channel.Id == galleryId) await HandleArtMessageDeletion(message);
        }

        private async Task HandleArtMessageDeletion(Cacheable<IMessage, ulong> messageId)
        {
            ITextChannel galleryChannel = (ITextChannel)_client.GetChannel(galleryId);
            ITextChannel galleryTalkChannel = (ITextChannel)_client.GetChannel(galleryTalkId);
            if (!(galleryTalkChannel is ITextChannel)) return;
            IMessage message = await galleryChannel.GetMessageAsync(messageId.Id);
            ulong messageLinkUrl = getMessageIDFromEmbed(message.Embeds.First());
            var messageList = await galleryTalkChannel.GetMessagesAsync(message.Id, Direction.After, 10).LastOrDefaultAsync();
            IMessage messageToEdit = null;
            foreach (var item in messageList.Reverse())
            {
                // only tests message with the bot
                if (item.Author.IsBot == false) continue;
                // if no embed return
                if (item.Embeds.Count == 0) continue;
                //test if the embed contains 
                if (item.Embeds.First().Description.Contains(messageLinkUrl.ToString()))
                {
                    messageToEdit = item;
                    break;
                }
            }
            //  if no message fits returns
            if (messageToEdit == null) return;
            //  edit the message
            IUserMessage userMessageToEdit = messageToEdit as IUserMessage;
            await userMessageToEdit.DeleteAsync();
        }
    }
}
