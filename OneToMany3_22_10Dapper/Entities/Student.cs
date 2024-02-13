using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneToMany3_22_10Dapper.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Firstname { get; set; }
        public int Age { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
        public override string ToString()
        {
            return Firstname + " " + Age;
        }
    }
}
