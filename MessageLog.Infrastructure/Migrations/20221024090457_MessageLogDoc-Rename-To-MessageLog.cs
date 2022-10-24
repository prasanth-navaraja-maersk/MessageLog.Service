using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MessageLog.Infrastructure.Migrations
{
    public partial class MessageLogDocRenameToMessageLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageLogDoc");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "MessageLogDocument",
                newName: "CorrelationId");

            migrationBuilder.CreateTable(
                name: "MessageLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CorrelationId = table.Column<long>(type: "bigint", nullable: true),
                    ExternalIdentifier = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MessageType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Metadata = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Source = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Destination = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Stage = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsError = table.Column<bool>(type: "boolean", nullable: false),
                    Retries = table.Column<int>(type: "integer", nullable: false),
                    BlobUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SystemCreateDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)"),
                    SystemModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageLogs");

            migrationBuilder.RenameColumn(
                name: "CorrelationId",
                table: "MessageLogDocument",
                newName: "MessageId");

            migrationBuilder.CreateTable(
                name: "MessageLogDoc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlobUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Destination = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ExternalIdentifier = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsError = table.Column<bool>(type: "boolean", nullable: false),
                    MessageId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MessageType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Retries = table.Column<int>(type: "integer", nullable: false),
                    Source = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Stage = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SystemCreateDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)"),
                    SystemModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageLogDoc", x => x.Id);
                });
        }
    }
}
