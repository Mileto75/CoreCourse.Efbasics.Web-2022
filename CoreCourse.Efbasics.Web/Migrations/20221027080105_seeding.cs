using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreCourse.Efbasics.Web.Migrations
{
    public partial class seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ContactInfo",
                columns: new[] { "Id", "CellNumber", "DateCreated", "Municipality", "Number", "PostalCode", "Street", "TelNumber" },
                values: new object[,]
                {
                    { 1, null, null, "Gent", null, "9000", "High street", null },
                    { 2, null, null, "Gent", null, "9000", "Low street", null },
                    { 3, null, null, "Gent", null, "9000", "Low street", null },
                    { 4, null, null, "Gent", null, "9000", "Low street", null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "ContactInfoId", "Firstname", "Lastname", "Username" },
                values: new object[,]
                {
                    { 1, 1, "Jimi", "Hendrix", "jimi@gmail.com" },
                    { 2, 2, "Rory", "Gallagher", "jimi@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "ContactInfoId", "DateCreated", "Firstname", "Lastname" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2022, 10, 27, 8, 1, 5, 9, DateTimeKind.Utc).AddTicks(6888), "Bart", "Soete" },
                    { 2, 4, new DateTime(2022, 10, 27, 8, 1, 5, 9, DateTimeKind.Utc).AddTicks(6891), "Willy", "Schokkelé" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "DateCreated", "Name", "TeacherId" },
                values: new object[,]
                {
                    { 1, null, "Wba", 1 },
                    { 2, null, "Wfa", 2 },
                    { 3, null, "Prb", 1 },
                    { 4, null, "Pra", 2 }
                });

            migrationBuilder.InsertData(
                table: "CourseStudent",
                columns: new[] { "CoursesId", "StudentsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseStudent",
                keyColumns: new[] { "CoursesId", "StudentsId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CourseStudent",
                keyColumns: new[] { "CoursesId", "StudentsId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CourseStudent",
                keyColumns: new[] { "CoursesId", "StudentsId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "CourseStudent",
                keyColumns: new[] { "CoursesId", "StudentsId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
