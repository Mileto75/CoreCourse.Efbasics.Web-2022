using CoreCourse.Efbasics.Web.ViewModels;

namespace CoreCourse.Efbasics.Web.ViewModels
{
    public class CoursesDetailViewModel : BaseViewModel
    {
        public BaseViewModel Teacher { get; set; }
        public IEnumerable<BaseViewModel> Students { get; set; }
        public Decimal Price { get; set; }
    }
}
