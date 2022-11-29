using CoreCourse.Efbasics.Core.Entities;
using CoreCourse.Efbasics.Web.Areas.Admin.Models;
using CoreCourse.Efbasics.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreCourse.Efbasics.Web.Services
{
    public class FormBuilderService : IFormBuilderService
    {
        private readonly SchoolDbContext _schoolDbContext;

        public FormBuilderService(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }

        public async Task<List<CheckboxModel>> BuildCoursesCheckboxes()
        {
           return await _schoolDbContext
                .Courses
                .Select(c => new CheckboxModel
                {
                    Value = c.Id,
                    Text = c.Name,
                }).ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> BuildTeacherDropDown()
        {
            return await _schoolDbContext
                .Teachers.Select(t => new SelectListItem
                {
                    Text = $"{t.Lastname} {t.Firstname}",
                    Value = t.Id.ToString(),
                }).ToListAsync();
        }
    }
}
