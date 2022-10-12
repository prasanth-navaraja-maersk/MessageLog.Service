using Microsoft.EntityFrameworkCore;

namespace MessageLog.Infrastructure
{
    public class LoggingContext : DbContext
    {
        public DbSet<MessageLog> MessageLogs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("User Id = postgres; Password=postgres;Server=localhost;Port=5432;Database=json-postgres;Integrated Security = true; Pooling=true");
        //.LogTo(Console.WriteLine, LogLevel.Information);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageLog>()
                .Property(b => b.MessageLogs)
                .HasColumnType("jsonb");
            modelBuilder.Entity<ErrorLog>()
                .Property(b => b.ErrorLogs)
                .HasColumnType("jsonb");
        }
    }
}