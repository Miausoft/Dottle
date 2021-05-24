using System;
using System.Collections.Generic;
using System.Text;
using Dottle.Domain.Entities;
using Dottle.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dottle.Tests.Controllers
{
    public class BaseControllerTest
    {
        protected BaseControllerTest(DbContextOptions<DatabaseContext> options)
        {
            ContextOptions = options;
            Seed();
        }

        protected DbContextOptions<DatabaseContext> ContextOptions { get; }
        
        private void Seed()
        {
            using (var context = new DatabaseContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var post1 = new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "Post1",
                    Description = "Post1 description",
                    Email = "email@gmail.com",
                    Phone = "+37061727810",
                    Address = "B. Banano 5-111",
                    Rates = new List<Rate> { },
                    Comments = new List<Comment> { },
                    TimeSheets = new List<TimeSheet> { }
                };

                var post2 = new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "Post2",
                    Description = "Post2 description",
                    Email = "email2@gmail.com",
                    Phone = "+37061727111",
                    Address = "B. Banano 5-111",
                    Rates = new List<Rate> { },
                    Comments = new List<Comment> { },
                    TimeSheets = new List<TimeSheet> { }
                };

                context.Posts.AddRange(post1, post2);
                context.SaveChanges();
            }
        }
    }
}
