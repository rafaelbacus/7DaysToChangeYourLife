using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class Updatedcolumnsizeforcomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                maxLength: 4096,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 4096);
        }
    }
}
