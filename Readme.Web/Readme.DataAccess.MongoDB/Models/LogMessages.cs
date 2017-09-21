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
        public string message_json { get; set; } = string.Empty;
        public DateTime sent_timestamp { get; set; } = DateTime.Now;
        public int message_type { get; set; } = 0;
        public ObjectId user_id_source { get; set; } = ObjectId.Empty;
        public ObjectId user_id_destination { get; set; } = ObjectId.Empty;
        public int user_Readme_id { get; set; } = 0;
    }
}
