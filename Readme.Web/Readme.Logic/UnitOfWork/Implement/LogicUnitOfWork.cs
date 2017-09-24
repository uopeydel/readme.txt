using Readme.DataAccess.Dapper.UnitOfWork.Interface;
using Readme.DataAccess.EntityFramework;
using Readme.DataAccess.EntityFramework.UnitOfWork.Interface;
using Readme.DataAccess.MongoDB.UnitOfWork.Interface;
using Readme.Logic.UnitOfWork.Interface;
using Readme.Logic.V1.Implement;
using Readme.Logic.V1.Interface;
using System;

namespace Readme.Logic.UnitOfWork.Implement
{
    public class LogicUnitOfWork : ILogicUnitOfWork, IDisposable
    {
        private IEntityUnitOfWork EntityUnitOfWork;
        private IDapperUnitOfWork DapperUnitOfWork;
        private IMongoDBUnitOfWork MongoDBUnitOfWork;

        private ILineAccountService ILineAccountService;

        private ILogUserService ILogUserService;
        private ILogMessagesService ILogMessagesService;
        private ILogFriendService ILogFriendService;

        private ILineService ILineService;

        private ISignalRService ISignalRService;

        public LogicUnitOfWork(IEntityUnitOfWork EntityUnitOfWork, IDapperUnitOfWork DapperUnitOfWork, IMongoDBUnitOfWork MongoDBUnitOfWork)
        {
            this.EntityUnitOfWork = EntityUnitOfWork;
            this.DapperUnitOfWork = DapperUnitOfWork;
            this.MongoDBUnitOfWork = MongoDBUnitOfWork;
        }



        public ILineAccountService LineAccountService
        {
            get { return ILineAccountService ?? (ILineAccountService = new LineAccountService(EntityUnitOfWork)); }
            set { ILineAccountService = value; }
        }



        public ILogUserService LogUserService
        {
            get { return ILogUserService ?? (ILogUserService = new LogUserService(EntityUnitOfWork, MongoDBUnitOfWork)); }
            set { ILogUserService = value; }
        }

        public ILogMessagesService LogMessagesService
        {
            get { return ILogMessagesService ?? (ILogMessagesService = new LogMessagesService(EntityUnitOfWork, MongoDBUnitOfWork)); }
            set { ILogMessagesService = value; }
        }

        
        public ILogFriendService LogFriendService
        {
            get { return ILogFriendService ?? (ILogFriendService = new LogFriendService(EntityUnitOfWork, MongoDBUnitOfWork)); }
            set { ILogFriendService = value; }
        }

        public ILineService LineService
        {
            get { return ILineService ?? (ILineService = new LineService()); }
            set { ILineService = value; }
        }

        public ISignalRService SignalRService
        {
            get { return ISignalRService ?? (ISignalRService = new SignalRService(EntityUnitOfWork)); }
            set { ISignalRService = value; }
        }


        public void Dispose()
        {

        }
    }
}
