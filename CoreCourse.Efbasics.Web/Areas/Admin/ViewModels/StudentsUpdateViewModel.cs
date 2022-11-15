using Microsoft.AspNetCore.Mvc;

namespace CoreCourse.Efbasics.Web.Areas.Admin.ViewModels
{
    public class StudentsUpdateViewModel : StudentsAddViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        public string ImageFilename { get; set; }
    }
}
