﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using valhallappweb.Handler;

namespace valhallappweb
{
    public class Program
    {

        /*APP INIT */
        static void Main(string[] args)
        {
            Console.WriteLine("Valhalla application start");
            //starts the discord bot
            Task.Run(() => new Program().RunBotAsync().GetAwaiter().GetResult());
            //starts the website
            CreateWebHostBuilder(args).Build().Run();
        }

        /*WEB APP INIT*/
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            {
            Console.WriteLine("Valhalla webpage start");
                var port = Environment.GetEnvironmentVariable("PORT");

                return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseUrls($"http://*:{port}");
            }

        /*DISCORD BOT INIT*/
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

            // Logging
            _client.Log += Client_Log;

            // Command init
            RegisterCommandsAsync();

            // Bot Authentification init
            var token = "ODE4MTAyNjcxODAxMzE5NDM0.YETLfA.yFrDFFKKp_LVJLiY6BfpTWypufg";
            await _client.LoginAsync(TokenType.Bot, token);

            // Start Discord bot
            await _client.StartAsync();

            await _client.SetStatusAsync(UserStatus.Idle);
            await _client.SetGameAsync("Valhalla 3.0", "https://cdn.discordapp.com/emojis/750294190067286047.png?v=1", ActivityType.CustomStatus);
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            await Task.Delay(-1);
        }

        // Logging
        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        // Command init
        public void RegisterCommandsAsync()
        {
            ReactionHandler reactionHandler = new ReactionHandler(_client);
            FiregatorHandler firegatorHandler = new FiregatorHandler(_client);
            MessageEditedHandler editedHandler = new MessageEditedHandler(_client);
            MessageDeleteHandler deleteHandler = new MessageDeleteHandler(_client);
            MessageAddedHandler messageHandler = new MessageAddedHandler(_client, _commands,_services);
            FireGatorTracker FireGator = new FireGatorTracker();
            _client.ReactionAdded += reactionHandler.HandleReactionAsync;
            _client.ReactionRemoved += reactionHandler.HandleReactionAsync;
            _client.ReactionsCleared += reactionHandler.HandleReactionClearAsync;
            _client.MessageReceived += messageHandler.HandleCommandAsync;
            _client.MessageDeleted += deleteHandler.HandleDeleteAsync;
            _client.MessageUpdated += editedHandler.HandleEditAsync;
            FireGator.Start();
            FireGator.OnThursday += firegatorHandler.HandleFiregatorAsync;
        }
    }
}