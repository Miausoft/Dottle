using System;
using System.Text.Json.Serialization;

namespace Dottle.Domain.Entities
{
    public class Reply
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public Guid CommentId { get; set; }

        [JsonIgnore]
        public Comment Comment { get; set; }

        public Guid PostId { get; set; }

        [JsonIgnore]
        public Post Post { get; set; }

        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
