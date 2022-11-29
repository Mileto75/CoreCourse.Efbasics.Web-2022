using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreCourse.Efbasics.Web.Services
{
    public interface IFormBuilderService
    {
        Task<IEnumerable<SelectListItem>> BuildTeacherDropDown();
    }
}
