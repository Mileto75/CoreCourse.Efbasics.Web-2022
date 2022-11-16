using System.ComponentModel.DataAnnotations;

namespace CoreCourse.Efbasics.Web.Areas.Admin.ViewModels
{
    public class CoursesAddViewModel
    {
        [Display(Name = "Course name:")]
        [Required(ErrorMessage = "Please provide a course name!")]
        public string Name { get; set; }
    }
}
