using mateuscerqueira.Application;
using mateuscerqueira.Application.Security;
using mateuscerqueira.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace mateuscerqueira.ToDoApp.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddIocServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataServices(configuration);
        services.AddApplicationServices(configuration);

        services.AddJwtAuthentication(configuration);

        return services;
    }

}