using System;
using System.ComponentModel.DataAnnotations;
using Readme.Common.Enum;
using MongoDB.Bson;

namespace Readme.Logic.DomainModel
{
    public class LogUsersDto
    {
        public ObjectId _id { get; set; } = ObjectId.Empty;
        public string displayName { get; set; } = string.Empty;
        public string pictureUrl { get; set; } = string.Empty;
        public string statusMessage { get; set; } = string.Empty;
        public DateTime followTimeStamp { get; set; } = DateTime.Now;
        public string uid { get; set; } = string.Empty;
        public string param_hook { get; set; } = string.Empty;
        public ActiveStatusLineLogEnum activeStatus { get; set; } = ActiveStatusLineLogEnum.None;
    }
}
