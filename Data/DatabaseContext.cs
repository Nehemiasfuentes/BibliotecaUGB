using Microsoft.EntityFrameworkCore;
using BibliotecaUGB.Models;

namespace BibliotecaUGB.Data
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext() {}
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options) { }
        public DbSet<Autor> Autores {get; set;}
        public DbSet<Libro> Libros {get; set;}
    }
}