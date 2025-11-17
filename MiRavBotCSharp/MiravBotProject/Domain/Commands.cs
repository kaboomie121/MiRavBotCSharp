using NetCord;
using NetCord.Gateway;
using NetCord.Services.ApplicationCommands;
using NetCord.Rest;
using System.Threading.Tasks;

namespace MiravBotProject.Domain;

public class Commands : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("ping", "Ping!")]
    public async Task<string> PingCommand()
    {
        //var user = Context.User;

        return $"Pong";
    }

    [SlashCommand("countmembers", "test command to see if the webscraper works")]
    public async Task<string> CountMembers() 
    {
        var test = await Methods.GetSquadronPlayers();

        return test.Count == 0 ? "0" : (test.Count - 1).ToString() + " members found!";
    }
}
