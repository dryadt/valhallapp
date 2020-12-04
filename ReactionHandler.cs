using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace valhallappweb
{
    public class ReactionHandler
    {
        public ReactionHandler(DiscordSocketClient _client)
        {
            this._client = _client;
        }

        DiscordSocketClient _client;
        const ulong botId = 779648566057762826;
        const ulong artChannelId = 482894390570909706;
        const ulong artTalkChannelId = 561322620931538944;
        const ulong serverId = 482631363233710106;


        /*----------------------------*/
        /*  MESSAGE REACTION HANDLER   */
        /*----------------------------*/

        public async Task HandleReactionClearAsync(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel)
        {
            if (artChannelId == channel.Id) UpdateBotMessage(message.Id);
            if (artTalkChannelId == channel.Id) AddreactionToArt(message.Id);
            await Task.Delay(0); // remove asap, it's just to remove a warning that makes me anxious
        }

        // Handle each reaction recieved
        public async Task HandleReactionAsync(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            await HandleReactionClearAsync(message, channel);
        }

        // chat -> gallery reaction transfert
        private async void AddreactionToArt(ulong messageId)
        {
            ITextChannel artChannel = (ITextChannel)_client.GetChannel(artChannelId);
            ITextChannel artTalkChannel = (ITextChannel)_client.GetChannel(artTalkChannelId);
            // verify neither of the channel aren't null
            if (artChannel is null || artTalkChannel is null) return;
            if (!(artChannel is ITextChannel) || !(artTalkChannel is ITextChannel)) return;
            // get original message
            IMessage message;
            message = await artTalkChannel.GetMessageAsync(messageId);
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
            IMessage originalMessage = await artChannel.GetMessageAsync(newMessageId);
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
            ITextChannel artChannel = (ITextChannel)_client.GetChannel(artChannelId);
            ITextChannel artTalkChannel = (ITextChannel)_client.GetChannel(artTalkChannelId);
            if (artTalkChannel is null) return;
            if (!(artTalkChannel is ITextChannel)) return;
            IMessage message;
            message = await artChannel.GetMessageAsync(messageId);
            // get emote list
            if (message == null) return;
            var emoteList = message.Reactions;
            var messageLinkUrl = $"https://discord.com/channels/{serverId}/{artChannelId}/{messageId}";
            // get 100 message around the timeperiod of the original message from the other channel
            var messageList = await artTalkChannel.GetMessagesAsync(messageId, Direction.After, 10).LastOrDefaultAsync();
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

        /*SIMPLE REUSABLE COMMANDS */

        private string GetUntilOrEmpty(string text, char charToStopAt)
        {
            string stringToReturn = "";
            foreach (char character in text)
            {
                if (character == charToStopAt) break;
                stringToReturn += character;
            }
            return stringToReturn;
        }

        private Embed PostEmbedImage(string username, ulong userId, string description, string userURL, string url, ulong messageId)
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
        public List<string> GetAllUrlFromString(string stringToAnalyse)
        {
            List<string> strList = new List<string>();
            var linkParser = new Regex(@"\b(?:https?://)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match m in linkParser.Matches(stringToAnalyse))
            {
                strList.Add(m.ToString());
            }
            return strList;
        }
    }
}
