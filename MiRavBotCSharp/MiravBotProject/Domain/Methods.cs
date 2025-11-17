namespace MiravBotProject.Domain;

public class Methods
{
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
}
