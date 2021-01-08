using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static valhallappweb.PublicFunction;

namespace valhallappweb.Handler
{
    public class FiregatorHandler
    {

        private readonly DiscordSocketClient _client;
        public FiregatorHandler(DiscordSocketClient _client)
        {
            this._client = _client;
        }

        public async Task HandleFiregatorAsync()
        {
            await MessageChannel(_client, "🙏IT'S FIREGATOR THURSDAY🙏!!!!!!!\nhttps://cdn.discordapp.com/attachments/727722397414850611/728076063837651075/video0_8.mov", memeId);
        }
    }
}
