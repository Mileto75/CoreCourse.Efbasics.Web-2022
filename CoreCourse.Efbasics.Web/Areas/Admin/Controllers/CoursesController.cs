using CoreCourse.Efbasics.Core.Entities;
using CoreCourse.Efbasics.Web.Areas.Admin.ViewModels;
using CoreCourse.Efbasics.Web.Data;
using CoreCourse.Efbasics.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Data.Common;

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
            //create viewmodel
            var coursesAddViewModel = new CoursesAddViewModel();
            //get the teachers from database
            var teachers = await _schoolDbContext
                .Teachers
                .OrderBy(t => t.Lastname)
                .ToListAsync();
            //put in list
            coursesAddViewModel.Teachers
                = teachers.Select(t => new SelectListItem
                {
                    Text = $"{t.Lastname} {t.Firstname}",
                    Value = t.Id.ToString(),
                });
            //pass to the view
            ViewBag.PageTitle = "Add a course";
            return View(coursesAddViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CoursesAddViewModel coursesAddViewModel)
        {
            //check if course exists
            //use custom model error
            //remove whitespaces
            if(await _schoolDbContext.Courses
                .FirstOrDefaultAsync(c => c.Name.ToUpper().Replace(" ","") == coursesAddViewModel.Name.ToUpper().Replace(" ","")) != null)
            {
                //add model error
                ModelState.AddModelError("Name", "Course already exists!");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.PageTitle = "Add a course";
                //reload the teachers
                var teachers = await _schoolDbContext
                .Teachers
                .OrderBy(t => t.Lastname)
                .ToListAsync();
                coursesAddViewModel.Teachers
                    = teachers.Select(t => new SelectListItem
                    {
                        Text = $"{t.Lastname} {t.Firstname}",
                        Value = t.Id.ToString(),
                    });
                return View(coursesAddViewModel);
            }
            //create the new course
            var course = new Course();
            //trim whites
            course.Name= coursesAddViewModel.Name.Trim();
            course.TeacherId = coursesAddViewModel.TeacherId;
            //add to the tracking context
            await _schoolDbContext.Courses.AddAsync(course);
            //save to the database
            try 
            {
                await _schoolDbContext.SaveChangesAsync();
            }
            catch(DbException dbException)
            {
                Console.WriteLine(dbException.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
