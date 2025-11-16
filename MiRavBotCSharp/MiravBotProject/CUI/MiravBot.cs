using NetCord;
using NetCord.Gateway;
using NetCord.Services;
using NetCord.Hosting;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;
using Microsoft.Extensions.Hosting;
using MiravBotProject.Domain;
using NetCord.Hosting.Rest;

namespace MiravBotProject.CUI
{
    public class MiravBot
    {
        public async Task Start(string[] args, Structs.TokenFile tokenFileData, Structs.BotConfig botConfigData)
        {
            Console.WriteLine("MiravBot is starting...");
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddDiscordGateway(options =>
            {
                if (!tokenFileData.devMode)
                {
                    if (!string.IsNullOrWhiteSpace(tokenFileData.token))
                        options.Token = tokenFileData.token;
                    else
                        throw new Exception("Token not found");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(tokenFileData.testtoken))
                        options.Token = tokenFileData.testtoken;
                    else
                        throw new Exception("Test Token not found");
                }
            }).AddApplicationCommands();

            var host = builder.Build();

            host.AddModules(typeof(Program).Assembly);

            await host.RunAsync();
        }
    }
}
