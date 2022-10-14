﻿using Finance.Common.Database.Relational.Abstractions;
using MessageLog.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MessageLog.Infrastructure
{
    public class LoggingContext : NpgsqlContextBase
    {
        private readonly string _connectionString;
        private const string ConnectionStringKey = "DB_CONNECTION_STRING";

        protected LoggingContext(DbContextOptions<LoggingContext> options) : base(options)
        {
            //_connectionString = configuration.GetConnectionString(ConnectionStringKey) ??
            //                    "User Id = postgres; Password=postgres;Server=localhost;Port=5432;Database=LoggingService-poc;Integrated Security = true; Pooling=true";
            _connectionString = "";
        }

        public DbSet<Entities.MessageLog> MessageLogs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(_connectionString);
        //.LogTo(Console.WriteLine, LogLevel.Information);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.MessageLog>()
                .Property(b => b.MessageLogs)
                .HasColumnType("jsonb");
            modelBuilder.Entity<ErrorLog>()
                .Property(b => b.ErrorLogs)
                .HasColumnType("jsonb");
        }
    }
}