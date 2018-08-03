using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class UpdatedSysException : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Trace",
                table: "SysException",
                newName: "StackTrace");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "SysException",
                newName: "RowModifiedDateTime");

            migrationBuilder.AddColumn<int>(
                name: "RowCreatedBy",
                table: "SysException",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RowCreatedDateTime",
                table: "SysException",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RowModifiedBy",
                table: "SysException",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowCreatedBy",
                table: "SysException");

            migrationBuilder.DropColumn(
                name: "RowCreatedDateTime",
                table: "SysException");

            migrationBuilder.DropColumn(
                name: "RowModifiedBy",
                table: "SysException");

            migrationBuilder.RenameColumn(
                name: "StackTrace",
                table: "SysException",
                newName: "Trace");

            migrationBuilder.RenameColumn(
                name: "RowModifiedDateTime",
                table: "SysException",
                newName: "DateTime");
        }
    }
}
