using CoreCourse.Efbasics.Core.Entities;
using CoreCourse.Efbasics.Web.Areas.Admin.ViewModels;
using CoreCourse.Efbasics.Web.Data;
using CoreCourse.Efbasics.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreCourse.Efbasics.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentsController : Controller
    {
        private readonly SchoolDbContext _schoolDbContext;

        public StudentsController(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }

        public IActionResult Index()
        {
            var studentsIndexViewModel = new StudentsIndexViewModel
            {
                Students = _schoolDbContext.Students
                .Select(s => new BaseViewModel
                {
                    Id = s.Id,
                    Name = $"{s.Firstname} {s.Lastname}"
                })
            };
            return View(studentsIndexViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.PageTitle = "Add student";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(StudentsAddViewModel studentsAddViewModel)
        {
            //check validation
            if (!ModelState.IsValid) 
            {
                return View(studentsAddViewModel);
            }
            //create student
            var student = new Student();
            //fill the data
            student.Firstname = studentsAddViewModel.Firstname;
            student.Lastname = studentsAddViewModel.Lastname;
            student.Username = studentsAddViewModel.Username;
            //add to the context
            _schoolDbContext.Students.Add(student);
            //save to db
            _schoolDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
