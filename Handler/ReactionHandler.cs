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
    public class ReactionHandler
    {
        public ReactionHandler(DiscordSocketClient _client)
        {
            this._client = _client;
        }

        private readonly DiscordSocketClient _client;

        /*----------------------------*/
        /*  MESSAGE REACTION HANDLER  */
        /*----------------------------*/

        public async Task HandleReactionClearAsync(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel)
        {
            if (galleryId == channel.Id) UpdateBotMessage(message.Id);
            if (galleryTalkId == channel.Id) AddreactionToGallery(message.Id);
            await Task.Delay(0); // remove asap, it's just to remove a warning that makes me anxious
        }

        // Handle each reaction recieved
        public async Task HandleReactionAsync(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            Console.WriteLine($"reaction: {reaction}");
            await HandleReactionClearAsync(message, channel);
        }

        // chat -> gallery reaction transfert
        private async void AddreactionToGallery(ulong messageId)
        {
            ITextChannel galleryChannel = (ITextChannel)_client.GetChannel(galleryId);
            ITextChannel galleryTalkChannel = (ITextChannel)_client.GetChannel(galleryTalkId);
            // verify neither of the channel aren't null
            if (galleryChannel is null || galleryTalkChannel is null) return;
            if (!(galleryChannel is ITextChannel) || !(galleryTalkChannel is ITextChannel)) return;
            // get original message
            IMessage message;
            message = await galleryTalkChannel.GetMessageAsync(messageId);
            // get message ID
            var reactionList = message.Reactions;
            if (message.Embeds.Count == 0) return;
            string oldDescription = message.Embeds.First().Description;
            oldDescription = GetAllUrlFromString(oldDescription).First();
            oldDescription = oldDescription.Remove(0, 29);
            oldDescription = oldDescription.Remove(0, GetUntilOrEmpty(oldDescription, '/').Length + 1);
            oldDescription = oldDescription.Remove(0, GetUntilOrEmpty(oldDescription, '/').Length + 1);
            ulong newMessageId = Convert.ToUInt64(oldDescription);
            // get original message
            IMessage originalMessage = await galleryChannel.GetMessageAsync(newMessageId);
            //  edit the message
            IUserMessage userMessageToEdit = originalMessage as IUserMessage;
            // react with the emote if it's not on the message already
            foreach (var reaction in reactionList)
            {
                // skip the sent message if it's already on the message
                if (userMessageToEdit.Reactions.ContainsKey(reaction.Key)) continue;
                // react with the emote if it's not on the message already
                Console.WriteLine($"Emote to add: {reaction.Key}");
                await userMessageToEdit.AddReactionAsync(reaction.Key);
            }
            // remove the react if it's on the message and not in the reaction list
            foreach (var reaction in userMessageToEdit.Reactions)
            {
                // skip the sent message if it's already on the message
                if (reactionList.ContainsKey(reaction.Key)) continue;
                // remove the emote if it's on the message but not on the list
                Console.WriteLine($"Emote to remove: {reaction.Key}");
                await userMessageToEdit.RemoveReactionAsync(reaction.Key, botId);
            }
        }

        // gallery -> chat reaction transfert
        private async void UpdateBotMessage(ulong messageId)
        {
            // get message by id and channel id
            ITextChannel galleryChannel = (ITextChannel)_client.GetChannel(galleryId);
            ITextChannel galleryTalkChannel = (ITextChannel)_client.GetChannel(galleryTalkId);
            if (galleryTalkChannel is null) return;
            if (!(galleryTalkChannel is ITextChannel)) return;
            IMessage message;
            message = await galleryChannel.GetMessageAsync(messageId);
            // get emote list
            if (message == null) return;
            var emoteList = message.Reactions;
            var messageLinkUrl = $"https://discord.com/channels/{serverId}/{galleryId}/{messageId}";
            // get 100 message around the timeperiod of the original message from the other channel
            var messageList = await galleryTalkChannel.GetMessagesAsync(messageId, Direction.After, 10).LastOrDefaultAsync();
            IMessage messageToEdit = null;
            foreach (var item in messageList.Reverse())
            {
                // only tests message with the bot
                if (item.Author.IsBot == false) continue;
                // if no embed return
                if (item.Embeds.Count == 0) continue;
                //test if the embed contains 
                if (item.Embeds.First().Description.Contains(messageLinkUrl))
                {
                    messageToEdit = item;
                    break;
                }
            }
            //  if no message fits returns
            if (messageToEdit == null) return;
            //  edit the message
            IUserMessage userMessageToEdit = messageToEdit as IUserMessage;
            await userMessageToEdit.ModifyAsync(messageItem => {
                messageItem.Content = "";
                messageItem.Embed = ModifyFooter(userMessageToEdit.Embeds, emoteList, message.Content, message.Id, message.Author.Id);
            });
        }

        // handler the generation of a new edited Embed for the chat
        private Embed ModifyFooter(IReadOnlyCollection<IEmbed> embeds, IReadOnlyDictionary<IEmote, ReactionMetadata> emoteList, string originalMessage, ulong originalMessageID, ulong userID)
        {
            IEmbed embedMessage = embeds.First();
            string username, userUrl, url;
            //get some values in the embed
            username = embedMessage.Author.Value.Name;
            userUrl = embedMessage.Author.Value.IconUrl;
            url = embedMessage.Image.Value.Url;
            Embed embedReturn;
            string cleanDescription = Regex.Replace(originalMessage, @"http[^\s]+", "");
            if (emoteList.Count > 0)
            {
                foreach (var emoteItem in emoteList)
                {
                    // For basic Emojis
                    if (emoteItem.Key is Emoji)
                    {
                        cleanDescription += $"\n{emoteItem.Key}x{emoteItem.Value.ReactionCount} ";
                    }
                    //for Custom Emojis.
                    else
                    {
                        Emote customeEmoji = (Emote)emoteItem.Key;
                        if (customeEmoji.Animated)
                            cleanDescription += $"\n<a:{emoteItem.Key.Name}:{customeEmoji.Id}> x {emoteItem.Value.ReactionCount}";
                        else
                            cleanDescription += $"\n<:{emoteItem.Key.Name}:{customeEmoji.Id}> x {emoteItem.Value.ReactionCount}";
                    }
                }
                embedReturn = PostEmbedImage(username, userID, cleanDescription, userUrl, url, originalMessageID);
            }
            else
                // if no emoji just generate the embed with no emojis.
                embedReturn = PostEmbedImage(username, userID, cleanDescription, userUrl, url, originalMessageID);
            return embedReturn;
        }
    }
}
