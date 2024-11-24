using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Lab_10.Models
{
    public class Team
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "The team name cannot be blank")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Please enter a team name between 3 and 50 characters in length")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9''-'\s]*$", ErrorMessage = "Please enter a team name beginning with a capital letter and enter only letters and spaces.")]
        [Display(Name = "Team Name")]
        public string Name { get; set; }

        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}