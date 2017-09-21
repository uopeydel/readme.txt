using Readme.DataAccess.Dapper.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readme.DataAccess.Dapper.Repositories.Implement
{
    public class RequestOrderRepository : IRequestOrderRepository
    {
        private ReadmeConnection ReadmeConnection;

        public RequestOrderRepository(ReadmeConnection connection)
        {
            ReadmeConnection = connection;
        }

        //public async Task<IEnumerable<object>> GetNewJobByPriority(int projectId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
