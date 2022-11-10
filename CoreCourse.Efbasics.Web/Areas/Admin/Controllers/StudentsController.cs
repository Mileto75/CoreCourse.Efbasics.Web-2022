using CoreCourse.Efbasics.Core.Entities;
using CoreCourse.Efbasics.Web.Areas.Admin.Models;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentsController(SchoolDbContext schoolDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _schoolDbContext = schoolDbContext;
            _webHostEnvironment = webHostEnvironment;
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
            //get the courses and put in the model
            var studentsAddViewModel
                = new StudentsAddViewModel();
            studentsAddViewModel.Courses
                = _schoolDbContext
                .Courses
                .Select(c => new CheckboxModel
                {
                    Value= c.Id,
                    Text= c.Name,
                }).ToList();

            return View(studentsAddViewModel);
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
            //get the selected courses
            var selectedCourses = studentsAddViewModel
                .Courses
                .Where(c => c.IsSelected == true)
                .Select(c => c.Value);
            //create student
            var student = new Student();
            //fill the data
            student.Firstname = studentsAddViewModel.Firstname;
            student.Lastname = studentsAddViewModel.Lastname;
            student.Username = studentsAddViewModel.Username;
            //add courses to student
            student.Courses = _schoolDbContext
                .Courses
                .Where(c => selectedCourses.Contains(c.Id)).ToList();
            //handle image upload
            //check for null
            if(studentsAddViewModel.Image == null )
            {
                student.Image = "person.jpg";
            }
            else
            {
                //create unique filename
                var fileName = $"{Guid.NewGuid()}" +
                    $"_{studentsAddViewModel.Image.FileName}";
                //create the path to store the file
                var pathToFile = Path
                    .Combine(_webHostEnvironment.WebRootPath,"Images",fileName);
                //create a filestream and copy file
                using(var fileStream = new FileStream(pathToFile
                    ,FileMode.Create))
                {
                    //copy file to disk
                    studentsAddViewModel.Image.CopyTo(fileStream);
                }
                //add the filename to student
                student.Image = fileName;
            }
            
            //add to the context
            _schoolDbContext.Students.Add(student);
            //save to db
            _schoolDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
