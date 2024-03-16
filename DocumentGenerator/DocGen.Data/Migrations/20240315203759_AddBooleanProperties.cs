using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocGen.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBooleanProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Company",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Client",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Client");
        }
    }
}
