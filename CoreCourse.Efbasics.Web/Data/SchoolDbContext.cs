using CoreCourse.Efbasics.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreCourse.Efbasics.Web.Data
{
    public class SchoolDbContext : DbContext
    {
        //define tables as Dbsets
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) 
            : base(options)
        {
        }
    }
}
