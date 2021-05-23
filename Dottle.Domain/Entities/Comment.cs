using System;
using System.Collections.Generic;

namespace Dottle.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<Comment> Replies { get; set; }

        public Post Post { get; set; }
    }
}
