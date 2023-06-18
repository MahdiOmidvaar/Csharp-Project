using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_Dashboard
{
    public abstract class User
    {
        [Key] 
        public int UserId { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string Name { get; set; }
        [Column(TypeName ="varchar")]
        [MaxLength(20)]
        public string Family { get; set; }
        [Column(TypeName ="varchar")]
        [MaxLength(11)]
        [Required]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        [Required]
        public string Password { get; set; }
        [Required]
        public Role RoleId { get; set; }

        public DateTime Birthdate { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsActive { get; set; }



        public User() { }

        public User( string Name, string Family, string PhoneNumber, string Password, Role RoleId)
        {
            this.Name = Name;
            this.Family = Family;
            this.PhoneNumber = PhoneNumber;
            this.Password = Password;
            this.RoleId = RoleId;
            Birthdate=DateTime.Now.AddYears(-25);
            RegisterDate = DateTime.Now;
            IsActive= true;

        }
        
    }
   
}
