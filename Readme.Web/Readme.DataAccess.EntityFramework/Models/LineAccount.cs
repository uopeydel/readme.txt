using System;
using System.Collections.Generic;

namespace Readme.DataAccess.EntityFramework.Models
{
    public partial class LineAccount
    {
        public LineAccount()
        {
            LineMember = new HashSet<LineMember>();
        }

        public int LineAccountId { get; set; }
        public string LineAccountName { get; set; }

        public ICollection<LineMember> LineMember { get; set; }
    }
}
