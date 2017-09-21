using System;
using System.ComponentModel.DataAnnotations;
using Readme.Common.Enum;
using MongoDB.Bson;

namespace Readme.Logic.DomainModel
{
    public class LogMessagesDto
    {
        public ObjectId _id { get; set; } = ObjectId.Empty;
        public string message_json { get; set; } = string.Empty;
        public DateTime sent_timestamp { get; set; } = DateTime.Now;
        public MessageTypeLineLogEnum message_type { get; set; } = MessageTypeLineLogEnum.none;
        public ObjectId user_id_source { get; set; } = ObjectId.Empty;
        public ObjectId user_id_destination { get; set; } = ObjectId.Empty;
        public int user_Readme_id { get; set; } = 0;
        public SenderType senderType { get; set; }
    }

    public enum SenderType
    {
        None = 0,
        System = 1,
        Customer = 2,
        User = 3
    }
}
