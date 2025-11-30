using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeFit.Migrations
{
    /// <inheritdoc />
    public partial class SeedExerciseTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b038e20-8bac-4563-931d-50e2ffff53f3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71cc3444-cc08-4968-ae0f-a693b21387e8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53a7dbb5-779f-4700-930d-5643b5b35d6b", null, "Adult", "ADULT" },
                    { "58ac300a-15e0-48bf-af68-bc363a99cc28", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "ExerciseType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Wyciskanie sztangi leżąc" },
                    { 2, null, "Przysiad ze sztangą" },
                    { 3, null, "Martwy ciąg" },
                    { 4, null, "Wyciskanie sztangi nad głowę" },
                    { 5, null, "Podciąganie na drążku" },
                    { 6, null, "Wiosłowanie sztangą" },
                    { 7, null, "Pompki" },
                    { 8, null, "Dipsy" },
                    { 9, null, "Uginanie ramion ze sztangą" },
                    { 10, null, "Wyciskanie francuskie" },
                    { 11, null, "Wznosy boczne" },
                    { 12, null, "Wspięcia na palce" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53a7dbb5-779f-4700-930d-5643b5b35d6b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58ac300a-15e0-48bf-af68-bc363a99cc28");

            migrationBuilder.DeleteData(
                table: "ExerciseType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExerciseType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExerciseType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExerciseType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ExerciseType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ExerciseType",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ExerciseType",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ExerciseType",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ExerciseType",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ExerciseType",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ExerciseType",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ExerciseType",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b038e20-8bac-4563-931d-50e2ffff53f3", null, "Adult", "ADULT" },
                    { "71cc3444-cc08-4968-ae0f-a693b21387e8", null, "Admin", "ADMIN" }
                });
        }
    }
}
