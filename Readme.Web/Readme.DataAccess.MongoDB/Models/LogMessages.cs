using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.DataAccess.MongoDB.Models
{
    public class LogMessages
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string MessageJson { get; set; } = string.Empty;
        public DateTime SentTimestamp { get; set; } = DateTime.Now;
        public int MessageType { get; set; } = 0;
        public ObjectId UserIdSource { get; set; } = ObjectId.Empty;
        public ObjectId UserIdDestination { get; set; } = ObjectId.Empty;
        
    }
}
