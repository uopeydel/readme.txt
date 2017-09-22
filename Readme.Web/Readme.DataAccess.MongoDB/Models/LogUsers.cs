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
        public string DisplayName { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public DateTime CreateTimeStamp { get; set; } = DateTime.Now;
        public string Email { get; set; } = string.Empty;
    }
}
