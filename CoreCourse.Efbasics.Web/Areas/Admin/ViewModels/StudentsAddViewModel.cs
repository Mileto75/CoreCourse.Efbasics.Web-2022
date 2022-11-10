

using System.ComponentModel.DataAnnotations;

namespace CoreCourse.Efbasics.Web.Areas.Admin.ViewModels
{
    public class StudentsAddViewModel
    {
        [Required(ErrorMessage = "Please provide Firstname")]
        [Display(Name = "Voornaam")]
        public string Firstname { get; set; }
        [Display(Name = "Naam")]
        [Required(ErrorMessage = "Please provide Lastname")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Please provide username")]
        [EmailAddress(ErrorMessage = "Please provide a valid email")]
        public string Username{ get; set; }
        [Display(Name = "Mugshot")]
        public IFormFile Image { get; set; }
    }
}
