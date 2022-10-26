using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCourse.Efbasics.Core.Entities
{
    public class Teacher : BaseEntity
    {
        //data annotations for database configuration
        [Required]
        [MaxLength(120)]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(120)]
        public string Lastname { get; set; }
        //one to many with Course
        public ICollection<Course> Courses { get; set; }
        //one to one with ContactInfo
        public ContactInfo ContactInfo { get; set; }
        public int? ContactInfoId { get; set; }
    }
}
