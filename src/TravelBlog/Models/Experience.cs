using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBlog.Models
{
    [Table("Experiences")]
    public class Experience
    {
        public Experience()
        {
            this.Location = new HashSet<Location>();
        }
        [Key]
        public int ExperienceId { get; set; }
        public string Title { get; set; }
        public string Food { get; set; }
        public string Drink { get; set; }
        public string See { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Location> Location { get; set; }
    }
}
