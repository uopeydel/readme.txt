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
                .Find(x=>
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
    }
}
