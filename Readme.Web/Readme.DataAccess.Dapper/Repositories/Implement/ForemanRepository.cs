using System.Collections.Generic;
using System.Threading.Tasks;
using Readme.DataAccess.Dapper.Models;
using Readme.DataAccess.Dapper.Repositories.Interface;
using System;
using Dapper;

namespace Readme.DataAccess.Dapper.Repositories.Implement
{
    public class ForemanRepository : IForemanRepository
    {
        private ReadmeConnection ReadmeConnection;

        public ForemanRepository(ReadmeConnection connection)
        {
            ReadmeConnection = connection;
        }

        public async Task<IEnumerable<ForemanIndexDapperModel>> GetFilterForemanListAsync(int companyId, int projectId, int filterId, string filterKeyWord)
        {
            try
            {
                await ReadmeConnection.Connection.OpenAsync();

                string sql1 = @" SELECT
                                dbo.EMPLOYEE.user_id AS UserId,
                                dbo.[USER].firstname AS UserFirstName,
                                dbo.[USER].lastname AS UserLastName,
                                dbo.EMPLOYEE.company_id AS CompanyId,
                                dbo.COMPANY.name AS CompanyName,
                                dbo.EMPLOYEE_ASSIGNEE.employee_assignee_id AS EmployeeAssigneeId

                                FROM dbo.EMPLOYEE_ASSIGNEE

                                INNER JOIN dbo.EMPLOYEE_ROLE ON dbo.EMPLOYEE_ASSIGNEE.employee_role_id = dbo.EMPLOYEE_ROLE.employee_role_id
                                INNER JOIN dbo.ROLE ON dbo.EMPLOYEE_ROLE.role_code = dbo.ROLE.role_code
                                INNER JOIN dbo.EMPLOYEE ON dbo.EMPLOYEE_ROLE.employee_id = dbo.EMPLOYEE.employee_id
                                INNER JOIN dbo.COMPANY ON dbo.EMPLOYEE.company_id = dbo.COMPANY.company_id
                                INNER JOIN dbo.[USER] ON dbo.EMPLOYEE.user_id = dbo.[USER].user_id

                                WHERE
                                EMPLOYEE_ASSIGNEE.project_id = @ProjectId AND
                                (EMPLOYEE_ROLE.role_code = @RoleCode1 OR EMPLOYEE_ROLE.role_code = @RoleCode2) AND
                                EMPLOYEE.company_id = @CompanyId AND
                                ([USER].firstname LIKE '%@FilterKeyWord%' OR [USER].lastname LIKE '%@FilterKeyWord%')";

                IEnumerable<ForemanIndexDapperModel> ForemanList = await ReadmeConnection.Connection.QueryAsync<ForemanIndexDapperModel>(sql1, new { ProjectId = projectId, RoleCode1 = 300, RoleCode2 = 302, CompanyId = companyId, FilterKeyWord = filterKeyWord });

                ReadmeConnection.Connection.Close();

                return ForemanList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
