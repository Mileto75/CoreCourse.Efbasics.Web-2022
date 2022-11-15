using CoreCourse.Efbasics.Core.Entities;
using CoreCourse.Efbasics.Web.Areas.Admin.Models;
using CoreCourse.Efbasics.Web.Areas.Admin.ViewModels;
using CoreCourse.Efbasics.Web.Data;
using CoreCourse.Efbasics.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

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

        public async Task<IActionResult> Index()
        {
            var studentsIndexViewModel = new StudentsIndexViewModel
            {
                Students = await _schoolDbContext.Students
                .Select(s => new BaseViewModel
                {
                    Id = s.Id,
                    Name = $"{s.Firstname} {s.Lastname}"
                }).ToListAsync()
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
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            //get the student
            var student = _schoolDbContext
                .Students
                .FirstOrDefault(s => s.Id == id);
            //check null
            if (student == null) 
            {
                return RedirectToAction("Index");
            }
            //mark for removal
            _schoolDbContext.Students.Remove(student);
            //send to db
            try
            {
                _schoolDbContext.SaveChanges();
            }
            catch(DbException dbException)
            {
                Console.WriteLine(dbException.Message);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            //student
            var student = await _schoolDbContext
                .Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == id);
            //null
            if(student == null) 
            {
                //show error view
                return NotFound();
            }
            //instantiate viewmodel
            StudentsUpdateViewModel studentsUpdateViewModel
                = new StudentsUpdateViewModel
                {
                    Id = student.Id,
                    Firstname = student.Firstname,
                    Lastname  = student.Lastname,
                    Username  = student.Username,
                    Courses = await _schoolDbContext
                    .Courses.Select(c => new CheckboxModel
                    {
                        Value= c.Id,
                        Text= c.Name,
                    }
                    ).ToListAsync(),
                    ImageFilename = student.Image
                };
            //check the courses checkboxes
            //loop over checkboxes
            foreach(var course in studentsUpdateViewModel.Courses)
            {
                if(student.Courses.Any(s => s.Id == course.Value))
                {
                    course.IsSelected = true;
                }
            }
            //pageTitle
            ViewBag.PageTitle = "Update student";
            return View(studentsUpdateViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StudentsUpdateViewModel studentsAddViewModel)
        {
            //get the student data
            return View();
        }

    }
    
}
