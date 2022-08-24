using FractalDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace FractalDemo.Datos
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones) : base(opciones)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenProducto> OrdenProducto { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdenProducto>().HasKey(op => new {op.Orden_Id, op.Producto_Id});
        }
    }
}
