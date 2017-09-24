using Readme.DataAccess.EntityFramework.UnitOfWork.Interface;
using Readme.DataAccess.MongoDB.Models;
using Readme.DataAccess.MongoDB.UnitOfWork.Interface;
using Readme.Logic.DomainModel;
using Readme.Logic.V1.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using Readme.Common.Enum;
using MongoDB.Bson;
using Newtonsoft.Json;
using Readme.Logic.DomainModel.LineModels;
using Microsoft.EntityFrameworkCore;

namespace Readme.Logic.V1.Implement
{
    public class LogFriendService : ILogFriendService
    {
        private IMongoDBUnitOfWork MongoDBUnitOfWork;
        private IEntityUnitOfWork EntityUnitOfWork;

        public LogFriendService(IEntityUnitOfWork EntityUnitOfWork, IMongoDBUnitOfWork MongoDBUnitOfWork)
        {
            this.MongoDBUnitOfWork = MongoDBUnitOfWork;
            this.EntityUnitOfWork = EntityUnitOfWork;
        }

        public async Task AddFriend(LogFriendsMongoDto LogFriendsData)
        {
            var IsAlreadyFriend = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogFriends()
                .Find(x =>
                (x.IdSource == LogFriendsData.IdSource && x.IdDestination == LogFriendsData.IdDestination)
                ||
                (x.IdSource == LogFriendsData.IdDestination && x.IdDestination == LogFriendsData.IdSource)
                ).AnyAsync();

            if (IsAlreadyFriend)
            {
                return;
            }
            else
            {
                LogFriends LogFriendData = new LogFriends()
                {
                    CreateTimeStamp = DateTime.Now,
                    IdDestination = LogFriendsData.IdDestination,
                    IdSource = LogFriendsData.IdSource
                };
                await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogFriends().InsertOneAsync(LogFriendData);

                //TODO: Send SignalR To Friend Destination alert have someone add friend
            }
        }

        public async Task<List<LogUsersMongoDto>> GetFriendList(ObjectId UserId)
        {
            var ListFriend = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogFriends()
                 .Find(x => x.IdDestination == UserId || x.IdSource == UserId)
                 .Project(p => new LogFriends()
                 {
                     IdSource = p.IdSource,
                     IdDestination = p.IdDestination
                 })
                 .ToListAsync();

            var ListFriendId = ListFriend
                .Where(w => w.IdSource != UserId)
                .Select(s => s.IdSource).ToArray()
                .Concat(
                    ListFriend
                    .Where(w2 => w2.IdDestination != UserId)
                    .Select(s2 => s2.IdDestination).ToArray()
                ).ToArray();

            var FriendList = await MongoDBUnitOfWork.MongoDBRepository.GetCollectionLogUsers()
                .Find(x => ListFriendId.Contains(x._id))
                .Project(a => new LogUsersMongoDto
                {
                    _id = a._id,
                    DisplayName = a.DisplayName,
                    PictureUrl = a.PictureUrl,
                    StatusMessage = a.StatusMessage
                })
                .ToListAsync();

            return FriendList;
        }
    }
}
