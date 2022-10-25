using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCourse.Efbasics.Core.Entities
{
    public class Student : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ICollection<Course> Courses { get; set;}
        public ContactInfo ContactInfo { get; set; }
        public int ContactInfoId { get; set; }
    }
}
