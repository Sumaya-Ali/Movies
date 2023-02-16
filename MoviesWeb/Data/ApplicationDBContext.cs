using Microsoft.EntityFrameworkCore;
using MoviesWeb.Models;

namespace MoviesWeb.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

    }
}
