using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreCourse.Efbasics.Web.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfoId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_ContactInfo_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalTable: "ContactInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfoId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_ContactInfo_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalTable: "ContactInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.CoursesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "Id", "ContactInfoId", "Firstname", "Image", "Lastname", "Username" },
                values: new object[,]
                {
                    { 1, 1, "Jimi", "person.jpg", "Hendrix", "jimi@gmail.com" },
                    { 2, 2, "Rory", "person.jpg", "Gallagher", "jimi@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "ContactInfoId", "DateCreated", "Firstname", "Image", "Lastname" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2022, 11, 21, 13, 43, 35, 411, DateTimeKind.Utc).AddTicks(7314), "Bart", "person.jpg", "Soete" },
                    { 2, 4, new DateTime(2022, 11, 21, 13, 43, 35, 411, DateTimeKind.Utc).AddTicks(7317), "Willy", "person.jpg", "Schokkelé" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "DateCreated", "Name", "Price", "TeacherId" },
                values: new object[,]
                {
                    { 1, null, "Wba", 260.00m, 1 },
                    { 2, null, "Wfa", 240.00m, 2 },
                    { 3, null, "Prb", 120.00m, 1 },
                    { 4, null, "Pra", 230.00m, 2 }
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

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_StudentsId",
                table: "CourseStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ContactInfoId",
                table: "Students",
                column: "ContactInfoId",
                unique: true,
                filter: "[ContactInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ContactInfoId",
                table: "Teachers",
                column: "ContactInfoId",
                unique: true,
                filter: "[ContactInfoId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "ContactInfo");
        }
    }
}
