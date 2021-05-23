using System;
using System.Collections.Generic;

namespace Dottle.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Rate> Rates { get; set; }
    }
}
