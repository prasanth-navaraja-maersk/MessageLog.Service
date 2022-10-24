using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessageLog.Infrastructure.Migrations
{
    public partial class AddedExternalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogMessageId",
                table: "ErrorLogDoc",
                newName: "MessageId");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "ErrorLogDoc",
                newName: "ExternalIdentifier");

            migrationBuilder.AddColumn<string>(
                name: "ExternalIdentifier",
                table: "MessageLogDoc",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ErrorCategory",
                table: "ErrorLogDoc",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalIdentifier",
                table: "MessageLogDoc");

            migrationBuilder.DropColumn(
                name: "ErrorCategory",
                table: "ErrorLogDoc");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "ErrorLogDoc",
                newName: "LogMessageId");

            migrationBuilder.RenameColumn(
                name: "ExternalIdentifier",
                table: "ErrorLogDoc",
                newName: "Category");
        }
    }
}
