using NetCord;
using NetCord.Gateway;
using NetCord.Services.ApplicationCommands;
using NetCord.Rest;
using System.Threading.Tasks;

namespace MiravBotProject.Domain;

public class Commands : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("ping", "Ping!")]
    public static string PingCommand()
    {
        return "Pong!";
    }
}
