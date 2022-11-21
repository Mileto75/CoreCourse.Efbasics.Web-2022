using CoreCourse.Efbasics.Core.Entities;
using CoreCourse.Efbasics.Web.Data.Seeding;
using Microsoft.EntityFrameworkCore;

namespace CoreCourse.Efbasics.Web.Data
{
    public class SchoolDbContext : DbContext
    {
        //define tables as Dbsets
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //database configuration using fluent api
            //configure Course
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(120);
            //configure Teacher
            modelBuilder.Entity<Student>()
                .Property(c => c.Firstname)
                .IsRequired()
                .HasMaxLength(120);
            modelBuilder.Entity<Student>()
                .Property(c => c.Lastname)
                .IsRequired()
                .HasMaxLength(120);
            modelBuilder.Entity<Student>()
                .Property(s => s.Username)
                .IsRequired()
                .HasMaxLength(120);
            //example of default date insertion in SQL server
            modelBuilder.Entity<Student>()
                .Property(s => s.DateCreated)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GetDate()");
            //set delete is null for Courses table
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .OnDelete(DeleteBehavior.SetNull);
            //set null on delete from contactinfo
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.ContactInfo)
                .WithOne(co => co.Teacher)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasColumnType("decimal")
                .HasPrecision(12,2);
            //end database configuration
            //seeding
            DataSeeder.Seed(modelBuilder);
        }
    }
}
