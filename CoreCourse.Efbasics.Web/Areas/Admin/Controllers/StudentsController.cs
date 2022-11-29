using CoreCourse.Efbasics.Core.Entities;
using CoreCourse.Efbasics.Web.Areas.Admin.Models;
using CoreCourse.Efbasics.Web.Areas.Admin.ViewModels;
using CoreCourse.Efbasics.Web.Data;
using CoreCourse.Efbasics.Web.Services;
using CoreCourse.Efbasics.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CoreCourse.Efbasics.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentsController : Controller
    {
        private readonly SchoolDbContext _schoolDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFormBuilderService _formBuilderService;

        public StudentsController(SchoolDbContext schoolDbContext, IWebHostEnvironment webHostEnvironment, IFormBuilderService formBuilderService)
        {
            _schoolDbContext = schoolDbContext;
            _webHostEnvironment = webHostEnvironment;
            _formBuilderService = formBuilderService;
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
        public async Task<IActionResult> Add()
        {
            ViewBag.PageTitle = "Add student";
            //get the courses and put in the model
            var studentsAddViewModel
                = new StudentsAddViewModel();
            studentsAddViewModel.Courses
                = await _formBuilderService.BuildCoursesCheckboxes();
            return View(studentsAddViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StudentsAddViewModel studentsAddViewModel)
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
                    await studentsAddViewModel.Image.CopyToAsync(fileStream);
                }
                //add the filename to student
                student.Image = fileName;
            }
            
            //add to the context
            await _schoolDbContext.Students.AddAsync(student);
            //save to db
            await _schoolDbContext.SaveChangesAsync();
            //set TempData message
            TempData["Message"] = "User added succesfully";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //get the student
            var student = await _schoolDbContext
                .Students
                .FirstOrDefaultAsync(s => s.Id == id);
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
                await _schoolDbContext.SaveChangesAsync();
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
                return View("Error");
            }
            //instantiate viewmodel
            StudentsUpdateViewModel studentsUpdateViewModel
                = new StudentsUpdateViewModel
                {
                    Id = student.Id,
                    Firstname = student.Firstname,
                    Lastname = student.Lastname,
                    Username = student.Username,
                    Courses = await _formBuilderService
                    .BuildCoursesCheckboxes(),
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
        public async Task<IActionResult> Update(StudentsUpdateViewModel studentsUpdateViewModel)
        {
            //check modelState
            if(!ModelState.IsValid) 
            {
                return View(studentsUpdateViewModel);
            }
            //get the student data
            var student = await _schoolDbContext
                .Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == studentsUpdateViewModel.Id);
            //null check
            if (student == null)
            {
                return View("Error");
            }

            //fill values
            student.Firstname = studentsUpdateViewModel.Firstname;
            student.Lastname = studentsUpdateViewModel.Lastname;
            student.Username = studentsUpdateViewModel.Username;
            //courses
            //clear the courses
            student.Courses.Clear();
            //get the selected courses
            var selectedCourses = studentsUpdateViewModel
                .Courses
                .Where(c => c.IsSelected == true)
                .Select(c => c.Value);
            //fill the student's courses
            student.Courses = await
                _schoolDbContext
                .Courses
                .Where(c => selectedCourses.Contains(c.Id)).ToListAsync();
            //handle image
            //check for image upload
            if (studentsUpdateViewModel.Image != null)
            {
                //delete old image
                var pathToDelete = Path
                    .Combine(_webHostEnvironment.WebRootPath,
                    "Images", student.Image);
                //delete it
                try
                {
                    System.IO.File.Delete(pathToDelete);
                }
                catch (FileNotFoundException fileNotFoundException)
                {
                    Console.WriteLine(fileNotFoundException.Message);
                }
                //create new filename
                var newFileName = $"{Guid.NewGuid()}_{studentsUpdateViewModel.Image.FileName}";
                //create Filestream and copy file
                var pathToFile = Path
                    .Combine(_webHostEnvironment.WebRootPath,
                    "Images", newFileName);
                using (FileStream fileStream = new FileStream(pathToFile,FileMode.Create))
                {
                    try
                    {
                        await studentsUpdateViewModel
                            .Image.CopyToAsync(fileStream);
                        
                    }catch(Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                    //add new filename to student
                    student.Image = newFileName;
                };
            }
            //store the student
            try 
            {
                await _schoolDbContext.SaveChangesAsync();
            }catch(DbException dbException) 
            {
                return View("Error");
            }
            //set tempdata message
            TempData["Message"] = "Student Updated succesfully";
            //redirect to update using anonymous object to send id
            return RedirectToAction("Update", new { Id = student.Id });
        }

    }
}


