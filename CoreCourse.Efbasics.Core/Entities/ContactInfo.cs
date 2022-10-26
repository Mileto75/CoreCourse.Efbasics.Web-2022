using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCourse.Efbasics.Core.Entities
{
    public class ContactInfo : BaseEntity
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string Municipality { get; set; }
        public string TelNumber { get; set; }
        public string CellNumber { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
    }
}
