using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using System.Text.Json;

namespace MiravBotProject.Domain;

public class Methods
{
    #region File Accessing

    public static async Task<Structs.TokenFile> GetTokenFileAsync()
    {
        string tokenDataFromFile = await File.ReadAllTextAsync("./token.json");
        return JsonSerializer.Deserialize<Structs.TokenFile>(tokenDataFromFile);
    }

    public static async Task<Structs.BotConfig> GetBotConfigAsync()
    {
        string configDataFromFile = await File.ReadAllTextAsync("./config.json");
        return JsonSerializer.Deserialize<Structs.BotConfig>(configDataFromFile);
    }

    #endregion

    private static readonly HttpClient client = new HttpClient();
    public static async Task<List<Dictionary<int, string>>> GetSquadronPlayers()
    {
        string url = "https://warthunder.com/en/community/claninfo/Midnight%20Ravens";

        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string content = await response.Content.ReadAsStringAsync();

        string contentReady = content.Replace(" ", "").Replace("\n", "");

        int index = contentReady.IndexOf("<divclass=\"squadrons-members__table\">");
        int lastIndex = contentReady.IndexOf("<divclass=\"socialsocial--footer\">");

        for (int counter = 0; counter <= 13; counter++)
        {
            for (int i = index + 2; i <= lastIndex; i++)
            {
                if (contentReady[i] == '<')
                    break;
            }
        }

        bool isHTML = false;
        int type = 0;
        int currentPersonNumber = 0;
        string currentWord = "";
        var personList = new List<Dictionary<int, string>>();

        for (int i = index; i <= lastIndex; i++)
        {
            if (contentReady[i] == '<')
                isHTML = true;
            if (!isHTML)
                currentWord += contentReady[i];
            if (contentReady[i] == '>')
            {
                isHTML = false;
                if (!string.IsNullOrWhiteSpace(currentWord) && currentWord[currentWord.Length - 1] != ' ')
                {
                    if (type == 6)
                    {
                        type = 0;
                        currentPersonNumber += 1;
                    }
                    else if (type != 0)
                    {
                        if (personList.Count == currentPersonNumber)
                        {
                            personList.Add([]);
                        }

                        if (!personList[currentPersonNumber].ContainsKey(type))
                        {
                            personList[currentPersonNumber].Add(type, currentWord);
                        }

                        personList[currentPersonNumber][type] = currentWord;

                        // Reset for the next word
                        currentWord = " ";
                        type++;
                    }

                    else
                    {
                        type += 1;
                        currentWord = "";
                    }

                }
            }
        }
        return personList;
    }

    public static int FindFirstIndex(string givenString, char findChar)
    {
        foreach (char c in givenString)
        {
            if(c == findChar)
                return (int)c;
        }
        return -1;
    }

    public static async Task<List<Structs.NoticeListStruct>> GetNoticeList(Structs.TokenFile tFD, Structs.BotConfig bCD, ApplicationCommandContext Context)
    {
        if (tFD.devMode)
        {
            //await Context.Channel.SendMessageAsync("Test"); (testing how the bot send a message to a channel)
            return new();
        }
        
        var returnList = new List<Structs.NoticeListStruct>();

        Context.Guild.Channels.TryGetValue(bCD.noticeListChannelId, out var NoticeListChannel);

        if(NoticeListChannel is TextChannel textChannel)
        {
            var messages = textChannel.GetMessagesAsync(new() { BatchSize = 123 });
        }

        

        return returnList;
    }

    public static async Task GetSquadronKickable(Structs.BotConfig botConfigFileData)
    {

    }
}
