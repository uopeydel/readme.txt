namespace Readme.Logic.DomainModel.LineModels
{
    public class MessageDto
    {
        public string type { get; set; }

        public string id { get; set; }

        public string text { get; set; }

        public string title { get; set; }

        public string altText { get; set; }

        //location
        public string address { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        
        //sticker
        public int packageId { get; set; } 

        public int stickerId { get; set; }

        //imagemap
        public string baseUrl { get; set; }
        
        public BaseSizeDto baseSize { get; set; }
        
        public ActionDto[] actions { get; set; }




        //template
        public TemplateDto template { get; set; }

        public string originalContentUrl { get; set; }

        public int duration { get; set; }

        public string previewImageUrl { get; set; }

    }
}
