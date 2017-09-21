using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.DataAccess.MongoDB.Models
{
    public class LogUsers
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string displayName { get; set; } = string.Empty;
        public string pictureUrl { get; set; } = string.Empty;
        public string statusMessage { get; set; } = string.Empty;
        public DateTime followTimeStamp { get; set; } = DateTime.Now;
        public string uid { get; set; } = string.Empty;
        public string param_hook { get; set; } = string.Empty;
        public int activeStatus { get; set; } = 0;
    }
}
