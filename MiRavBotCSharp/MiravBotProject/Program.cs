using MiravBotProject.CUI;
using MiravBotProject.Domain;
using System.Text.Json;

namespace MiravBotProject;

internal class Program
{
    static async Task Main(string[] args)
    {
        string tokenDataFromFile = await File.ReadAllTextAsync("./token.json");
        Structs.TokenFile dataTokenFile = JsonSerializer.Deserialize<Structs.TokenFile>(tokenDataFromFile);

        string configDataFromFile = await File.ReadAllTextAsync("./config.json");
        Structs.BotConfig dataBotConfig = JsonSerializer.Deserialize<Structs.BotConfig>(configDataFromFile);

        Console.WriteLine($"User: {dataTokenFile.user}");
        Console.WriteLine($"Dev mode: {dataTokenFile.devMode}");

        MiravBot bot = new MiravBot();
        try
        {
            await bot.Start(args, dataTokenFile, dataBotConfig);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
