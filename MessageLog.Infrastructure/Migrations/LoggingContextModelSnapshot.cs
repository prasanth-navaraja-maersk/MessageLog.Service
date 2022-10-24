﻿// <auto-generated />
using System;
using System.Text.Json;
using MessageLog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MessageLog.Infrastructure.Migrations
{
    [DbContext(typeof(LoggingContext))]
    partial class LoggingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MessageLog.Infrastructure.Entities.ErrorLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<JsonDocument>("ErrorLogs")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("LogMessageId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("LogMessageType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("SystemCreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP")
                        .HasDefaultValueSql("(localtimestamp)");

                    b.Property<DateTime?>("SystemModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP")
                        .HasDefaultValueSql("(localtimestamp)");

                    b.HasKey("Id");

                    b.ToTable("ErrorLog", (string)null);
                });

            modelBuilder.Entity("MessageLog.Infrastructure.Entities.ErrorLogDoc", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("BlobUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long?>("CorrelationId")
                        .HasColumnType("bigint");

                    b.Property<string>("ErrorCategory")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ErrorMessage")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ExternalIdentifier")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("SystemCreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP")
                        .HasDefaultValueSql("(localtimestamp)");

                    b.HasKey("Id");

                    b.ToTable("ErrorLogDoc", (string)null);
                });

            modelBuilder.Entity("MessageLog.Infrastructure.Entities.MessageLogDoc", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("BlobUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ExternalIdentifier")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsError")
                        .HasColumnType("boolean");

                    b.Property<string>("MessageId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("MessageType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Retries")
                        .HasColumnType("integer");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("SystemCreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP")
                        .HasDefaultValueSql("(localtimestamp)");

                    b.Property<DateTime?>("SystemModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP")
                        .HasDefaultValueSql("(localtimestamp)");

                    b.HasKey("Id");

                    b.ToTable("MessageLogDoc", (string)null);
                });

            modelBuilder.Entity("MessageLog.Infrastructure.Entities.MessageLogDocument", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("MessageId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<JsonDocument>("MessageLogDocuments")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("MessageType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("SystemCreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP")
                        .HasDefaultValueSql("(localtimestamp)");

                    b.Property<DateTime?>("SystemModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP")
                        .HasDefaultValueSql("(localtimestamp)");

                    b.HasKey("Id");

                    b.ToTable("MessageLogDocument", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
