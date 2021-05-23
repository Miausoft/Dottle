using Dottle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dottle.Persistence.Configuration
{
    class TimeSheetConfiguration : IEntityTypeConfiguration<TimeSheet>
    {
        public void Configure(EntityTypeBuilder<TimeSheet> builder)
        {
            builder.HasKey(t => new { t.DayOfWeek, t.PostId });
            builder.HasCheckConstraint("OpensBeforeCloses", "OpensAt < ClosesAt");
        }
    }
}
