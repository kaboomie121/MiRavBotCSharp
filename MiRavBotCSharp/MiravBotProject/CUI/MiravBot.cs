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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MiravBotProject.CUI
{
    public class MiravBot
    {
        public async Task Start(string[] args)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("MiravBot is starting...");
            var builder = Host.CreateApplicationBuilder(args);

            var tokenFileData = await Methods.GetTokenFileAsync();

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

            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            logger.LogCritical("Test of the logger");

            await host.RunAsync();
            TimeSpan timeSpan = DateTime.Now - start;
            logger.LogInformation($"Bot started up in {timeSpan.TotalMilliseconds} ms.");
        }
    }
}
