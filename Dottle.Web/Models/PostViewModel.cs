using System;
using System.Collections.Generic;
using Dottle.Domain.Entities;

namespace Dottle.Web.Models
{
    public class PostViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public double Rate { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<TimeSheet> TimeSheets { get; set; }

        public Guid UserId { get; set; }
    }
}
