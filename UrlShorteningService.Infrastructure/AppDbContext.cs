using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UrlShorteningService.Domain.Entities;


namespace UrlShorteningService.Infrastructure
{
	public class AppDbContext : DbContext
	{
        public DbSet<Link> Links { get; set; }

        public AppDbContext() : base()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath($"{Directory.GetParent(Directory.GetCurrentDirectory())}\\UrlShorteningService.API")
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();

                IConfigurationRoot configuration = builder.Build();

                optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"], sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("UrlShorteningService.Infrastructure");
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(builder);
        }
    }
}

