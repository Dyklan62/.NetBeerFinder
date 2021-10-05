using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class UtilisateurContext : DbContext
    {
        public UtilisateurContext(DbContextOptions<UtilisateurContext> options)
            : base(options)
        {
        }

        public DbSet<Utilisateur> Utilisateur { get; set; }
    }
}