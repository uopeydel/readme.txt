using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.Web.Api.Models.V1
{
    public class LogFriendsModel
    {
        public string IdSource { get; set; } = string.Empty;
        public string IdDestination { get; set; } = string.Empty;
        public DateTime CreateTimeStamp { get; set; } = DateTime.Now;
        public int AvailableStatus { get; set; } = 1;
    }
}
