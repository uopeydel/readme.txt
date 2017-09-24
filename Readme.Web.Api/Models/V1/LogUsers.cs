using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.Web.Api.Models.V1
{
    public class LogUsersModel
    {
        public string _id { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public DateTime CreateTimeStamp { get; set; } = DateTime.Now;
        public string Email { get; set; } = string.Empty;
        public string UID { get; set; } = string.Empty;
    }
}
