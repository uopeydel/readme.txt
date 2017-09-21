using Readme.Web.Api.Hubs.Enum;
namespace Readme.Web.Api
{
    //Model
    public class UserMap
    {
        public UserMap(string connectionId, int userId, ClientTypeEnum clientType)
        {
            ConnectionId = connectionId;
            UserId = userId;
            ClientType = clientType;
        }
        public string ConnectionId { get; }
        public int UserId { get; }
        public ClientTypeEnum ClientType { get; }
    }
}