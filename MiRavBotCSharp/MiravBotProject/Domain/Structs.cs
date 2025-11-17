namespace MiravBotProject.Domain;

public class Structs
{
    public struct NoticeListStruct
    {
        public string Name { get; set; }

        public Enums.NoticeListConditions Condition { get; set; }
    }

    public struct TokenFile
    {
        public string user { get; set; }
        public bool devMode { get; set; }
        public string token { get; set; }
        public string testtoken { get; set; }
    }

    public struct BotConfig
    {
        public ulong discordGuild { get; set; }
        public ulong testDiscordGuild { get; set; }
        public int firstDeadline { get; set; }
        public int firstActivityRequirement { get; set; }
        public int secondDeadline { get; set; }
        public int secondActivityRequirement { get; set; }
        public int exemptionSQRating { get; set; }
        public int joinDeadline { get; set; }
        public ulong noticeListChannelId { get; set; }
        public ulong squadronMemberRoleId { get; set; }
        public ulong squadronStaffId { get; set; }
        public ulong communityHostRoleId { get; set; }
        public ulong DBChannelId { get; set; }
    }
}
