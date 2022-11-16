using CoreCourse.Efbasics.Core.Entities;
using CoreCourse.Efbasics.Web.Areas.Admin.ViewModels;
using CoreCourse.Efbasics.Web.Data;
using CoreCourse.Efbasics.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace CoreCourse.Efbasics.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoursesController : Controller
    {
        private readonly SchoolDbContext _schoolDbContext;

        public CoursesController(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var coursesIndexViewModel = new CoursesIndexViewModel();
            coursesIndexViewModel.Courses = await _schoolDbContext
                .Courses
                .Select(c => new CoursesDetailViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Teacher = new BaseViewModel
                    {
                        Id = c.TeacherId,
                        Name = $"{c.Teacher.Firstname} {c.Teacher.Lastname}",
                    }
                }).ToListAsync(); ;
            return View(coursesIndexViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.PageTitle = "Add a course";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CoursesAddViewModel coursesAddViewModel)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.PageTitle = "Add a course";
                return View(coursesAddViewModel);
            }
            return RedirectToAction("Index");
        }
    }
}
