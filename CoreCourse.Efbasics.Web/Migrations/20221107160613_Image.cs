using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreCourse.Efbasics.Web.Migrations
{
    public partial class Image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "person.jpg");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "person.jpg");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Image" },
                values: new object[] { new DateTime(2022, 11, 7, 16, 6, 13, 523, DateTimeKind.Utc).AddTicks(1975), "person.jpg" });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "Image" },
                values: new object[] { new DateTime(2022, 11, 7, 16, 6, 13, 523, DateTimeKind.Utc).AddTicks(1978), "person.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 10, 27, 8, 1, 5, 9, DateTimeKind.Utc).AddTicks(6888));

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 10, 27, 8, 1, 5, 9, DateTimeKind.Utc).AddTicks(6891));
        }
    }
}
