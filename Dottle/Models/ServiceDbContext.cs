using Microsoft.EntityFrameworkCore;

namespace Dottle.Models
{
    public class ServiceDbContext : DbContext
    {

        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
        {

        }

        public DbSet<UserRegisterModel> Users { get; set; }
        
        public DbSet<PostModel> Posts { get; set; }
    }
}
