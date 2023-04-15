using GRA.Infra.DataStore.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GRA.Infra.DataStore.EntityFrameworkCore.Startup
{
    public static class StartupCollection
    {
        public static void AddEntityFrameworkCoreExtension(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(opt =>
            {
                opt.UseInMemoryDatabase("myDatabase");
            });
        }
    }
}
