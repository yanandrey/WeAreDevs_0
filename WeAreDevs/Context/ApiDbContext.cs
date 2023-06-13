using Microsoft.EntityFrameworkCore;
using WeAreDevs.Models;

namespace WeAreDevs.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Curso> Cursos { get; set; }

        //fluent api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Id).ValueGeneratedOnAdd();
                x.HasIndex(y => y.Id).IsUnique();
            });

            modelBuilder.Entity<Curso>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Id).ValueGeneratedOnAdd();
                x.HasIndex(y => y.Id).IsUnique();
            });
        }
    }
}