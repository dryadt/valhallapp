using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace valhallappweb
{
    public static class PublicFunction
    {
        public static readonly ulong botId = 779648566057762826;
        public static readonly ulong galleryId = 482894390570909706;
        public static readonly ulong galleryTalkId = 561322620931538944;
        public static readonly ulong serverId = 482631363233710106;

        /* GENERAL COMMANDS */
        public static void MessageChannel(DiscordSocketClient client, string messageContent, ulong channelId)
        {
            var channel = client.GetChannel(channelId) as IMessageChannel;
            channel.SendMessageAsync(messageContent);
        }

        public static ulong getMessageIDFromEmbed(IEmbed embed)
        {
            string messageID = GetAllUrlFromString(embed.Description).First();
            messageID = messageID.Remove(0, 29);
            messageID = messageID.Remove(0, GetUntilOrEmpty(messageID, '/').Length + 1);
            messageID = messageID.Remove(0, GetUntilOrEmpty(messageID, '/').Length + 1);
            return Convert.ToUInt64(messageID);

        }


        /*COMMANDS FOR COMMANDS HANDLERS*/

        public static void DisplayCommandLine(string commandName, string username, string channel)
        {
            Console.WriteLine($"{commandName} command executed by user: {username} on channel: {channel}");
        }
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
                if (c < '0' || c > '9')
                    return false;
            return true;
        }
        public static Embed PostEmbedPercent(string username, string selectedUser, string userIconURL, int percent, string commandType)
        {
            // removes all urls
            var embed = new EmbedBuilder();
            embed.WithAuthor(username, userIconURL)
                .WithDescription($"{selectedUser} is {percent}% {commandType}")
                .WithColor(Color.DarkBlue)
                .Build();
            return embed.Build();
        }

        /*COMMANDS FOR HANDLERS*/

        public static string GetUntilOrEmpty(string text, char charToStopAt)
        {
            string stringToReturn = "";
            foreach (char character in text)
            {
                if (character == charToStopAt) break;
                stringToReturn += character;
            }
            return stringToReturn;
        }

        public static Embed PostEmbedText(string username, string userURL, string title, string description)
        {
            var embed = new EmbedBuilder();
            embed.WithAuthor(username, userURL)
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(Color.Purple)
                .Build();
            return embed.Build();
        }

        public static Embed PostEmbedImage(string username, ulong userId, string description, string userURL, string url, ulong messageId)
        {
            // removes all urls
            Console.WriteLine($"url to post {url}");
            var embed = new EmbedBuilder();
            embed.WithAuthor(username, userURL, $"{url}")
                .WithDescription($"[<@{userId}> posted:](https://discord.com/channels/{serverId}/{galleryId}/{messageId})\n{description}")
                .WithColor(Color.Purple)
                .WithImageUrl(url)
                .Build();
            return embed.Build();
        }
        public static List<string> GetAllUrlFromString(string stringToAnalyse)
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
