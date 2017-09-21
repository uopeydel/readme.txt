using Readme.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.Logic.DomainModel
{
    public class PostponeAppointmentLineDto
    {
        public int UserId { get; set; }
        public int WIssueId { get; set; }

        public int IssueId { get; set; }
        public string IssueCode { get; set; }

        public int ServiceOrderId { get; set; }

        public int DefectCategoryId { get; set; }
        public string DefectCategoryName { get; set; }

        public int DefectChildId { get; set; }
        public string DefectChildName { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }
        public string Remark { get; set; }

        
        public string AppointmentDescription { get; set; }

        //
        public string ChannelAccessToken { get; set; }
        public string RequestOrderCode { get; set; }
        public string LineUID { get; set; }
        public string LineHook { get; set; }

        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
    }
}
