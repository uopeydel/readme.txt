namespace Readme.Logic.DomainModel.LineModels
{
    public  class TemplateDto
    {
        public string type { get; set; }
        public string text { get; set; } 
        public string thumbnailImageUrl { get; set; }
        public string title { get; set; }
        public ActionDto[] actions { get; set; }

        public ColumnDto[] columns { get; set; }
    }
     
}
