using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MessageLog.Infrastructure.Migrations
{
    public partial class AddedMessageLogDocErrorLogDoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorLogDoc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogMessageId = table.Column<long>(type: "bigint", nullable: false),
                    ErrorMessage = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Category = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SystemCreateDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogDoc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageLogDoc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MessageType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Stage = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Source = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Destination = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Retries = table.Column<int>(type: "integer", nullable: false),
                    SystemCreateDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)"),
                    SystemModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true, defaultValueSql: "(localtimestamp)"),
                    IsError = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageLogDoc", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLogDoc");

            migrationBuilder.DropTable(
                name: "MessageLogDoc");
        }
    }
}
