using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dottle.Domain.Entities
{
    public class Rate
    {
        [Range(1, 5)]
        public int Value { get; set; }

        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public Guid PostId { get; set; }

        [JsonIgnore]
        public Post Post { get; set; }
    }
}
