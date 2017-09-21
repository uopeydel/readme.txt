using Readme.DataAccess.MongoDB.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readme.DataAccess.MongoDB.UnitOfWork.Interface
{
    public interface IMongoDBUnitOfWork
    {
        IMongoDBRepository MongoDBRepository { get; set; }
    }
}