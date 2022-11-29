using CoreCourse.Efbasics.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreCourse.Efbasics.Web.Services
{
    public interface IFormBuilderService
    {
        Task<IEnumerable<SelectListItem>> BuildTeacherDropDown();
        Task<List<CheckboxModel>> BuildCoursesCheckboxes();
    }
}
