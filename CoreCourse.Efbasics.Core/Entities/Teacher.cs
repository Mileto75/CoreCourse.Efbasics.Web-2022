using System.ComponentModel.DataAnnotations;

namespace CoreCourse.Efbasics.Core.Entities
{
    public class Teacher : BaseEntity
    {
        [Required]
        [MaxLength(120)]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(120)]
        public string Lastname { get; set; }
        //foreign key nullable
        public int? DepartmentId { get; set; }
        //public ContactInfo ContactInfo { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public int ContactInfoId { get; set; }

    }
}