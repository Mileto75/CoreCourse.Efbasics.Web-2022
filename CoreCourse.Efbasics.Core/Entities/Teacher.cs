﻿namespace CoreCourse.Efbasics.Core.Entities
{
    public class Teacher
    {
        public int  Id{ get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}