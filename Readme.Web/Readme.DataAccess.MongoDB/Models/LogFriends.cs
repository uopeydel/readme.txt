using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.DataAccess.MongoDB.Models
{
    public class LogFriends
    {
        public ObjectId IdSource { get; set; } = ObjectId.Empty;
        public ObjectId IdDestination { get; set; } = ObjectId.Empty;
        public DateTime CreateTimeStamp { get; set; } = DateTime.Now;
        public int AvailableStatus { get; set; } = 1;
    }
}
