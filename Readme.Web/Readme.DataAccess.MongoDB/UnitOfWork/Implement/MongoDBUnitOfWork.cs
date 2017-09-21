using Readme.DataAccess.MongoDB.Common.Implement;
using Readme.DataAccess.MongoDB.Common.Interface;
using Readme.DataAccess.MongoDB.Models;
using Readme.DataAccess.MongoDB.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.DataAccess.MongoDB.UnitOfWork.Implement
{
    public class MongoDBUnitOfWork : IMongoDBUnitOfWork, IDisposable
    {
        private MongoDBConnection MongoDBConnection = new MongoDBConnection();

        private IMongoDBRepository _mongoDbRepository;

        public IMongoDBRepository MongoDBRepository
        {
            get { return _mongoDbRepository ?? (_mongoDbRepository = new MongoDBRepository(MongoDBConnection)); }
            set { _mongoDbRepository = value; }
        }

        public void Dispose()
        {

        }
    }
}
