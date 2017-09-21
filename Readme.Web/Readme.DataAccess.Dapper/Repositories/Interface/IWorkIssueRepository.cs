using Readme.DataAccess.Dapper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readme.DataAccess.Dapper.Repositories.Interface
{
    public interface IWorkIssueRepository
    {
        Task<IEnumerable<WorkIssueIndexModel>> GetAllWorkIssueAsync(int employeeAssigneeId);
    }
}
