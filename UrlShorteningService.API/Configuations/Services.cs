using UrlShorteningService.Application.Interfaces;
using UrlShorteningService.Infrastructure.Services;
using UrlShorteningService.Infrastructure.Repositories;
using UrlShorteningService.Persistence.Repositories;
using UrlShorteningService.Persistence.UnitOfWork;
using UrlShorteningService.Infrastructure.UnitOfWork;
using UrlShorteningService.Infrastructure.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrlShorteningService.Infrastructure;

namespace UrlShorteningService.API.Configuations
{
	public static class Services
	{
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ILinkService, LinkService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"], sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("UrlShorteningService.Infrastructure");
                });
            });

            return services;
        }
    }
}

