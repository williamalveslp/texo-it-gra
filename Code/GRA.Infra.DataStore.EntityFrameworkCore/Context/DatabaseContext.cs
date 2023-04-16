using GRA.Domain.Entities.MovieFeature;
using Microsoft.EntityFrameworkCore;

namespace GRA.Infra.DataStore.EntityFrameworkCore.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; } = null!;
    }
}
