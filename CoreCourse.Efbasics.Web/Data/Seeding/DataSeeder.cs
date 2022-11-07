using CoreCourse.Efbasics.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreCourse.Efbasics.Web.Data.Seeding
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            //prepare the data
            //mind the order
            var contactInfos = new ContactInfo[]
            {
                new ContactInfo{Id=1,Street="High street",PostalCode="9000",
                Municipality="Gent"},
                new ContactInfo{Id=2,Street="Low street",PostalCode="9000",
                Municipality="Gent"},
                new ContactInfo{Id=3,Street="Low street",PostalCode="9000",
                Municipality="Gent"},
                new ContactInfo{Id=4,Street="Low street",PostalCode="9000",
                Municipality="Gent"},

            };
            //students
            var students = new Student[]
            {
                new Student{Id=1,Image="person.jpg", Firstname="Jimi",Lastname="Hendrix",Username="jimi@gmail.com",ContactInfoId=1},
                new Student{Id=2,Image="person.jpg", Firstname="Rory",Lastname="Gallagher",Username="jimi@gmail.com",ContactInfoId=2},
            };
            //teachers
            var teachers = new Teacher[]
            {
                new Teacher{Id=1,Image="person.jpg",Firstname="Bart",Lastname="Soete",ContactInfoId=3,DateCreated=DateTime.UtcNow},
                new Teacher{Id=2,Image="person.jpg",Firstname="Willy",Lastname="Schokkelé",ContactInfoId=4,DateCreated=DateTime.UtcNow},
            };
            //Courses
            var courses = new Course[]
            {
                new Course{Id=1,Name="Wba",TeacherId=1},
                new Course{Id=2,Name="Wfa",TeacherId=2},
                new Course{Id=3,Name="Prb",TeacherId=1},
                new Course{Id=4,Name="Pra",TeacherId=2},
            };
            //CourseStudents
            //anonymous objects bacause no entity
            var courseStudents = new[]
            {
                new{CoursesId=1,StudentsId=1 },
                new{CoursesId=1,StudentsId=2 },
                new{CoursesId=2,StudentsId=1 },
                new{CoursesId=2,StudentsId=2 },
                
            };
            //call the HasData methods
            //mind the order
            modelBuilder.Entity<ContactInfo>().HasData(contactInfos);
            modelBuilder.Entity<Student>().HasData(students);
            modelBuilder.Entity<Teacher>().HasData(teachers);
            modelBuilder.Entity<Course>().HasData(courses);
            //use overloaded Entity method with literal table name
            modelBuilder.Entity($"{nameof(Course)}{nameof(Student)}").HasData(courseStudents);
        }
    }
}
