using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Dashboard
{
    public class Master: User
    {
        public string Degree { get; set; }
        public float Salary { get; set; }

        [ForeignKey("CourseId")]
        public virtual IEnumerable<Course> Courses { get; set; }

        public Master() { }
        public Master(string Degree, float Salary, string Name, string Family, string PhoneNumber, string Password, Role RoleId ) : base (Name, Family,PhoneNumber,Password ,RoleId)
        {
            this.Degree = Degree;
            this.Salary = Salary;
        }

        

    }
}
