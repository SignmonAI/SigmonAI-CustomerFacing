using core.Models;
using Microsoft.EntityFrameworkCore;

namespace core.Contexts
{
    public class SigmonDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SigmonDbContext(DbContextOptions<SigmonDbContext> options): base(options) {}
    }
}