using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnalysisData.Migrations
{
    /// <inheritdoc />
    public partial class secondintial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("537145fd-9f43-43be-9488-c249ee9e4060"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "ImageURL", "LastName", "Password", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { new Guid("afd87793-ebb0-4e32-a9cb-e37024b7c5fa"), "admin@gmail.com", "admin", null, "admin", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", "09131111111", 1, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("afd87793-ebb0-4e32-a9cb-e37024b7c5fa"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "ImageURL", "LastName", "Password", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { new Guid("537145fd-9f43-43be-9488-c249ee9e4060"), "admin@gmail.com", "admin", null, "admin", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", "09131111111", 1, "admin" });
        }
    }
}
