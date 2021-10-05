using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class BeerContext : DbContext
    {
        public BeerContext(DbContextOptions<BeerContext> options)
            : base(options)
        {
        }

        public DbSet<Beer> Beer { get; set; }
    }
}