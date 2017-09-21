using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.Logic.DomainModel
{
    public class RqNotiDataDto
    {
        public string DomainName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int ProjectId { get; set; }
        public int? RequestOrderId { get; set; }
        public int? ServiceOrderId { get; set; }
    }

}