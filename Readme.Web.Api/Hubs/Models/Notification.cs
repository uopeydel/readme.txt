
using Readme.Web.Api.Hubs.Enum;

namespace KwanJai.Web.Api
{
    public class Notification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int Badge { get; set; }
        public int UserId { get; set; }
        public ClientTypeEnum ClientType { get; set; }
    }
}