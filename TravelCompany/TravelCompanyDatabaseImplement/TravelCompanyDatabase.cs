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
                optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-GGPI73LN\SQLEXPRESS;Initial Catalog=TravelCompanyDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Condition> Conditions { set; get; }
        public virtual DbSet<Travel> Travels { set; get; }
        public virtual DbSet<TravelCondition> TravelConditions { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<WarehouseCondition> WarehouseConditions { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Implementer> Implementers { get; set; }
    }
}
