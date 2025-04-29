using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using test.ITSTK.producto.api.Models;

namespace test.ITSTK.producto.api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Producto>().HasKey(p => p.Id);
            modelBuilder.Entity<Producto>().Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Producto>().Property(p => p.Precio).IsRequired().HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Producto>().Property(p => p.Descripcion).IsRequired(false);
            modelBuilder.Entity<Producto>().Property(p => p.FechaCreacion).HasDefaultValueSql("GETDATE()");
        }
    }
}
