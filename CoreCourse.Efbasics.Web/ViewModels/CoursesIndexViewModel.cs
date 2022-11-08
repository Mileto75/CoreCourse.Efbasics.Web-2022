namespace CoreCourse.Efbasics.Web.ViewModels
{
    public class CoursesIndexViewModel
    {
        //a list of courses
        public IEnumerable<CoursesDetailViewModel>
            Courses { get; set; }
    }
}
