using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBlog.Models
{
    [Table("People")]
    public class Person
    {
        public Person()
        {
            this.Experience = new HashSet<Experience>();
            this.Location = new HashSet<Location>();
        }
        [Key]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public virtual ICollection<Experience> Experience { get; set; }
        public virtual ICollection<Location> Location { get; set; }
    }
}
