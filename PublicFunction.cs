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
        public static readonly ulong artChannelId = 482894390570909706;
        public static readonly ulong artTalkChannelId = 561322620931538944;
        public static readonly ulong serverId = 482631363233710106;


        /*COMMANDS FOR COMMANDS*/

        public static void DisplayCommandLine(string CommandName, SocketCommandContext Context)
        {
            //Console.WriteLine($"{CommandName} command executed by user: {Context.User.Username} on channel: {Context.Channel.Name}");
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

        public static Embed PostEmbedImage(string username, ulong userId, string description, string userURL, string url, ulong messageId)
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
