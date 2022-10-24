using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MessageLog.Infrastructure.Migrations
{
    public partial class MessageLogToMessageLogDocumentRenaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageLog");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "ErrorLogDoc");

            migrationBuilder.AddColumn<string>(
                name: "BlobUrl",
                table: "MessageLogDoc",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExternalIdentifier",
                table: "ErrorLogDoc",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "BlobUrl",
                table: "ErrorLogDoc",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CorrelationId",
                table: "ErrorLogDoc",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MessageLogDocument",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MessageType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MessageLogDocuments = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    SystemCreateDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)"),
                    SystemModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageLogDocument", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageLogDocument");

            migrationBuilder.DropColumn(
                name: "BlobUrl",
                table: "MessageLogDoc");

            migrationBuilder.DropColumn(
                name: "BlobUrl",
                table: "ErrorLogDoc");

            migrationBuilder.DropColumn(
                name: "CorrelationId",
                table: "ErrorLogDoc");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalIdentifier",
                table: "ErrorLogDoc",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MessageId",
                table: "ErrorLogDoc",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "MessageLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MessageLogs = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    MessageType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SystemCreateDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)"),
                    SystemModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageLog", x => x.Id);
                });
        }
    }
}
