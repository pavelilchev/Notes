namespace Notes.Data
{
    using Microsoft.EntityFrameworkCore;
    using Notes.Models;

    public class NotesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //builder.UseSqlServer("Server=.;Database=NotesDb;Integrated Security=true");
            builder.UseSqlServer(
               "Server = .; Database = NotesDb; Trusted_Connection = True;");


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.Notes)
                .WithOne(n => n.Owner)
                .HasForeignKey(n => n.OwnerId);
        }
    }
}
