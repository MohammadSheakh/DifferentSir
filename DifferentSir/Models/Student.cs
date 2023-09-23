using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentSir.Models
{
    public class Student
    {
        // we can add public properties here 
        public int StudentId { get; set; }

        [Display(Name = "Name")] // bujhi nai eita 😢
        // [Required]
        [Required(ErrorMessage = "Please enter student name.")] // for custom error massage
        public string StudentName { get; set; }

        [Range(10, 20)]
        public int Age { get; set;}

        public Standard standard { get; set;} // complex type 
    }
}