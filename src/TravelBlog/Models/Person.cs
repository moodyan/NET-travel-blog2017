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
       
        [Key]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
