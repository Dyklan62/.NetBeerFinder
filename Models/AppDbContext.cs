using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class AppDbContext : DbContext
    {

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

    }
}