using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.Logic.DomainModel
{
    public class MaNotiDataDto
    {
        public string DomainName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public int? IssueId { get; set; }
        public int? WIssueId { get; set; }
    }

}