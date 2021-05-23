using Dottle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dottle.Persistence.Configuration
{
    public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.Property(r => r.Text).IsRequired();
            builder.Property(r => r.DateTime).IsRequired();

            builder.Property(r => r.Text).HasMaxLength(500);
        }
    }
}
