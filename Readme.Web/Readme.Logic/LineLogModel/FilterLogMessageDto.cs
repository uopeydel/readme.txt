using Readme.Common.Enum;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.Logic.DomainModel
{
    public class FilterLogMessageDto
    {
        public int MaxMessage { get; set; }
        public DateTime LastTimestamp { get; set; }
        public DateTime? SentTimeStampMin { get; set; }
        public DateTime? SentTimeStampMax { get; set; }
        public MessageTypeLineLogEnum MessageType { get; set; }
        public ObjectId UserIdSource { get; set; }
        public ObjectId UserIdDestination { get; set; }
        public ObjectId UserIdCustomer { get; set; }
    }
}

