using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_Dashboard
{
    public class Student: User
    {
        public int StudentCode { get; set; }
        public static int Code { get; set; } = 100;
        [Column(TypeName ="varchar")]
        [Required]
        [MaxLength(20)]

        public string Degree { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public Student() { }

        public Student(string Degree, string Name, string Family, string PhoneNumber, string Password, Role RoleId): base (Name, Family, PhoneNumber, Password, RoleId)
        {
            Code++;
            StudentCode= Code;
            this.Degree = Degree;
            Courses = new List<Course>();
        }
    }
}
