﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace valhallappweb
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Valhalla application start");
            Task.Run(() => new Program().RunBotAsync().GetAwaiter().GetResult());
            CreateWebHostBuilder(args).Build().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            {
            Console.WriteLine("Valhalla webpage start");
                var port = Environment.GetEnvironmentVariable("PORT");

                return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseUrls("http://*:"+port);
            }

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            Console.WriteLine("Valhalla bot start");
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            var token = Environment.GetEnvironmentVariable("TOKEN");
            //string token = File.ReadAllText(@"./Modules/Token.txt");

            _client.Log += Client_Log;
            await RegisterCommandsAsync();


            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            MessageChannel("Bot is starting...", 779779716482727996);
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
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

        public void CheckImageArtChannel(SocketUserMessage message)
        {
            const ulong artChannelId = 482894390570909706;
            const ulong artTalkChannelId = 561322620931538944;
            // if the message has no attachments and no embed as well isn't in the art channel, return
            //Console.WriteLine("Message has " + message.Attachments.Count + " attachment(s) and " + message.Embeds.Count + " embed(s)");
            if (message.Channel.Id != artChannelId || (message.Attachments.Count == 0 && message.Embeds.Count == 0)) return;
            foreach (var attachment in message.Attachments) MessageChannel(attachment.Url, artTalkChannelId);
            foreach (var embed in message.Embeds) MessageChannel(embed.Url, artTalkChannelId);
        }
        public void MessageChannel(string messageContent, ulong channelId)
        {
            Console.WriteLine("url of image:" + messageContent);
            var chnl = _client.GetChannel(channelId) as IMessageChannel; 
            chnl.SendMessageAsync(messageContent);
        }
    }
}
