using core.Models;
using Microsoft.EntityFrameworkCore;

namespace core.Contexts
{
    public class SigmonDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            #region Subscription

            model.Entity<Subscription>()
                    .HasMany(s => s.Bills)
                    .WithOne(b => b.Subscription);

            model.Entity<Subscription>()
                    .HasOne(s => s.User)
                    .WithOne(u => u.Subscription);

            #endregion

            #region Bill

            model.Entity<Bill>()
                    .HasOne(b => b.Subscription)
                    .WithMany(s => s.Bills);

            #endregion
        }
    }
}