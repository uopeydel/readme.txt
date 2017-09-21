using Readme.DataAccess.Dapper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readme.DataAccess.Dapper.Repositories.Interface
{
    public interface IForemanRepository
    {
        Task<IEnumerable<ForemanIndexDapperModel>> GetFilterForemanListAsync(int companyId, int projectId, int filterId, string filterKeyWord);
    }
}
