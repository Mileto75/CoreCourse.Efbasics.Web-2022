﻿// <auto-generated />
using System;
using CoreCourse.Efbasics.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoreCourse.Efbasics.Web.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    partial class SchoolDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoreCourse.Efbasics.Core.Entities.ContactInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CellNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Municipality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContactInfo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Municipality = "Gent",
                            PostalCode = "9000",
                            Street = "High street"
                        },
                        new
                        {
                            Id = 2,
                            Municipality = "Gent",
                            PostalCode = "9000",
                            Street = "Low street"
                        },
                        new
                        {
                            Id = 3,
                            Municipality = "Gent",
                            PostalCode = "9000",
                            Street = "Low street"
                        },
                        new
                        {
                            Id = 4,
                            Municipality = "Gent",
                            PostalCode = "9000",
                            Street = "Low street"
                        });
                });

            modelBuilder.Entity("CoreCourse.Efbasics.Core.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Wba",
                            TeacherId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Wfa",
                            TeacherId = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = "Prb",
                            TeacherId = 1
                        },
                        new
                        {
                            Id = 4,
                            Name = "Pra",
                            TeacherId = 2
                        });
                });

            modelBuilder.Entity("CoreCourse.Efbasics.Core.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ContactInfoId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.HasIndex("ContactInfoId")
                        .IsUnique()
                        .HasFilter("[ContactInfoId] IS NOT NULL");

                    b.ToTable("Students", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContactInfoId = 1,
                            Firstname = "Jimi",
                            Image = "person.jpg",
                            Lastname = "Hendrix",
                            Username = "jimi@gmail.com"
                        },
                        new
                        {
                            Id = 2,
                            ContactInfoId = 2,
                            Firstname = "Rory",
                            Image = "person.jpg",
                            Lastname = "Gallagher",
                            Username = "jimi@gmail.com"
                        });
                });

            modelBuilder.Entity("CoreCourse.Efbasics.Core.Entities.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ContactInfoId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.HasIndex("ContactInfoId")
                        .IsUnique()
                        .HasFilter("[ContactInfoId] IS NOT NULL");

                    b.ToTable("Teachers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContactInfoId = 3,
                            DateCreated = new DateTime(2022, 11, 7, 16, 6, 13, 523, DateTimeKind.Utc).AddTicks(1975),
                            Firstname = "Bart",
                            Image = "person.jpg",
                            Lastname = "Soete"
                        },
                        new
                        {
                            Id = 2,
                            ContactInfoId = 4,
                            DateCreated = new DateTime(2022, 11, 7, 16, 6, 13, 523, DateTimeKind.Utc).AddTicks(1978),
                            Firstname = "Willy",
                            Image = "person.jpg",
                            Lastname = "Schokkelé"
                        });
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("CoursesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("CourseStudent");

                    b.HasData(
                        new
                        {
                            CoursesId = 1,
                            StudentsId = 1
                        },
                        new
                        {
                            CoursesId = 1,
                            StudentsId = 2
                        },
                        new
                        {
                            CoursesId = 2,
                            StudentsId = 1
                        },
                        new
                        {
                            CoursesId = 2,
                            StudentsId = 2
                        });
                });

            modelBuilder.Entity("CoreCourse.Efbasics.Core.Entities.Course", b =>
                {
                    b.HasOne("CoreCourse.Efbasics.Core.Entities.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("CoreCourse.Efbasics.Core.Entities.Student", b =>
                {
                    b.HasOne("CoreCourse.Efbasics.Core.Entities.ContactInfo", "ContactInfo")
                        .WithOne("Student")
                        .HasForeignKey("CoreCourse.Efbasics.Core.Entities.Student", "ContactInfoId");

                    b.Navigation("ContactInfo");
                });

            modelBuilder.Entity("CoreCourse.Efbasics.Core.Entities.Teacher", b =>
                {
                    b.HasOne("CoreCourse.Efbasics.Core.Entities.ContactInfo", "ContactInfo")
                        .WithOne("Teacher")
                        .HasForeignKey("CoreCourse.Efbasics.Core.Entities.Teacher", "ContactInfoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("ContactInfo");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("CoreCourse.Efbasics.Core.Entities.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreCourse.Efbasics.Core.Entities.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoreCourse.Efbasics.Core.Entities.ContactInfo", b =>
                {
                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("CoreCourse.Efbasics.Core.Entities.Teacher", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
