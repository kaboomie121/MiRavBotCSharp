using Microsoft.Extensions.Hosting;
using NetCord;
using NetCord.Gateway;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MiravBotProject.Domain;

public class Commands() : ApplicationCommandModule<ApplicationCommandContext>
{
    private static readonly Structs.TokenFile TokenData = Methods.GetTokenFileAsync().Result;
    private static readonly Structs.BotConfig BotConfig = Methods.GetBotConfigAsync().Result;

    [SlashCommand("ping", "Ping!")]
    public async Task<string> PingCommand()
    {
        TimeSpan delay = DateTimeOffset.UtcNow - Context.Interaction.CreatedAt;

        return $"Pong! {delay.TotalMilliseconds} ms.";
    }

    [SlashCommand("countmembers", "test command to see if the webscraper works")]
    public async Task<string> CountMembers() 
    {
        var test = await Methods.GetSquadronPlayers();

        return test.Count == 0 ? "0" : (test.Count - 1).ToString() + " members found!";
    }

    [SlashCommand("uptime", "How long the bot has been up")]
    public async Task<string> UpTime()
    {
        DateTimeOffset start = Process.GetCurrentProcess().StartTime.ToUniversalTime();

        var deltatime = DateTime.Now - start;

        return $"I've been online since <t:{start.ToUnixTimeSeconds()}:f>, which is \n" +
                $"{deltatime.Days} day(s) and\n" +
                $"{deltatime.Hours} hour(s) and,\n" +
                $"{deltatime.Minutes} minute(s) and,\n" +
                $"{deltatime.Seconds} second(s) ago";
    }

    [SlashCommand("kicklist", "A list of players who need to be kicked from the ingame squadron")]
    public async Task KickList()
    {
        await Methods.GetNoticeList(TokenData, BotConfig, Context);

        var callback = InteractionCallback.Message("Function not operational");
        await Context.Interaction.SendResponseAsync(callback);
    }
}
