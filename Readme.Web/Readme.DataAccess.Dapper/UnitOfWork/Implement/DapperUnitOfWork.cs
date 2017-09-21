using Readme.DataAccess.Dapper.Repositories.Implement;
using Readme.DataAccess.Dapper.Repositories.Interface;
using Readme.DataAccess.Dapper.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.DataAccess.Dapper.UnitOfWork.Implement
{

    public class DapperUnitOfWork : IDapperUnitOfWork, IDisposable
    {
        private ReadmeConnection ReadmeConnection = new ReadmeConnection();

        private IRequestOrderRepository _requestOrderRepository;
        private IWorkIssueRepository _workIssueRepository;
        private IForemanRepository _foremanRepository;

        public IRequestOrderRepository RequestOrderRepository
        {
            get { return _requestOrderRepository ?? (_requestOrderRepository = new RequestOrderRepository(ReadmeConnection)); }
            set { _requestOrderRepository = value; }
        }

        public IWorkIssueRepository WorkIssueRepository
        {
            get { return _workIssueRepository ?? (_workIssueRepository = new WorkIssueRepository(ReadmeConnection)); }
            set { _workIssueRepository = value; }
        }

        public IForemanRepository ForemanRepository
        {
            get { return _foremanRepository ?? (_foremanRepository = new ForemanRepository(ReadmeConnection)); }
            set { _foremanRepository = value; }
        }

        public void Dispose()
        {
            ReadmeConnection.Connection.Dispose();
        }

    }
}
