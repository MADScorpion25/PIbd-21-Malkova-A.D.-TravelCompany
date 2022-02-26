using Microsoft.EntityFrameworkCore;
using TravelCompanyDatabaseImplement.Models;

namespace TravelCompanyDatabaseImplement
{
    public class TravelCompanyDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TravelCompanyDatabase;Username=postgres;Password=MADScorpion");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Condition> Conditions { set; get; }
        public virtual DbSet<Travel> Travels { set; get; }
        public virtual DbSet<TravelCondition> TravelConditions { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
    }
}
