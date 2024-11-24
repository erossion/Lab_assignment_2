using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_10.Models
{
    public class Status
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}