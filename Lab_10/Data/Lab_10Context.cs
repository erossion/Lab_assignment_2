using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab_10.Data
{
    public class Lab_10Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Lab_10Context() : base("name=Lab_10Context")
        {
        }

        public System.Data.Entity.DbSet<Lab_10.Models.Tasks> Tasks { get; set; }

        public System.Data.Entity.DbSet<Lab_10.Models.Status> Status { get; set; }

        public System.Data.Entity.DbSet<Lab_10.Models.Team> Teams { get; set; }

        public System.Data.Entity.DbSet<Lab_10.Models.Person> People { get; set; }
    }
}
