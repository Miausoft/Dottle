using System;

namespace Dottle.Domain.Entities
{
    public class Reply
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public Comment Comment { get; set; }

        public Post Post { get; set; }
    }
}
