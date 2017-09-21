using System;
using System.ComponentModel.DataAnnotations;

namespace Readme.DataAccess.Dapper.Models
{
    public class WorkIssueIndexModel
    {
        [Required]
        public int WorkIssueId { get; set; }
        [Required]
        public string WorkIssueCode { get; set; }
        [Required]
        public int WorkStatus { get; set; }
        public int? DefectCategoryId { get; set; }
        public string DefectCategoryName { get; set; }
        [Required]
        public DateTimeOffset InfromDate { get; set; }
        [Required]
        public DateTime? WorkDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public object DefectImageUrl { get; set; }
    }
}
