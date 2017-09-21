using Readme.DataAccess.Dapper.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.DataAccess.Dapper.UnitOfWork.Interface
{
    public interface IDapperUnitOfWork
    {
        IRequestOrderRepository RequestOrderRepository { get; set; }
        IWorkIssueRepository WorkIssueRepository { get; set; }
        IForemanRepository ForemanRepository { get; set; }
    }
}
