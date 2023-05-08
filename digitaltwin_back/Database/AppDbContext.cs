using auth.Models;
using Microsoft.EntityFrameworkCore;

namespace auth.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Data> Datas { get; set; }
        public DbSet<Consumption> Consumptions { get; set; }
        public DbSet<NewConsumption> NewConsumptions { get; set; }
        public DbSet<Onandoff> Onandoffs { get; set; }
        public DbSet<NewOnandoff> NewOnandoffs { get; set; }
        public DbSet<Userusing> Userusings { get; set; }
        public DbSet<Userloading> Userloadings { get; set; }
        public DbSet<Recentconsumption> Recentconsumptions { get; set; }
        public DbSet<Airelectricity> Airelectricitys { get; set; }
        public DbSet<Airstate> Airstates { get; set; }
        public DbSet<Electricityfee> Electricityfees { get; set; }
        public DbSet<Runstrategy> Runstrategys { get; set; }
        public DbSet<Systemair> Systemairs { get; set; }
        public DbSet<Systemelectricity> Systemelectricitys { get; set; }
    }
}
