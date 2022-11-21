using CoreCourse.Efbasics.Core.Entities;
using CoreCourse.Efbasics.Web.Data;
using CoreCourse.Efbasics.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreCourse.Efbasics.Web.Controllers
{
    public class CoursesController : Controller
    {
        //declare Db context
        private readonly SchoolDbContext _schoolDbContext;

        public CoursesController(SchoolDbContext schoolDbContext)
        {
            //dependency injection
            _schoolDbContext = schoolDbContext;
        }

        public IActionResult Index()
        {
            //show a list of courses
            //get the courses
            var courses = _schoolDbContext
                .Courses
                .Include(c => c.Students)
                .Include(c => c.Teacher);
            //viewmodel
            CoursesIndexViewModel coursesIndexViewModel
                = new CoursesIndexViewModel();
            //fill the model
            coursesIndexViewModel.Courses
                = courses.Select(c => new CoursesDetailViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Teacher = new BaseViewModel
                    {
                        Id = c.Teacher.Id,
                        Name = $"{c.Teacher.Firstname} {c.Teacher.Lastname}"
                    },
                    Price= c.Price,
                });
            return View(coursesIndexViewModel);

        }
    }
}
