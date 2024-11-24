using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab_10.Models
{
    public class Tasks
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "The task name cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a task name between 3 and 50 characters in length")]
        [RegularExpression(@"^[a-zA-Z0-9,\s]*$", ErrorMessage = "Please enter a task name made up of only letters, numbers and spaces")]
        [Display(Name = "Task Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The task description cannot be blank")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Please enter a task description between 10 and 200 characters in length")]
        [RegularExpression(@"^[a-zA-Z0-9,\s]*$", ErrorMessage = "Please enter a task description made up of only letters, numbers and spaces")]
        public string Description { get; set; }
        public int? StatusID { get; set; }
        public virtual Status Status { get; set; }

        public int? TeamID { get; set; }
        public virtual Team Team { get; set; }
    }
}