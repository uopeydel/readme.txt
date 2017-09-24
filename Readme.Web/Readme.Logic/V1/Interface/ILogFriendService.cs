using MongoDB.Bson;
using Readme.DataAccess.MongoDB.Models;
using Readme.Logic.DomainModel;
using Readme.Logic.DomainModel.LineModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readme.Logic.V1.Interface
{
     
    public interface ILogFriendService
    {
        Task AddFriend(LogFriendsMongoDto LogFriendsData);
        Task<List<LogUsersMongoDto>> GetFriendList(ObjectId UserId);
    }
}
