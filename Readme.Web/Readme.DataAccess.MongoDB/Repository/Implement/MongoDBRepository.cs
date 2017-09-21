using Readme.DataAccess.MongoDB.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Readme.DataAccess.MongoDB;
using System.Threading.Tasks;
using System.Linq;
using Readme.DataAccess.MongoDB.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Readme.DataAccess.MongoDB.Common.Implement
{
    public class MongoDBRepository : IMongoDBRepository
    {
        private MongoDBConnection MongoDBConnection;

        public MongoDBRepository(MongoDBConnection connection)
        {
            MongoDBConnection = connection;
        }

        public IMongoCollection<LogUsers> GetCollectionLogUsers()
        {
            var Coll = MongoDBConnection._database.GetCollection<LogUsers>("LogUsers");
            return Coll;
        }

        public IMongoCollection<LogMessages> GetCollectionLogMessages()
        {
            var Coll = MongoDBConnection._database.GetCollection<LogMessages>("LogMessages");
            return Coll;
        }

    }
}
