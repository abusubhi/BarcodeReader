using BarcodeReader.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BarcodeReader
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<BarcodeMaster> BarcodesMaster { get; set; }

        public DbSet<BarcodeCheck> BarcodesCheck { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
