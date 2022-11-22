using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCourse.Efbasics.Core.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        //course has many students
        public ICollection<Student> Students { get; set; }
        //course has one teacher
        public Teacher Teacher { get; set; }
        //unshadowed foreign key property
        //nullable for allow nulls
        public int? TeacherId { get; set; }

        public decimal Price { get; set; }

    }
}
