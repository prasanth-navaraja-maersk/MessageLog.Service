using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MessageLog.Infrastructure.Migrations
{
    public partial class ErrorLogDocRenameToErrorLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLogDoc");

            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CorrelationId = table.Column<long>(type: "bigint", nullable: true),
                    ExternalIdentifier = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ErrorCategory = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ErrorMessage = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    BlobUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SystemCreateDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.CreateTable(
                name: "ErrorLogDoc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlobUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CorrelationId = table.Column<long>(type: "bigint", nullable: true),
                    ErrorCategory = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ErrorMessage = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ExternalIdentifier = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SystemCreateDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogDoc", x => x.Id);
                });
        }
    }
}
