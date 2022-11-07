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
        public string Username { get; set; }
        public string Image { get; set; }
        public ICollection<Course> Courses { get; set;}
        //one to one contactinfo propa
        public ContactInfo ContactInfo { get; set; }
        //unshadowed foreign key property
        public int? ContactInfoId { get; set; }
    }
}
