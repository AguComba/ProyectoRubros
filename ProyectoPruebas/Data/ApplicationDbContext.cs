using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoPruebas.Models;

namespace ProyectoPruebas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Rubro> Rubro { get; set; }
        public DbSet<SubRubro> SubRubros { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
    }
}