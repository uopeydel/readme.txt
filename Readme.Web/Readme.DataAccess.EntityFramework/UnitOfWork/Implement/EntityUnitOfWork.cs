using Readme.DataAccess.EntityFramework.Common.Implement;
using Readme.DataAccess.EntityFramework.Common.Interface;
using Readme.DataAccess.EntityFramework.Models;
using Readme.DataAccess.EntityFramework.UnitOfWork.Interface;
using System;
using System.Threading.Tasks;

namespace Readme.DataAccess.EntityFramework
{
    public class EntityUnitOfWork : IEntityUnitOfWork, IDisposable
    {
        private ReadmeContext _dbContext;

        private IBaseRepository<LineAccount> _lineAccountRepository;
        private IBaseRepository<LineMember> _lineMemberRepository;

        public EntityUnitOfWork(ReadmeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBaseRepository<LineAccount> LineAccountRepository
        {
            get { return _lineAccountRepository ?? new BaseRepository<LineAccount>(_dbContext); }
            set { _lineAccountRepository = value; }
        }

        public IBaseRepository<LineMember> LineMemberRepository
        {
            get { return _lineMemberRepository ?? new BaseRepository<LineMember>(_dbContext); }
            set { _lineMemberRepository = value; }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
