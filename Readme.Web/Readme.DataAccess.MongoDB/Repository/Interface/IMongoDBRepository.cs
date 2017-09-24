using Readme.DataAccess.MongoDB.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Readme.DataAccess.MongoDB.Common.Interface
{
    public interface IMongoDBRepository
    {
        IMongoCollection<LogUsers> GetCollectionLogUsers();
        IMongoCollection<LogMessages> GetCollectionLogMessages();
        IMongoCollection<LogFriends> GetCollectionLogFriends();
    }
}
