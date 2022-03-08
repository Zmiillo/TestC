using Microsoft.EntityFrameworkCore;

namespace TestC.Models
{
    public class DocContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DocContext(DbContextOptions<DocContext> options)
            : base(options)
        {
            //  Database.EnsureDeleted();
            Database.EnsureCreated();   
        } 
    }
}