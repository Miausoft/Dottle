using System;
using System.ComponentModel.DataAnnotations;

namespace Dottle.Domain.Entities
{
    public class Rate
    {
        [Range(1, 5)]
        public int Value { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
