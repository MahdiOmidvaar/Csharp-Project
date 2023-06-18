using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_Dashboard
{
   public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int RoleId { get; set; }
        [Column("Title" , TypeName = "varchar")]
        [Required]
        [MaxLength(20)]
        public string RoleTitle { get; set; }
        [Column("Name", TypeName= "varchar")]
        [Required]
        [MaxLength(20)]
        [Index("key_Name", IsUnique = true)]
        public string RoleName { get; set; }

        public Role() { }
       
        public Role(int RoleId, string RoleTitle, string RoleName)
        {
            this.RoleId = RoleId;
            this.RoleTitle = RoleTitle;
            this.RoleName = RoleName;
        }
    }
}
