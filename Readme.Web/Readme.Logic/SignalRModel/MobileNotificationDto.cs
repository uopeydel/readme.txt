using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.Logic.DomainModel
{
    public class MobileAppNotificationDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int Badge { get; set; }


        public int UserId { get; set; }
        public int? ProjectId { get; set; }
        public int? ROId { get; set; }
        public int? SOId { get; set; }
        public int? IssueId { get; set; }
        public int? WIssueId { get; set; }
    }
}