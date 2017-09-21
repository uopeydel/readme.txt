using System.Collections.Generic;
using System.Threading.Tasks;
using Readme.DataAccess.Dapper.Models;
using Readme.DataAccess.Dapper.Repositories.Interface;
using System;
using Dapper;

namespace Readme.DataAccess.Dapper.Repositories.Implement
{
    public class WorkIssueRepository : IWorkIssueRepository
    {
        private ReadmeConnection ReadmeConnection;

        public WorkIssueRepository(ReadmeConnection connection)
        {
            ReadmeConnection = connection;
        }

        public async Task<IEnumerable<WorkIssueIndexModel>> GetAllWorkIssueAsync(int employeeAssigneeId)
        {
            try
            {
                await ReadmeConnection.Connection.OpenAsync();

                string sql = @" SELECT      [w_issue_id] AS WorkIssueId, 
                                            [WORK_ISSUE].[code] AS WorkIssueCode,
                                            [status] AS WorkIssueStatus,
                                            [add_defect_category_id] AS DefectCategoryId,
                                            [DEFECT_CATEGORY].[name] AS DefectCategoryName,
                                            [examine_date] AS InfromDate,
                                            [working_date] AS WorkDate,
                                            [close_date] AS CloseDate,
                                            [examine_signature] AS DefectImageUrl
                                FROM        [WORK_ISSUE]
                                LEFT JOIN   [DEFECT_CATEGORY] ON [DEFECT_CATEGORY].[defect_category_id] = [WORK_ISSUE].[add_defect_category_id]
                                WHERE       [WORK_ISSUE].[employee_assignee_id] = @param_employeeAssigneeId";

                IEnumerable<WorkIssueIndexModel> WorkIssueList = await ReadmeConnection.Connection.QueryAsync<WorkIssueIndexModel>(sql, new { param_employeeAssigneeId = employeeAssigneeId });

                ReadmeConnection.Connection.Close();

                return WorkIssueList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
