namespace Readme.Logic.DomainModel.LineModels
{
    public class ColumnDto
    {
        public string thumbnailImageUrl { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public ActionDto[] actions { get; set; }
    }
}
