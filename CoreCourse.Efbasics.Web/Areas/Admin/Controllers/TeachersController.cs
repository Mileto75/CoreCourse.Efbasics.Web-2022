using CoreCourse.Efbasics.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace CoreCourse.Efbasics.Web.Areas.Admin.Controllers
{
    public class TeachersController : Controller
    {
        private readonly SchoolDbContext _schoolDbContext;

        public TeachersController(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
