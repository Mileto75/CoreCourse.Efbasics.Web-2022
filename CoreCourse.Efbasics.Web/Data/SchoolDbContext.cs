using CoreCourse.Efbasics.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreCourse.Efbasics.Web.Data
{
    public class SchoolDbContext : DbContext
    {
        //define tables as Dbsets
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
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
            modelBuilder.Entity<Student>()
                .Property(c => c.Firstname)
                .IsRequired()
                .HasMaxLength(120);
            modelBuilder.Entity<Student>()
                .Property(c => c.Lastname)
                .IsRequired()
                .HasMaxLength(120);
        }
    }
}
