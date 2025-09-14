using mateuscerqueira.Application.Security.Interfaces;
using mateuscerqueira.Application.Security.Services;
using mateuscerqueira.Data.Context;
using mateuscerqueira.Data.Interceptors;
using mateuscerqueira.Data.Repositories;
using mateuscerqueira.Data.Uow;
using mateuscerqueira.ToDoApp.Domain.Core.Interfaces;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace mateuscerqueira.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(
                configuration.GetConnectionString("ToDoAppCS"),
                b => b.MigrationsAssembly("mateuscerqueira.Data")
            );
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDataProtectionService, DataProtectionService>();
        services.AddScoped<IUserResolverService, UserResolverService>();
        services.AddScoped<PasswordService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

        return services;
    }
}
