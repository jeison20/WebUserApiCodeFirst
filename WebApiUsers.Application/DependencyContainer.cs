using Microsoft.Extensions.DependencyInjection;
using WebApiUsers.Application.Ports.Primary;
using WebApiUsers.Application.UseCases;

namespace WebApiUsers.Application
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApplication(
          this IServiceCollection services)
        {           
            services.AddScoped<IUserInformationPrimaryPort, UserInformationApplication>();

            return services;
        }
    }
}
