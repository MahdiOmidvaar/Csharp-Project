﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Dashboard
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required]
        [Column(TypeName ="varchar")]
        [MaxLength(15)]
        public string CourseName { get; set; }
        [Required]
        public int CourseUnit { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Master Master { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public Course() { }
        public Course( string CourseName,int CourseUnit, Master Master)
        {
            this.CourseName = CourseName;
            this.CourseUnit = CourseUnit;
            this.Master = Master;
            RegisterDate = DateTime.Now;
            IsActive = true;
        }
        
    }
}
