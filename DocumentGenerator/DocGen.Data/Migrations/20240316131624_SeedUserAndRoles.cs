using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DocGen.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6bb41b5a-8b5b-4378-a739-3ad34c8976a3", "6bb41b5a-8b5b-4378-a739-3ad34c8976a3", "IdentityRole", "ADMIN", "ADMIN" },
                    { "762e22de-7afb-42c6-afbc-a14435f446a0", "762e22de-7afb-42c6-afbc-a14435f446a0", "IdentityRole", "USER", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "868decd8-dfa1-44ac-a3b7-eae0d513a199", 0, "868decd8-dfa1-44ac-a3b7-eae0d513a199", "IdentityUser", "test@test.bg", true, false, null, "TEST@TEST.BG", "KRASIMIR", "AQAAAAIAAYagAAAAEOSOT95a92GMkfxkSyRDAc3w464tBLiNi758Ej+jxAqtJT43l9Y2nW2VooYIHTWm+g==", null, true, "868decd8-dfa1-44ac-a3b7-eae0d513a199", false, "Krasimir" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6bb41b5a-8b5b-4378-a739-3ad34c8976a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "762e22de-7afb-42c6-afbc-a14435f446a0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "868decd8-dfa1-44ac-a3b7-eae0d513a199");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}
