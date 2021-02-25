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
            if (channel.Id == galleryId) await HandleGalleryEdit(socketMessage, channel, message);
        }

        private async Task HandleGalleryEdit(SocketMessage socketMessage, ISocketMessageChannel galleryChannel, Cacheable<IMessage, ulong> message)
        {
            ITextChannel galleryTalkChannel = (ITextChannel)_client.GetChannel(galleryTalkId);
            if (!(galleryTalkChannel is ITextChannel)|| !(galleryChannel is ITextChannel)) return;
            var messageList = await galleryTalkChannel.GetMessagesAsync(message.Id, Direction.After, 10).LastOrDefaultAsync();
            // get all media urls of the message
            var UrlList = GetAllUrlFromString(socketMessage.Content);
            foreach (var attachment in socketMessage.Attachments) UrlList.Add(attachment.Url);
            // Delete the message if it's null
            if (UrlList.Count==0)
            {
                await socketMessage.DeleteAsync();
                await galleryTalkChannel.SendMessageAsync(
                    $"{socketMessage.Author.Username} do not edit messages so it doesn't return any media!",
                    embed: PostEmbedText(socketMessage.Author.Username, socketMessage.Author.GetAvatarUrl(), "Deleted message content:", socketMessage.Content));
                return;
            }
            IMessage originalMessage = await galleryChannel.GetMessageAsync(socketMessage.Id);
            foreach (var item in messageList.Reverse())
            {
                // only tests message with the bot
                if (item.Author.IsBot == false) continue;
                //if no url in message return
                if (item.Content.Contains(socketMessage.Id.ToString()))
                {
                    IUserMessage userMessageToEdit = item as IUserMessage;
                    Console.WriteLine("test");
                    Console.WriteLine(userMessageToEdit.Content);
                    Console.WriteLine(socketMessage.Content);
                    string messageStringContent = Regex.Replace(socketMessage.Content, @"http[^\s]+", "");
                    Console.WriteLine(messageStringContent);
                    string messageContent = $"{messageStringContent}\nUrl link: {GetAllUrlFromString(socketMessage.Content).First()}\nDiscord link: https://discord.com/channels/{serverId}/{galleryId}/{message.Id}";
                    await userMessageToEdit.ModifyAsync(editMessage => editMessage.Content= messageContent);
                    break;
                }
                // if no embed return
                if (item.Embeds.Count == 0) continue;
                //test if the embed contains 
                if (item.Embeds.First().Description.Contains(socketMessage.Id.ToString()))
                {
                    IUserMessage userMessageToEdit = item as IUserMessage;
                    await userMessageToEdit.ModifyAsync(editMessage => editMessage.Embed = EditEmbed(originalMessage, userMessageToEdit));
                }
            }
        }

        private Embed EditEmbed(IMessage originalMessage, IUserMessage userMessageToEdit)
        {
            string cleanDescription = Regex.Replace(originalMessage.Content, @"http[^\s]+", "");
            Console.WriteLine($"Number of reaction {originalMessage.Reactions.Count}");
            foreach (var emoteItem in originalMessage.Reactions)
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
            return PostEmbedImage(
                originalMessage.Author.Username,
                originalMessage.Author.Id,
                cleanDescription,
                originalMessage.Author.GetAvatarUrl(),
                userMessageToEdit.Embeds.First().Image.Value.Url,
                originalMessage.Id);
        }
    }
}