using Finance.Common.Database.Relational.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MessageLog.Service.Entities
{
    public class LoggingContext : NpgsqlContextBase
    {
        protected LoggingContext(DbContextOptions<LoggingContext> options) : base(options)
        {
        }

        public DbSet<MessageLog> MessageLogs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("User Id = postgres; Password=postgres;Server=localhost;Port=5432;Database=LoggingService-poc;Integrated Security = true; Pooling=true");
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