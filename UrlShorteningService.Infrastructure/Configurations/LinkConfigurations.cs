using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShorteningService.Domain.Entities;

namespace UrlShorteningService.Infrastructure.Configurations
{
	public class LinkConfigurations : IEntityTypeConfiguration<Link>
	{
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PerfectUrl).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ShortenedUrl).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Ip).HasMaxLength(20);
            builder.Property(x => x.CreateDate).HasDefaultValue(DateTime.Now).IsRequired();
            builder.Property(x => x.Deleted).HasDefaultValue(false).IsRequired();

            builder.HasIndex(x => x.PerfectUrl).IsUnique();
            builder.HasIndex(x => x.ShortenedUrl).IsUnique();

            builder.HasQueryFilter(x => x.Deleted == false);
        }
    }
}

