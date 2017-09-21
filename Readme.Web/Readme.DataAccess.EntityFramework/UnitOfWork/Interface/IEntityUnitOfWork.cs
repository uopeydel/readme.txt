using Readme.DataAccess.EntityFramework.Common.Interface;
using Readme.DataAccess.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readme.DataAccess.EntityFramework.UnitOfWork.Interface
{
    public interface IEntityUnitOfWork
    {
        IBaseRepository<LineAccount> LineAccountRepository { get; set; }
        IBaseRepository<LineMember> LineMemberRepository { get; set; }
        Task<int> SaveAsync();
    }
}