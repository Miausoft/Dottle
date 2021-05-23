using System;
using System.Collections.Generic;

namespace Dottle.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
        
        public ICollection<Rate> Rates { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
        
        public ICollection<TimeSheet> TimeSheets { get; set; }
        
        public User User { get; set; }
    }
}
