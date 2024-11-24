using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab_10.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "The person name cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a person name between 3 and 50 characters in length")]
        [RegularExpression(@"^[a-zA-Z0-9,\s]*$", ErrorMessage = "Please enter a person name made up of only letters, numbers and spaces")]
        [Display(Name = "Person Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The person address cannot be blank")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Please enter a person address between 10 and 200 characters in length")]
        [RegularExpression(@"^[a-zA-Z0-9,\s]*$", ErrorMessage = "Please enter a person address made up of only letters, numbers and spaces")]
        public string Address { get; set; }

        public int? TeamID { get; set; }
        public virtual Team Team { get; set; }
    }
}