namespace Readme.Logic.DomainModel.LineModels
{
    public class SourceDto
    {
        public string groupId { get; set; }

        public string userId { get; set; }

        public string type { get; set; }

        public string[] multicastToUserId { get; set; }
    }
}
