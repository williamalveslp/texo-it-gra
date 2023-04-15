using GRA.Domain.Core.Handlers;
using GRA.Domain.Core.Requests;
using MediatR;
using System.Reflection;

namespace GRA.API.IoC
{
    /// <summary>
    /// Register the Handlers.
    /// </summary>
    public static partial class Register
    {
        /// <summary>
        /// Add the register to Handlers.
        /// </summary>
        /// <param name="services"></param>
        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotificationRequest>, DomainNotificationHandler>();
        }
    }
}
