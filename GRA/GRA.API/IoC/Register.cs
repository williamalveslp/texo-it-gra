namespace GRA.API.IoC
{
    /// <summary>
    /// Register general layers.
    /// </summary>
    public static partial class Register
    {
        /// <summary>
        /// Register general layers.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterGeneralLayers(this IServiceCollection services)
        {
            // Handlers (MediatR, Notifications, etc).
            services.AddHandlers();

            // Repositories.
            services.AddRepositories();

            // AppServices.
            services.AddAppServices();
        }
    }
}
