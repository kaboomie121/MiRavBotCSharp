using MiravBotProject.CUI;
using MiravBotProject.Domain;
using System.Text.Json;

namespace MiravBotProject;

internal class Program
{
    static async Task Main(string[] args)
    {
        var dataTokenFile = await Methods.GetTokenFileAsync();

        Console.WriteLine($"User: {dataTokenFile.user}");
        Console.WriteLine($"Dev mode: {dataTokenFile.devMode}");

        MiravBot bot = new MiravBot();
        try
        {
            await bot.Start(args);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
