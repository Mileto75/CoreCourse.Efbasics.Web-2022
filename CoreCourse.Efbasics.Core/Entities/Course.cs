using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCourse.Efbasics.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        //navigation property
        public int TeacherId { get; set; }
        //unshadowed foreign key props
    }
}
