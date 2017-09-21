using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.DataAccess.Dapper.Models
{
    public class ForemanIndexDapperModel
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int EmployeeAssigneeId { get; set; }
        public List<ForemanIssueDetailDto> IssueDetailList { get; set; }
    }

    public partial class ForemanIssueDetailDto
    {
        public int EmployeeAssigneeId { get; set; }
        public string ProjectName { get; set; }
        public string IssueCode { get; set; }
        public string WIssueCode { get; set; }
        public string DefectName { get; set; }
        public DateTime? OpenJobDate { get; set; }
        public DateTime? CloseJobDate { get; set; }
        
    }
}
