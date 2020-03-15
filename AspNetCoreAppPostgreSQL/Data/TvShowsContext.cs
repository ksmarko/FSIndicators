using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAppPostgreSQL.Models
{
    public class TvShowsContext : DbContext
    {
        public TvShowsContext (DbContextOptions<TvShowsContext> options)
            : base(options)
        {
        }
        public DbSet<ARR> ARR { get; set; }

        public DbSet<Investment> Investments { get; set; }
    }
}
