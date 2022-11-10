using Microsoft.AspNetCore.Mvc;

namespace CoreCourse.Efbasics.Web.Areas.Admin.Models
{
    public class CheckboxModel
    {
        public string Text { get; set; }
        [HiddenInput]
        public int Value { get; set; }
        public bool IsSelected { get; set; }
    }
}
