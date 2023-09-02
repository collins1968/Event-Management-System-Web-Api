using Assessment_5.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Assessment_5.Data
{
    public class AppDbContext: DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
       
    }
}
