using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MessageLog.Infrastructure.Migrations
{
    public partial class ErrorLogRenameToErrorLogDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLog");

            migrationBuilder.CreateTable(
                name: "ErrorLogDocument",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogMessageId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LogMessageType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ErrorLogDocuments = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    SystemCreateDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)"),
                    SystemModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogDocument", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLogDocument");

            migrationBuilder.CreateTable(
                name: "ErrorLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ErrorLogs = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    LogMessageId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LogMessageType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SystemCreateDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)"),
                    SystemModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog", x => x.Id);
                });
        }
    }
}
