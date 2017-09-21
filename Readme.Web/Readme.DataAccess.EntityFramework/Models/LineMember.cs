using System;
using System.Collections.Generic;

namespace Readme.DataAccess.EntityFramework.Models
{
    public partial class LineMember
    {
        public int LineMemberId { get; set; }
        public int? LineAccountId { get; set; }
        public string LineMemberName { get; set; }

        public LineAccount LineAccount { get; set; }
    }
}
