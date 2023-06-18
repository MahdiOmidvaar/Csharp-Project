using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Dashboard
{
    public class Employee: User
    {
        public string Department { get; set; }
        public float Salary { get; set; }

        public Employee() { }
        public Employee(string Department, float Salary, string Name, string Family, string PhoneNumber, string Password, Role RoleId) :base(Name,Family, PhoneNumber, Password, RoleId) 
        {
            this.Department = Department;
            this.Salary = Salary;
        }
       
    }
}
