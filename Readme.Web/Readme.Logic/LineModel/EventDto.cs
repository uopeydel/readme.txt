namespace Readme.Logic.DomainModel.LineModels
{
    public class EventDto
    {
        public string to { get; set; }
        public int? fromUserId { get; set; }
        public string ChannelAccessToken { get; set; }
        public string type { get; set; }

        public string replyToken { get; set; }

        public SourceDto source { get; set; }

        public long timestamp { get; set; }

        public MessageDto message { get; set; }

        public PostbackDto postback { get; set;  }

    }

}
