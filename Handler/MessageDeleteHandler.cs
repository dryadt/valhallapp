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

        public async Task HandleDeleteAsync(Cacheable<IMessage, ulong> message, ISocketMessageChannel channel)
        {
            if (channel.Id == galleryId) await HandleArtMessageDeletion(message);
        }

        private async Task HandleArtMessageDeletion(Cacheable<IMessage, ulong> messageId)
        {
            ITextChannel galleryTalkChannel = (ITextChannel)_client.GetChannel(galleryTalkId);
            if (!(galleryTalkChannel is ITextChannel)) return;
            var messageList = await galleryTalkChannel.GetMessagesAsync(messageId.Id, Direction.After, 10).LastOrDefaultAsync();
            foreach (var item in messageList.Reverse())
            {
                // only tests message with the bot
                if (item.Author.IsBot == false) continue;
                // if no embed return
                if (item.Embeds.Count == 0) continue;
                //test if the embed contains 
                if (item.Embeds.First().Description.Contains(messageId.Id.ToString()))
                {
                    IUserMessage userMessageToDelete = item as IUserMessage;
                    await userMessageToDelete.DeleteAsync();
                    break;
                }
            }
        }
    }
}
