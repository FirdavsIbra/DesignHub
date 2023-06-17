using database.Entities;
using Microsoft.EntityFrameworkCore;

namespace database
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<FileEntity> FileEntities { get; set; }
        public DbSet<Sketch> Sketches { get; set; }
    }
}
