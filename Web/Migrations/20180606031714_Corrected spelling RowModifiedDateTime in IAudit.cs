using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class CorrectedspellingRowModifiedDateTimeinIAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateJoined",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RoModifiedDateTime",
                table: "Users",
                newName: "RowModifiedDateTime");

            migrationBuilder.RenameColumn(
                name: "RoModifiedDateTime",
                table: "Roles",
                newName: "RowModifiedDateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RowModifiedDateTime",
                table: "Users",
                newName: "RoModifiedDateTime");

            migrationBuilder.RenameColumn(
                name: "RowModifiedDateTime",
                table: "Roles",
                newName: "RoModifiedDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateJoined",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
