using Microsoft.EntityFrameworkCore;

namespace MoviesWebApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
        
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<movie> Movies { get; set; }

    }
}
