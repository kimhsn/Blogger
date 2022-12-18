using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDotNet.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "id_admin_blog", "id_admin_blog", "admin_blog", "ADMIN_BLOG" },
                    { "id_superviseur", "id_superviseur", "superviseur", "SUPERVISEUR" },
                    { "id_utilisateur", "id_utilisateur", "utilisateur", "UTILISATEUR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "superviseur", 0, "41fee155-1789-44ee-93bf-37727a8a6ca5", "superviseur@gmail.com", true, false, null, null, "JOHN_SUPERVISEUR", "AQAAAAEAACcQAAAAEDCrAcL3gPuuTR5cpHoOvx3rSD6gs6M8JdJDhAv3pNEgEjGxQQS1QNhhYjeTkYeDOw==", null, false, "0d0d6e20-879c-42b4-9c5b-7ca7236a6bdb", false, "john superviseur" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "id_superviseur", "superviseur" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "id_admin_blog");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "id_utilisateur");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "id_superviseur", "superviseur" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "id_superviseur");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "superviseur");
        }
    }
}
