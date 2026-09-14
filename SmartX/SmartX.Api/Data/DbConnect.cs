using Microsoft.EntityFrameworkCore;
using SmartX.Api.Models;

namespace SmartX.Api.Data
{
    public class DbConnect :DbContext
    {
        public DbConnect(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Sensor> Sensors {  get; set; }
        public DbSet<Device> Devices {  get; set; }
        public DbSet<TelemetryRecord> TelemetryRecords {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
