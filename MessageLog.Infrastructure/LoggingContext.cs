using Finance.Common.Database.Relational.Abstractions;
using MessageLog.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MessageLog.Infrastructure
{
    public class LoggingContext : NpgsqlContextBase
    {
        private readonly string _connectionString;
        private const string ConnectionStringKey = "DB_CONNECTION_STRING";

        public LoggingContext(DbContextOptions<LoggingContext> options, IConfiguration configuration) : base(options)
        {
            _connectionString = configuration.GetConnectionString(ConnectionStringKey) ??
                                "User Id = postgres; Password=postgres;Server=localhost;Port=5432;Database=LoggingService-poc;Integrated Security = true; Pooling=true";
        }

        public DbSet<MessageLogDocument> MessageLogDocuments { get; set; }
        public DbSet<Entities.MessageLog> MessageLogs { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<ErrorLogDoc> ErrorLogDoc { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MessageLogDocument>()
                .Property(b => b.MessageLogDocuments)
                .HasColumnType("jsonb");
            modelBuilder.Entity<ErrorLog>()
                .Property(b => b.ErrorLogs)
                .HasColumnType("jsonb");

            modelBuilder.Entity<Entities.MessageLog>()
                .ToTable(nameof(MessageLogs));
            modelBuilder.Entity<ErrorLogDoc>()
                .ToTable(nameof(ErrorLogDoc));
        }
    }
}