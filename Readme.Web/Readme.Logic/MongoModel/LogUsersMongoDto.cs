using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Readme.Logic.DomainModel
{
    public class LogUsersMongoDto
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 3 characters required")]
        [Required(ErrorMessage = "Required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid")]
        public string DisplayName { get; set; } = string.Empty;

        public string StatusMessage { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public DateTime CreateTimeStamp { get; set; } = DateTime.Now;
        public string Email { get; set; } = string.Empty;

        [MyStringLength(10, MinimumLength = 3)]
        public string UID { get; set; } = string.Empty;
    }

    public class MyStringLengthAttribute : StringLengthAttribute
    {
        public MyStringLengthAttribute(int maximumLength)
            : base(maximumLength)
        {
        }

        public override bool IsValid(object value)
        {
            string val = Convert.ToString(value);
            if (val.Length < base.MinimumLength)
                base.ErrorMessage = "Minimum length should be 3";
            if (val.Length > base.MaximumLength)
                base.ErrorMessage = "Maximum length should be 6";
            return base.IsValid(value);
        }
    }

}
